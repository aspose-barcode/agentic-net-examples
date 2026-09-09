// Title: Batch barcode generation with 90-degree rotation
// Description: Demonstrates generating multiple Code128 barcodes, rotating each by 90 degrees, and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create barcodes in bulk. Typical use cases include preparing rotated barcode assets for printing or embedding in documents. Developers often need to batch‑process barcodes with specific visual transformations, such as rotation, scaling, or color changes.
// Prompt: Create a batch process that rotates each generated barcode by 90 degrees before saving as PNG files.
// Tags: barcode symbology, rotation, png, aspose.barcode, generation, encode types

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates batch creation of Code128 barcodes rotated 90° and saved as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a temporary folder, creates barcodes, applies rotation, and writes PNG files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch process
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Sample list of barcode texts to generate
        List<string> codeTexts = new List<string>
        {
            "ABC123",
            "9876543210",
            "Aspose2024",
            "ZXCVBNM",
            "12345"
        };

        // Generate each barcode with 90-degree rotation and save as PNG
        foreach (string text in codeTexts)
        {
            // Build the output file path for the rotated barcode image
            string filePath = Path.Combine(batchFolder, $"{text}_rotated.png");

            // Initialize the barcode generator for Code128 symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Apply a 90° rotation to the generated barcode
                generator.Parameters.RotationAngle = 90f;

                // Save the rotated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Output the location of the generated barcode images
        Console.WriteLine("Barcodes generated in folder:");
        Console.WriteLine(batchFolder);
    }
}