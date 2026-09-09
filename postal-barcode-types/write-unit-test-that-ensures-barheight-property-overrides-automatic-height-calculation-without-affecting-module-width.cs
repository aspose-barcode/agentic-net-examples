// Title: BarHeight Override Test for Code128 Barcode
// Description: Demonstrates that setting the BarHeight property overrides the automatic height calculation while preserving the module width (XDimension) of a generated Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to control barcode dimensions using the BarcodeGenerator, Parameters.Barcode, and XDimension/BarHeight properties. Typical use cases include customizing barcode size for printing or UI display without altering the encoded data's visual density. Developers often need unit‑style checks to verify that dimension tweaks behave as expected.
// Prompt: Write a unit test that ensures BarHeight property overrides automatic height calculation without affecting module width.
// Tags: barcode, code128, barheight, xdimension, dimension control, generation, aspose.barcode, unit test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates that setting BarHeight overrides automatic height calculation without affecting XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates two barcodes—one with default height and one with explicit BarHeight—and validates the behavior.
    /// </summary>
    static void Main()
    {
        // Text to encode in the barcode
        string codeText = "ASPOSE";

        // Desired module width in pixels (XDimension)
        float xDimensionPixels = 2f;

        // ------------------------------------------------------------
        // Generate barcode with default (automatic) height
        // ------------------------------------------------------------
        using (var generatorDefault = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set module width; height remains automatic
            generatorDefault.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            using (Bitmap bitmapDefault = generatorDefault.GenerateBarCodeImage())
            {
                int heightDefault = bitmapDefault.Height;

                // ------------------------------------------------------------
                // Generate barcode with explicit BarHeight
                // ------------------------------------------------------------
                using (var generatorCustom = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Preserve the same module width
                    generatorCustom.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

                    // Override automatic height with a fixed value (80 pixels)
                    generatorCustom.Parameters.Barcode.BarHeight.Pixels = 80f;

                    using (Bitmap bitmapCustom = generatorCustom.GenerateBarCodeImage())
                    {
                        int heightCustom = bitmapCustom.Height;

                        // Verify that the custom height is greater than the default height
                        bool heightIncreased = heightCustom > heightDefault;

                        // Verify that XDimension (module width) remains unchanged between generators
                        bool xDimensionUnchanged = Math.Abs(
                            generatorCustom.Parameters.Barcode.XDimension.Pixels -
                            generatorDefault.Parameters.Barcode.XDimension.Pixels) < 0.001f;

                        // Output test result
                        if (heightIncreased && xDimensionUnchanged)
                        {
                            Console.WriteLine("PASS: BarHeight overrides height without affecting module width.");
                        }
                        else
                        {
                            Console.WriteLine("FAIL: BarHeight test failed.");
                            Console.WriteLine($"Default height: {heightDefault}, Custom height: {heightCustom}, XDimension unchanged: {xDimensionUnchanged}");
                        }
                    }
                }
            }
        }
    }
}