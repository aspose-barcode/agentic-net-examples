// Title: Center-aligned barcode text with automatic scaling
// Description: Demonstrates how to center the human‑readable text of a Code128 barcode and enable automatic scaling to fit a narrow image width.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and related parameter classes to customize barcode appearance. Typical scenarios include creating compact barcodes for limited‑space labels while preserving readability. Developers often need to adjust text alignment, font sizing, and image dimensions to meet layout constraints.
// Prompt: Align barcode text to center and enable automatic scaling to fit within narrow barcode width.
// Tags: code128, text alignment, auto scaling, png, aspose.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with centered human‑readable text and automatic scaling
/// to fit a narrow image width, then saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, configures text alignment and scaling,
    /// and writes the result to "barcode.png".
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data that will be encoded in the barcode.
            generator.CodeText = "1234567890";

            // Configure automatic scaling (interpolation) so the barcode adapts to the image size.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define a narrow image width (150 points) and a suitable height (50 points).
            generator.Parameters.ImageWidth.Point = 150f;
            generator.Parameters.ImageHeight.Point = 50f;

            // Center‑align the human‑readable text beneath the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Center;

            // Enable automatic font sizing so the text scales proportionally with the barcode.
            generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

            // Save the generated barcode image to a PNG file.
            generator.Save("barcode.png");
        }
    }
}