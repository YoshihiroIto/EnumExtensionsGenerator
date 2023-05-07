namespace EnumExtensionsGenerator;

using System;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class ExternalEnumExtensionsAttribute : EnumExtensionsAttribute
{
    public Type? Enum { get; set; }
}