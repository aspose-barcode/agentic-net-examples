// Title: Generate Code 16K barcode and save as high‑resolution TIFF
// Description: Demonstrates creating a Code 16K barcode with the maximum 77‑character data length and saving it as a 300 DPI TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to configure barcode parameters such as resolution, module size, and aspect ratio using the BarcodeGenerator class. Typical use cases include producing high‑quality printable barcodes for inventory, shipping, or compliance labeling. Developers often need to set EncodeTypes, adjust XDimension, and export to raster formats like TIFF.
// Prompt: Generate Code 16K barcode with maximum 77 characters, save high‑resolution TIFF.
// Tags: code16k, barcode generation, tiff, high resolution, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code 16K barcode with the maximum allowed data length
/// and saves it as a high‑resolution TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the barcode generator,
    /// and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "Code16KExample");
        Directory.CreateDirectory(outputDir);

        // 77-character alphanumeric string (maximum for Code 16K)
        string codeText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789ABCDE";

        // Full path for the resulting TIFF file
        string outputPath = Path.Combine(outputDir, "Code16K.tiff");

        // Initialize the barcode generator with the Code 16K symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set high resolution (300 DPI) for better print quality
            generator.Parameters.Resolution = 300f;

            // Define module (X) size in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Set aspect ratio greater than 8 as recommended for Code 16K
            generator.Parameters.Barcode.Code16K.AspectRatio = 10;

            // Save the barcode as a TIFF image with the specified resolution
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Code 16K barcode saved to: {outputPath}");
    }
}