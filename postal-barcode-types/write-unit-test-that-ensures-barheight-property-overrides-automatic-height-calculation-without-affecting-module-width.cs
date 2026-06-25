using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how BarHeight affects barcode image dimensions while keeping module width constant.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcodes with different BarHeight values and compares dimensions.
    /// </summary>
    static void Main()
    {
        // Prepare test data
        const string codeText = "123456";
        const float xDim = 2f;          // Module width (XDimension) in points
        const float barHeight1 = 20f;   // First bar height in points
        const float barHeight2 = 60f;   // Second bar height in points (different from first)

        // Generate first barcode using the first bar height
        int width1, height1;
        GenerateBarcode(codeText, xDim, barHeight1, out width1, out height1);

        // Generate second barcode using the second bar height (same XDimension)
        int width2, height2;
        GenerateBarcode(codeText, xDim, barHeight2, out width2, out height2);

        // Verify that the module width (image width) remains unchanged
        bool widthUnchanged = width1 == width2;

        // Verify that the image height reflects the BarHeight change
        bool heightChanged = height2 > height1;

        // Output verification results
        Console.WriteLine($"Width unchanged: {widthUnchanged} (width1={width1}, width2={width2})");
        Console.WriteLine($"Height increased: {heightChanged} (height1={height1}, height2={height2})");

        // Report overall test outcome
        if (widthUnchanged && heightChanged)
        {
            Console.WriteLine("Test passed: BarHeight overrides automatic height calculation without affecting module width.");
        }
        else
        {
            Console.WriteLine("Test failed: Unexpected dimensions.");
        }
    }

    static void GenerateBarcode(string codeText, float xDimension, float barHeight, out int imageWidth, out int imageHeight)
    {
        // Initialize barcode generator with Code128 symbology and the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Disable automatic sizing so that explicit BarHeight is used
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set module width (XDimension) and explicit bar height
            generator.Parameters.Barcode.XDimension.Point = xDimension;
            generator.Parameters.Barcode.BarHeight.Point = barHeight;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Load the image from the stream to obtain actual dimensions
                using (var image = Image.FromStream(ms))
                {
                    imageWidth = image.Width;
                    imageHeight = image.Height;
                }
            }
        }
    }
}