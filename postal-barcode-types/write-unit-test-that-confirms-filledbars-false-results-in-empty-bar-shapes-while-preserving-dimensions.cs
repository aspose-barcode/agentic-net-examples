using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates the effect of the <c>FilledBars</c> property on generated barcodes
/// while verifying that image dimensions remain unchanged.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates two barcodes (with <c>FilledBars</c> true and false),
    /// compares their dimensions, and checks that the bar pixel becomes
    /// background color when <c>FilledBars</c> is disabled.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare test data: barcode text and symbology type.
        // --------------------------------------------------------------------
        const string codeText = "123456";
        BaseEncodeType symbology = EncodeTypes.Code128;

        // --------------------------------------------------------------------
        // Generate barcode with default FilledBars (true) and capture its size
        // and a sample bar pixel color.
        // --------------------------------------------------------------------
        int widthTrue, heightTrue;
        Color barColorTrue;
        using (var genTrue = new BarcodeGenerator(symbology, codeText))
        {
            using (var msTrue = new MemoryStream())
            {
                // Save barcode image to memory stream as PNG.
                genTrue.Save(msTrue, BarCodeImageFormat.Png);
                msTrue.Position = 0;

                // Load the image from the stream.
                using (var imgTrue = Image.FromStream(msTrue) as Bitmap)
                {
                    widthTrue = imgTrue.Width;
                    heightTrue = imgTrue.Height;

                    // Sample a pixel near the left side (likely a bar).
                    barColorTrue = imgTrue.GetPixel(widthTrue / 10, heightTrue / 2);
                }
            }
        }

        // --------------------------------------------------------------------
        // Generate barcode with FilledBars set to false and capture its size
        // and the same sample pixel color.
        // --------------------------------------------------------------------
        int widthFalse, heightFalse;
        Color barColorFalse;
        using (var genFalse = new BarcodeGenerator(symbology, codeText))
        {
            // Disable filled bars.
            genFalse.Parameters.Barcode.FilledBars = false;

            using (var msFalse = new MemoryStream())
            {
                // Save barcode image to memory stream as PNG.
                genFalse.Save(msFalse, BarCodeImageFormat.Png);
                msFalse.Position = 0;

                // Load the image from the stream.
                using (var imgFalse = Image.FromStream(msFalse) as Bitmap)
                {
                    widthFalse = imgFalse.Width;
                    heightFalse = imgFalse.Height;

                    // Sample the same pixel location as before.
                    barColorFalse = imgFalse.GetPixel(widthFalse / 10, heightFalse / 2);
                }
            }
        }

        // --------------------------------------------------------------------
        // Verify that dimensions are identical for both images.
        // --------------------------------------------------------------------
        bool dimensionsMatch = widthTrue == widthFalse && heightTrue == heightFalse;

        // --------------------------------------------------------------------
        // Verify that the sampled pixel is background (white) when FilledBars is false.
        // --------------------------------------------------------------------
        bool barIsEmpty = barColorFalse.ToArgb() == Color.White.ToArgb();

        // --------------------------------------------------------------------
        // Output verification results.
        // --------------------------------------------------------------------
        if (dimensionsMatch && barIsEmpty)
        {
            Console.WriteLine("PASS: FilledBars false produces empty bars while preserving dimensions.");
        }
        else
        {
            Console.WriteLine("FAIL:");
            if (!dimensionsMatch)
                Console.WriteLine($"  Dimensions differ: true({widthTrue}x{heightTrue}) vs false({widthFalse}x{heightFalse})");
            if (!barIsEmpty)
                Console.WriteLine($"  Bar pixel not empty: color ARGB={barColorFalse.ToArgb():X8}");
        }
    }
}