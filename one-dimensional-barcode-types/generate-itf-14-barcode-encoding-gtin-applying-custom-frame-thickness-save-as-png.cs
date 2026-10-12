// Title: Generate ITF‑14 barcode with custom frame thickness and save as PNG
// Description: This example creates an ITF‑14 barcode encoding a 14‑digit GTIN, applies a custom frame border thickness, and saves the image as a PNG file.
// Category-Description: Demonstrates Aspose.BarCode barcode generation for the ITF‑14 symbology, covering configuration of barcode parameters such as X‑dimension and border settings. Typical use cases include encoding GTIN‑14 values for product packaging and applying visual styling before exporting to common image formats. Developers working with barcode creation often need to adjust module size, border type, and output format using the BarcodeGenerator class and related parameter objects.
// Prompt: Generate ITF‑14 barcode encoding GTIN, applying custom frame thickness, save as PNG.
// Tags: itf14, barcode, generation, png, aspose.barcode, gtin, frame border, x-dimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating an ITF‑14 barcode with a custom frame border and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures its appearance, and writes the PNG file to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "ITF14Barcode.png");

        // 14‑digit GTIN to be encoded in the ITF‑14 barcode.
        string gtin = "01234567890128";

        // Initialize the barcode generator with ITF‑14 symbology and the GTIN value.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.ITF14, gtin))
        {
            // Set the module (X‑dimension) size to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure a frame border around the barcode with a custom thickness of 5 points.
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;
            generator.Parameters.Barcode.ITF.BorderThickness.Point = 5f;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"ITF‑14 barcode saved to: {outputPath}");
    }
}