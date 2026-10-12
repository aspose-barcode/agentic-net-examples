// Title: Demonstrate BarHeight property effect on barcode dimensions
// Description: Shows how setting BarHeight changes the barcode image height while keeping module width unchanged, useful for custom barcode sizing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode dimensions using the BarcodeGenerator and its Parameters. It focuses on the BarHeight and XDimension properties, common when developers need precise control over barcode size for printing or UI display. The snippet demonstrates measuring image dimensions to verify that height adjustments do not affect module width, a typical validation scenario for barcode rendering.
// Prompt: Write a unit test that ensures BarHeight property overrides automatic height calculation without affecting module width.
// Tags: barcode, barheight, dimensions, code128, aspose.barcode, unit-test, image, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that demonstrates the effect of the BarHeight property on generated barcode dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Generates two barcodes with different BarHeight values and verifies that width remains constant while height changes.
    /// </summary>
    static void Main()
    {
        // Define barcode content and symbology
        string codeText = "ASPOSE";
        BaseEncodeType encode = EncodeTypes.Code128;

        // First barcode with BarHeight set to 40 pixels
        int width1, height1;
        using (var generator = new BarcodeGenerator(encode, codeText))
        {
            // Set module width (XDimension) and explicit bar height
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;

            // Render barcode to memory stream and load as bitmap to read dimensions
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                using (var bitmap = new Bitmap(ms))
                {
                    width1 = bitmap.Width;
                    height1 = bitmap.Height;
                }
            }
        }

        // Second barcode with BarHeight set to 80 pixels
        int width2, height2;
        using (var generator = new BarcodeGenerator(encode, codeText))
        {
            // Keep the same module width but change bar height
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 80f;

            // Render and capture dimensions as before
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;
                using (var bitmap = new Bitmap(ms))
                {
                    width2 = bitmap.Width;
                    height2 = bitmap.Height;
                }
            }
        }

        // Verify that width is unchanged while height differs
        bool widthUnchanged = width1 == width2;
        bool heightChanged = height1 != height2;

        if (widthUnchanged && heightChanged)
        {
            Console.WriteLine("PASS: BarHeight overrides height without affecting module width.");
        }
        else
        {
            Console.WriteLine("FAIL: Unexpected dimensions.");
            Console.WriteLine($"Width1={width1}, Width2={width2}, Height1={height1}, Height2={height2}");
        }
    }
}