﻿namespace EnumExtensionsGenerator.Benchmarks.Scenarios;

[EnumExtensions]
public enum SequentialEnum
{
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
}

[EnumExtensions]
public enum UnsequentialEnum
{
    A = 10,
    B = 20,
    C = 30,
    D = 40,
    E = 50,
    F = 60,
    G = 70,
    H = 80,
    I = 90,
    J = 100,
    K = 110,
}
