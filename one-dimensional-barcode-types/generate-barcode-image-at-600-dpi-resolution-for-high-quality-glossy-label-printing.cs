// Title: Generate 600 DPI Code128 barcode image for glossy label printing
// Description: Demonstrates how to create a Code128 barcode image at 600 DPI, suitable for high‑resolution glossy label output.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce high‑resolution barcode graphics. Developers often need to adjust resolution and dimensions for print‑ready barcodes, especially for glossy labels or packaging where clarity is critical. The snippet shows typical API calls for setting DPI, X‑dimension, and saving to PNG.
// Prompt: Generate a barcode image at 600 DPI resolution for high‑quality glossy label printing.
// Tags: code128, barcode, resolution, dpi, png, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode image at 600 DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Environment.CurrentDirectory, "barcode_600dpi.png");

        // Initialize the barcode generator with Code128 symbology and data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set image resolution to 600 DPI for high‑quality printing
            generator.Parameters.Resolution = 600f;

            // Define the X dimension (module width) in millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}