// Title: Detect and count barcodes in generated images using Aspose.BarCode
// Description: This example generates barcode images for several symbologies, reads each image, and records the number of barcodes detected per image.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create barcodes with BarcodeGenerator, save them, and use BarCodeReader to detect and count barcodes. Useful for developers testing detection consistency across different barcode types and image formats.
// Prompt: Record the number of barcodes detected per image under each preset to analyze consistency.
// Tags: barcode, generation, recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates barcode images, reads them back, and reports how many barcodes were detected per image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, detects them, and outputs detection counts.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define a list of presets (symbology + sample text + output file name)
        var presets = new List<(BaseEncodeType EncodeType, string CodeText, string FileName)>
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // -----------------------------------------------------------------
        // Generate barcode images for each preset
        // -----------------------------------------------------------------
        foreach (var preset in presets)
        {
            string filePath = Path.Combine(tempFolder, preset.FileName);
            using (var generator = new BarcodeGenerator(preset.EncodeType, preset.CodeText))
            {
                // Optional: set a modest XDimension for visibility
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // -----------------------------------------------------------------
        // Read each image and count detected barcodes
        // -----------------------------------------------------------------
        Console.WriteLine("Barcode detection results:");
        foreach (var preset in presets)
        {
            string filePath = Path.Combine(tempFolder, preset.FileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {preset.FileName}");
                continue;
            }

            int count = 0;
            using (var reader = new BarCodeReader(filePath))
            {
                // Iterate over all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    count++; // Increment count for each detected barcode
                    // Output details of each detection
                    Console.WriteLine($"Image: {preset.FileName}, Detected: {result.CodeText}, Type: {result.CodeType}");
                }
            }

            // Report total number of barcodes detected in the current image
            Console.WriteLine($"Image: {preset.FileName}, Total barcodes detected: {count}");
        }

        // -----------------------------------------------------------------
        // Clean up temporary files (optional)
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – files will be removed by the OS temp cleanup
        }
    }
}