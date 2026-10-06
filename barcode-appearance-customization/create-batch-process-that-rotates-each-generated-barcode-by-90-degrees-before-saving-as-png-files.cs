// Title: Batch barcode generation with 90-degree rotation and PNG output
// Description: Demonstrates generating multiple barcodes, rotating each by 90 degrees, and saving them as PNG images in a temporary directory.
// Category-Description: This example belongs to the Aspose.BarCode generation and image manipulation category. It showcases the use of BarcodeGenerator, setting rotation via Parameters.RotationAngle, and exporting to PNG with BarCodeImageFormat. Developers often need batch processing to create rotated barcode assets for printing, UI display, or integration into documents.
// Prompt: Create a batch process that rotates each generated barcode by 90 degrees before saving as PNG files.
// Tags: barcode, rotation, png, aspose.barcode, batch, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of various barcode types, applying a 90° rotation, and saving them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary output folder, generates rotated barcodes, and saves them as PNG images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch output
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchRotate_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine("Output folder: " + outputFolder);

        // Define sample barcodes to generate (text and corresponding symbology)
        var barcodes = new List<(string CodeText, BaseEncodeType Encode)>
        {
            ("123456789012", EncodeTypes.EAN13),
            ("CODE128", EncodeTypes.Code128),
            ("ASPOSE", EncodeTypes.QR),
            ("DATA", EncodeTypes.DataMatrix),
            ("PDF417", EncodeTypes.Pdf417)
        };

        // Iterate through each barcode definition, generate, rotate, and save as PNG
        foreach (var (codeText, encode) in barcodes)
        {
            // Initialize the generator with the specified symbology and text
            using (var generator = new BarcodeGenerator(encode, codeText))
            {
                // Apply a 90-degree rotation to the barcode image
                generator.Parameters.RotationAngle = 90f;

                // Build a file name that includes the code text and symbology type
                string fileName = $"{codeText}_{encode.GetType().Name}.png";
                string filePath = Path.Combine(outputFolder, fileName);

                // Save the rotated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Saved rotated barcode: {filePath}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}