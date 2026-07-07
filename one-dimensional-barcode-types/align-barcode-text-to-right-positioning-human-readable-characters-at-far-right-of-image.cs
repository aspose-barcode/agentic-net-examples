// Title: Align barcode text to the right
// Description: Demonstrates how to position the human‑readable text of a barcode at the far right edge of the generated image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode text formatting category, showing how to control the alignment of human‑readable characters via the CodeTextParameters API. Typical use cases include customizing barcode labels for printing where text placement matters, such as aligning numbers to the right margin. Developers often need to adjust TextAlignment, Font, and other visual properties to meet layout requirements.
// Prompt: Align barcode text to the right, positioning human‑readable characters at the far right of the image.
// Tags: code128, text-alignment, png, barcodegenerator, codetextparameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with its human‑readable text aligned to the right side of the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, sets right alignment for the text, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with the sample value "1234567890"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the human‑readable text alignment to the far right of the barcode image
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Define the output file name (PNG format)
            string outputFile = "right_aligned.png";

            // Save the generated barcode image to the specified file
            generator.Save(outputFile);

            // Inform the user where the image was saved
            Console.WriteLine($"Barcode image saved to: {outputFile}");
        }
    }
}