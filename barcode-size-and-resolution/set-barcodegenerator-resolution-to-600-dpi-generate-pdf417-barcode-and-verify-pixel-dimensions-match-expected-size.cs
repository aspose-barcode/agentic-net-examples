// Title: Generate PDF417 Barcode at 600 DPI and Verify Image Size
// Description: This example creates a PDF417 barcode with a resolution of 600 dpi, sets its physical dimensions, and checks that the resulting bitmap matches the expected pixel size.
// Category-Description: Demonstrates Aspose.BarCode barcode generation with high‑resolution settings. It showcases the use of BarcodeGenerator, EncodeTypes, and AutoSizeMode to control image size in inches, a common requirement for printing and scanning applications. Developers often need to set resolution, define physical dimensions, and validate pixel output when integrating barcodes into documents or labels.
// Prompt: Set BarcodeGenerator resolution to 600 dpi, generate PDF417 barcode, and verify pixel dimensions match expected size.
// Tags: pdf417, resolution, barcode generation, image size, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a PDF417 barcode at 600 dpi,
/// defines its physical size, and validates the resulting pixel dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, size verification, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the expected physical size of the barcode in inches.
        const float expectedWidthInches = 2f;
        const float expectedHeightInches = 1f;
        const float resolutionDpi = 600f;

        // Calculate the expected pixel dimensions based on the resolution.
        int expectedPixelWidth = (int)(expectedWidthInches * resolutionDpi);
        int expectedPixelHeight = (int)(expectedHeightInches * resolutionDpi);

        // Initialize the PDF417 barcode generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Set the image resolution to 600 dpi.
            generator.Parameters.Resolution = resolutionDpi;

            // Configure auto‑size mode to use interpolation and set the image size in inches.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Inches = expectedWidthInches;
            generator.Parameters.ImageHeight.Inches = expectedHeightInches;

            // Generate the barcode image as a bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Verify that the bitmap dimensions match the expected pixel size.
                bool widthMatches = bitmap.Width == expectedPixelWidth;
                bool heightMatches = bitmap.Height == expectedPixelHeight;

                Console.WriteLine($"Generated image size: {bitmap.Width}x{bitmap.Height} pixels");
                Console.WriteLine($"Expected image size: {expectedPixelWidth}x{expectedPixelHeight} pixels");
                Console.WriteLine($"Width match: {widthMatches}");
                Console.WriteLine($"Height match: {heightMatches}");

                // Save the generated barcode image to a PNG file.
                bitmap.Save("pdf417.png", ImageFormat.Png);
            }
        }
    }
}