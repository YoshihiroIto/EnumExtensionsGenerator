﻿namespace EnumExtensionsGenerator;

using System;

[AttributeUsage(AttributeTargets.Enum)]
public class EnumExtensionsAttribute : Attribute
{
    public string? ExtensionClassNamespace { get; set; }
    public string? ExtensionClassName { get; set; }
}
