// Title: Replace QR code displayed text with a custom URL
// Description: Demonstrates how to change the text shown beneath a QR code by using CodetextParameters.Text.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize QR code appearance using the BarcodeGenerator class and its Parameters.Barcode.CodeTextParameters. Developers often need to modify the human‑readable text under 2‑D barcodes for branding or linking purposes. The snippet shows setting TwoDDisplayText and saving the image, a common task when integrating QR codes into applications.
// Prompt: Replace displayed text of a QR code with a custom URL by setting CodetextParameters.Text.
// Tags: qr code, barcode generation, codetextparameters, display text, aspose.barcode, image output

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates replacing the displayed text of a QR code with a custom URL using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, sets custom display text, and saves the image.
    /// </summary>
    static void Main()
    {
        // Create a QR code generator with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleData"))
        {
            // Set the human‑readable text displayed under the QR code.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "https://customurl.com";

            // Save the generated QR code as a PNG image.
            generator.Save("qr.png");
        }

        // Inform the user that the QR code has been generated.
        Console.WriteLine("QR code generated and saved as qr.png");
    }
}