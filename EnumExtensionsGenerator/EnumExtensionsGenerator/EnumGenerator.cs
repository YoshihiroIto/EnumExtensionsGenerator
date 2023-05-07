using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using INamedTypeSymbol = Microsoft.CodeAnalysis.INamedTypeSymbol;

namespace EnumExtensionsGenerator;

[Generator(LanguageNames.CSharp)]
public class EnumGenerator : IIncrementalGenerator
{
    private const string EnumExtensionsAttributeName = "EnumExtensionsAttribute";
    private const string ExternalEnumExtensionsAttributeName = "ExternalEnumExtensionsAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var enumExtensionsSource = context.SyntaxProvider.ForAttributeWithMetadataName(
            $"{nameof(EnumExtensionsGenerator)}.{EnumExtensionsAttributeName}",
            predicate: static (node, _) => node is EnumDeclarationSyntax,
            transform: static (context, token) => context);
        context.RegisterSourceOutput(enumExtensionsSource, EmitEnumExtensions);

        var externalEnumExtensionsSource = context.SyntaxProvider.ForAttributeWithMetadataName(
            $"{nameof(EnumExtensionsGenerator)}.{ExternalEnumExtensionsAttributeName}",
            predicate: static (node, _) => node is CompilationUnitSyntax,
            transform: static (context, token) => context);
        context.RegisterSourceOutput(externalEnumExtensionsSource, EmitExternalEnumExtensions);
    }

    private static void EmitEnumExtensions(SourceProductionContext context,
        GeneratorAttributeSyntaxContext source)
    {
        if (source.TargetSymbol is not INamedTypeSymbol enumSymbol)
            return;

        var attribute = source.Attributes.FirstOrDefault(x => x.AttributeClass?.Name == EnumExtensionsAttributeName);
        if (attribute is null)
            return;

        var data = ParseEnumExtensionsAttribute(attribute);

        var enumUnit = EnumUnit.Create(
            enumSymbol,
            extensionClassNamespace: data.ExtensionClassNamespace,
            extensionClassName: data.ExtensionClassName);

        context.AddSource(enumUnit.Filename, enumUnit.ToSourceCode());
    }

    private static void EmitExternalEnumExtensions(SourceProductionContext context,
        GeneratorAttributeSyntaxContext source)
    {
        foreach (var attribute in source.Attributes)
        {
            var data = ParseExternalEnumExtensionsAttribute(attribute);

            if (data.EnumSymbol is null)
                continue;

            var enumUnit = EnumUnit.Create(
                data.EnumSymbol,
                extensionClassNamespace: data.ExtensionClassNamespace,
                extensionClassName: data.ExtensionClassName);

            context.AddSource(enumUnit.Filename, enumUnit.ToSourceCode());
        }
    }

    private static (string? ExtensionClassNamespace, string? ExtensionClassName)
        ParseEnumExtensionsAttribute(AttributeData attribute)
    {
        string? extensionClassNamespace = default;
        string? extensionClassName = default;

        foreach (var arg in attribute.NamedArguments)
        {
            switch (arg.Key)
            {
                case nameof(EnumExtensionsAttribute.ExtensionClassNamespace):
                    extensionClassNamespace = arg.Value.Value as string;
                    break;

                case nameof(EnumExtensionsAttribute.ExtensionClassName):
                    extensionClassName = arg.Value.Value as string;
                    break;
            }
        }

        return (extensionClassNamespace, extensionClassName);
    }

    private static (INamedTypeSymbol? EnumSymbol, string? ExtensionClassNamespace, string? ExtensionClassName)
        ParseExternalEnumExtensionsAttribute(AttributeData attribute)
    {
        var (extensionClassNamespace, extensionClassName) = ParseEnumExtensionsAttribute(attribute);

        INamedTypeSymbol? enumSymbol = default;

        foreach (var arg in attribute.NamedArguments)
        {
            switch (arg.Key)
            {
                case nameof(ExternalEnumExtensionsAttribute.Enum):
                    enumSymbol = arg.Value.Value as INamedTypeSymbol;
                    break;
            }
        }

        return (enumSymbol, extensionClassNamespace, extensionClassName);
    }
}