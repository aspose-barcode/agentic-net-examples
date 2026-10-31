// Title: Han Xin Barcode Module Size Scaling Test
// Description: Demonstrates how changing the X-dimension (module size) of a Han Xin barcode affects the generated image dimensions proportionally.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on size and dimension adjustments. It uses BarcodeGenerator, EncodeTypes, and the XDimension property to control module size, a common requirement when developers need to fit barcodes into specific layout constraints or achieve desired visual density.
// Prompt: Write unit test confirming that changing module size alters overall Han Xin barcode dimensions proportionally.
// Tags: hanxin, barcode, module-size, dimension-scaling, generation, aspose.barcode, unit-test

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates Han Xin barcodes with different module sizes and verifies that the image dimensions scale proportionally.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcodes with different X-dimensions and checks dimension scaling.
    /// </summary>
    static void Main()
    {
        // Generate barcode dimensions for a small module size (2 points)
        var sizeSmall = GenerateSize(2f);

        // Generate barcode dimensions for a larger module size (4 points)
        var sizeLarge = GenerateSize(4f);

        // Expected dimensions are double the small size because the module size doubled
        int expectedWidth = sizeSmall.width * 2;
        int expectedHeight = sizeSmall.height * 2;

        // Allow a tolerance of 1 pixel due to rounding differences
        bool widthMatches = Math.Abs(sizeLarge.width - expectedWidth) <= 1;
        bool heightMatches = Math.Abs(sizeLarge.height - expectedHeight) <= 1;

        // Output test result
        if (widthMatches && heightMatches)
        {
            Console.WriteLine("PASS: dimensions scaled proportionally.");
        }
        else
        {
            Console.WriteLine("FAIL: dimension scaling mismatch.");
            Console.WriteLine($"Small size: {sizeSmall.width}x{sizeSmall.height}");
            Console.WriteLine($"Large size: {sizeLarge.width}x{sizeLarge.height}");
            Console.WriteLine($"Expected large size: {expectedWidth}x{expectedHeight}");
        }
    }

    /// <summary>
    /// Generates a Han Xin barcode image with the specified X-dimension (module size) and returns its width and height.
    /// </summary>
    /// <param name="xDimensionPoint">The module size in points.</param>
    /// <returns>A tuple containing the image width and height in pixels.</returns>
    static (int width, int height) GenerateSize(float xDimensionPoint)
    {
        // Initialize the barcode generator for Han Xin symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "1234567890"))
        {
            // Set the module size (X-dimension) for the barcode
            generator.Parameters.Barcode.XDimension.Point = xDimensionPoint;

            // Generate the barcode image and capture its dimensions
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return (bitmap.Width, bitmap.Height);
            }
        }
    }
}