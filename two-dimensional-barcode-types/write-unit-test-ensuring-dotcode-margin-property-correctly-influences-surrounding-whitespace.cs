// Title: DotCode Margin Influence Test
// Description: Demonstrates how the DotCode barcode margin property adds whitespace around the generated image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DotCode and configuring padding via Parameters.Barcode.Padding. Developers often need to control surrounding whitespace for layout or printing requirements, and this snippet shows how to verify margin effects.
// Prompt: Write unit test ensuring DotCode margin property correctly influences surrounding whitespace.
// Tags: dotcode, margin, padding, barcode, generation, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates testing the effect of the DotCode barcode margin on image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates two DotCode barcodes with and without padding,
    /// compares their dimensions, and reports the test result.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for test output files
        string tempDir = Path.Combine(Path.GetTempPath(), "DotCodeMarginTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the barcode images
        string fileNoMargin = Path.Combine(tempDir, "dotcode_nomargin.png");
        string fileWithMargin = Path.Combine(tempDir, "dotcode_margin.png");

        // Generate barcode without any padding
        GenerateDotCode(fileNoMargin, 0);

        // Generate barcode with 20 pixels padding on each side
        GenerateDotCode(fileWithMargin, 20);

        // Variables to hold image dimensions
        int widthNoMargin, heightNoMargin;
        int widthWithMargin, heightWithMargin;

        // Load the image without margin and capture its size
        using (Bitmap bmp = new Bitmap(fileNoMargin))
        {
            widthNoMargin = bmp.Width;
            heightNoMargin = bmp.Height;
        }

        // Load the image with margin and capture its size
        using (Bitmap bmp = new Bitmap(fileWithMargin))
        {
            widthWithMargin = bmp.Width;
            heightWithMargin = bmp.Height;
        }

        // Verify that the margin added 40 pixels to both width and height (20 left + 20 right, etc.)
        bool widthOk = widthWithMargin == widthNoMargin + 40;
        bool heightOk = heightWithMargin == heightNoMargin + 40;

        // Output test result
        if (widthOk && heightOk)
        {
            Console.WriteLine("Test passed: Margin correctly influences surrounding whitespace.");
        }
        else
        {
            Console.WriteLine("Test failed:");
            Console.WriteLine($"Expected width {widthNoMargin + 40}, actual {widthWithMargin}");
            Console.WriteLine($"Expected height {heightNoMargin + 40}, actual {heightWithMargin}");
        }

        // Cleanup temporary files and directory
        try { File.Delete(fileNoMargin); } catch { }
        try { File.Delete(fileWithMargin); } catch { }
        try { Directory.Delete(tempDir, true); } catch { }
    }

    /// <summary>
    /// Generates a DotCode barcode image with the specified padding.
    /// </summary>
    /// <param name="filePath">The full path where the image will be saved.</param>
    /// <param name="paddingPixels">The padding (margin) in pixels to apply on all sides.</param>
    static void GenerateDotCode(string filePath, int paddingPixels)
    {
        // Initialize the barcode generator for DotCode symbology
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.DotCode, "Test"))
        {
            // Set the module size (X dimension) in pixels
            gen.Parameters.Barcode.XDimension.Pixels = 5f;

            // Apply uniform padding on all four sides
            gen.Parameters.Barcode.Padding.Left.Pixels = paddingPixels;
            gen.Parameters.Barcode.Padding.Right.Pixels = paddingPixels;
            gen.Parameters.Barcode.Padding.Top.Pixels = paddingPixels;
            gen.Parameters.Barcode.Padding.Bottom.Pixels = paddingPixels;

            // Save the generated barcode as a PNG image
            gen.Save(filePath, BarCodeImageFormat.Png);
        }
    }
}