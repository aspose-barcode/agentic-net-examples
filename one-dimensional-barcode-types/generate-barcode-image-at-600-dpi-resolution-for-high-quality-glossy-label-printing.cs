// Title: Generate a 600 DPI Code128 barcode image for glossy label printing
// Description: Demonstrates how to create a Code128 barcode at 600 DPI resolution, suitable for high‑quality glossy label output.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce high‑resolution raster images. Developers often need to adjust resolution and module size when printing barcodes on premium media; this snippet shows the typical API calls for such scenarios.
// Prompt: Generate a barcode image at 600 DPI resolution for high‑quality glossy label printing.
// Tags: code128, generation, png, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode image at 600 DPI and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_600dpi.png");

        // Initialize the barcode generator with Code128 symbology and data string
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 600 DPI for high‑quality printing
            generator.Parameters.Resolution = 600f;

            // Optionally define the module (X) dimension in millimeters (e.g., 0.5 mm)
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}