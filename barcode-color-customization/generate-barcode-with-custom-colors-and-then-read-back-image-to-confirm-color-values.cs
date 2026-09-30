// Title: Generate barcode with custom colors and verify colors
// Description: Demonstrates creating a Code128 barcode image with custom foreground and background colors, saving it as PNG, then reading the barcode and inspecting pixel colors to confirm the applied colors.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to customize barcode appearance (BarColor, BackColor) and BarCodeReader to decode the image. Typical use cases include branding, UI integration, and validation of visual barcode properties. Developers often need to adjust colors to match corporate design guidelines and verify the output programmatically.
// Prompt: Generate a barcode with custom colors and then read back the image to confirm color values.
// Tags: code128, barcode generation, barcode recognition, custom colors, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode with custom colors, decodes it, and inspects pixel values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, reads it back, and validates color values.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Set up a temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "custom_color_barcode.png");

        // --------------------------------------------------------------------
        // Generate a Code128 barcode with custom foreground (blue) and background (yellow) colors.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply custom colors.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;   // Foreground (bars)
            generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;       // Background

            // Save the barcode as a PNG file.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Decode the barcode image to confirm that the encoded text is correct.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                Console.WriteLine($"Decoded Symbology: {result.CodeType}");
            }
        }

        // --------------------------------------------------------------------
        // Load the saved image and sample pixel colors to verify custom colors.
        // --------------------------------------------------------------------
        using (var bitmap = new Aspose.Drawing.Bitmap(barcodePath))
        {
            // Sample a pixel from the top-left corner (expected background color).
            Color bgPixel = bitmap.GetPixel(0, 0);

            // Sample a pixel from the image center (likely part of a barcode bar, foreground color).
            int centerX = bitmap.Width / 2;
            int centerY = bitmap.Height / 2;
            Color fgPixel = bitmap.GetPixel(centerX, centerY);

            Console.WriteLine($"Background pixel ARGB: 0x{bgPixel.ToArgb():X8}");
            Console.WriteLine($"Foreground pixel ARGB: 0x{fgPixel.ToArgb():X8}");
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional). Failures are ignored to avoid affecting program outcome.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome.
        }
    }
}