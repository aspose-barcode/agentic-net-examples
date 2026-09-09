// Title: Generate a Planet postal barcode with custom margins and verify dimensions
// Description: Demonstrates creating a Planet postal barcode, applying custom padding, saving images, and checking that the resulting image dimensions reflect the added margins.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension, BarHeight, and Padding. Typical use cases include generating postal barcodes for mailing applications where precise image sizing and margin control are required. Developers often need to adjust padding to meet printing specifications, and this sample illustrates how to verify the effect programmatically.
// Prompt: Generate a postal barcode with custom margin settings and verify image dimensions match expectations.
// Tags: barcode, postal, planet, padding, image-dimensions, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Planet postal barcode with and without custom padding,
/// saving the images, and verifying that the padded image dimensions match the expected size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, generates barcode images,
    /// applies padding, saves files, and prints dimension verification results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "PostalBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Initialize barcode generator for Planet symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Planet, "123456"))
        {
            // Set basic barcode appearance parameters
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;

            // Generate barcode image without any custom padding
            using (var bitmapNoPad = generator.GenerateBarCodeImage())
            {
                int widthNoPad = bitmapNoPad.Width;
                int heightNoPad = bitmapNoPad.Height;
                string noPadPath = Path.Combine(outputDir, "planet_nopad.png");
                bitmapNoPad.Save(noPadPath, ImageFormat.Png);

                // Apply custom padding (10 pixels on each side)
                generator.Parameters.Barcode.Padding.Left.Pixels = 10f;
                generator.Parameters.Barcode.Padding.Top.Pixels = 10f;
                generator.Parameters.Barcode.Padding.Right.Pixels = 10f;
                generator.Parameters.Barcode.Padding.Bottom.Pixels = 10f;

                // Generate barcode image with the specified padding
                using (var bitmapPad = generator.GenerateBarCodeImage())
                {
                    int widthPad = bitmapPad.Width;
                    int heightPad = bitmapPad.Height;
                    string padPath = Path.Combine(outputDir, "planet_pad.png");
                    bitmapPad.Save(padPath, ImageFormat.Png);

                    // Calculate expected dimensions: original size plus left+right and top+bottom padding
                    int expectedWidth = widthNoPad + 20; // 10 left + 10 right
                    int expectedHeight = heightNoPad + 20; // 10 top + 10 bottom

                    bool widthMatches = widthPad == expectedWidth;
                    bool heightMatches = heightPad == expectedHeight;

                    // Output dimension information and verification results
                    Console.WriteLine($"No padding size: {widthNoPad}x{heightNoPad}");
                    Console.WriteLine($"With padding size: {widthPad}x{heightPad}");
                    Console.WriteLine($"Expected size with padding: {expectedWidth}x{expectedHeight}");
                    Console.WriteLine($"Width match: {widthMatches}");
                    Console.WriteLine($"Height match: {heightMatches}");
                }
            }
        }

        // Inform the user where the generated images are stored
        Console.WriteLine($"Barcode images saved to: {outputDir}");
    }
}