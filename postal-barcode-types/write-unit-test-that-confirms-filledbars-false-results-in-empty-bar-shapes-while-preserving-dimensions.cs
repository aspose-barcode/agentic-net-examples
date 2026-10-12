// Title: Barcode FilledBars Property Demonstration
// Description: Shows how setting FilledBars to false creates empty bar shapes while keeping barcode dimensions unchanged.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode appearance using the BarcodeGenerator class. Developers often need to adjust visual properties such as bar fill, dimensions, and output format for integration into reports, labels, or UI components. The snippet demonstrates typical use of EncodeTypes, BarCodeImageFormat, and drawing utilities to validate rendering outcomes.
// Prompt: Write a unit test that confirms FilledBars false results in empty bar shapes while preserving dimensions.
// Tags: barcode symbology, generation, filledbars, code128, png, dimensions, unit-test, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the effect of the FilledBars property on barcode rendering.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates two barcodes (filled and empty) and validates dimensions and pixel count.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated images.
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the filled and empty barcode images.
        string filledPath = Path.Combine(tempDir, "filled.png");
        string emptyPath = Path.Combine(tempDir, "empty.png");

        // Generate a barcode with default (filled) bars.
        using (var genFilled = new BarcodeGenerator(EncodeTypes.Code128, "TEST123"))
        {
            genFilled.Parameters.Barcode.XDimension.Pixels = 2f;   // Set bar width.
            genFilled.Parameters.Barcode.BarHeight.Pixels = 50f; // Set bar height.
            genFilled.Save(filledPath, BarCodeImageFormat.Png);   // Save as PNG.
        }

        // Generate a barcode with empty (non‑filled) bars.
        using (var genEmpty = new BarcodeGenerator(EncodeTypes.Code128, "TEST123"))
        {
            genEmpty.Parameters.Barcode.XDimension.Pixels = 2f;   // Set bar width.
            genEmpty.Parameters.Barcode.BarHeight.Pixels = 50f; // Set bar height.
            genEmpty.Parameters.Barcode.FilledBars = false;     // Disable bar fill.
            genEmpty.Save(emptyPath, BarCodeImageFormat.Png);   // Save as PNG.
        }

        // Load both images and compare their dimensions and black pixel counts.
        using (var bmpFilled = new Bitmap(filledPath))
        using (var bmpEmpty = new Bitmap(emptyPath))
        {
            bool dimensionsEqual = bmpFilled.Width == bmpEmpty.Width && bmpFilled.Height == bmpEmpty.Height;
            int filledBlack = CountBlackPixels(bmpFilled);
            int emptyBlack = CountBlackPixels(bmpEmpty);
            bool blackCountReduced = emptyBlack < filledBlack;

            if (dimensionsEqual && blackCountReduced)
            {
                Console.WriteLine("PASS: FilledBars false results in empty bars while preserving dimensions.");
            }
            else
            {
                Console.WriteLine("FAIL:");
                if (!dimensionsEqual)
                    Console.WriteLine($"  Dimensions differ: filled {bmpFilled.Width}x{bmpFilled.Height}, empty {bmpEmpty.Width}x{bmpEmpty.Height}");
                if (!blackCountReduced)
                    Console.WriteLine($"  Black pixel count not reduced: filled {filledBlack}, empty {emptyBlack}");
            }
        }
    }

    /// <summary>
    /// Counts the number of black pixels in the provided bitmap.
    /// </summary>
    /// <param name="bmp">Bitmap to analyze.</param>
    /// <returns>Number of black pixels.</returns>
    static int CountBlackPixels(Bitmap bmp)
    {
        int count = 0;
        for (int y = 0; y < bmp.Height; y++)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                Color color = bmp.GetPixel(x, y);
                if (color.ToArgb() == Color.Black.ToArgb())
                    count++;
            }
        }
        return count;
    }
}