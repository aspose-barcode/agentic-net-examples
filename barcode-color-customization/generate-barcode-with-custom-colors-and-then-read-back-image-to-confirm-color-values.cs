// Title: Generate and Verify Barcode with Custom Colors
// Description: Creates a Code128 barcode image with blue bars on a yellow background, then reads the image to confirm color values and decode the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to customize barcode appearance using the BarcodeGenerator class and verify the result with BarCodeReader. Typical use cases include branding, UI integration, and quality checks where specific colors are required. Developers often need to set bar and background colors, save to common image formats, and ensure the barcode remains readable.
// Prompt: Generate a barcode with custom colors and then read back the image to confirm color values.
// Tags: code128, barcode generation, barcode recognition, custom colors, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates creating a barcode with custom colors, saving it as an image,
/// inspecting pixel colors, and reading the barcode back to verify the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a colored barcode, checks pixel colors,
    /// and decodes the barcode from the saved image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the barcode image.
        string imagePath = "custom_color_barcode.png";

        // Create a barcode generator for Code128 symbology with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Apply custom colors: blue bars on a yellow background.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Save the generated barcode as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image to inspect specific pixel colors.
        using (var bitmap = (Bitmap)Image.FromFile(imagePath))
        {
            // Sample the top-left pixel, which should represent the background color.
            Color topLeft = bitmap.GetPixel(0, 0);
            Console.WriteLine($"Top-left pixel color: {topLeft}");

            // Sample a pixel near the image center, likely part of a barcode bar.
            int centerX = bitmap.Width / 2;
            int centerY = bitmap.Height / 2;
            Color centerPixel = bitmap.GetPixel(centerX, centerY);
            Console.WriteLine($"Center pixel color: {centerPixel}");
        }

        // Use BarCodeReader to decode the barcode from the saved image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded text: {result.CodeText}");
            }
        }
    }
}