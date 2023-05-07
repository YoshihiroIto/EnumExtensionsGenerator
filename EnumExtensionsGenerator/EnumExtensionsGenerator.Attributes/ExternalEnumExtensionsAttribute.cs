namespace EnumExtensionsGenerator;

using System;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class ExternalEnumExtensionsAttribute : Attribute
{
    public Type? Enum { get; set; }
}