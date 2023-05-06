using Xunit;

namespace EnumExtensionsGenerator.Tests;

[EnumExtensions]
public enum EmptyEnum
{
}

public sealed class EmptyEnumTests
{
    [Fact]
    public void MembersCount()
    {
        Assert.Equal(0, EmptyEnumExtensions.MembersCount);
    }
}