// Title: Generate barcode with auto height and fixed width
// Description: Demonstrates setting a barcode's width to 40 mm while leaving height to auto‑size, and shows handling of an invalid zero height.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, AutoSizeMode, and image dimension properties. Typical use cases include creating barcodes that fit a specific layout width while allowing the library to calculate optimal height. Developers often need to control size constraints and validate automatic sizing behavior.
// Prompt: Write code generating barcode with BarCodeHeight zero, BarCodeWidth 40 mm, and verify auto‑height behavior.
// Tags: barcode, code128, autoheight, width, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode with a fixed width of 40 mm,
/// lets the library determine the optimal height automatically, and demonstrates
/// handling of an invalid zero‑height setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates output directory, attempts an invalid height setting,
    /// generates the barcode with auto height, saves it, and prints the resulting image size.
    /// </summary>
    static void Main()
    {
        // Prepare output folder and file path
        string outDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outDir);
        string outPath = Path.Combine(outDir, "barcode.png");

        // Attempt to set BarHeight to zero (expected to throw ArgumentException)
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                generator.Parameters.Barcode.BarHeight.Point = 0f;
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Setting BarHeight to 0 threw: " + ex.Message);
        }

        // Generate barcode with auto height and fixed width of 40 mm
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable automatic sizing mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            // Set desired image width in millimeters
            generator.Parameters.ImageWidth.Millimeters = 40f;
            // Do not set BarHeight; library will calculate optimal height
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Load the generated image and output its dimensions in pixels
        using (var bitmap = new Bitmap(outPath))
        {
            Console.WriteLine($"Generated barcode size: {bitmap.Width}x{bitmap.Height} pixels");
        }
    }
}