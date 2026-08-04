// Title: Generate Code128 barcode with caption using Document unit and 600 dpi PNG output
// Description: Demonstrates creating a Code128 barcode, adding a caption with FontUnit.Document, and saving it as a high‑resolution 600 dpi PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode parameters such as resolution, caption placement, and font units. It showcases the use of BarcodeGenerator, EncodeTypes, and FontUnit classes, which are commonly needed when developers need precise control over barcode appearance and high‑quality image output for printing or digital media.
// Prompt: Use Unit.Document for FontUnit of barcode caption, then produce high‑resolution 600 dpi PNG output.
// Tags: code128, barcode, caption, png, high-resolution, 600dpi, aspose.barcodes, fontunit, document-unit

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode with a caption,
/// sets the caption font size using <c>FontUnit.Document</c>,
/// and saves the result as a 600 dpi PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the output resolution to 600 DPI for high‑quality rendering.
            generator.Parameters.Resolution = 600f;

            // Configure a caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Text = "Sample Caption";

            // Specify the caption font size using Document units (points).
            generator.Parameters.CaptionAbove.Font.Size.Document = 12f;

            // Choose a widely available font family for the caption.
            generator.Parameters.CaptionAbove.Font.FamilyName = "Helvetica";

            // Save the generated barcode as a PNG file with the specified resolution.
            generator.Save("barcode_600dpi.png");
        }
    }
}