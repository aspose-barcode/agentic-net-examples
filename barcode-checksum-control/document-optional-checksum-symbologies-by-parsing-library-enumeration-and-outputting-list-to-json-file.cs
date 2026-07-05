// Title: Document optional‑checksum symbologies from Aspose.BarCode
// Description: Parses the EncodeTypes enumeration to find symbologies with optional checksum and writes them to a JSON file.
// Prompt: Document optional‑checksum symbologies by parsing the library enumeration and outputting the list to a JSON file.
// Tags: barcode symbology, documentation, json output, aspose.barcode, encode types

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to extract symbologies with optional checksum from Aspose.BarCode and export them to JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds a list of optional‑checksum symbologies and writes it to a JSON file.
    /// </summary>
    static void Main()
    {
        // Define the set of symbology names that have an optional checksum according to the documentation.
        var optionalChecksumNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Code39FullASCII",          // Code39 (full ASCII) – optional checksum
            "Standard2of5",             // Standard 2 of 5 – optional checksum
            "Interleaved2of5",          // Interleaved 2 of 5 – optional checksum
            "Matrix2of5",               // Matrix 2 of 5 – optional checksum
            "ItalianPost25",            // Italian Post 25 – optional checksum
            "DeutschePostIdentcode",    // Deutsche Post Identcode – optional checksum
            "DeutschePostLeitcode",     // Deutsche Post Leitcode – optional checksum
            "VIN",                      // Vehicle Identification Number – optional checksum
            "Codabar"                   // Codabar – optional checksum
        };

        // Retrieve all public static fields from EncodeTypes that represent symbology encode types.
        var encodeTypeFields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        var symbologies = new List<object>();

        // Iterate over each field to determine if it corresponds to an optional‑checksum symbology.
        foreach (var field in encodeTypeFields)
        {
            // Ensure the field type derives from BaseEncodeType; otherwise, skip it.
            if (!typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
                continue;

            // Get the actual encode type instance.
            var encodeInstance = (BaseEncodeType)field.GetValue(null);
            if (encodeInstance == null)
                continue;

            // Use the TypeName property (e.g., "Code39FullASCII") for lookup.
            string name = encodeInstance.TypeName;
            bool hasOptionalChecksum = optionalChecksumNames.Contains(name);

            // Include only symbologies where the checksum is optional.
            if (hasOptionalChecksum)
            {
                symbologies.Add(new
                {
                    Symbology = name,
                    OptionalChecksum = true
                });
            }
        }

        // Serialize the collected symbologies to a formatted JSON string.
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(symbologies, jsonOptions);

        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "optional_checksum_symbologies.json");

        // Write the JSON content to the file.
        File.WriteAllText(outputPath, json);

        // Inform the user where the file was saved.
        Console.WriteLine($"Optional checksum symbologies written to: {outputPath}");
    }
}