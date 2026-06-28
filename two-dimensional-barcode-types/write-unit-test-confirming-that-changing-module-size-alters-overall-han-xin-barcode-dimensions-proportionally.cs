using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how Han Xin barcode dimensions scale with different module sizes (XDimension).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two Han Xin barcodes with different XDimension values
    /// and verifies that their dimensions scale proportionally.
    /// </summary>
    static void Main()
    {
        // Sample code text for Han Xin barcode
        const string codeText = "1234567890";

        // First module size (XDimension) in points
        float xDim1 = 2f;
        // Second module size (double the first)
        float xDim2 = 4f;

        // Variables to hold dimensions of the first generated barcode
        int width1, height1;

        // Generate first barcode image with XDimension = xDim1
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Disable automatic sizing to use explicit XDimension
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            // Set module size for the barcode
            generator.Parameters.Barcode.XDimension.Point = xDim1;

            // Render barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the image from the stream to obtain its dimensions
                using (var bitmap = new Bitmap(ms))
                {
                    width1 = bitmap.Width;
                    height1 = bitmap.Height;
                }
            }
        }

        // Variables to hold dimensions of the second generated barcode
        int width2, height2;

        // Generate second barcode image with XDimension = xDim2
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Disable automatic sizing to use explicit XDimension
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            // Set a larger module size for the barcode
            generator.Parameters.Barcode.XDimension.Point = xDim2;

            // Render barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the image from the stream to obtain its dimensions
                using (var bitmap = new Bitmap(ms))
                {
                    width2 = bitmap.Width;
                    height2 = bitmap.Height;
                }
            }
        }

        // Verify proportionality of dimensions (allow small rounding differences)
        bool widthProportional = Math.Abs((double)width2 * xDim1 - (double)width1 * xDim2) <= 2.0;
        bool heightProportional = Math.Abs((double)height2 * xDim1 - (double)height1 * xDim2) <= 2.0;

        // Output result based on proportionality check
        if (widthProportional && heightProportional)
        {
            Console.WriteLine("PASS: Barcode dimensions scale proportionally with module size.");
            Console.WriteLine($"Size1 (XDim={xDim1}): {width1}x{height1}");
            Console.WriteLine($"Size2 (XDim={xDim2}): {width2}x{height2}");
        }
        else
        {
            Console.WriteLine("FAILED: Barcode dimensions do not scale proportionally.");
            Console.WriteLine($"Size1 (XDim={xDim1}): {width1}x{height1}");
            Console.WriteLine($"Size2 (XDim={xDim2}): {width2}x{height2}");
        }
    }
}