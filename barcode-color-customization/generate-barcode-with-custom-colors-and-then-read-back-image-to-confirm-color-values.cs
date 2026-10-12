// Title: Generate Barcode with Custom Colors and Verify Colors
// Description: This example creates a Code128 barcode image with a green background and red bars, saves it as PNG, then reads the image to confirm the colors match the expected values.
// Category-Description: Demonstrates Aspose.BarCode generation and image verification using Aspose.Drawing. It showcases the BarcodeGenerator class for creating barcodes with custom visual properties and how to load the resulting image to inspect pixel colors. Typical use cases include branding barcodes with corporate colors and validating output in automated tests. Developers working with barcode creation and image processing often need to customize appearance and programmatically verify results.
// Prompt: Generate a barcode with custom colors and then read back the image to confirm color values.
// Tags: barcode symbology, generation, custom colors, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode with custom colors and verifying the colors by reading the saved image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "custom_color_barcode.png");

        // Define custom colors: green background and red bars.
        Color backgroundColor = Color.FromArgb(255, 0, 255, 0); // Green
        Color barColor = Color.FromArgb(255, 255, 0, 0);       // Red

        // Generate the barcode with the specified colors.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Parameters.BackColor = backgroundColor;      // Set background color.
            generator.Parameters.Barcode.BarColor = barColor;      // Set bar (foreground) color.
            generator.Save(outputPath, BarCodeImageFormat.Png);    // Save as PNG.
        }

        // Verify that the image file was created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the saved image to verify the colors.
        using (var bitmap = new Bitmap(outputPath))
        {
            // Sample the background pixel (top-left corner).
            Color sampledBackground = bitmap.GetPixel(0, 0);

            // Find a pixel that differs from the background (assumed to be a barcode bar).
            Color sampledBar = Color.Empty;
            bool barFound = false;
            for (int y = 0; y < bitmap.Height && !barFound; y++)
            {
                for (int x = 0; x < bitmap.Width && !barFound; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (!pixel.Equals(sampledBackground))
                    {
                        sampledBar = pixel;
                        barFound = true;
                    }
                }
            }

            // Output verification results for background color.
            Console.WriteLine($"Expected Background: {backgroundColor.ToArgb()}, Sampled: {sampledBackground.ToArgb()}, Match: {sampledBackground.Equals(backgroundColor)}");

            // Output verification results for bar color, if a bar pixel was found.
            if (barFound)
            {
                Console.WriteLine($"Expected Bar Color: {barColor.ToArgb()}, Sampled: {sampledBar.ToArgb()}, Match: {sampledBar.Equals(barColor)}");
            }
            else
            {
                Console.WriteLine("No bar pixel detected for verification.");
            }
        }
    }
}