// Title: Generate high‑contrast Code128 barcode image with white background and black bars
// Description: Demonstrates how to create a Code128 barcode with a white background and black foreground, suitable for high‑contrast scanning environments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to customize barcode colors and export them as image files for printing or display. The snippet illustrates typical steps: setting output paths, configuring colors, and saving the image, which are common tasks when integrating barcode generation into .NET applications.
// Prompt: Set barcode background to white, foreground to black, and generate image for high‑contrast scanning environments.
// Tags: code128, color, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a high‑contrast Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode with white background and black bars,
    /// then saves it as a PNG file in a temporary directory.
    /// </summary>
    /// <param name="args">Optional command‑line arguments; the first argument can specify the barcode text.</param>
    static void Main(string[] args)
    {
        // Determine a temporary output directory for the generated image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Use the first command‑line argument as the barcode text, or fall back to a default value.
        string codeText = args.Length > 0 ? args[0] : "1234567890";

        // Build the full file path for the PNG image.
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set high‑contrast colors: white background and black bars.
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}