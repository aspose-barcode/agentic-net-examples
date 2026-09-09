// Title: Generate MaxiCode barcode with 300 DPI resolution
// Description: Demonstrates how to set the barcode image resolution to 300 DPI when creating a MaxiCode, improving print quality.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure image resolution and dimensions for barcode output. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce high‑resolution barcodes, a common requirement for printing and packaging scenarios. Developers often need to adjust DPI to meet printer specifications and ensure readability.
// Prompt: Set the barcode image DPI to 300 when generating a MaxiCode to improve print quality.
// Tags: maxicode, dpi, resolution, barcode generation, aspose.barcode, png, image quality

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a 300 DPI resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates the barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output file
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the generated PNG image
        string outPath = Path.Combine(outputDir, "maxicode.png");

        // Initialize the barcode generator for MaxiCode with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the image resolution to 300 DPI for higher print quality
            generator.Parameters.Resolution = 300f;

            // Optionally adjust the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 15f;

            // Save the generated barcode as a PNG file
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outPath}");
    }
}