// Title: Demonstrate BarHeight Override in Code128 Barcode Generation
// Description: Shows how setting the BarHeight property on a BarcodeGenerator overrides the automatic height calculation while keeping the module width unchanged. The example creates two PNG images and compares their dimensions.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to control barcode dimensions using the Parameters.Barcode API. It covers typical use cases such as customizing bar height for visual requirements without affecting X-dimension (module width). Developers working with barcode rendering often need to fine‑tune size parameters while preserving overall layout, and this snippet demonstrates that pattern.
// Prompt: Write a unit test that ensures BarHeight property overrides automatic height calculation without affecting module width.
// Tags: barcode, code128, barheight, xdimension, image, png, aspose.barcode, generation, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates overriding the BarHeight property of a Code128 barcode and verifies that the width remains unchanged.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates two barcodes, compares their dimensions, and reports the result.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarHeightTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define common barcode data and output file paths
        string codeText = "ASPOSE";
        string defaultPath = Path.Combine(tempFolder, "default.png");
        string customPath = Path.Combine(tempFolder, "custom.png");

        // Generate barcode with default BarHeight (automatic calculation)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set module (X) dimension to a fixed pixel value
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(defaultPath, BarCodeImageFormat.Png);
        }

        // Generate barcode with overridden BarHeight while keeping the same X-dimension
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 80f; // Explicit height override
            generator.Save(customPath, BarCodeImageFormat.Png);
        }

        // Load the generated images to retrieve their pixel dimensions
        int defaultWidth, defaultHeight, customWidth, customHeight;
        using (var bmpDefault = new Bitmap(defaultPath))
        {
            defaultWidth = bmpDefault.Width;
            defaultHeight = bmpDefault.Height;
        }
        using (var bmpCustom = new Bitmap(customPath))
        {
            customWidth = bmpCustom.Width;
            customHeight = bmpCustom.Height;
        }

        // Determine if the height changed and the width stayed the same
        bool heightChanged = customHeight != defaultHeight;
        bool widthUnchanged = customWidth == defaultWidth;

        // Output test result based on dimension comparison
        if (heightChanged && widthUnchanged)
        {
            Console.WriteLine("PASSED: BarHeight overrides automatic height without affecting module width.");
        }
        else
        {
            Console.WriteLine("FAILED: BarHeight test did not meet expectations.");
            Console.WriteLine($"Default Height={defaultHeight}, Custom Height={customHeight}");
            Console.WriteLine($"Default Width={defaultWidth}, Custom Width={customWidth}");
        }

        // Cleanup temporary files and folder; ignore any errors during cleanup
        try
        {
            File.Delete(defaultPath);
            File.Delete(customPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }
}