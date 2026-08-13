// Title: Batch barcode generation with 90° rotation
// Description: Demonstrates generating multiple barcode types, rotating each by 90 degrees, and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, set rotation, and export images. Developers often need to create batches of barcodes with specific orientation for printing or UI display, and this snippet illustrates typical API usage for such scenarios.
// Prompt: Create a batch process that rotates each generated barcode by 90 degrees before saving as PNG files.
// Tags: barcode symbology, rotation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a set of barcodes, rotates each by 90 degrees, and saves them as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates output folder, defines sample barcodes,
    /// rotates each barcode, and saves the result as PNG images.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            // Create the directory if it does not already exist
            Directory.CreateDirectory(outputFolder);
        }

        // Collection of barcode specifications: type, data, and target file name
        var samples = new (BaseEncodeType type, string text, string fileName)[]
        {
            (EncodeTypes.Code128, "ABC123456", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DataMatrixSample", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text", "pdf417.png"),
            (EncodeTypes.EAN13, "123456789012", "ean13.png")
        };

        // Iterate over each sample, generate, rotate, and save the barcode
        foreach (var sample in samples)
        {
            string outputPath = Path.Combine(outputFolder, sample.fileName);

            // Initialize the barcode generator with the specified type and data
            using (BarcodeGenerator generator = new BarcodeGenerator(sample.type, sample.text))
            {
                // Apply a 90-degree rotation to the generated barcode
                generator.Parameters.RotationAngle = 90f;

                // Optional: set a consistent image size for all barcodes
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the rotated barcode as a PNG file
                generator.Save(outputPath, BarCodeImageFormat.Png);

                // Inform the user about the saved file location
                Console.WriteLine($"Saved rotated barcode to: {outputPath}");
            }
        }
    }
}