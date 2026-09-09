// Title: Generate Code128 barcode with transparent background and save as PNG
// Description: Demonstrates creating a Code128 barcode with a fully transparent background and saving it as a PNG image that retains the alpha channel.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class, set a transparent background via the BackColor property, and export the result as a PNG with an alpha channel. Typical use cases include overlaying barcodes on UI elements or documents where background transparency is required. Developers working with barcode rendering often need to control colors, formats, and image properties for seamless integration.
// Prompt: Implement method to generate barcode with transparent background and save as PNG with alpha channel.
// Tags: code128, barcode, transparent background, png, alpha channel, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode with a transparent background and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the output directory, generates the barcode, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Determine the output directory path relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the output directory exists; create it if it does not
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the resulting PNG file and the barcode text to encode
        string outputPath = Path.Combine(outputDir, "barcode_transparent.png");
        string codeText = "Sample123";

        // Initialize the barcode generator with Code128 symbology and the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set the background color to fully transparent (alpha = 0)
            generator.Parameters.BackColor = Color.FromArgb(0, 255, 255, 255);

            // Set the barcode bars to black for clear visibility
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode as a PNG image, preserving the alpha channel for transparency
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}