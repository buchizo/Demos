using System;
using System.Runtime.InteropServices;

Console.WriteLine("Hello, Wasi C# Console!");
Console.WriteLine($"{RuntimeInformation.OSDescription}:{RuntimeInformation.OSArchitecture}");
Console.WriteLine($"With love from {RuntimeInformation.FrameworkDescription}");
