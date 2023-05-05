namespace EnumExtensionsGenerator.Sample;

[EnumExtensions]
public enum Color
{
    Red,
    Green,
    Blue,
    Yellow,
    White
}

[EnumExtensions]
public enum City : byte
{
    Tokyo,
    Osaka,
}

[EnumExtensions]
public enum Fruits
{
    Apple = 1,
    Lemon = 2,
    Melon = 4,
    Banana = 8,
}
