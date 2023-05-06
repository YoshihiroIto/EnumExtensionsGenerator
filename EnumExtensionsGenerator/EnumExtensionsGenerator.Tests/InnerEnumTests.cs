using Xunit;

namespace EnumExtensionsGenerator.Tests;

public sealed class InnerEnumTests
{
    [EnumExtensions]
    public enum InnerEnum
    {
        A,
        B,
        C
    }
    
    [Fact]
    public void ToName()
    {
        var a = InnerEnum.A;

        Assert.Equal("A", a.ToName());
        Assert.True(InnerEnumTests_InnerEnumExtensions.TryParse("B", out var value));
        Assert.Equal(InnerEnum.B, value);
    }
}