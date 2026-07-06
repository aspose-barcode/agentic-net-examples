// Title: Align QR Code Top Caption to Center
// Description: Demonstrates how to center a top caption on a QR code using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on customizing barcode captions. It shows how to use the BarcodeGenerator class along with CaptionParameters to modify caption text and alignment. Developers often need to add descriptive text above or below barcodes and control its positioning for better visual integration in documents and UI.
// Prompt: Align top caption to center for QR codes by setting CaptionParameters.Top.Alignment to CaptionAlignment.Center.
// Tags: qr code, caption alignment, barcode generation, aspose.barcode, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR code with a centered top caption.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, adds a top caption,
    /// centers the caption, and saves the image to a file.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set the text for the caption that appears above the barcode.
            generator.Parameters.CaptionAbove.Text = "Top Caption";

            // Center the top caption horizontally.
            generator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;

            // Save the generated barcode image with the centered caption to a PNG file.
            generator.Save("qr_with_centered_top_caption.png");
        }
    }
}