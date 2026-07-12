// Title: Verify BarHeight overrides automatic height calculation while preserving module width
// Description: Demonstrates how setting the BarHeight property affects barcode image height without changing the module width.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on size manipulation using AutoSizeMode, XDimension, and BarHeight properties. It shows typical usage for developers needing precise control over barcode dimensions, such as customizing bar height while keeping module width constant, a common requirement in label printing and scanning applications.
// Prompt: Write a unit test that ensures BarHeight property overrides automatic height calculation without affecting module width.
// Tags: barcode, code128, barheight, modulewidth, autosizemode, xdimension, aspose.barcode, unit-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that validates the effect of the BarHeight property on barcode image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Generates two barcodes: one with default height and one with an explicit BarHeight.
    /// Verifies that the module width (image width) remains unchanged while the height changes.
    /// </summary>
    static void Main()
    {
        // Generate first barcode with default BarHeight
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing to control dimensions manually
            generator1.Parameters.AutoSizeMode = AutoSizeMode.None;
            // Set module width (XDimension) to 2 points
            generator1.Parameters.Barcode.XDimension.Point = 2f;

            using (var image1 = generator1.GenerateBarCodeImage())
            {
                int width1 = image1.Width;   // Capture width of first image
                int height1 = image1.Height; // Capture height of first image

                // Generate second barcode with explicit BarHeight
                using (var generator2 = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
                {
                    generator2.Parameters.AutoSizeMode = AutoSizeMode.None;
                    generator2.Parameters.Barcode.XDimension.Point = 2f; // Same module width
                    generator2.Parameters.Barcode.BarHeight.Point = 80f; // Custom bar height

                    using (var image2 = generator2.GenerateBarCodeImage())
                    {
                        int width2 = image2.Width;   // Capture width of second image
                        int height2 = image2.Height; // Capture height of second image

                        // Verify that module width (image width) is unchanged
                        if (width1 != width2)
                        {
                            throw new Exception($"Module width changed: before={width1}, after={width2}");
                        }

                        // Verify that bar height (image height) is affected
                        if (height1 == height2)
                        {
                            throw new Exception($"BarHeight did not change height: before={height1}, after={height2}");
                        }

                        Console.WriteLine("BarHeight overrides automatic height calculation without affecting module width.");
                        Console.WriteLine($"Width: {width1}px, Height before: {height1}px, Height after: {height2}px");
                    }
                }
            }
        }
    }
}