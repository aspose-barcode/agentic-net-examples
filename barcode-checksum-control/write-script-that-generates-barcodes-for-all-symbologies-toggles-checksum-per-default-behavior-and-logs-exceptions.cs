// Title: Generate Barcodes for All Supported Symbologies with Checksum and Error Logging
// Description: The example creates a temporary folder, iterates through every barcode symbology defined in Aspose.BarCode's EncodeTypes, generates a PNG image with checksum enabled, and logs any errors encountered.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, demonstrating how to use the BarcodeGenerator class together with EncodeTypes to produce barcodes across all supported symbologies. Typical use cases include bulk barcode creation, testing symbology support, or preparing assets for printing. Developers often need to toggle checksum settings, specify output formats, and handle exceptions during batch processing.
// Prompt: Write a script that generates barcodes for all symbologies, toggles checksum per default behavior, and logs exceptions.
// Tags: barcode, symbology, generation, checksum, error handling, aspose.barcode, png, batch processing

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes for every supported symbology using Aspose.BarCode,
/// enabling checksum where applicable, and logging any exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary output directory, iterates over all EncodeTypes,
    /// generates PNG barcodes with checksum enabled, and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the generated barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        // Retrieve all public static fields of EncodeTypes (each represents a symbology)
        FieldInfo[] symbologyFields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);

        // Iterate through each symbology and generate a barcode
        foreach (FieldInfo field in symbologyFields)
        {
            string symbologyName = field.Name;
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Use a generic code text; some symbologies may require specific formats
            string codeText = "1234567890";

            // Build the full file path for the PNG image
            string filePath = Path.Combine(outputFolder, $"{symbologyName}.png");

            try
            {
                // Create the barcode generator for the current symbology
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Enable checksum (default behavior) where supported
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                    // Save the barcode image as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated {symbologyName} barcode: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any exception that occurs during generation or saving
                Console.WriteLine($"Error generating {symbologyName}: {ex.Message}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}