using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using INamedTypeSymbol = Microsoft.CodeAnalysis.INamedTypeSymbol;

namespace EnumExtensionsGenerator;

[Generator(LanguageNames.CSharp)]
public class EnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var enumExtensionsSource = context.SyntaxProvider.ForAttributeWithMetadataName(
            "EnumExtensionsGenerator.EnumExtensionsAttribute",
            predicate: static (node, _) => node is EnumDeclarationSyntax,
            transform: static (context, token) => context);
        context.RegisterSourceOutput(enumExtensionsSource, EmitEnumExtensions);

        var externalEnumExtensionsSource = context.SyntaxProvider.ForAttributeWithMetadataName(
            "EnumExtensionsGenerator.ExternalEnumExtensionsAttribute",
            predicate: static (node, _) => node is CompilationUnitSyntax,
            transform: static (context, token) => context);
        context.RegisterSourceOutput(externalEnumExtensionsSource, EmitExternalEnumExtensions);
    }

    private static void EmitEnumExtensions(SourceProductionContext context,
        GeneratorAttributeSyntaxContext source)
    {
        if (source.TargetSymbol is not INamedTypeSymbol enumSymbol)
            return;

        var enumUnit = EnumUnit.Create(enumSymbol);

        context.AddSource(enumUnit.Filename, enumUnit.ToSourceCode());
    }

    private static void EmitExternalEnumExtensions(SourceProductionContext context,
        GeneratorAttributeSyntaxContext source)
    {
        foreach (var attribute in source.Attributes)
        {
            foreach (var arg in attribute.NamedArguments)
            {
                if (arg.Key == nameof(ExternalEnumExtensionsAttribute.Enum))
                {
                    if (arg.Value.Value is not INamedTypeSymbol enumSymbol)
                        continue;

                    var enumUnit = EnumUnit.Create(enumSymbol);

                    context.AddSource(enumUnit.Filename, enumUnit.ToSourceCode());
                }
            }
        }
    }
}