// Title: High‑Resolution Code128 Barcode Generation for Label Printing
// Description: Demonstrates configuring Aspose.BarCode to generate a 300 DPI Code128 barcode image suitable for high‑resolution label printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to adjust resolution, module size, bar height, colors, and auto‑size settings using the BarcodeGenerator, EncodeTypes, and Parameters classes. Typical use cases include creating printable barcodes for product labels, shipping tags, and inventory stickers where precise dimensions and high image quality are required. Developers often need to fine‑tune these settings to meet printer specifications and label design guidelines.
// Prompt: Adjust generator settings to produce a barcode image suitable for high‑resolution printing on labels.
// Tags: code128, generation, png, barcodegenerator, encodetypes, parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the high‑resolution barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode image with settings optimized for high‑resolution label printing.
    /// </summary>
    static void Main()
    {
        // Create a Code128 barcode generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set image resolution to 300 DPI for crisp printing
            generator.Parameters.Resolution = 300;

            // Define a small module size (XDimension) for fine detail (0.5 points)
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Set bar height appropriate for label size (50 points)
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Use black bars on a white background for maximum contrast
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Disable automatic resizing to preserve exact dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the barcode image as a PNG file
            generator.Save("highres_label.png");
        }

        // Inform the user that the image has been created
        Console.WriteLine("Barcode image generated: highres_label.png");
    }
}