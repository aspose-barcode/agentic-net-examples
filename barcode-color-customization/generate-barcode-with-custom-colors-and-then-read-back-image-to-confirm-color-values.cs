// Title: Generate barcode with custom colors and verify colors via image analysis
// Description: This example creates a Code128 barcode with custom background, bar, border, and text colors, saves it as a PNG file, reads the barcode to confirm the encoded text, and inspects pixel colors to verify the applied colors.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows. It uses BarcodeGenerator to customize visual appearance, BarCodeReader to decode the symbol, and Aspose.Drawing.Bitmap for pixel-level validation. Typical for developers who need to style barcodes for branding while ensuring the output remains readable. This example belongs to the “Barcode creation, customization, and validation” collection, useful for search queries about customizing barcode colors with Aspose.BarCode.
/// Prompt: Generate a barcode with custom colors and then read back the image to confirm color values.
// Tags: barcode, code128, custom colors, generation, recognition, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode with custom colors, reading it back, and verifying pixel colors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define output file path and barcode text
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_color_barcode.png");
        string codeText = "1234567890";

        // ------------------------------------------------------------
        // Generate barcode with custom colors
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set background, bar, border, and text colors
            generator.Parameters.BackColor = Color.Yellow;
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.Border.Color = Color.Green;
            generator.Parameters.Barcode.CodeTextParameters.Color = Color.Red;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read back the barcode and verify decoded text
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Decoded text: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Load the saved image and check pixel colors
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(outputPath))
        {
            // Sample background pixel (top-left corner)
            Color bgPixel = bitmap.GetPixel(0, 0);
            // Sample a pixel near the center (likely part of the bars)
            Color barPixel = bitmap.GetPixel(bitmap.Width / 2, bitmap.Height / 2);

            Console.WriteLine($"Background pixel ARGB: 0x{bgPixel.ToArgb():X8}");
            Console.WriteLine($"Center pixel ARGB:    0x{barPixel.ToArgb():X8}");

            // Compare sampled colors with expected values
            bool bgMatch = bgPixel.ToArgb() == Color.Yellow.ToArgb();
            bool barMatch = barPixel.ToArgb() == Color.Blue.ToArgb();

            Console.WriteLine($"Background color matches expected: {bgMatch}");
            Console.WriteLine($"Bar color matches expected (approx): {barMatch}");
        }
    }
}