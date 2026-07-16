// Title: Han Xin barcode module size scaling verification
// Description: Demonstrates how changing the XDimension (module size) of a Han Xin barcode proportionally affects its overall image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Han Xin symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as XDimension to control module size. Developers often need to adjust module size for different printing resolutions or layout requirements, and this snippet illustrates the expected proportional scaling of width and height when the module size changes.
// Prompt: Write unit test confirming that changing module size alters overall Han Xin barcode dimensions proportionally.
// Tags: hanxin, barcode, module-size, scaling, generation, aspose.barcode, unit-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates two Han Xin barcodes with different module sizes and verifies that the image dimensions scale proportionally.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates barcodes, measures dimensions, and checks scaling factors.
    /// </summary>
    static void Main()
    {
        // Sample text to encode in the Han Xin barcode
        const string codeText = "1234567890ABCDEFGabcdefg,Han Xin Code";

        // --------------------------------------------------------------------
        // First barcode generation using a base XDimension (module size)
        // --------------------------------------------------------------------
        float xDim1 = 2f; // module size in points
        int width1, height1;
        using (var generator1 = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Apply the base XDimension to the barcode parameters
            generator1.Parameters.Barcode.XDimension.Point = xDim1;

            // Generate the barcode image and capture its dimensions
            using (var image1 = generator1.GenerateBarCodeImage())
            {
                width1 = image1.Width;
                height1 = image1.Height;
            }
        }

        // --------------------------------------------------------------------
        // Second barcode generation using a doubled XDimension
        // --------------------------------------------------------------------
        float xDim2 = 4f; // double the base module size
        int width2, height2;
        using (var generator2 = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Apply the larger XDimension to the barcode parameters
            generator2.Parameters.Barcode.XDimension.Point = xDim2;

            // Generate the barcode image and capture its dimensions
            using (var image2 = generator2.GenerateBarCodeImage())
            {
                width2 = image2.Width;
                height2 = image2.Height;
            }
        }

        // --------------------------------------------------------------------
        // Compute expected scaling factor based on XDimension change
        // --------------------------------------------------------------------
        float expectedFactor = xDim2 / xDim1;

        // Actual scaling factors derived from image dimensions
        float widthFactor = (float)width2 / width1;
        float heightFactor = (float)height2 / height1;

        // Allow a small tolerance (5%) to accommodate rounding differences
        const float tolerance = 0.05f;

        bool widthMatches = Math.Abs(widthFactor - expectedFactor) <= tolerance;
        bool heightMatches = Math.Abs(heightFactor - expectedFactor) <= tolerance;

        // --------------------------------------------------------------------
        // Output test result
        // --------------------------------------------------------------------
        if (widthMatches && heightMatches)
        {
            Console.WriteLine("PASSED: Module size change scaled dimensions proportionally.");
            Console.WriteLine($"XDimension {xDim1} -> {xDim2}, Width {width1} -> {width2}, Height {height1} -> {height2}");
        }
        else
        {
            Console.WriteLine("FAILED: Dimensions did not scale as expected.");
            Console.WriteLine($"Expected factor: {expectedFactor}");
            Console.WriteLine($"Width factor: {widthFactor} (match: {widthMatches})");
            Console.WriteLine($"Height factor: {heightFactor} (match: {heightMatches})");
        }
    }
}