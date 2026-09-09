// Title: Generate a barcode with custom colors and verify color values
// Description: Demonstrates creating a Code128 barcode with a custom background and bar color, saving it as a PNG, and then reading the image to confirm the colors match the specified ARGB values.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to customize barcode appearance using the BarcodeGenerator class and how to validate the output with Aspose.Drawing. Typical use cases include branding, UI integration, and quality assurance where specific colors are required. Developers often need to set BackColor and BarColor, save to common image formats, and programmatically verify visual properties.
// Prompt: Generate a barcode with custom colors and then read back the image to confirm color values.
// Tags: barcode, code128, generation, custom colors, png, aspose.barcode, aspose.drawing, verification

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with custom colors and verification of the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves it, and checks the background and bar colors.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_color_barcode.png");

        // Define custom colors: a light green background and a blue bar.
        Color backgroundColor = Color.FromArgb(255, 200, 255, 200);
        Color barColor = Color.Blue;

        // Generate the barcode with the specified colors.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply the custom background and bar colors.
            generator.Parameters.BackColor = backgroundColor;
            generator.Parameters.Barcode.BarColor = barColor;

            // Save the barcode image as PNG.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image for color verification.
        using (Image img = Image.FromFile(outputPath))
        {
            using (Bitmap bitmap = new Bitmap(img))
            {
                // Sample a background pixel (top-left corner).
                Color sampledBackground = bitmap.GetPixel(0, 0);

                // Sample a pixel near the center, which is likely part of a barcode bar.
                int centerX = bitmap.Width / 2;
                int centerY = bitmap.Height / 2;
                Color sampledBar = bitmap.GetPixel(centerX, centerY);

                // Compare the sampled colors with the expected ARGB values.
                bool backgroundMatches = sampledBackground.ToArgb() == backgroundColor.ToArgb();
                bool barMatches = sampledBar.ToArgb() == barColor.ToArgb();

                // Output verification results.
                Console.WriteLine($"Background color match: {backgroundMatches}");
                Console.WriteLine($"Bar color match: {barMatches}");
                Console.WriteLine($"Sampled background ARGB: {sampledBackground.ToArgb():X8}");
                Console.WriteLine($"Sampled bar ARGB: {sampledBar.ToArgb():X8}");
            }
        }
    }
}