// Title: Generate a Postnet barcode with transparent background and save as PNG
// Description: This example creates a US Postal Service Postnet barcode, applies a transparent background, and saves the image as a PNG file that retains the alpha channel.
// Category-Description: Demonstrates Aspose.BarCode generation for postal symbologies. It uses the BarcodeGenerator class with EncodeTypes.Postnet, configures visual parameters such as background and bar colors, and saves the result in a format supporting transparency (PNG). Typical use cases include creating shipping labels, mailing automation, and integrating barcode images into documents where a clear background is required. Developers working with barcode creation, especially for postal services, frequently need to control colors and output formats using the Aspose.BarCode API.
// Prompt: Generate a postal barcode with transparent background and save as PNG with alpha channel.
// Tags: postnet, postal barcode, transparent background, png, alpha channel, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Postnet barcode with a transparent background and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes a confirmation message.
    /// </summary>
    static void Main()
    {
        // Generate a postal (Postnet) barcode with transparent background and save as PNG.
        GeneratePostalBarcode("postal.png", "12345");

        // Inform the user that the barcode has been saved.
        Console.WriteLine("Barcode saved to postal.png");
    }

    /// <summary>
    /// Creates a Postnet barcode image with a transparent background and saves it as PNG.
    /// </summary>
    /// <param name="outputPath">Full file path where the PNG image will be saved.</param>
    /// <param name="zipCode">ZIP code data to encode in the barcode.</param>
    static void GeneratePostalBarcode(string outputPath, string zipCode)
    {
        // Ensure the output directory exists.
        string directory = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Initialize the barcode generator for Postnet symbology with the provided ZIP code.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, zipCode))
        {
            // Set the background to transparent so the PNG retains an alpha channel.
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the bar (foreground) color; black is the typical choice.
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image, which supports transparency.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}