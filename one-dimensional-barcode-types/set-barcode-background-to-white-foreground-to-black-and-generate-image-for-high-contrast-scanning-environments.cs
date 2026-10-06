// Title: Generate High‑Contrast Code128 Barcode Image
// Description: Demonstrates how to create a Code128 barcode with a white background and black bars, suitable for high‑contrast scanning environments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Developers often need to adjust colors for readability on various media, and this snippet shows the typical steps for setting background and foreground colors before saving the image.
// Prompt: Set barcode background to white, foreground to black, and generate image for high‑contrast scanning environments.
// Tags: code128, barcode, color, high-contrast, image, png, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an entry point that generates a high‑contrast barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Creates a Code128 barcode with white background and black bars, then saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "high_contrast_barcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set high‑contrast colors: white background and black bars
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}