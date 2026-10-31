// Title: Generate PDF417 barcode at 600 dpi and verify pixel dimensions
// Description: This example creates a PDF417 barcode image with a resolution of 600 dpi, forces specific pixel dimensions, and checks that the generated bitmap matches the expected size.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on setting image resolution, controlling pixel dimensions, and validating output size. It uses BarcodeGenerator, EncodeTypes, and image handling classes (Bitmap, ImageFormat) to produce high‑resolution barcodes, a common requirement for printing and scanning applications where precise sizing is critical. Ideal for developers needing to customize barcode appearance and verify rendering results.
// Prompt: Set BarcodeGenerator resolution to 600 dpi, generate PDF417 barcode, and verify pixel dimensions match expected size.
// Tags: pdf417, barcode, resolution, image, generation, aspose.barcode, bitmap, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a PDF417 barcode at 600 dpi, enforce specific pixel dimensions,
/// and verify that the resulting bitmap matches the expected size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output folder, configures the barcode generator,
    /// validates the image size, and saves the PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string imagePath = Path.Combine(outputDir, "Pdf417_600dpi.png");

        // Initialize the barcode generator for PDF417 symbology with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleText"))
        {
            // Set the image resolution to 600 dots per inch
            generator.Parameters.Resolution = 600f;

            // Force the barcode image to specific pixel dimensions (300 × 150)
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 300f;
            generator.Parameters.ImageHeight.Pixels = 150f;

            // Generate the barcode as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Verify that the bitmap dimensions match the expected pixel size
                bool widthMatch = bitmap.Width == 300;
                bool heightMatch = bitmap.Height == 150;

                Console.WriteLine($"Generated bitmap size: {bitmap.Width}x{bitmap.Height} pixels");
                Console.WriteLine($"Resolution set to: {generator.Parameters.Resolution} dpi");
                Console.WriteLine(widthMatch && heightMatch
                    ? "Pixel dimensions match expected size."
                    : "Pixel dimensions do NOT match expected size.");

                // Save the bitmap as a PNG file
                bitmap.Save(imagePath, ImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {imagePath}");
            }
        }
    }
}