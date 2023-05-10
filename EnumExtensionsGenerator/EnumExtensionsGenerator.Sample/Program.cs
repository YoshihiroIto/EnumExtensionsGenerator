using EnumExtensionsGenerator;
using EnumExtensionsGenerator.Sample;
using Material_ClassNamespace;

[assembly: ExternalEnumExtensions(Enum = typeof(ConsoleColor), ExtensionClassNamespace = "AAA_ClassNamespace", ExtensionClassName = "AAA_ClassName")]
[assembly: ExternalEnumExtensions(Enum = typeof(ConsoleKey))]


var material = Material.Water;
Console.WriteLine(material.ToName());

var color = Color.Green;
Console.WriteLine(color.ToName());

var fruits = Fruits.Melon;
Console.WriteLine(fruits.ToName());






[EnumExtensions(ExtensionClassNamespace = "Material_ClassNamespace", ExtensionClassName = "Material_ClassName")]
public enum Material
{
    Fire,
    Water,
    Wood
}
