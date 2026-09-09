// Title: Verify FilledBars Property Generates Empty Bars While Keeping Dimensions
// Description: Demonstrates generating a barcode with filled bars and with empty bars, then checks that disabling FilledBars results in white bar shapes while preserving image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance using the BarcodeGenerator class. It shows typical use cases such as adjusting XDimension, toggling the FilledBars property, and validating output images. Developers working with barcode rendering often need to verify visual properties programmatically, and this snippet serves as a reference for unit‑style checks.
// Prompt: Write a unit test that confirms FilledBars false results in empty bar shapes while preserving dimensions.
// Tags: barcode, code128, filledbars, imagevalidation, aspose.barcode, unit-test, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes with filled and empty bars and validates that disabling
/// FilledBars produces white bars while keeping the image dimensions unchanged.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary barcode images, compares their dimensions,
    /// and checks pixel colors to confirm the effect of the FilledBars property.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "FilledBarsTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Barcode content and symbology
        string codeText = "ASPOSE";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // -------------------------------------------------
        // Generate barcode with default filled bars (true)
        // -------------------------------------------------
        Bitmap filledBitmap;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set bar width (XDimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // FilledBars defaults to true; no change needed
            using (filledBitmap = generator.GenerateBarCodeImage())
            {
                // Save the image for optional manual inspection
                using (var stream = new FileStream(Path.Combine(tempDir, "filled.png"), FileMode.Create, FileAccess.Write))
                {
                    filledBitmap.Save(stream, Aspose.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        // -------------------------------------------------
        // Generate barcode with empty (unfilled) bars
        // -------------------------------------------------
        Bitmap emptyBitmap;
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            // Disable filled bars to produce white bar shapes
            generator.Parameters.Barcode.FilledBars = false;
            using (emptyBitmap = generator.GenerateBarCodeImage())
            {
                // Save the image for optional manual inspection
                using (var stream = new FileStream(Path.Combine(tempDir, "empty.png"), FileMode.Create, FileAccess.Write))
                {
                    emptyBitmap.Save(stream, Aspose.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        // Verify that both images share the same dimensions
        bool dimensionsEqual = filledBitmap.Width == emptyBitmap.Width && filledBitmap.Height == emptyBitmap.Height;

        // Choose sample points across the barcode width
        int[] sampleXs = new int[] { filledBitmap.Width / 4, filledBitmap.Width / 2, (filledBitmap.Width * 3) / 4 };
        int sampleY = filledBitmap.Height / 2;

        bool filledHasBlack = false;
        bool emptyHasWhite = false;

        // Inspect pixel colors at the sample points
        foreach (int x in sampleXs)
        {
            var filledColor = filledBitmap.GetPixel(x, sampleY);
            var emptyColor = emptyBitmap.GetPixel(x, sampleY);

            if (filledColor.ToArgb() == Aspose.Drawing.Color.Black.ToArgb())
                filledHasBlack = true;
            if (emptyColor.ToArgb() == Aspose.Drawing.Color.White.ToArgb())
                emptyHasWhite = true;
        }

        // Output test result
        if (dimensionsEqual && filledHasBlack && emptyHasWhite)
        {
            Console.WriteLine("PASSED: FilledBars false produces empty bars while preserving dimensions.");
        }
        else
        {
            Console.WriteLine("FAILED:");
            if (!dimensionsEqual)
                Console.WriteLine("- Image dimensions differ.");
            if (!filledHasBlack)
                Console.WriteLine("- Filled barcode does not contain expected black bars.");
            if (!emptyHasWhite)
                Console.WriteLine("- Empty barcode does not contain expected white bars.");
        }

        // Clean up temporary files and directory
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Suppress any cleanup errors
        }
    }
}