// Title: DotCode Margin Effect on Image Dimensions
// Description: Demonstrates how setting the DotCode barcode margin influences the generated image size.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. It illustrates typical scenarios where developers need to control barcode padding (margin) to affect surrounding whitespace, useful for layout and printing requirements. The example serves as a reference for creating unit tests that validate margin behavior across different barcode symbologies.
// Prompt: Write unit test ensuring DotCode margin property correctly influences surrounding whitespace.
// Tags: dotcode, margin, barcode, generation, image, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that verifies the effect of the DotCode barcode margin on image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two DotCode barcodes—one without margin and one with a 10‑point margin—then compares their dimensions.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "DotCodeMarginTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the generated images
        string noMarginPath = Path.Combine(tempFolder, "dotcode_nomargin.png");
        string marginPath = Path.Combine(tempFolder, "dotcode_margin.png");

        // Generate barcodes with and without margin
        GenerateDotCode(noMarginPath, 0f);
        GenerateDotCode(marginPath, 10f); // 10 points padding on each side

        // Load images and capture their dimensions
        int widthNoMargin, heightNoMargin;
        int widthMargin, heightMargin;

        using (Bitmap bmpNoMargin = new Bitmap(noMarginPath))
        {
            widthNoMargin = bmpNoMargin.Width;
            heightNoMargin = bmpNoMargin.Height;
        }

        using (Bitmap bmpMargin = new Bitmap(marginPath))
        {
            widthMargin = bmpMargin.Width;
            heightMargin = bmpMargin.Height;
        }

        // Determine whether the margin increased the image size
        bool widthIncreased = widthMargin > widthNoMargin;
        bool heightIncreased = heightMargin > heightNoMargin;

        if (widthIncreased && heightIncreased)
        {
            Console.WriteLine("PASSED: Margin increased image dimensions as expected.");
        }
        else
        {
            Console.WriteLine("FAILED: Margin did not affect image dimensions as expected.");
            Console.WriteLine($"No-margin size: {widthNoMargin}x{heightNoMargin}");
            Console.WriteLine($"Margin size:    {widthMargin}x{heightMargin}");
        }

        // Cleanup generated files and temporary folder (optional)
        try
        {
            File.Delete(noMarginPath);
            File.Delete(marginPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }

    /// <summary>
    /// Generates a DotCode barcode image with the specified padding (margin) on all sides.
    /// </summary>
    /// <param name="filePath">The full path where the barcode image will be saved.</param>
    /// <param name="paddingPoints">Margin size in points to apply to each side of the barcode.</param>
    static void GenerateDotCode(string filePath, float paddingPoints)
    {
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, "Aspose"))
        {
            // Set basic barcode size
            generator.Parameters.Barcode.XDimension.Pixels = 5;

            // Apply uniform padding (margin) on all four sides
            generator.Parameters.Barcode.Padding.Left.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Right.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Top.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingPoints;

            // Save the generated barcode as a PNG image
            generator.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}