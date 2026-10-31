// Title: Barcode Color Comparison Example
// Description: Demonstrates generating the same barcode with default and custom colors and programmatically comparing the visual differences.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class and related drawing classes. It covers setting bar, background, border, and text colors, saving images with Aspose.Drawing, and performing pixel‑by‑pixel image comparison—common tasks for developers needing visual validation of barcode styling.
// Prompt: Apply different custom colors to the same barcode type and compare visual differences programmatically.
// Tags: barcode symbology, color customization, image comparison, code128, aspose.barcode, aspose.drawing, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an example that creates two Code128 barcodes with different color schemes,
/// saves them as PNG files, and compares their pixel data to highlight visual differences.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, saves them, and outputs the pixel difference count.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "1234567890";

        // Create a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeColorComparison_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // First barcode: default colors (black bars on white background)
        using (Bitmap bmp1 = CreateBarcode(
            codeText,
            Aspose.Drawing.Color.Black,   // Bar color
            Aspose.Drawing.Color.White,   // Background color
            Aspose.Drawing.Color.Black,   // Border color
            Aspose.Drawing.Color.Black)) // Text color
        {
            // Save the default-colored barcode image
            string path1 = Path.Combine(outputDir, "barcode_default.png");
            using (FileStream fs = new FileStream(path1, FileMode.Create, FileAccess.Write))
            {
                bmp1.Save(fs, ImageFormat.Png);
            }

            // Second barcode: custom colors
            using (Bitmap bmp2 = CreateBarcode(
                codeText,
                Aspose.Drawing.Color.Blue,    // Bar color
                Aspose.Drawing.Color.Yellow,  // Background color
                Aspose.Drawing.Color.Green,   // Border color
                Aspose.Drawing.Color.Red))    // Text color
            {
                // Save the custom-colored barcode image
                string path2 = Path.Combine(outputDir, "barcode_custom.png");
                using (FileStream fs = new FileStream(path2, FileMode.Create, FileAccess.Write))
                {
                    bmp2.Save(fs, ImageFormat.Png);
                }

                // Compare the two images pixel by pixel
                int diffCount = CompareBitmaps(bmp1, bmp2);
                Console.WriteLine($"Generated barcodes saved to: {outputDir}");
                Console.WriteLine($"Pixel differences between default and custom colored barcodes: {diffCount}");
            }
        }
    }

    /// <summary>
    /// Generates a barcode image with specified colors.
    /// </summary>
    /// <param name="codeText">The text to encode.</param>
    /// <param name="barColor">Color of the barcode bars.</param>
    /// <param name="backColor">Background color.</param>
    /// <param name="borderColor">Border color.</param>
    /// <param name="textColor">Color of the human‑readable text.</param>
    /// <returns>A Bitmap containing the generated barcode.</returns>
    static Bitmap CreateBarcode(string codeText, Color barColor, Color backColor, Color borderColor, Color textColor)
    {
        // Initialize the generator for Code128 symbology
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText);

        // Apply color customizations
        generator.Parameters.Barcode.BarColor = barColor;
        generator.Parameters.BackColor = backColor;
        generator.Parameters.Border.Color = borderColor;
        generator.Parameters.Border.Width.Pixels = 2f;
        generator.Parameters.Barcode.CodeTextParameters.Color = textColor;
        generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

        // Ensure consistent size for comparison
        generator.Parameters.Barcode.XDimension.Point = 2f;

        // Generate the barcode image
        Bitmap bitmap = generator.GenerateBarCodeImage();

        // Clean up generator resources
        generator.Dispose();

        return bitmap;
    }

    /// <summary>
    /// Compares two bitmaps pixel by pixel and returns the count of differing pixels.
    /// </summary>
    /// <param name="bmpA">First bitmap.</param>
    /// <param name="bmpB">Second bitmap.</param>
    /// <returns>
    /// Number of pixels that differ, or -1 if the images have different dimensions.
    /// </returns>
    static int CompareBitmaps(Bitmap bmpA, Bitmap bmpB)
    {
        // Verify that both images share the same dimensions
        if (bmpA.Width != bmpB.Width || bmpA.Height != bmpB.Height)
            return -1; // Different dimensions

        int diff = 0;
        for (int y = 0; y < bmpA.Height; y++)
        {
            for (int x = 0; x < bmpA.Width; x++)
            {
                // Compare ARGB values of each pixel
                if (bmpA.GetPixel(x, y).ToArgb() != bmpB.GetPixel(x, y).ToArgb())
                    diff++;
            }
        }
        return diff;
    }
}