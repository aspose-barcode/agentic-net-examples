// Title: Generate PDF417 barcode at 600 dpi and validate image size
// Description: Demonstrates setting the BarcodeGenerator resolution to 600 dpi, creating a PDF417 barcode, saving it as PNG, and confirming the resulting pixel dimensions.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure barcode resolution and image size using the BarcodeGenerator and related parameter classes. Typical use cases include high‑resolution printing, precise layout calculations, and verification of generated barcode dimensions. Developers often need to adjust DPI, image width/height, and validate output for compliance with printing standards.
// Prompt: Set BarcodeGenerator resolution to 600 dpi, generate PDF417 barcode, and verify pixel dimensions match expected size.
// Tags: pdf417, barcode generation, resolution, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a PDF417 barcode at 600 dpi,
/// saves it as a PNG file, and verifies the resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation,
    /// saves the image, and validates DPI and pixel size.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "pdf417.png";

        // Create a PDF417 barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Set the image resolution to 600 dpi.
            generator.Parameters.Resolution = 600f;

            // Define the desired image size in points (1 point = 1/72 inch).
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 100f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved image to verify its DPI and pixel dimensions.
        using (var image = Image.FromFile(outputPath))
        {
            // Retrieve actual DPI values from the image.
            float horizontalDpi = image.HorizontalResolution;
            float verticalDpi   = image.VerticalResolution;

            // Retrieve actual pixel dimensions.
            int actualWidth  = image.Width;
            int actualHeight = image.Height;

            // Calculate expected pixel dimensions based on points and resolution.
            // pixels = points * (dpi / 72)
            int expectedWidth  = (int)Math.Round(200f * 600f / 72f);
            int expectedHeight = (int)Math.Round(100f * 600f / 72f);

            // Output diagnostic information.
            Console.WriteLine($"Resolution: {horizontalDpi} dpi (H), {verticalDpi} dpi (V)");
            Console.WriteLine($"Actual size: {actualWidth}×{actualHeight} px");
            Console.WriteLine($"Expected size: {expectedWidth}×{expectedHeight} px");

            // Verify that the image DPI matches the requested 600 dpi.
            if (Math.Abs(horizontalDpi - 600f) > 0.1f || Math.Abs(verticalDpi - 600f) > 0.1f)
                Console.WriteLine("Resolution mismatch!");
            else
                Console.WriteLine("Resolution matches the expected 600 dpi.");

            // Verify that the pixel dimensions match the calculated expectations.
            if (actualWidth == expectedWidth && actualHeight == expectedHeight)
                Console.WriteLine("Pixel dimensions match the expected size.");
            else
                Console.WriteLine("Pixel dimensions do NOT match the expected size.");
        }
    }
}