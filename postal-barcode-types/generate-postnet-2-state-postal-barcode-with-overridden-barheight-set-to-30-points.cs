// Title: Generate Postnet 2‑state barcode with custom bar height
// Description: Demonstrates creating a Postnet 2‑state postal barcode and setting its bar height to 30 points.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on postal symbologies. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameter customization (BarHeight). Developers often need to generate printable postal barcodes with specific dimensions for mailing applications.
// Prompt: Generate a Postnet 2‑state postal barcode with overridden BarHeight set to 30 points.
// Tags: postnet, barcode, generation, barheight, png, aspnet, aspnet.barcode, encodetypes

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Postnet 2‑state barcode with a custom bar height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a Postnet barcode, sets bar height to 30 points, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a Postnet barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Override the default bar height to 30 points
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            // Define the output file path and save the barcode as a PNG image
            string outputPath = "postnet.png";
            generator.Save(outputPath);
            Console.WriteLine($"Postnet barcode saved to {outputPath}");
        }
    }
}