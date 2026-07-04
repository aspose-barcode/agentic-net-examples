// Title: Generate a Code128 barcode with custom foreground and background colors
// Description: Demonstrates creating a barcode image with blue bars on a yellow background, then verifies the colors and reads the barcode.
// Prompt: Generate a barcode with custom colors and then read back the image to confirm color values.
// Tags: code128, barcode generation, custom colors, png, aspose.barcode, aspose.drawing, barcode recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode with custom colors, verifies pixel colors, and reads the barcode back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, checks colors, and decodes the barcode.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the barcode image
        string filePath = "custom_color_barcode.png";

        // Create a barcode generator using Code128 symbology and the desired code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set custom foreground (bar) color to blue
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            // Set custom background color to yellow
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;

            // Adjust image size to ensure bars are clearly visible
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode as a PNG file
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image for pixel color verification
        using (var bitmap = new Bitmap(filePath))
        {
            // Retrieve the background color from the top-left pixel (expected to be background)
            Aspose.Drawing.Color bgPixel = bitmap.GetPixel(0, 0);

            // Retrieve the bar color from the center of the image (likely a bar)
            int centerX = bitmap.Width / 2;
            int centerY = bitmap.Height / 2;
            Aspose.Drawing.Color barPixel = bitmap.GetPixel(centerX, centerY);

            // Define the expected colors for comparison
            Aspose.Drawing.Color expectedBg = Aspose.Drawing.Color.Yellow;
            Aspose.Drawing.Color expectedBar = Aspose.Drawing.Color.Blue;

            // Output the actual and expected background colors with match status
            Console.WriteLine($"Background pixel at (0,0): {bgPixel}");
            Console.WriteLine($"Expected background: {expectedBg} - {(bgPixel.ToArgb() == expectedBg.ToArgb() ? "Match" : "Mismatch")}");

            // Output the actual and expected bar colors with match status
            Console.WriteLine($"Bar pixel at ({centerX},{centerY}): {barPixel}");
            Console.WriteLine($"Expected bar color: {expectedBar} - {(barPixel.ToArgb() == expectedBar.ToArgb() ? "Match" : "Mismatch")}");
        }

        // Read the barcode from the image to confirm the encoded text
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Detected codetext: {result.CodeText}");
            }
        }
    }
}