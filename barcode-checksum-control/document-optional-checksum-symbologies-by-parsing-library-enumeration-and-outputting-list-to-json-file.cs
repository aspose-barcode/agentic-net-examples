using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace OptionalChecksumSymbologies
{
    /// <summary>
    /// Demonstrates how to list barcode symbologies where the checksum is optional
    /// and writes the result to a JSON file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// Retrieves symbologies with optional checksums and serializes them to JSON.
        /// </summary>
        static void Main()
        {
            // Define the set of symbology names that support an optional checksum.
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

            // Get all public static fields from EncodeTypes; each field represents a barcode symbology.
            var fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
            var optionalSymbologies = new List<string>();

            // Iterate through the fields and collect those whose names are in the optional checksum set.
            foreach (var field in fields)
            {
                // The field name corresponds directly to the symbology name.
                string name = field.Name;
                if (optionalChecksumNames.Contains(name))
                {
                    optionalSymbologies.Add(name);
                }
            }

            // Serialize the list of optional symbologies to a formatted JSON string.
            string json = JsonSerializer.Serialize(optionalSymbologies, new JsonSerializerOptions { WriteIndented = true });

            // Determine the output file path in the current working directory.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "optional_checksum_symbologies.json");

            // Write the JSON content to the file, overwriting any existing file.
            using (var writer = new StreamWriter(outputPath, false))
            {
                writer.Write(json);
            }

            // Inform the user where the JSON file has been saved.
            Console.WriteLine($"Optional checksum symbologies written to: {outputPath}");
        }
    }
}