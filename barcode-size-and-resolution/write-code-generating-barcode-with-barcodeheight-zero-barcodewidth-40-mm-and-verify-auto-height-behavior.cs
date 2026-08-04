// Title: Generate barcode with auto‑height and fixed width
// Description: Creates a Code128 barcode image with a width of 40 mm while allowing the height to be calculated automatically.
// Category-Description: Demonstrates Aspose.BarCode image generation using the AutoSizeMode feature. This example shows how to set a fixed barcode width in millimeters, leave the height unset, and let the library compute the optimal height. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, which are commonly used for creating barcode images in various formats for printing or display.
// Prompt: Write code generating barcode with BarCodeHeight zero, BarCodeWidth 40 mm, and verify auto‑height behavior.
// Tags: barcode, code128, autosize, width, height, png, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a fixed width of 40 mm
/// and automatically calculated height, then outputs the image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it as PNG,
    /// and prints the resulting image size in pixels and millimeters.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 with the sample text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable auto‑size mode so the barcode height is calculated automatically.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the desired barcode width to 40 mm.
            generator.Parameters.ImageWidth.Millimeters = 40f;

            // Do NOT set BarHeight (leaving it at default) to allow auto‑height.
            // Save the barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the generated image to verify its actual dimensions.
        using (var bitmap = new Bitmap(outputPath))
        {
            int widthPx = bitmap.Width;
            int heightPx = bitmap.Height;

            // Use the generator's default resolution (96 dpi) for conversion to millimeters.
            float dpi = 96f;
            float widthMm = widthPx * 25.4f / dpi;
            float heightMm = heightPx * 25.4f / dpi;

            // Output the image size in both pixels and millimeters.
            Console.WriteLine($"Generated barcode image size: {widthPx} px × {heightPx} px");
            Console.WriteLine($"Width: {widthMm:F2} mm (expected 40 mm)");
            Console.WriteLine($"Height: {heightMm:F2} mm (auto‑calculated)");
        }
    }
}