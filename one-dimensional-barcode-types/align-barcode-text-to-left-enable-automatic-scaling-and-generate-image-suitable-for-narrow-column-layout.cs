// Title: Align barcode text left with auto scaling for narrow column
// Description: Demonstrates how to left‑align the human‑readable text of a Code128 barcode, enable automatic scaling, and set image dimensions for a narrow column layout.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameters such as CodeTextParameters, AutoSizeMode, and image size settings. Typical use cases include creating compact barcodes for reports, invoices, or mobile screens where space is limited. Developers often need to control text alignment and scaling to fit barcodes into constrained layouts.
// Prompt: Align barcode text to left, enable automatic scaling, and generate image suitable for narrow column layout.
// Tags: code128, barcode, text-alignment, autoscaling, narrow-layout, png, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with left‑aligned text, automatic scaling, and a narrow image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates, configures, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set human‑readable text alignment to the left side of the barcode
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Enable automatic scaling using interpolation to fit a narrow column
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define a narrow image width and a suitable height (points)
            generator.Parameters.ImageWidth.Point = 150f;   // narrow width
            generator.Parameters.ImageHeight.Point = 50f;   // appropriate height

            // Generate the barcode image as a bitmap
            using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
            {
                // Save the generated image as a PNG file
                generator.Save("barcode.png");
            }
        }
    }
}