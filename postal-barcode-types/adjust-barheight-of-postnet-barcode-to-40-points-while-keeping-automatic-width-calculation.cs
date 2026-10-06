// Title: Adjust Postnet Barcode BarHeight to 40 Points
// Description: Demonstrates how to set the bar height of a Postnet barcode to 40 points while allowing the library to compute the optimal width automatically.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameter customization. Developers often need to create postal barcodes (e.g., Postnet) with specific visual dimensions for printing or display, while relying on automatic width calculation to maintain correct encoding. The snippet illustrates typical steps: initializing the generator, adjusting bar dimensions, and saving the image.
// Prompt: Adjust the BarHeight of a Postnet barcode to 40 points while keeping automatic width calculation.
// Tags: postnet, barcode, barheight, aspose.barcode, image generation, png, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Postnet barcode with a custom bar height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Postnet barcode with a bar height of 40 points,
    /// saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "PostnetExample");
        Directory.CreateDirectory(outputDir);

        // Full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "PostnetBarHeight40.png");

        // Initialize the barcode generator for Postnet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "123456"))
        {
            // Set bar height to 40 points; width will be calculated automatically
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Postnet barcode saved to: {outputPath}");
    }
}