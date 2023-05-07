using EnumExtensionsGenerator;
using Xunit;

[assembly: ExternalEnumExtensions(
    Enum = typeof(ConsoleColor),
    ExtensionClassNamespace = "External_ClassNamespaceClassNameTestsEnum_NAMESPACE",
    ExtensionClassName = "External_ClassNamespaceClassNameTestsEnum_CLASS"
)]

namespace EnumExtensionsGenerator.Tests;

[EnumExtensions(
    ExtensionClassNamespace = "ClassNamespaceClassNameTestsEnum_NAMESPACE",
    ExtensionClassName = "ClassNamespaceClassNameTestsEnum_CLASS")]
public enum ClassNamespaceClassNameTestsEnum
{
    AAA,
}

public sealed class ClassNamespaceClassNameTests
{
    [Fact]
    public void ClassNamespace()
    {
        Assert.Equal(
            ClassNamespaceClassNameTestsEnum.AAA,
            ClassNamespaceClassNameTestsEnum_NAMESPACE.ClassNamespaceClassNameTestsEnum_CLASS.Parse("AAA")
        );
    }

    [Fact]
    public void ExternalClassNamespace()
    {
        Assert.Equal(
            ConsoleColor.Green,
            External_ClassNamespaceClassNameTestsEnum_NAMESPACE.External_ClassNamespaceClassNameTestsEnum_CLASS
                .Parse("Green")
        );
    }
}