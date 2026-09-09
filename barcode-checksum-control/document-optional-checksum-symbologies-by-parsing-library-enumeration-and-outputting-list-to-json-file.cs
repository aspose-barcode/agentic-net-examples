// Title: Generate JSON list of optional‑checksum barcode symbologies
// Description: This example reflects over Aspose.BarCode's EncodeTypes to collect symbologies that support optional checksums and writes the list to a JSON file.
// Category-Description: Demonstrates how to enumerate barcode symbologies using Aspose.BarCode's API, a common task for developers creating documentation or validation tools. It showcases reflection on the EncodeTypes class, JSON serialization with System.Text.Json, and file I/O. These examples help developers understand which symbologies support optional checksum features and how to programmatically retrieve such metadata.
// Prompt: Document optional‑checksum symbologies by parsing the library enumeration and outputting the list to a JSON file.
// Tags: barcode symbology, documentation, json output, reflection, aspose.barcode, encode types

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates extracting optional‑checksum barcode symbologies from Aspose.BarCode and exporting them to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Collects symbology names that have optional checksum support and writes them to a JSON file.
    /// </summary>
    static void Main()
    {
        // Define the set of symbology names known to support optional checksum according to documentation
        var optionalNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Codabar",
            "Code39",
            "ItalianPost25",
            "Interleaved2of5",
            "Matrix2of5",
            "MSI",
            "Pharmacode",
            "PatchCode",
            "PZN",
            "Standard2of5"
        };

        // List that will hold the matching symbology names found via reflection
        var optionalSymbologies = new List<string>();

        // Use reflection to enumerate all public static fields of EncodeTypes
        var fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields)
        {
            // If the field name is in the predefined optional checksum set, add it to the result list
            if (optionalNames.Contains(field.Name))
            {
                optionalSymbologies.Add(field.Name);
            }
        }

        // Serialize the resulting list to a formatted JSON string
        string json = JsonSerializer.Serialize(optionalSymbologies, new JsonSerializerOptions { WriteIndented = true });

        // Determine the output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "optional_checksum_symbologies.json");

        // Write the JSON content to the file
        File.WriteAllText(outputPath, json);

        // Inform the user where the file was written
        Console.WriteLine($"Optional checksum symbologies written to: {outputPath}");
    }
}