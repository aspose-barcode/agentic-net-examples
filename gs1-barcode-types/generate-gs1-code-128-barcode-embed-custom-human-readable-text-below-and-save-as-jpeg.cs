// Title: Generate GS1 Code 128 barcode with custom caption and save as JPEG
// Description: Demonstrates creating a GS1 Code 128 barcode, adding a human‑readable caption below, and exporting the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator with EncodeTypes.GS1Code128, configure visual parameters such as X‑dimension, code‑text location, and custom captions, and save the result in a raster image format. Developers often need to produce GS1‑compliant barcodes for product identification and include readable text for scanning stations or printed labels.
// Prompt: Generate a GS1 Code 128 barcode, embed custom human‑readable text below, and save as JPEG.
// Tags: barcode, gs1, code128, caption, jpeg, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a GS1 Code 128 barcode with a custom caption and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, configures visual settings, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "GS1Code128.jpg");

        // GS1 Code 128 requires a 14‑digit GTIN after AI (01)
        string gs1CodeText = "(01)12345678901231";

        // Initialize the barcode generator for GS1 Code 128 with the specified data
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, gs1CodeText))
        {
            // Set the module (X) size to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Position the encoded text below the barcode bars
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Add a custom caption beneath the barcode
            generator.Parameters.CaptionBelow.Text = "Sample GS1 Code 128";

            // Reduce resolution to 72 DPI to keep the JPEG file size small
            generator.Parameters.Resolution = 72f;
            generator.Parameters.UseAntiAlias = false;

            // Save the generated barcode as a JPEG image
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}