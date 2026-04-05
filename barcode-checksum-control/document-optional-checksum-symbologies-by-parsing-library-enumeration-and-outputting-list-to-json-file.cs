using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Get all public static fields of type BaseEncodeType from EncodeTypes
        var encodeType = typeof(EncodeTypes);
        var fields = encodeType.GetFields(BindingFlags.Public | BindingFlags.Static);
        var allSymbologies = new List<string>();
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(BaseEncodeType))
            {
                allSymbologies.Add(field.Name);
            }
        }

        // Known optional‑checksum symbologies (checksum possible but not mandatory)
        var optionalChecksumSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Code39Standard",
            "Code39Extended",
            "Standard2of5",
            "Interleaved2of5",
            "Matrix2of5",
            "ItalianPost25",
            "DeutschePostIdentcode",
            "DeutschePostLeitcode",
            "VIN"
        };

        var optionalChecksumSymbologies = new List<string>();
        foreach (var name in allSymbologies)
        {
            if (optionalChecksumSet.Contains(name))
            {
                optionalChecksumSymbologies.Add(name);
            }
        }

        // Serialize to JSON
        string json = JsonSerializer.Serialize(optionalChecksumSymbologies, new JsonSerializerOptions { WriteIndented = true });

        // Save to file
        string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "optional_checksum_symbologies.json");
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Optional checksum symbologies written to: {outputPath}");
    }
}