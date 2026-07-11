// Title: Automatic Bar Height Fallback for Barcode Generation
// Description: Demonstrates how to let Aspose.BarCode automatically determine bar height when BarHeight is set to zero, using AutoSizeMode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to create barcodes with dynamic sizing. Developers often need to generate barcodes that fit specific image dimensions without manually calculating bar heights; this pattern provides a reliable fallback to automatic sizing.
// Prompt: Implement fallback logic to use automatic bar height when BarHeight property is set to zero.
// Tags: barcode, autosize, barheight, code128, image, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode and applies automatic bar height sizing when the desired height is zero.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image with optional automatic height adjustment.
    /// </summary>
    static void Main()
    {
        // Sample barcode text to encode.
        const string codeText = "123456";

        // Desired bar height (set to 0 to trigger automatic sizing).
        float desiredBarHeight = 0f;

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Determine whether to use automatic sizing based on the desired bar height.
            if (desiredBarHeight <= 0f)
            {
                // Enable automatic resizing using interpolation.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Set target image dimensions (example values).
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 100f;
            }
            else
            {
                // Apply the explicitly specified bar height.
                generator.Parameters.Barcode.BarHeight.Point = desiredBarHeight;
            }

            // Save the generated barcode image to a file.
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated: barcode.png");
    }
}