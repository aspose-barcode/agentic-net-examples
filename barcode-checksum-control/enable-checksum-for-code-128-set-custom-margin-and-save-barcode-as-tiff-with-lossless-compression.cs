// Title: Generate Code 128 barcode with checksum, custom margins, and TIFF output
// Description: Demonstrates enabling the checksum for a Code 128 barcode, applying uniform custom margins, and saving the result as a lossless TIFF image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as checksum, padding, and image format. It uses the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, which are commonly employed by developers to create printable barcodes with precise layout requirements for packaging, shipping, and inventory systems.
// Prompt: Enable checksum for Code 128, set custom margin, and save the barcode as TIFF with lossless compression.
// Tags: code128, checksum, margin, tiff, lossless, aspose.barcode, barcode-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code 128 barcode with checksum enabled,
/// applies custom margins, and saves it as a lossless TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the resulting TIFF file
        string outputPath = Path.Combine(outputDir, "Code128_Checksum_Margin.tiff");

        // Initialize the barcode generator for Code 128 with the desired data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Enable checksum (required for Code 128 to ensure data integrity)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set uniform custom margins of 10 points on all sides
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the barcode as a lossless TIFF image
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Output the location of the saved barcode image
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}