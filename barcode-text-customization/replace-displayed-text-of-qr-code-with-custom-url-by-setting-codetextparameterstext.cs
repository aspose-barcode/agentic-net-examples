// Title: Generate QR code with custom display URL
// Description: Demonstrates how to replace the displayed text of a QR code with a custom URL using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize QR code appearance via the CodeTextParameters API. It shows setting TwoDDisplayText to a URL while keeping the encoded data unchanged, a common requirement for branding or linking purposes. Developers working with QR code generation often need to modify the human‑readable text without affecting the encoded payload.
// Prompt: Replace displayed text of a QR code with a custom URL by setting CodetextParameters.Text.
// Tags: qr code, custom url, display text, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image where the displayed text is replaced with a custom URL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the QR code and saves it as a PNG file.
    /// </summary>
    static void Main(string[] args)
    {
        // Determine output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_custom_url.png");

        // Create a QR code generator with initial data
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "OriginalData"))
        {
            // Set the human‑readable text displayed under the QR code to a custom URL
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "https://example.com";

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine($"QR code image saved to: {outputPath}");
    }
}