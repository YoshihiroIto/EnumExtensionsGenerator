namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[EnumExtensions]
public enum SequentialEnum
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

[EnumExtensions]
public enum UnsequentialEnum
{
    Sunday = 10,
    Monday = 20,
    Tuesday = 30,
    Wednesday = 40,
    Thursday = 50,
    Friday = 60,
    Saturday = 70
}