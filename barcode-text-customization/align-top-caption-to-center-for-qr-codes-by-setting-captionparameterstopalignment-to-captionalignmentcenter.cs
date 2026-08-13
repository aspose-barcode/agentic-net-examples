// Title: Center Top Caption on QR Code using Aspose.BarCode
// Description: Demonstrates generating a QR code image with a top caption that is horizontally centered. Shows how to configure caption text, alignment, and font size using Aspose.BarCode API.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to work with BarcodeGenerator, EncodeTypes, and CaptionParameters to customize barcode appearance. Developers often need to add readable text above or below barcodes for branding, instructions, or product information; this snippet shows the typical steps for setting caption text, alignment, and styling before saving the image.
// Prompt: Align top caption to center for QR codes by setting CaptionParameters.Top.Alignment to CaptionAlignment.Center.
// Tags: qr code, caption alignment, barcode generation, aspose.barcode, png output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a QR code with a centered top caption and saves it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, configures a centered top caption, and writes the result to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the text for the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Center the top caption horizontally.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Optionally increase the font size for better readability.
            generator.Parameters.CaptionAbove.Font.Size.Point = 12f;

            // Save the generated barcode with the caption to a PNG file.
            generator.Save("qr_with_caption.png");
        }

        // Inform the user that the image has been created.
        Console.WriteLine("QR code with centered top caption generated successfully.");
    }
}