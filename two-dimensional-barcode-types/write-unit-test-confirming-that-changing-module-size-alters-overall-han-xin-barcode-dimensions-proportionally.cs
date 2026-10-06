// Title: Han Xin barcode dimension scaling verification
// Description: Demonstrates how changing the XDimension (module size) of a Han Xin barcode affects its overall image dimensions proportionally.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters to control module size. Developers often need to adjust XDimension to meet size requirements for printing or display, and this snippet shows how to verify that scaling behaves as expected.
// Prompt: Write unit test confirming that changing module size alters overall Han Xin barcode dimensions proportionally.
// Tags: hanxin, barcode, dimension scaling, xdimension, aspose.barcode, generation, unit-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates verification that Han Xin barcode dimensions scale proportionally with XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two Han Xin barcodes with different XDimension values and checks that width and height scale proportionally.
    /// </summary>
    static void Main()
    {
        // Define barcode content and two module sizes to compare
        string codeText = "1234567890";
        float dimSmall = 2f;
        float dimLarge = 4f;

        // Variables to hold image dimensions for each size
        int widthSmall, heightSmall, widthLarge, heightLarge;

        // Generate barcode with small XDimension
        using (var generatorSmall = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Set module size (XDimension) for small barcode
            generatorSmall.Parameters.Barcode.XDimension.Point = dimSmall;
            // Render barcode to bitmap and capture dimensions
            using (var bitmapSmall = generatorSmall.GenerateBarCodeImage())
            {
                widthSmall = bitmapSmall.Width;
                heightSmall = bitmapSmall.Height;
            }
        }

        // Generate barcode with large XDimension
        using (var generatorLarge = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Set module size (XDimension) for large barcode
            generatorLarge.Parameters.Barcode.XDimension.Point = dimLarge;
            // Render barcode to bitmap and capture dimensions
            using (var bitmapLarge = generatorLarge.GenerateBarCodeImage())
            {
                widthLarge = bitmapLarge.Width;
                heightLarge = bitmapLarge.Height;
            }
        }

        // Compute expected scaling factor and verify dimensions within tolerance
        double scaleFactor = dimLarge / dimSmall;
        bool widthProportional = Math.Abs(widthLarge - widthSmall * scaleFactor) <= 1;
        bool heightProportional = Math.Abs(heightLarge - heightSmall * scaleFactor) <= 1;

        // Output test result
        if (widthProportional && heightProportional)
        {
            Console.WriteLine("PASS: Barcode dimensions scale proportionally with XDimension.");
        }
        else
        {
            Console.WriteLine("FAIL: Dimension scaling mismatch.");
            Console.WriteLine($"Small XDimension ({dimSmall}) -> Width: {widthSmall}, Height: {heightSmall}");
            Console.WriteLine($"Large XDimension ({dimLarge}) -> Width: {widthLarge}, Height: {heightLarge}");
        }
    }
}