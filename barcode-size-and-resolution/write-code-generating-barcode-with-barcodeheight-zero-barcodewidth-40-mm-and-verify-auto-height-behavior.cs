// Title: Generate barcode with auto‑height and fixed width
// Description: Demonstrates creating a Code128 barcode with a fixed width of 40 mm while letting the height be calculated automatically.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to control image dimensions using the AutoSizeMode, ImageWidth, and BarHeight properties. Developers often need to generate barcodes with specific width constraints while allowing the library to determine optimal height for readability. The code uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce PNG output.
// Prompt: Write code generating barcode with BarCodeHeight zero, BarCodeWidth 40 mm, and verify auto‑height behavior.
// Tags: code128, barcode, auto-size, width, height, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a fixed width of 40 mm,
/// lets the library automatically determine the height, saves the image,
/// and then verifies the resulting height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it as PNG,
    /// and prints the image dimensions and calculated height in millimeters.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated barcode image
        const string outputPath = "barcode.png";

        // Create a barcode generator for Code128 with sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable auto‑size mode based on interpolation to let height adjust automatically
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired barcode width to 40 mm (height will be auto‑calculated)
            generator.Parameters.ImageWidth.Millimeters = 40f;

            // Do NOT set BarHeight; the auto‑height behavior will be applied

            // Save the generated barcode image in PNG format
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved image to verify the resulting height
        using (var image = (Image)Image.FromFile(outputPath))
        {
            // Image dimensions in pixels
            int widthPx = image.Width;
            int heightPx = image.Height;

            // Resolution (dpi) used during generation (default is 96)
            const float resolutionDpi = 96f; // same as generator.Parameters.Resolution default

            // Convert height from pixels to millimeters: (pixels / dpi) * 25.4
            float heightMm = heightPx / resolutionDpi * 25.4f;

            // Output the image size and calculated barcode height
            Console.WriteLine($"Barcode image size: {widthPx}x{heightPx} pixels");
            Console.WriteLine($"Calculated barcode height: {heightMm:F2} mm (auto‑height)");
        }
    }
}