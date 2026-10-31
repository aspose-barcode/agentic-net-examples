// Title: Generate Code 128 barcode with checksum, custom margins, and TIFF lossless output
// Description: Demonstrates how to enable the checksum for a Code 128 barcode, apply uniform padding, and save the result as a lossless TIFF image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation series, illustrating common barcode creation tasks such as configuring symbology options, adjusting layout margins, and selecting image formats. It showcases the BarcodeGenerator class and its Parameters property, which developers frequently use to customize barcode appearance and output for printing or digital distribution.
// Prompt: Enable checksum for Code 128, set custom margin, and save the barcode as TIFF with lossless compression.
// Tags: code128, checksum, margin, tiff, lossless, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code 128 barcode with checksum enabled,
/// applies a custom 10‑point margin on all sides, and saves the image as a
/// lossless TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Determine the output directory and ensure it exists.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated TIFF file.
        string filePath = Path.Combine(outputDir, "code128.tiff");

        // Create a barcode generator for Code 128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Enable checksum (required for Code 128 to ensure data integrity).
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Apply a uniform 10‑point padding on all four sides.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the barcode as a TIFF image using lossless compression.
            generator.Save(filePath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}