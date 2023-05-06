namespace EnumExtensionsGenerator.Tests;

using Xunit;

[EnumExtensions]
public enum SequentialEnum
{
    AAA,
    BBB,
    CCC,
    DDD
}

public sealed class SequentialEnumExtensionsTests
{
    [Fact]
    public void ToName()
    {
        var a = SequentialEnum.AAA;

        Assert.Equal("AAA", a.ToName());
        Assert.Equal("BBB", SequentialEnum.BBB.ToName());
    }

    [Fact]
    public void ToConstant()
    {
        var a = SequentialEnum.AAA;

        Assert.Equal(0, a.ToConstant());
        Assert.Equal(1, SequentialEnum.BBB.ToConstant());
    }

    [Fact]
    public void IsDefinedByName()
    {
        Assert.True(SequentialEnumExtensions.IsDefined("AAA"));
        Assert.False(SequentialEnumExtensions.IsDefined("X"));
    }

    [Fact]
    public void IsDefinedByConstant()
    {
        Assert.True(SequentialEnumExtensions.IsDefined(2));
        Assert.False(SequentialEnumExtensions.IsDefined(999));
    }

    [Fact]
    public void IsDefinedByValue()
    {
        Assert.True(SequentialEnumExtensions.IsDefined(SequentialEnum.AAA));
        Assert.False(SequentialEnumExtensions.IsDefined((SequentialEnum)999));
    }
    
    [Fact]
    public void Parse()
    {
        Assert.Equal(SequentialEnum.AAA, SequentialEnumExtensions.Parse("AAA"));
        Assert.Throws<ArgumentOutOfRangeException>(() => SequentialEnumExtensions.Parse("aaa"));
    }
    
    [Fact]
    public void ParseIgnoreCase()
    {
        Assert.Equal(SequentialEnum.AAA, SequentialEnumExtensions.ParseIgnoreCase("aaa"));
        Assert.Throws<ArgumentOutOfRangeException>(() => SequentialEnumExtensions.Parse("abc"));
    }

    [Fact]
    public void TryParse()
    {
        Assert.True(SequentialEnumExtensions.TryParse("AAA", out var a));
        Assert.Equal(SequentialEnum.AAA, a);

        Assert.False(SequentialEnumExtensions.TryParse("abc", out _));
    }

    [Fact]
    public void TryParseIgnoreCase()
    {
        Assert.True(SequentialEnumExtensions.TryParseIgnoreCase("aAa", out var a));
        Assert.Equal(SequentialEnum.AAA, a);
    }

    [Fact]
    public void MembersCount()
    {
        Assert.Equal(4, SequentialEnumExtensions.MembersCount);
    }

    [Fact]
    public void ValuesSpan()
    {
        Assert.Equal(SequentialEnum.AAA, SequentialEnumExtensions.ValuesSpan[0]);
        Assert.Equal(SequentialEnum.BBB, SequentialEnumExtensions.ValuesSpan[1]);
        Assert.Equal(SequentialEnum.CCC, SequentialEnumExtensions.ValuesSpan[2]);
        Assert.Equal(SequentialEnum.DDD, SequentialEnumExtensions.ValuesSpan[3]);
    }

    [Fact]
    public void Values()
    {
        Assert.Equal(SequentialEnum.AAA, SequentialEnumExtensions.Values[0]);
        Assert.Equal(SequentialEnum.BBB, SequentialEnumExtensions.Values[1]);
        Assert.Equal(SequentialEnum.CCC, SequentialEnumExtensions.Values[2]);
        Assert.Equal(SequentialEnum.DDD, SequentialEnumExtensions.Values[3]);
    }

    [Fact]
    public void NamesSpan()
    {
        Assert.Equal("AAA", SequentialEnumExtensions.NamesSpan[0]);
        Assert.Equal("BBB", SequentialEnumExtensions.NamesSpan[1]);
        Assert.Equal("CCC", SequentialEnumExtensions.NamesSpan[2]);
        Assert.Equal("DDD", SequentialEnumExtensions.NamesSpan[3]);
    }

    [Fact]
    public void Names()
    {
        Assert.Equal("AAA", SequentialEnumExtensions.Names[0]);
        Assert.Equal("BBB", SequentialEnumExtensions.Names[1]);
        Assert.Equal("CCC", SequentialEnumExtensions.Names[2]);
        Assert.Equal("DDD", SequentialEnumExtensions.Names[3]);
    }

    [Fact]
    public void ConstantsSpan()
    {
        Assert.Equal(0, SequentialEnumExtensions.ConstantsSpan[0]);
        Assert.Equal(1, SequentialEnumExtensions.ConstantsSpan[1]);
        Assert.Equal(2, SequentialEnumExtensions.ConstantsSpan[2]);
        Assert.Equal(3, SequentialEnumExtensions.ConstantsSpan[3]);
    }

    [Fact]
    public void Constants()
    {
        Assert.Equal(0, SequentialEnumExtensions.Constants[0]);
        Assert.Equal(1, SequentialEnumExtensions.Constants[1]);
        Assert.Equal(2, SequentialEnumExtensions.Constants[2]);
        Assert.Equal(3, SequentialEnumExtensions.Constants[3]);
    }
}