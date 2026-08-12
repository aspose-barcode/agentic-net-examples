// Title: Han Xin Barcode Module Size Scaling Test
// Description: Demonstrates how changing the XDimension (module size) of a Han Xin barcode affects the generated image dimensions proportionally.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and barcode parameters to control visual output. Typical scenarios include adjusting barcode size for different printing requirements or UI layouts. Developers often need to verify that parameter changes like XDimension result in expected dimensional scaling.
// Prompt: Write unit test confirming that changing module size alters overall Han Xin barcode dimensions proportionally.
// Tags: hanxin, barcode, module size, xdimension, scaling, aspose.barcode, generation, unit-test, image-dimensions

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a simple console demonstration that verifies the proportional scaling of
/// Han Xin barcode dimensions when the module size (XDimension) is changed.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Han Xin barcode with the specified XDimension and returns its pixel dimensions.
    /// </summary>
    /// <param name="xDimension">The module size in points to apply to the barcode.</param>
    /// <returns>A tuple containing the width and height of the generated barcode image.</returns>
    static (int Width, int Height) GetBarcodeSize(float xDimension)
    {
        // Create a Han Xin barcode generator with a short codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "ABC"))
        {
            // Set the module size (XDimension) in points.
            generator.Parameters.Barcode.XDimension.Point = xDimension;

            // Generate the barcode image.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Return the pixel dimensions of the generated image.
                return (bitmap.Width, bitmap.Height);
            }
        }
    }

    /// <summary>
    /// Entry point of the program. Compares barcode dimensions for two different XDimension values
    /// and reports whether the scaling is proportional within an acceptable tolerance.
    /// </summary>
    static void Main()
    {
        // Define two different XDimension values to test scaling.
        float xDim1 = 2f;
        float xDim2 = 4f;

        // Obtain barcode sizes for each XDimension.
        var size1 = GetBarcodeSize(xDim1);
        var size2 = GetBarcodeSize(xDim2);

        // Expected width ratio based on the XDimension change.
        double expectedRatio = (double)xDim2 / xDim1;

        // Actual width ratio of the generated images.
        double actualRatio = (double)size2.Width / size1.Width;

        // Allow a small tolerance due to rounding differences.
        double tolerance = 0.1;

        // Evaluate whether the actual ratio matches the expected ratio within tolerance.
        if (Math.Abs(actualRatio - expectedRatio) <= tolerance)
        {
            Console.WriteLine("PASSED: Barcode dimensions scale proportionally with XDimension.");
        }
        else
        {
            Console.WriteLine("FAILED: Expected ratio {0:F2}, but got {1:F2}.", expectedRatio, actualRatio);
        }

        // Output the measured dimensions for reference.
        Console.WriteLine("XDimension {0} -> Width {1}, Height {2}", xDim1, size1.Width, size1.Height);
        Console.WriteLine("XDimension {0} -> Width {1}, Height {2}", xDim2, size2.Width, size2.Height);
    }
}