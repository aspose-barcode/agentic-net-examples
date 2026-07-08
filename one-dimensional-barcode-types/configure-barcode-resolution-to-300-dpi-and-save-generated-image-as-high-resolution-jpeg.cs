// Title: Generate High-Resolution Barcode JPEG
// Description: This example creates a Code128 barcode, configures the image resolution to 300 DPI, and saves it as a high‑resolution JPEG file.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on the BarcodeGenerator class to produce printable barcodes. Typical use cases include creating high‑quality barcode images for packaging, shipping labels, or marketing materials. Developers often need to adjust resolution and output format using the Parameters and Save methods.
// Prompt: Configure barcode resolution to 300 DPI and save the generated image as a high‑resolution JPEG.
// Tags: code128, resolution, jpeg, generation, aspose.barcode

using System;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode image with a resolution of 300 DPI
/// and saves it as a high‑resolution JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        const string outputPath = "barcode_300dpi.jpg";

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure the image resolution to 300 DPI for high‑quality output
            generator.Parameters.Resolution = 300f;

            // Save the barcode as a JPEG image using the specified resolution
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}