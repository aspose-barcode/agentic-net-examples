// Title: Replace QR Code Display Text with Custom URL using Aspose.BarCode
// Description: Demonstrates how to generate a QR code and override the human‑readable text displayed beneath the symbol with a custom URL.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize QR code appearance using the BarcodeGenerator class and its CodeTextParameters. Typical use cases include branding QR codes where the encoded data differs from the displayed text, such as showing a marketing URL while encoding other information. Developers often need to adjust TwoDDisplayText to control what end users see without altering the underlying payload.
// Prompt: Replace displayed text of a QR code with a custom URL by setting CodetextParameters.Text.
// Tags: qr code,display text,custom url,aspose.barcode,generation,code text parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a QR code, sets a custom display text (URL), and saves the image to disk.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, customizes its displayed text, and writes the result to a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated QR code image.
        string outputPath = "qr.png";

        // Initialize a QR code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the actual data to be encoded in the QR code.
            generator.CodeText = "Data to encode";

            // Override the human‑readable text shown below the QR code with a custom URL.
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "https://example.com";

            // Save the generated QR code image to the specified file.
            generator.Save(outputPath);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to {outputPath}");
    }
}