﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EnumExtensionsGenerator;

[Generator(LanguageNames.CSharp)]
public class EnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static context =>
        {
            context.AddSource("EnumExtensionsAttribute.g.cs", """
namespace EnumExtensionsGenerator;

using System;

[AttributeUsage(AttributeTargets.Enum)]
internal sealed class EnumExtensionsAttribute : Attribute
{
}
""");
        });

        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            "EnumExtensionsGenerator.EnumExtensionsAttribute",
            predicate: static (node, _) => node is EnumDeclarationSyntax,
            transform: static (context, token) => context);

        context.RegisterSourceOutput(source, Emit);
    }

    private static void Emit(SourceProductionContext context, GeneratorAttributeSyntaxContext source)
    {
        if (source.TargetSymbol is not INamedTypeSymbol enumSymbol)
            return;

        var enumUnit = EnumUnit.Create(enumSymbol);
        
        context.AddSource(enumUnit.Filename, enumUnit.ToSourceCode());
    }
}