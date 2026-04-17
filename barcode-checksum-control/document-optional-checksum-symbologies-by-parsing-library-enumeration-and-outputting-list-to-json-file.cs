using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Retrieve all encode type names from the Aspose.BarCode library
        string[] allNames = EncodeTypes.GetNames();

        // List of symbologies where checksum is optional (possible but not mandatory)
        var optionalChecksumSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Code39",
            "Code39FullASCII",
            "Standard2of5",
            "Interleaved2of5",
            "Matrix2of5",
            "ItalianPost25",
            "DeutschePostIdentcode",
            "DeutschePostLeitcode",
            "VIN"
        };

        var optionalChecksumSymbologies = new List<string>();

        foreach (string name in allNames)
        {
            if (optionalChecksumSet.Contains(name))
            {
                optionalChecksumSymbologies.Add(name);
            }
        }

        // Serialize the list to JSON
        string json = JsonSerializer.Serialize(optionalChecksumSymbologies, new JsonSerializerOptions { WriteIndented = true });

        // Define output file path
        string outputPath = Path.Combine(Environment.CurrentDirectory, "OptionalChecksumSymbologies.json");

        // Write JSON to file
        File.WriteAllText(outputPath, json);

        Console.WriteLine($"Optional checksum symbologies written to: {outputPath}");
    }
}