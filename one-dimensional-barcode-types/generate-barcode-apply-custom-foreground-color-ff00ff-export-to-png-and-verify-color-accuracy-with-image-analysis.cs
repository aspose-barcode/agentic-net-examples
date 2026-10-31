// Title: Generate Code128 barcode with custom magenta foreground and verify color
// Description: This example creates a Code128 barcode, sets its foreground color to magenta (#FF00FF), saves it as a PNG, and checks that the generated bars use the expected color by scanning the image pixels.
// Category-Description: Demonstrates Aspose.BarCode generation features, specifically customizing barcode appearance with BarColor, exporting to PNG via BarCodeImageFormat, and performing simple image analysis using Aspose.Drawing. Typical use cases include branding, visual verification, and automated testing of barcode rendering. Developers working with barcode creation, color customization, and image validation will find this pattern useful.
// Prompt: Generate a barcode, apply custom foreground color #FF00FF, export to PNG, and verify color accuracy with image analysis.
// Tags: barcode, code128, color, png, image-analysis, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom foreground color,
/// exporting it to PNG, and verifying the color via pixel analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, validates the bar color,
    /// and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Define barcode parameters and expected color
        // ------------------------------------------------------------
        string codeText = "12345678";
        string tempFile = Path.Combine(Path.GetTempPath(), "barcode_" + Guid.NewGuid().ToString("N") + ".png");
        Color expectedBarColor = Color.FromArgb(255, 255, 0, 255); // #FF00FF (magenta)

        // ------------------------------------------------------------
        // Generate barcode with custom foreground color and save as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = expectedBarColor;
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Verify that the file was created
        if (!File.Exists(tempFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Verify color by scanning pixels until a non‑white pixel is found
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(tempFile))
        {
            bool verificationDone = false;
            for (int y = 0; y < bitmap.Height && !verificationDone; y++)
            {
                for (int x = 0; x < bitmap.Width && !verificationDone; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (!pixel.Equals(Color.White))
                    {
                        bool match = pixel.ToArgb() == expectedBarColor.ToArgb();
                        Console.WriteLine(match
                            ? "Bar color matches expected #FF00FF."
                            : $"Bar color does NOT match. Found ARGB: {pixel.ToArgb():X8}");
                        verificationDone = true;
                    }
                }
            }

            if (!verificationDone)
            {
                Console.WriteLine("No non-white pixels found; cannot verify bar color.");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary file
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempFile);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}