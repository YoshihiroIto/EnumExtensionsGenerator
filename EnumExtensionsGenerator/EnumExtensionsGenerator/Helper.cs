using Microsoft.CodeAnalysis;

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
}