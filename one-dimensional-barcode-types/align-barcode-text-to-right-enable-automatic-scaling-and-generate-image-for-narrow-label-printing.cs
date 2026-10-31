// Title: Right-aligned Code128 barcode with auto scaling for narrow label printing
// Description: Generates a Code128 barcode image with the text aligned to the right, automatic scaling to fit custom dimensions, and a small X‑dimension for narrow label output.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure barcode text alignment, auto‑size mode, image dimensions, and resolution using the BarcodeGenerator, EncodeTypes, and related parameter classes. Typical use cases include creating barcodes for compact labels, receipts, or product tags where precise sizing and text placement are required. Developers often need to adjust these settings to meet printing hardware constraints and visual design guidelines.
// Prompt: Align barcode text to right, enable automatic scaling, and generate image for narrow label printing.
// Tags: code128, barcode, text-alignment, autoscaling, image-generation, png, narrow-label, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a right‑aligned Code128 barcode with automatic scaling for a narrow label.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode image and saves it to the current directory.
    /// </summary>
    static void Main()
    {
        // Determine output file path in the current working directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "narrow_label.png");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Align the human‑readable text to the right side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Enable automatic scaling to fit the defined image size using interpolation
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image dimensions for a narrow label (pixels)
            generator.Parameters.ImageWidth.Pixels = 200f;
            generator.Parameters.ImageHeight.Pixels = 100f;

            // Use a small X‑dimension for narrow bars (points)
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Optional: define image resolution (dots per inch)
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}