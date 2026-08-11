// Title: Generate right-aligned Code128 barcode with auto scaling for narrow label printing
// Description: Demonstrates how to create a Code128 barcode, align its human‑readable text to the right, enable automatic scaling, and output a PNG image sized for narrow label printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and various Parameters such as AutoSizeMode, image dimensions, XDimension, and CodeTextParameters. Typical use cases include creating compact barcodes for small labels, receipts, or product tags where precise alignment and scaling are required. Developers often need to adjust image size, resolution, and text alignment to meet printing specifications.
// Prompt: Align barcode text to right, enable automatic scaling, and generate image for narrow label printing.
// Tags: code128, barcode generation, auto scaling, text alignment, narrow label, png, aspose.barcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing; // Required for BarCodeImageFormat enum

/// <summary>
/// Example program that generates a right‑aligned Code128 barcode with automatic scaling,
/// sized for a narrow label and saved as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures scaling, alignment, and image size,
    /// then saves the result to a file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the desired code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Enable automatic scaling using interpolation mode to keep the barcode readable
            // when the image size changes.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set the target image dimensions (in points) suitable for a narrow label.
            generator.Parameters.ImageWidth.Point = 150f;   // Label width
            generator.Parameters.ImageHeight.Point = 50f;   // Label height

            // Reduce the module (X) dimension to keep the barcode compact on the small label.
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Align the human‑readable text to the right side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Increase the resolution to 300 DPI for higher print quality on narrow labels.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a PNG image.
            generator.Save("narrow_label.png");
        }
    }
}