// Title: List optional‑checksum symbologies and export to JSON
// Description: Demonstrates how to identify barcode symbologies that support optional checksums by inspecting the Aspose.BarCode EncodeTypes enumeration and writing the results to a JSON file.
// Category-Description: This example belongs to the Aspose.BarCode enumeration and symbology discovery category. It shows how to use reflection with EncodeTypes and BaseEncodeType, create BarcodeGenerator instances, and query the IsChecksumEnabled property. Developers often need to programmatically list supported symbologies for validation, UI population, or documentation purposes.
// Prompt: Document optional‑checksum symbologies by parsing the library enumeration and outputting the list to a JSON file.
// Tags: barcode symbology, optional checksum, enumeration parsing, json output, aspose.barcode, reflection

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that discovers barcode symbologies with optional checksum support and writes them to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans EncodeTypes, creates generators, checks checksum setting, and serializes results.
    /// </summary>
    static void Main()
    {
        // Collection to hold names of symbologies with optional checksum
        var optionalChecksumSymbologies = new List<string>();

        // Retrieve all public static fields of EncodeTypes (each represents a barcode symbology)
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            // Consider only fields whose type derives from BaseEncodeType
            if (!typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
                continue;

            string symbologyName = field.Name;
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Attempt to instantiate a generator with a sample code text
            try
            {
                using (var generator = new BarcodeGenerator(encodeType, "12345"))
                {
                    // If the default checksum setting is 'No', the checksum is optional for this symbology
                    if (generator.Parameters.Barcode.IsChecksumEnabled == EnableChecksum.No)
                    {
                        optionalChecksumSymbologies.Add(symbologyName);
                    }
                }
            }
            catch
            {
                // Skip symbologies that cannot be instantiated with the sample text
                continue;
            }
        }

        // Serialize the list to a formatted JSON string
        string json = JsonSerializer.Serialize(optionalChecksumSymbologies, new JsonSerializerOptions { WriteIndented = true });

        // Determine output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "optional_checksum_symbologies.json");

        // Write JSON content to the file
        File.WriteAllText(outputPath, json);

        // Inform the user where the file was saved
        Console.WriteLine($"Optional checksum symbologies written to: {outputPath}");
    }
}