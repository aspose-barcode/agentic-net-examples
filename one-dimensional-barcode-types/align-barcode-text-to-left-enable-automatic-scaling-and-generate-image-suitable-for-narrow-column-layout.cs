// Title: Left-aligned Code128 barcode with automatic scaling for narrow columns
// Description: Demonstrates how to left‑align the human‑readable text of a Code128 barcode, enable automatic scaling via small X‑dimension, and save the result as a PNG suitable for narrow column layouts.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to customize barcode appearance. Typical use cases include creating compact barcodes for reports, invoices, or mobile layouts where column width is limited. Developers often need to adjust text alignment, module size, and padding to fit design constraints, and this snippet shows the common pattern for achieving that.
// Prompt: Align barcode text to left, enable automatic scaling, and generate image suitable for narrow column layout.
// Tags: code128, barcode generation, text alignment, automatic scaling, png, aspose.barcode, narrow column

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a left‑aligned Code128 barcode with automatic scaling,
/// suitable for narrow column layouts, and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory,
    /// configures the barcode generator, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Determine a temporary folder to store the generated image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the folder if it does not already exist
            Directory.CreateDirectory(outputDir);
        }

        // Full path for the resulting PNG file
        string outputPath = Path.Combine(outputDir, "LeftAlignedBarcode.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "NarrowColumnDemo"))
        {
            // Align the human‑readable text to the left side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Set a small module size; automatic scaling will adjust the overall width
            generator.Parameters.Barcode.XDimension.Pixels = 1f;

            // Apply minimal padding around the barcode to keep it compact
            generator.Parameters.Barcode.Padding.Left.Point = 2f;
            generator.Parameters.Barcode.Padding.Right.Point = 2f;
            generator.Parameters.Barcode.Padding.Top.Point = 2f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image has been saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}