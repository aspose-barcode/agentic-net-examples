// Title: Generate barcode image with AutoSizeMode.Nearest using Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode, configuring AutoSizeMode to Nearest, and specifying only image width and height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes, AutoSizeMode, and image size parameters. Developers commonly generate barcodes for labeling, inventory, and shipping, needing precise control over image dimensions while letting the library auto‑size the barcode content. The snippet shows typical steps: instantiate BarcodeGenerator, set parameters, and save to PNG.
// Prompt: Generate a barcode image using AutoSizeMode.Nearest, providing only ImageHeight and ImageWidth parameters.
// Tags: barcode, code128, autosizemode, image, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode image with AutoSizeMode.Nearest.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a barcode, configures size, and saves as PNG.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "123456";

        // Build the full output path for the PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure AutoSizeMode to Nearest so the library adjusts the barcode to fit the given dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set only the desired image width and height (in pixels). The barcode will be scaled accordingly.
            generator.Parameters.ImageWidth.Pixels = 300f;   // Desired width
            generator.Parameters.ImageHeight.Pixels = 150f; // Desired height

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}