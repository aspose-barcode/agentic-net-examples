// Title: Generate barcodes for all supported symbologies using Aspose.BarCode
// Description: This example iterates through every EncodeTypes value, creates a barcode with default checksum handling, and saves each as a PNG file.
// Category-Description: Demonstrates bulk barcode generation in the Aspose.BarCode library, covering the EncodeTypes enumeration, BarcodeGenerator class, and image export via BarCodeImageFormat. Useful for developers needing to produce sample images, test all symbologies, or batch‑create barcodes without custom checksum settings. Part of a collection of Aspose.BarCode examples showing encoding, rendering, and error handling.
// Prompt: Write a script that generates barcodes for all symbologies, toggles checksum per default behavior, and logs exceptions.
// Tags: barcode, symbology, generation, checksum, exception handling, aspose.barcode, png, csharp

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program that generates a PNG barcode for each supported symbology using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output folder, iterates over all EncodeTypes, generates barcodes with default checksum behavior, saves them, and logs any errors.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine("Output directory: " + outputDir);

        // Retrieve all public static fields of the EncodeTypes enumeration.
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            // Cast the field value to BaseEncodeType; skip if not a valid type.
            BaseEncodeType encodeType = field.GetValue(null) as BaseEncodeType;
            if (encodeType == null)
            {
                Console.WriteLine($"Skipping {field.Name}: not a BaseEncodeType.");
                continue;
            }

            // Use the field name as a simple placeholder for the barcode text.
            string codeText = field.Name;
            string filePath = Path.Combine(outputDir, $"{field.Name}.png");

            try
            {
                // Generate the barcode with default checksum behavior and save as PNG.
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // No explicit checksum setting; default behavior is applied.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                Console.WriteLine($"Generated: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during generation.
                Console.WriteLine($"Error generating {field.Name}: {ex.Message}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}