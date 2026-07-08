// Title: Align Code128 barcode text left with auto‑scaling for narrow column layout
// Description: Demonstrates how to left‑align human‑readable text, enable automatic scaling, and set image size for a narrow column using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and Parameters classes to customize barcode appearance. Typical scenarios include creating compact barcodes for reports, invoices, or mobile screens where space is limited. Developers often need to control text alignment, scaling mode, and image dimensions to fit specific layout constraints.
// Prompt: Align barcode text to left, enable automatic scaling, and generate image suitable for narrow column layout.
// Tags: code128, alignment, autoscaling, png, barcodegenerator, parameters, aspose.barcode, imagegeneration

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with left‑aligned text, automatic scaling, and a compact image size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample value.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable automatic scaling using interpolation to fit the specified dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define image size (points) suitable for a narrow column layout.
            generator.Parameters.ImageWidth.Point = 150f;
            generator.Parameters.ImageHeight.Point = 50f;

            // Align the human‑readable text to the left side of the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Left;

            // Optional: adjust the module (X) dimension for clearer rendering at small sizes.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG file.
            generator.Save("barcode.png");
        }

        // Inform the user that the image has been created.
        Console.WriteLine("Barcode image generated: barcode.png");
    }
}