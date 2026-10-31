// Title: Generate JSON list of optional‑checksum barcode symbologies
// Description: The example extracts barcode symbologies that support optional checksums from Aspose.BarCode's EncodeTypes and writes them to a JSON file.
// Category-Description: This example belongs to the barcode metadata extraction category, demonstrating how to inspect the Aspose.BarCode API via reflection to retrieve information about supported symbologies. It uses the EncodeTypes class and BaseEncodeType objects to identify symbologies with optional checksum features, a common need when configuring barcode generation or validation logic. Developers often need to programmatically enumerate symbology capabilities for documentation, UI population, or custom processing.
// Prompt: Document optional‑checksum symbologies by parsing the library enumeration and outputting the list to a JSON file.
// Tags: barcode symbology, reflection, json output, aspose.barcode, encode types, optional checksum

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to discover barcode symbologies that support optional checksums
/// and serialize the resulting list to a JSON file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reflects over <see cref="EncodeTypes"/> to find
    /// symbologies with optional checksum support and writes their type names to a JSON file.
    /// </summary>
    static void Main()
    {
        // Define the set of field names in EncodeTypes that correspond to optional‑checksum symbologies.
        var optionalFieldNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Codabar",
            "Code39FullASCII",
            "ItalianPost25",
            "Interleaved2Of5",
            "Matrix2Of5",
            "MSI",
            "Pharmacode",
            "PatchCode",
            "PZN",
            "Standard2Of5"
        };

        // List that will hold the human‑readable type names of the matching symbologies.
        var optionalSymbologies = new List<string>();

        // Retrieve all public static fields from the EncodeTypes class via reflection.
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields)
        {
            // Process only the fields whose names are in the predefined optional checksum set.
            if (optionalFieldNames.Contains(field.Name))
            {
                // Each field holds a BaseEncodeType instance; extract its TypeName property.
                if (field.GetValue(null) is BaseEncodeType encodeType)
                {
                    optionalSymbologies.Add(encodeType.TypeName);
                }
            }
        }

        // Serialize the list to a formatted JSON string.
        string json = JsonSerializer.Serialize(optionalSymbologies, new JsonSerializerOptions { WriteIndented = true });

        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "optional_checksum_symbologies.json");

        // Write the JSON content to the file.
        File.WriteAllText(outputPath, json);

        // Inform the user where the file was created.
        Console.WriteLine($"Optional checksum symbologies written to: {outputPath}");
    }
}