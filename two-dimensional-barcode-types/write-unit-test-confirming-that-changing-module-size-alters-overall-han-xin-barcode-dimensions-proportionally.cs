// Title: Verify proportional scaling of Han Xin barcode dimensions with XDimension changes
// Description: This example generates Han Xin barcodes with different module sizes and checks that the resulting image dimensions scale proportionally.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and size measurement, focusing on the Han Xin symbology. It uses BarcodeGenerator, BarcodeParameters, and XDimension settings to illustrate how module size affects overall image dimensions—common when developers need precise control over barcode scaling for printing or UI rendering. This pattern is typical for unit tests validating barcode size behavior.
// Prompt: Write unit test confirming that changing module size alters overall Han Xin barcode dimensions proportionally.
// Tags: hanxin, barcode, module size, xdimension, scaling, unit test, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that verifies changing the XDimension (module size) of a Han Xin barcode
/// scales the generated image dimensions proportionally.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Han Xin barcode image with the specified XDimension (in points) and returns its width and height.
    /// </summary>
    /// <param name="xDimensionPoint">The module size to apply to the barcode.</param>
    /// <returns>A tuple containing the image width and height in pixels.</returns>
    static (int Width, int Height) GetHanXinSize(float xDimensionPoint)
    {
        // Create a barcode generator for Han Xin symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "1234567890"))
        {
            // Set the module size (XDimension) for the barcode.
            generator.Parameters.Barcode.XDimension.Point = xDimensionPoint;

            // Generate the barcode image and capture its dimensions.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                return (bitmap.Width, bitmap.Height);
            }
        }
    }

    /// <summary>
    /// Entry point that runs the proportionality check and outputs the result.
    /// </summary>
    static void Main()
    {
        // Define two different module sizes to compare.
        const float firstXDim = 2f;
        const float secondXDim = 4f;

        // Obtain image dimensions for each module size.
        var size1 = GetHanXinSize(firstXDim);
        var size2 = GetHanXinSize(secondXDim);

        // Verify that width and height roughly double when XDimension doubles.
        bool widthProportional = Math.Abs(size2.Width - size1.Width * 2) <= 1;
        bool heightProportional = Math.Abs(size2.Height - size1.Height * 2) <= 1;

        // Output the test result.
        if (widthProportional && heightProportional)
        {
            Console.WriteLine("PASS: Module size change scales dimensions proportionally.");
            Console.WriteLine($"Size with XDimension {firstXDim}: {size1.Width}x{size1.Height}");
            Console.WriteLine($"Size with XDimension {secondXDim}: {size2.Width}x{size2.Height}");
        }
        else
        {
            Console.WriteLine("FAIL: Dimensions are not proportional to module size change.");
            Console.WriteLine($"Size with XDimension {firstXDim}: {size1.Width}x{size1.Height}");
            Console.WriteLine($"Size with XDimension {secondXDim}: {size2.Width}x{size2.Height}");
        }
    }
}