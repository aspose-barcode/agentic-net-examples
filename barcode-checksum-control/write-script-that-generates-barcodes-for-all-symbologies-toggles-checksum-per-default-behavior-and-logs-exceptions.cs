// Title: Generate Barcodes for All Supported Symbologies with Default Checksum
// Description: Creates PNG images for every barcode symbology supported by Aspose.BarCode, using a sample numeric value and default checksum handling.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It demonstrates how to enumerate all EncodeTypes, instantiate a BarcodeGenerator for each, configure default checksum behavior, and save the resulting images. Developers working with barcode creation, batch processing, or testing supported symbologies will find this pattern useful. Key API classes include EncodeTypes, BaseEncodeType, BarcodeGenerator, and the Parameters.Barcode settings.
// Prompt: Write a script that generates barcodes for all symbologies, toggles checksum per default behavior, and logs exceptions.
// Tags: barcode, symbology, generation, checksum, png, aspose.barcode, logging

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation of barcode images for every supported symbology using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates PNG files for all symbologies, applies default checksum settings, and logs any errors.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Retrieve all symbology names supported by Aspose.BarCode.
        string[] symbologyNames = EncodeTypes.GetNames();

        // Iterate over each symbology name and attempt to generate a barcode.
        foreach (string symName in symbologyNames)
        {
            try
            {
                // Resolve the symbology name to a BaseEncodeType via reflection.
                FieldInfo field = typeof(EncodeTypes).GetField(symName, BindingFlags.Public | BindingFlags.Static);
                if (field == null)
                {
                    Console.WriteLine($"[WARN] Symbology field not found: {symName}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
                if (encodeType == null)
                {
                    Console.WriteLine($"[WARN] Unable to obtain encode type for: {symName}");
                    continue;
                }

                // Use a generic numeric string; many symbologies accept this format.
                // If a specific symbology requires a different format, an exception will be caught.
                string codeText = "1234567890";

                // Create the barcode generator with the resolved type and code text.
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Ensure checksum uses the default behavior for the symbology.
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Default;

                    // Build a safe file name using the symbology's type name.
                    string fileName = $"{encodeType.TypeName}.png";
                    string filePath = Path.Combine(outputDir, fileName);

                    // Save the barcode image as PNG.
                    generator.Save(filePath);
                    Console.WriteLine($"[INFO] Generated barcode for {symName} -> {fileName}");
                }
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during generation for this symbology.
                Console.WriteLine($"[ERROR] Failed to generate barcode for {symName}: {ex.Message}");

                // Append detailed error information to a log file without interrupting the loop.
                string logPath = Path.Combine(outputDir, "generation_errors.log");
                try
                {
                    File.AppendAllText(logPath, $"[{DateTime.Now}] {symName}: {ex}{Environment.NewLine}");
                }
                catch
                {
                    // Suppress any logging failures to avoid crashing the program.
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}