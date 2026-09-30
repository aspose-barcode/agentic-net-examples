// Title: Batch Barcode Generation with 90° Rotation and PNG Output
// Description: Generates multiple barcodes, rotates each image by 90 degrees, and saves them as PNG files in a temporary folder.
// Category-Description: This example demonstrates batch processing of barcodes using Aspose.BarCode for .NET. It shows how to create various barcode symbologies, apply image rotation via the BarcodeGenerator.Parameters.RotationAngle property, and export the results as PNG files. Developers working with barcode generation, image manipulation, and file output can use this pattern for automated reporting, labeling, or bulk image preparation.
// Prompt: Create a batch process that rotates each generated barcode by 90 degrees before saving as PNG files.
// Tags: barcode symbology, rotation, png, aspose.barcode, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of barcodes, rotating each by 90 degrees, and saving as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates barcodes, rotates them, and saves the images.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define a list of barcodes to generate (type, text, output file name)
        var barcodes = new List<(BaseEncodeType Type, string Text, string FileName)>
        {
            (EncodeTypes.Code128, "ABC123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        // Generate each barcode, rotate it 90 degrees, and save as PNG
        foreach (var (type, text, fileName) in barcodes)
        {
            // Build the full file path for the current barcode image
            string filePath = Path.Combine(outputFolder, fileName);

            // Initialize the barcode generator with the specified type and text
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Rotate the barcode image by 90 degrees
                generator.Parameters.RotationAngle = 90f;

                // Save the rotated barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Inform the user where the barcode images have been saved
        Console.WriteLine($"Barcodes have been generated in: {outputFolder}");
    }
}