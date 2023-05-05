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

    private string ClassName => $"{_name}Extensions";

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
        var name = enumSymbol.Name;
        var @namespace = enumSymbol.ContainingNamespace.IsGlobalNamespace
            ? ""
            : enumSymbol.ContainingNamespace.ToString();
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
        var declToNumber = $"{_baseType} ToNumber(this {Fullname} value)";
        var declIsDefinedName = "bool IsDefined(string value)";
        var declIsDefinedValue = $"bool IsDefined({_baseType} value)";
        var declTryParse = $"bool TryParse(string name, out {Fullname} value)";
        var declTryParseIgnoreCase = $"bool TryParseIgnoreCase(string name, out {Fullname} value)";

        return $$"""
// <auto-generated/>
using System;
using System.Collections.Generic
using System.Diagnostics.CodeAnalysis;
#nullable enable
{{namespaceLine}}

public static class {{ClassName}}
{
{{MakeMethod(declToName, (x, _) => $"{x}: return nameof({x})")}}
{{MakeMethod(declToNumber, (x, y) => $"{x}: return {y}")}}
{{MakeMethod(declIsDefinedName, (x, _) => $"nameof({x}): return true", defaultCase: "return false")}}
{{MakeMethod(declIsDefinedValue, (x, y) => $"{y}: return true", defaultCase: "return false")}}
{{MakeMethod(declTryParse,
    (x, _) => $"nameof({x}): value = {x}; return true",
    inputName: "name",
    defaultCase: "value = default; return false")}}
{{MakeMethod(declTryParseIgnoreCase,
    (x, _) => $$"""{ } s when s.Equals(nameof({{x}}), StringComparison.OrdinalIgnoreCase): value = {{x}}; return true""",
    inputName: "name",
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
    {{_accessibility.ToCode()}} static IReadOnlyList<{{Fullname}}> Values => _values ??= NewValues();
    {{_accessibility.ToCode()}} static IReadOnlyList<string> Names => _names ??= NewNames();

    private static {{Fullname}}[]? _values;
    private static string[]? _names;

    private static {{Fullname}}[] NewValues()
    {
        return new[]
        {
        {{
                string.Join(",\n            ", _members.Select(MakeMemberFullName))
        }}
        };
    }

    private static string[] NewNames()
    {
        return new[]
        {
        {{
                string.Join(",\n            ", _members.Select(x => MakeMemberFullName(x).SurroundWithNameOf()))
        }}
        };
    }
""";
    }

    private string MakeMethod(string decl, Func<string, object?, string> line,
        string inputName = "value",
        string defaultCase = "throw new ArgumentOutOfRangeException()")
    {
        return $$"""
    {{_accessibility.ToCode()}} static {{decl}}
    {
        switch ({{inputName}})
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