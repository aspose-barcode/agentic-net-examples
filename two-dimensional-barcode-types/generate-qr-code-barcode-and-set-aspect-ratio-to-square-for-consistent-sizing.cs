// Title: Generate QR Code with Square Aspect Ratio
// Description: Demonstrates creating a QR Code barcode and forcing a square module aspect ratio for uniform sizing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR code parameters such as aspect ratio using the BarcodeGenerator and its QR settings. Developers often need to produce QR codes with consistent dimensions for UI layouts, printing, or scanning reliability. The key API classes shown are BarcodeGenerator, EncodeTypes, and the QR parameter object.
// Prompt: Generate QR Code barcode and set aspect ratio to square for consistent sizing.
// Tags: qr code, aspect ratio, square, generation, png, aspose.barcode, encode types

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code barcode with a square aspect ratio using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR Code, sets its aspect ratio to 1 (square), saves it as PNG, and writes a confirmation message.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Force the QR modules to be square by setting the aspect ratio to 1.
            generator.Parameters.Barcode.QR.AspectRatio = 1f;

            // Persist the generated QR code as a PNG image file.
            generator.Save("qr_square.png");
        }

        // Output a simple confirmation to the console.
        Console.WriteLine("QR code generated: qr_square.png");
    }
}