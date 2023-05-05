using EnumExtensionsGenerator;
using EnumExtensionsGenerator.Sample;

var material = Material.Water;
Console.WriteLine(material.ToName());

var color = Color.Green;
Console.WriteLine(color.ToName());






[EnumExtensions]
public enum Material
{
    Fire,
    Water,
    Wood
}
