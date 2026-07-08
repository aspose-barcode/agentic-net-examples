// Title: Create Code128 barcode with custom foreground color and verify it
// Description: This example creates a Code128 barcode, sets its bar color to #123456, saves it as a PNG file, and checks that the exact color appears in the generated image.
// Category-Description: Demonstrates Aspose.BarCode generation and image verification techniques. It uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing classes to customize barcode appearance, save the image, and programmatically inspect pixel data. Ideal for developers needing to ensure visual fidelity of generated barcodes in automated pipelines.
// Prompt: Create a barcode, set ForeColor to #123456, and verify the exact color appears in the saved image.
// Tags: code128, color, png, barcode, generation, verification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a barcode with a custom foreground color and verifying the color in the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, sets its bar color to #123456, saves it as PNG,
    /// and scans the resulting image to confirm the exact color is present.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.png";

        // Create a BarcodeGenerator for Code128 symbology with sample text "Test123".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Set the foreground (bar) color to the custom hex value #123456.
            generator.Parameters.Barcode.BarColor = Color.FromArgb(0x12, 0x34, 0x56);
            // Set the background color to white to ensure good contrast.
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image in PNG format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Load the saved PNG image for pixel inspection.
        using (var bitmap = (Bitmap)Image.FromFile(outputPath))
        {
            bool colorFound = false;
            // Define the target color to search for in the image.
            Color targetColor = Color.FromArgb(0x12, 0x34, 0x56);

            // Scan the image pixels until the target color is found.
            for (int y = 0; y < bitmap.Height && !colorFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !colorFound; x++)
                {
                    // Compare the ARGB values of the current pixel and the target color.
                    if (bitmap.GetPixel(x, y).ToArgb() == targetColor.ToArgb())
                    {
                        colorFound = true;
                    }
                }
            }

            // Output verification result.
            Console.WriteLine(colorFound ? "Bar color verified." : "Bar color not found in the image.");
        }
    }
}