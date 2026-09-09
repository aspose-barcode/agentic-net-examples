// Title: Generate Code128 barcode without human‑readable text and verify bars only
// Description: This example creates a Code128 barcode image with the text hidden, saves it as PNG, and programmatically checks that the image contains only barcode bars.
// Category-Description: Demonstrates Aspose.BarCode generation and basic image verification. It uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing classes (Bitmap, Color) to produce a barcode, hide the human‑readable code text, and confirm that the output image contains only the barcode pattern. Ideal for developers needing to generate clean barcodes for scanning systems where text display is undesirable.
// Prompt: Generate a barcode, set ShowCodeText to false, and confirm only bars are present in the output file.
// Tags: code128, barcode generation, hide codetext, image verification, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode without displaying the code text
/// and verifying that the resulting image contains only barcode bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and verifies the image.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file path
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "barcode.png");

        // Generate barcode without human‑readable text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Hide the code text by setting its location to None
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;
            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the saved image contains only bars (no text)
        bool onlyBars = true;
        using (var bitmap = new Bitmap(outputPath))
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            // Assume any text would appear in the bottom 20% of the image
            int textRegionStart = (int)(height * 0.8);
            for (int y = textRegionStart; y < height && onlyBars; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    // Background is white; any non‑white pixel in the text region suggests text
                    if (pixel.ToArgb() != Color.White.ToArgb())
                    {
                        onlyBars = false;
                        break;
                    }
                }
            }
        }

        // Output verification result
        Console.WriteLine(onlyBars
            ? "Verification passed: only bars are present in the output file."
            : "Verification failed: unexpected elements detected in the output file.");
    }
}