// Title: Document Optional-Checksum Symbologies to JSON
// Description: Demonstrates how to identify barcode symbologies where the checksum is optional and export the information to a JSON file.
// Category-Description: This example belongs to the Aspose.BarCode enumeration and metadata extraction category. It shows how to use the EncodeTypes enumeration and BaseEncodeType class to discover symbology characteristics, a common task for developers needing to generate barcodes with flexible checksum requirements. Such snippets help when building barcode generation tools, validation utilities, or documentation generators.
// Prompt: Document optional‑checksum symbologies by parsing the library enumeration and outputting the list to a JSON file.
// Tags: barcode symbology, documentation, json, aspose.barcode, encode types

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that extracts optional‑checksum barcode symbologies from Aspose.BarCode
/// and writes them to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that performs the extraction and serialization.
    /// </summary>
    static void Main()
    {
        // Define symbologies where checksum is optional (possible but not required)
        var optionalChecksumNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
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

        // Retrieve all public static fields of EncodeTypes via reflection
        var encodeFields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        var optionalSymbologies = new List<object>();

        // Iterate over each field to find matching optional‑checksum symbologies
        foreach (var field in encodeFields)
        {
            // Each field holds a BaseEncodeType instance representing a barcode symbology
            if (field.GetValue(null) is BaseEncodeType encodeType)
            {
                // If the field name is in the predefined optional list, add it to the result
                if (optionalChecksumNames.Contains(field.Name))
                {
                    optionalSymbologies.Add(new
                    {
                        Name = field.Name,
                        TypeName = encodeType.TypeName
                    });
                }
            }
        }

        // Configure JSON serializer to produce indented output for readability
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(optionalSymbologies, jsonOptions);

        // Determine output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "optional_checksum_symbologies.json");

        // Write the JSON content to the file
        using (var stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
        using (var writer = new StreamWriter(stream))
        {
            writer.Write(json);
        }

        // Inform the user where the file was written
        Console.WriteLine($"Optional checksum symbologies written to: {outputPath}");
    }
}