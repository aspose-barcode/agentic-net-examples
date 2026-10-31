// Title: Generate Code128 barcode with auto‑height by setting BarHeight to zero
// Description: Demonstrates how to create a Code128 barcode image with a fixed width of 40 mm while allowing the library to determine the optimal bar height automatically.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, ImageWidth, and BarHeight parameters. Developers often need to control barcode dimensions for printing or UI display, and auto‑height functionality simplifies layout by automatically adjusting bar height when set to zero or omitted.
// Prompt: Write code generating barcode with BarCodeHeight zero, BarCodeWidth 40 mm, and verify auto‑height behavior.
// Tags: barcode, code128, auto-height, image-width, millimeters, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a fixed width and automatic height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, saves it, and outputs the resulting image dimensions.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary folder
        string outputPath = Path.Combine(Path.GetTempPath(), "AutoHeightBarcode.png");

        // Create a barcode generator for Code128 symbology with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the image width to 40 millimeters
            generator.Parameters.ImageWidth.Millimeters = 40f;

            // Attempt to set the bar height to zero; this triggers an exception,
            // confirming that zero is not a valid explicit value.
            try
            {
                generator.Parameters.Barcode.BarHeight.Millimeters = 0f;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Setting BarHeight to zero threw an exception (expected): " + ex.Message);
                // Do not set BarHeight to enable the library's auto‑height feature
            }

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved image to verify its dimensions
        using (var bitmap = new Bitmap(outputPath))
        {
            Console.WriteLine($"Generated barcode image size: {bitmap.Width} px (width) x {bitmap.Height} px (height)");
        }
    }
}