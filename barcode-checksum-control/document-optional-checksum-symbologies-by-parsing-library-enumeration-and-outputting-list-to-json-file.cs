// Title: List Optional-Checksum Symbologies and Export to JSON
// Description: The example enumerates barcode symbologies that support disabling the checksum, then writes their names to a JSON file.
// Category-Description: This sample belongs to the Aspose.BarCode enumeration and configuration category. It demonstrates how to use the EncodeTypes enumeration, BaseEncodeType class, and BarcodeGenerator to probe symbology capabilities such as optional checksum handling. Developers often need to programmatically discover supported features across symbologies for validation, UI generation, or documentation purposes.
// Prompt: Document optional‑checksum symbologies by parsing the library enumeration and outputting the list to a JSON file.
// Tags: barcode symbology, enumeration, json output, aspose.barcode, optional checksum

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to discover barcode symbologies that allow optional checksum disabling
/// and export the list to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the list of optional‑checksum symbologies and writes it to a JSON file.
    /// </summary>
    static void Main()
    {
        // Retrieve symbologies where checksum can be turned off
        var optionalChecksumSymbologies = GetOptionalChecksumSymbologies();

        // Convert the list to a formatted JSON string
        string json = JsonSerializer.Serialize(optionalChecksumSymbologies, new JsonSerializerOptions { WriteIndented = true });

        // Determine output path in the current working directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "optional_checksum_symbologies.json");

        // Persist the JSON content to disk
        File.WriteAllText(outputPath, json);

        // Inform the user about the result
        Console.WriteLine($"Found {optionalChecksumSymbologies.Count} symbologies with optional checksum.");
        Console.WriteLine($"Output written to: {outputPath}");
    }

    /// <summary>
    /// Scans all EncodeTypes fields, attempts to disable checksum, and collects those that succeed.
    /// </summary>
    /// <returns>List of symbology names that support optional checksum.</returns>
    private static List<string> GetOptionalChecksumSymbologies()
    {
        var result = new List<string>();

        // Retrieve all public static fields of EncodeTypes that are BaseEncodeType instances
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (var field in fields)
        {
            // Skip fields that are not BaseEncodeType (e.g., helper constants)
            if (field.FieldType != typeof(BaseEncodeType))
                continue;

            string symbologyName = field.Name;
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Attempt to disable checksum; if no exception, checksum is optional for this symbology
            try
            {
                using (var generator = new BarcodeGenerator(encodeType, "12345"))
                {
                    // Some symbologies may not support disabling checksum and will throw
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                    result.Add(symbologyName);
                }
            }
            catch
            {
                // Either the symbology does not support disabling checksum or the code text is invalid; ignore
            }
        }

        return result;
    }
}