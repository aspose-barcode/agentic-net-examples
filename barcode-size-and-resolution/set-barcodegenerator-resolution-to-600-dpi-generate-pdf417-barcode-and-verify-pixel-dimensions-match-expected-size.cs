// Title: Generate PDF417 barcode at 600 dpi and verify image dimensions
// Description: Demonstrates how to configure Aspose.BarCode's BarcodeGenerator to produce a PDF417 barcode image with a specific resolution, save it as PNG, and confirm that the pixel size and DPI match the expected values.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to create high‑resolution barcodes. Typical use cases include printing, scanning, and embedding barcodes in documents where exact pixel dimensions and DPI are required. Developers often need to control image size, resolution, and module dimensions to meet printing standards or UI constraints.
// Prompt: Set BarcodeGenerator resolution to 600 dpi, generate PDF417 barcode, and verify pixel dimensions match expected size.
// Tags: pdf417, barcode, resolution, dpi, image, generation, aspose.barcode, bitmap, verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a PDF417 barcode image at 600 dpi,
/// saves it to a temporary folder, and validates its pixel dimensions and resolution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures the barcode generator,
    /// produces the image, and writes verification results to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary output folder for the generated image.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Define barcode content, output path, expected size, and resolution.
        // --------------------------------------------------------------------
        string codeText = "Sample PDF417";
        string imagePath = Path.Combine(outputFolder, "pdf417.png");
        int expectedWidth = 600;   // expected width in pixels
        int expectedHeight = 300;  // expected height in pixels
        float resolutionDpi = 600f; // desired resolution in dots per inch

        // --------------------------------------------------------------------
        // Initialize the generator for PDF417 symbology with the provided text.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, codeText))
        {
            // Set the image resolution (DPI).
            generator.Parameters.Resolution = resolutionDpi;

            // Configure explicit image dimensions using AutoSizeMode.Nearest.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = expectedWidth;
            generator.Parameters.ImageHeight.Pixels = expectedHeight;

            // Optional: define the module (X) size in pixels for finer control.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // ----------------------------------------------------------------
            // Generate the barcode bitmap and save it as PNG.
            // ----------------------------------------------------------------
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image for visual inspection (optional).
                bitmap.Save(imagePath, ImageFormat.Png);

                // Verify that the generated bitmap matches the expected dimensions.
                bool sizeMatches = bitmap.Width == expectedWidth && bitmap.Height == expectedHeight;

                // Verify that the bitmap's DPI matches the requested resolution.
                bool resolutionMatches = Math.Abs(bitmap.HorizontalResolution - resolutionDpi) < 0.01f &&
                                         Math.Abs(bitmap.VerticalResolution - resolutionDpi) < 0.01f;

                // Output verification results.
                Console.WriteLine($"Generated barcode saved to: {imagePath}");
                Console.WriteLine($"Expected size: {expectedWidth}x{expectedHeight} px");
                Console.WriteLine($"Actual size:   {bitmap.Width}x{bitmap.Height} px");
                Console.WriteLine($"Size match:    {sizeMatches}");
                Console.WriteLine($"Resolution set: {resolutionDpi} dpi");
                Console.WriteLine($"Actual resolution: H={bitmap.HorizontalResolution} dpi, V={bitmap.VerticalResolution} dpi");
                Console.WriteLine($"Resolution match: {resolutionMatches}");
            }
        }
    }
}