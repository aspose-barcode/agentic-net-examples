// Title: Verify BarHeight overrides automatic height calculation while preserving module width
// Description: Demonstrates how to test that setting the BarHeight property changes the barcode image height without affecting the module (XDimension) width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode dimensions using the BarcodeGenerator, Parameters.AutoSizeMode, and Parameters.Barcode properties. Typical use cases include unit testing barcode rendering, customizing size for printing, and ensuring consistent module width across different heights. Developers often need to validate that manual height settings do not alter the calculated module width.
// Prompt: Write a unit test that ensures BarHeight property overrides automatic height calculation without affecting module width.
// Tags: code128, barheight, autosizemode, xdimension, bitmap, aspose.barcode, generation, unit-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates a simple verification that setting BarHeight manually changes image height while keeping module width constant.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates two barcodes with different BarHeight values and checks width and height differences.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare test data
        // --------------------------------------------------------------------
        const string codeText = "123456";
        const float xDimension = 2f; // points (module width)
        const float barHeight1 = 30f; // points (first barcode height)
        const float barHeight2 = 60f; // points (second barcode height)

        // --------------------------------------------------------------------
        // First barcode with BarHeight = 30 (module width unchanged)
        // --------------------------------------------------------------------
        int width1, height1;
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Disable automatic sizing so manual dimensions are respected
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            // Set module width
            generator.Parameters.Barcode.XDimension.Point = xDimension;
            // Set explicit bar height
            generator.Parameters.Barcode.BarHeight.Point = barHeight1;

            // Generate the image and capture its dimensions
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                width1 = image.Width;
                height1 = image.Height;
            }
        }

        // --------------------------------------------------------------------
        // Second barcode with BarHeight = 60 (module width unchanged)
        // --------------------------------------------------------------------
        int width2, height2;
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.Barcode.XDimension.Point = xDimension;
            generator.Parameters.Barcode.BarHeight.Point = barHeight2;

            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                width2 = image.Width;
                height2 = image.Height;
            }
        }

        // --------------------------------------------------------------------
        // Verify that module width (image width) is unchanged while height changes
        // --------------------------------------------------------------------
        bool widthUnchanged = width1 == width2;
        bool heightChanged = height1 != height2;

        if (widthUnchanged && heightChanged)
        {
            Console.WriteLine("PASSED: BarHeight overrides automatic height calculation without affecting module width.");
        }
        else
        {
            Console.WriteLine("FAILED:");
            if (!widthUnchanged)
                Console.WriteLine($"  Image width changed (was {width1}, now {width2}).");
            if (!heightChanged)
                Console.WriteLine($"  Image height did not change (both {height1}).");
        }
    }
}