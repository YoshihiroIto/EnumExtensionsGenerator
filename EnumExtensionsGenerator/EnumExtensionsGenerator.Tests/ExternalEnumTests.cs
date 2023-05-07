using EnumExtensionsGenerator;
using Xunit;

[assembly: ExternalEnumExtensions(Enum = typeof(ConsoleKey))]

namespace EnumExtensionsGenerator.Tests;

public sealed class ExternalEnumTests
{
    [Fact]
    public void ToName()
    {
        var enter = ConsoleKey.Enter;

        Assert.Equal("Enter", enter.ToName());
        Assert.True(ConsoleKeyExtensions.TryParse("Print", out var value));
        Assert.Equal(ConsoleKey.Print, value);
    }
}