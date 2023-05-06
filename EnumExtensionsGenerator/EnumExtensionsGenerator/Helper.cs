using Microsoft.CodeAnalysis;
using System;

namespace EnumExtensionsGenerator;

internal static class Helper
{
    public static string ToCode(this Accessibility src)
    {
        return src switch
        {
            Accessibility.NotApplicable => "",
            Accessibility.Private => "private",
            Accessibility.ProtectedAndInternal => "protected internal",
            Accessibility.Protected => "protected",
            Accessibility.Internal => "internal",
            Accessibility.ProtectedOrInternal => "protected internal",
            Accessibility.Public => "public",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public static string SurroundWithNameOf(this string src)
    {
        return $"nameof({src})";
    }

    public static string ToUnsignedType(string type)
    {
        return type switch
        {
            nameof(Byte) => nameof(Byte),
            nameof(SByte) => nameof(Byte),
            nameof(Int16) => nameof(UInt16),
            nameof(UInt16) => nameof(UInt16),
            nameof(Int32) => nameof(UInt32),
            nameof(UInt32) => nameof(UInt32),
            nameof(Int64) => nameof(UInt64),
            nameof(UInt64) => nameof(UInt64),
            _ => throw new NotImplementedException()
        };
    }

    public static bool IsEqual(int i, object o, string typeName)
    {
        // ReSharper disable BuiltInTypeReferenceStyle
        // ReSharper disable RedundantCast
        return typeName switch
        {
            nameof(Byte) => (Byte)i == (Byte)o,
            nameof(SByte) => (SByte)i == (SByte)o,
            nameof(Int16) => (Int16)i == (Int16)o,
            nameof(UInt16) => (UInt16)i == (UInt16)o,
            nameof(Int32) => (Int32)i == (Int32)o,
            nameof(UInt32) => (UInt32)i == (UInt32)o,
            nameof(Int64) => (Int64)i == (Int64)o,
            nameof(UInt64) => (UInt64)i == (UInt64)o,
            _ => throw new NotImplementedException()
        };
        // ReSharper restore RedundantCast
        // ReSharper restore BuiltInTypeReferenceStyle
    }
    
}