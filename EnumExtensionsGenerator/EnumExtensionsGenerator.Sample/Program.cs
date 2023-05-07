using EnumExtensionsGenerator;
using EnumExtensionsGenerator.Sample;

[assembly: ExternalEnumExtensions(Enum = typeof(ConsoleColor))]
[assembly: ExternalEnumExtensions(Enum = typeof(ConsoleKey))]


var material = Material.Water;
Console.WriteLine(material.ToName());

var color = Color.Green;
Console.WriteLine(color.ToName());

var fruits = Fruits.Melon;
Console.WriteLine(fruits.ToName());






[EnumExtensions]
public enum Material
{
    Fire,
    Water,
    Wood
}
