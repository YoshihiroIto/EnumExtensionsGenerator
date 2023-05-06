﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnumExtensionsGenerator;

internal sealed class EnumUnit
{
    public string Filename =>
        HasNamespace
            ? $"{_namespace}_{_name}_EnumExtensions.g.cs"
            : $"{_name}_EnumExtensions.g.cs";

    private string ClassName => $"{_name.Replace('.', '_')}Extensions";

    private string Fullname =>
        HasNamespace
            ? $"{_namespace}.{_name}"
            : _name;

    private readonly string _name;
    private readonly string _namespace;
    private readonly string _baseType;
    private readonly Accessibility _accessibility;
    private readonly IReadOnlyList<Member> _members;

    private bool HasNamespace => _namespace != "";

    public static EnumUnit Create(INamedTypeSymbol enumSymbol)
    {
        var @namespace = enumSymbol.ContainingNamespace.IsGlobalNamespace
            ? ""
            : enumSymbol.ContainingNamespace.ToString();

        var name =
            @namespace == ""
                ? enumSymbol.ConstructedFrom.ToString()
                : enumSymbol.ConstructedFrom.ToString().Substring(@namespace.Length + 1);

        var baseType = enumSymbol.EnumUnderlyingType?.Name ?? "int";
        var units = enumSymbol.GetMembers()
            .Where(x => x is IFieldSymbol { ConstantValue: not null })
            .Select(x => new Member(x.Name, ((IFieldSymbol)x).ConstantValue))
            .ToArray();

        return new(name, @namespace, baseType, enumSymbol.DeclaredAccessibility, units);
    }

    public string ToSourceCode()
    {
        var namespaceLine = HasNamespace ? $"namespace {_namespace};" : "";

        var declToName = $"string ToName(this {Fullname} value)";
        var declToConstant = $"{_baseType} ToConstant(this {Fullname} value)";
        var declIsDefinedName = "bool IsDefined(ReadOnlySpan<char> name)";
        var declIsDefinedConstant = $"bool IsDefined({_baseType} constant)";
        var declIsDefinedValue = $"bool IsDefined({Fullname} value)";
        var declParse = $"{Fullname} Parse(ReadOnlySpan<char> name)";
        var declParseIgnoreCase = $"{Fullname} ParseIgnoreCase(ReadOnlySpan<char> name)";
        var declTryParse = $"bool TryParse(ReadOnlySpan<char> name, out {Fullname} value)";
        var declTryParseIgnoreCase = $"bool TryParseIgnoreCase(ReadOnlySpan<char> name, out {Fullname} value)";

        return $$"""
// <auto-generated/>
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
#nullable enable
{{namespaceLine}}

public static class {{ClassName}}
{
{{MakeMethod(declToName, (x, _) => $"{x}: return nameof({x})")}}
{{MakeMethod(declToConstant, (x, y) => $"{x}: return {y}")}}
{{MakeMethod(declIsDefinedName, (x, _) => $"nameof({x}): return true", inputValueName: "name", defaultCase: "return false")}}
{{MakeMethod(declIsDefinedConstant, (x, y) => $"{y}: return true", inputValueName: "constant", defaultCase: "return false")}}
{{MakeMethod(declIsDefinedValue, (x, _) => $"{x}: return true", defaultCase: "return false")}}
{{MakeMethod(declParse,
    (x, _) => $"nameof({x}): return {x}",
    inputValueName: "name")}}
{{MakeMethod(declParseIgnoreCase,
    (x, _) => $$"""{ } when name.Equals(nameof({{x}}), StringComparison.OrdinalIgnoreCase): return {{x}}""",
    inputValueName: "name" )}}
{{MakeMethod(declTryParse,
    (x, _) => $"nameof({x}): value = {x}; return true",
    inputValueName: "name",
    defaultCase: "value = default; return false")}}
{{MakeMethod(declTryParseIgnoreCase,
    (x, _) => $$"""{ } when name.Equals(nameof({{x}}), StringComparison.OrdinalIgnoreCase): value = {{x}}; return true""",
    inputValueName: "name",
    defaultCase: "value = default; return false")}}
{{MakeValues()}}
}
""";
    }

    private string MakeValues()
    {
        return $$"""
    public const int MembersCount = {{_members.Count}};
    {{_accessibility.ToCode()}} static ReadOnlySpan<{{Fullname}}> ValuesSpan => _values ??= NewValues();
    {{_accessibility.ToCode()}} static ReadOnlySpan<string> NamesSpan => _names ??= NewNames();
    {{_accessibility.ToCode()}} static ReadOnlySpan<{{_baseType}}> ConstantsSpan => _constants ??= NewConstants();
    {{_accessibility.ToCode()}} static IReadOnlyList<{{Fullname}}> Values => _values ??= NewValues();
    {{_accessibility.ToCode()}} static IReadOnlyList<string> Names => _names ??= NewNames();
    {{_accessibility.ToCode()}} static IReadOnlyList<{{_baseType}}> Constants => _constants ??= NewConstants();

    private static {{Fullname}}[]? _values;
    private static string[]? _names;
    private static {{_baseType}}[]? _constants;

    private static {{Fullname}}[] NewValues()
    {
        return new {{Fullname}}[]
        {
        {{
            string.Join(",\n            ", _members.Select(MakeMemberFullName))
        }}
        };
    }

    private static string[] NewNames()
    {
        return new string[]
        {
        {{
            string.Join(",\n            ", _members.Select(x => MakeMemberFullName(x).SurroundWithNameOf()))
        }}
        };
    }

    private static {{_baseType}}[] NewConstants()
    {
        return new {{_baseType}}[]
        {
        {{
            string.Join(",\n            ", _members.Select(x => x.Value))
        }}
        };
    }
""";
    }

    private string MakeMethod(string decl, Func<string, object?, string> line,
        string inputValueName = "value",
        string defaultCase = "throw new ArgumentOutOfRangeException()")
    {
        return $$"""
    {{_accessibility.ToCode()}} static {{decl}}
    {
        switch ({{inputValueName}})
        {
{{ForEachMembers(line)}}
            default: {{defaultCase}};
        }
    }

""";
    }

    private string ForEachMembers(Func<string, object?, string> line)
    {
        var code = new StringBuilder();

        foreach (var member in _members)
        {
            var memberFullname = MakeMemberFullName(member);
            code.AppendLine($"            case {line(memberFullname, member.Value)};");
        }

        return code.ToString();
    }

    private string MakeMemberFullName(Member member)
    {
        return HasNamespace
            ? $"{_namespace}.{_name}.{member.Name}"
            : $"{_name}.{member.Name}";
    }

    private EnumUnit(string name, string @namespace, string baseType, Accessibility accessibility,
        IReadOnlyList<Member> members)
    {
        _name = name;
        _namespace = @namespace;
        _baseType = baseType;
        _accessibility = accessibility;
        _members = members;
    }

    private sealed class Member
    {
        public readonly string Name;
        public readonly object? Value;

        public Member(string name, object? value)
        {
            Name = name;
            Value = value;
        }
    }
}