// Title: Generate high‑contrast barcode image with white background and black bars
// Description: Demonstrates how to configure Aspose.BarCode to produce a barcode with a white background and black foreground, suitable for high‑contrast scanning environments.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Developers often need to adjust colors for readability, export barcodes to common image formats, and integrate them into applications that require optimal scan performance. The snippet shows typical steps for setting background and bar colors before saving the image.
// Prompt: Set barcode background to white, foreground to black, and generate image for high‑contrast scanning environments.
// Tags: barcode, high-contrast, background-color, foreground-color, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an entry point that generates a high‑contrast barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with white background and black bars, then saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string outputPath = "high_contrast_barcode.png";

        // Create a barcode generator for Code128 symbology with sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set high‑contrast colors: white background and black bars
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image in PNG format to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}