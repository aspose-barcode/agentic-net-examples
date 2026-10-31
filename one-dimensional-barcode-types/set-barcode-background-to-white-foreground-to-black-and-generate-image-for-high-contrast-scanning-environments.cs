// Title: Generate high‑contrast Code128 barcode image
// Description: Creates a Code128 barcode with a white background and black bars, saving it as a PNG file for optimal scanning in high‑contrast environments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure visual properties of a barcode using the BarcodeGenerator class. Typical use cases include customizing colors for readability, generating images for printing or display, and preparing assets for scanning devices. Developers often need to set background and bar colors, choose image formats, and save the result to a file.
// Prompt: Set barcode background to white, foreground to black, and generate image for high‑contrast scanning environments.
// Tags: code128, barcode, color, background, foreground, png, image, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a high‑contrast Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures colors, saves the PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the background color to white for high‑contrast scanning.
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Set the bar (foreground) color to black.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the generated barcode as a PNG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}