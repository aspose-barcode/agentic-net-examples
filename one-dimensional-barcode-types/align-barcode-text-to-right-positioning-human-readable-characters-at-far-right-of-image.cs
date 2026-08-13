// Title: Right-Aligned Human-Readable Text in a Barcode Image
// Description: Demonstrates how to generate a Code128 barcode with the human‑readable text positioned at the far right below the bars.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize human‑readable text placement using the BarcodeGenerator, EncodeTypes, CodeLocation, and TextAlignment classes. Typical use cases include creating barcodes for labels, receipts, or packaging where the readable text must be aligned to a specific side of the image. Developers often need to adjust text location and alignment to meet branding or layout requirements.
// Prompt: Align barcode text to the right, positioning human‑readable characters at the far right of the image.
// Tags: barcode, code128, text-alignment, right-align, image, aspose.barcode, generation

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing; // Required for Aspose.Drawing.Bitmap if needed

/// <summary>
/// Generates a Code128 barcode with right‑aligned human‑readable text placed below the bars.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, configures text alignment, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the data to be encoded in the barcode.
            generator.CodeText = "123ABC";

            // Make the human‑readable text visible and position it below the barcode.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Align the human‑readable text to the far right side of the image.
            generator.Parameters.Barcode.CodeTextParameters.Alignment = TextAlignment.Right;

            // Save the generated barcode as a PNG image.
            generator.Save("right_aligned.png");
        }
    }
}