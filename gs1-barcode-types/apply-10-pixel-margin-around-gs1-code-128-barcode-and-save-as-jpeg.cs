// Title: Apply 10‑pixel margin to GS1 Code 128 barcode and save as JPEG
// Description: Demonstrates how to generate a GS1 Code 128 barcode with a 10‑pixel margin on all sides and save the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.GS1Code128. Developers commonly need to customize barcode appearance, such as adding padding or margins, and export to common image formats like JPEG. The snippet shows setting padding properties and saving the result, a typical task when integrating barcodes into documents or web pages.
// Prompt: Apply a 10‑pixel margin around a GS1 Code 128 barcode and save as JPEG.
// Tags: gs1code128, margin, jpeg, barcodegenerator, encodetypes

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a GS1 Code 128 barcode with a 10‑pixel margin and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// applies padding, and writes the resulting image to disk.
    /// </summary>
    static void Main()
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // GS1 Code 128 barcode text (example with AI (01) and 14‑digit GTIN)
        string codeText = "(01)12345678901231";

        // Output file path
        string outputPath = Path.Combine(outputDir, "GS1Code128_10pxMargin.jpg");

        // Generate barcode with 10‑pixel padding on all sides and save as JPEG
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, codeText))
        {
            // Set 10‑pixel margin (padding) for each side
            generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

            // Save the barcode image as JPEG
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}