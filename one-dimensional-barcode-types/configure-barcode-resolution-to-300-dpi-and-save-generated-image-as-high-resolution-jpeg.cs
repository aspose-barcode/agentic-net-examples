// Title: Generate high‑resolution barcode image (300 DPI) and save as JPEG
// Description: Demonstrates how to set the barcode generator resolution to 300 DPI and save the result as a high‑resolution JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce raster barcode images. Typical scenarios include creating printable barcodes for labels, packaging, or documents where high resolution is required. Developers often need to adjust resolution and output format to meet printing standards.
// Prompt: Configure barcode resolution to 300 DPI and save the generated image as a high‑resolution JPEG.
// Tags: barcode, code128, resolution, jpeg, image generation, aspose.barcode, highdpi

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates configuring barcode resolution to 300 DPI and saving as a high‑resolution JPEG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, sets 300 DPI resolution, and writes a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Build the full path for the JPEG file.
        string outputPath = Path.Combine(outputDir, "barcode_300dpi.jpg");

        // Initialize the barcode generator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 300 DPI for high‑quality output.
            generator.Parameters.Resolution = 300f;

            // Save the barcode as a JPEG image using the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}