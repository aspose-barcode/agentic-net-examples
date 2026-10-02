// Title: Generate PDF417 barcode at 600 dpi and verify image dimensions
// Description: This example creates a PDF417 barcode with a resolution of 600 dpi, saves it as a PNG, and checks that the generated image matches the expected pixel width and height.
// Category-Description: Demonstrates Aspose.BarCode generation features, focusing on setting image resolution, defining canvas size, and validating output dimensions. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes—common tools for developers who need high‑resolution barcodes for printing or scanning applications. This snippet belongs to a collection of examples illustrating barcode creation, image manipulation, and quality verification.
// Prompt: Set BarcodeGenerator resolution to 600 dpi, generate PDF417 barcode, and verify pixel dimensions match expected size.
// Tags: pdf417, barcode, resolution, image, png, aspose.barcode, generation, verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a PDF417 barcode at 600 dpi, saving it as PNG, and validating its pixel dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, saves it, checks dimensions, and cleans up the file.
    /// </summary>
    static void Main()
    {
        // Define temporary output file path
        string outputPath = Path.Combine(Path.GetTempPath(), "Pdf417_600dpi.png");

        // Initialize barcode generator for PDF417 symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample123"))
        {
            // Set image resolution to 600 dots per inch
            generator.Parameters.Resolution = 600f;

            // Specify expected canvas size in pixels (width x height)
            generator.Parameters.ImageWidth.Pixels = 600f;
            generator.Parameters.ImageHeight.Pixels = 300f;

            // Save the generated barcode image to a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Generate an in‑memory bitmap to inspect actual dimensions
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                Console.WriteLine($"Generated image size: {actualWidth}x{actualHeight} pixels");
                Console.WriteLine($"Expected image size: 600x300 pixels");

                // Verify that the actual dimensions match the expected values
                if (actualWidth == 600 && actualHeight == 300)
                {
                    Console.WriteLine("Pixel dimensions match expected size.");
                }
                else
                {
                    Console.WriteLine("Pixel dimensions do NOT match expected size.");
                }
            }
        }

        // Optional clean‑up: delete the temporary PNG file if it exists
        if (File.Exists(outputPath))
        {
            File.Delete(outputPath);
        }
    }
}