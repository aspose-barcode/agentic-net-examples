// Title: Generate Code128 barcode image with custom colors
// Description: Demonstrates how to set the barcode foreground to blue and background to white, then save the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, its Parameters, and color customization. Developers often need to customize barcode appearance for branding or UI integration, and this snippet shows typical steps: configure colors, generate a bitmap, and save to file.
// Prompt: Set barcode foreground to blue and background to white before generating the image.
// Tags: code128, barcode, color, png, aspose.barcode, image-generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with custom foreground and background colors and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, applies color settings, generates an image, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the barcode (foreground) color to blue.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Set the background color to white.
            generator.Parameters.BackColor = Color.White;

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to the specified path in PNG format.
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}