namespace EnumExtensionsGenerator.Tests;

using Xunit;

[EnumExtensions]
public enum Color
{
    Red = 10,
    Green = 20,
    Blue,
    Yellow,
    White
}

public sealed class EnumExtensionsTests
{
    [Fact]
    public void ToName()
    {
        var color = Color.Red;

        Assert.Equal("Red", color.ToName());
        Assert.Equal("White", Color.White.ToName());
    }

    [Fact]
    public void ToNumber()
    {
        var color = Color.Red;

        Assert.Equal(10, color.ToNumber());
        Assert.Equal(23, Color.White.ToNumber());
    }

    [Fact]
    public void IsDefinedByName()
    {
        Assert.True(ColorExtensions.IsDefined("Red"));
        Assert.False(ColorExtensions.IsDefined("Black"));
    }

    [Fact]
    public void IsDefinedByNumber()
    {
        Assert.True(ColorExtensions.IsDefined(10));
        Assert.False(ColorExtensions.IsDefined(999));
    }

    [Fact]
    public void TryParse()
    {
        Assert.True(ColorExtensions.TryParse("White", out var white0));
        Assert.Equal(Color.White, white0);

        Assert.False(ColorExtensions.TryParse("WHITE", out var white1));
    }

    [Fact]
    public void TryParseIgnoreCase()
    {
        Assert.True(ColorExtensions.TryParseIgnoreCase("WHITE", out var white0));
        Assert.Equal(Color.White, white0);
    }

    [Fact]
    public void MembersCount()
    {
        Assert.Equal(5, ColorExtensions.MembersCount);
    }

    [Fact]
    public void ValuesSpan()
    {
        Assert.Equal(Color.Red, ColorExtensions.ValuesSpan[0]);
        Assert.Equal(Color.Green, ColorExtensions.ValuesSpan[1]);
        Assert.Equal(Color.Blue, ColorExtensions.ValuesSpan[2]);
        Assert.Equal(Color.Yellow, ColorExtensions.ValuesSpan[3]);
        Assert.Equal(Color.White, ColorExtensions.ValuesSpan[4]);
    }

    [Fact]
    public void Values()
    {
        Assert.Equal(Color.Red, ColorExtensions.Values[0]);
        Assert.Equal(Color.Green, ColorExtensions.Values[1]);
        Assert.Equal(Color.Blue, ColorExtensions.Values[2]);
        Assert.Equal(Color.Yellow, ColorExtensions.Values[3]);
        Assert.Equal(Color.White, ColorExtensions.Values[4]);
    }

    [Fact]
    public void NamesSpan()
    {
        Assert.Equal("Red", ColorExtensions.NamesSpan[0]);
        Assert.Equal("Green", ColorExtensions.NamesSpan[1]);
        Assert.Equal("Blue", ColorExtensions.NamesSpan[2]);
        Assert.Equal("Yellow", ColorExtensions.NamesSpan[3]);
        Assert.Equal("White", ColorExtensions.NamesSpan[4]);
    }

    [Fact]
    public void Names()
    {
        Assert.Equal("Red", ColorExtensions.Names[0]);
        Assert.Equal("Green", ColorExtensions.Names[1]);
        Assert.Equal("Blue", ColorExtensions.Names[2]);
        Assert.Equal("Yellow", ColorExtensions.Names[3]);
        Assert.Equal("White", ColorExtensions.Names[4]);
    }
}