// Title: Generate QR Code with Medium Error Correction
// Description: Demonstrates creating a QR Code barcode, setting its error correction level to medium, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure QR Code parameters such as error correction level using the BarcodeGenerator and QRErrorLevel classes. Typical use cases include creating robust QR codes for URLs or data payloads where a balance between data capacity and resilience to damage is required. Developers often need to adjust error correction to meet scanning reliability requirements across various environments.
// Prompt: Generate QR Code barcode and set error correction level to medium for balanced robustness.
// Tags: qr code, error correction, barcode generation, png output, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code with medium error correction and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a QR Code generator with the desired text (e.g., a URL)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the QR Code to use Medium error correction (LevelM) for balanced robustness
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR Code image to a PNG file named "qr.png"
            generator.Save("qr.png");
        }

        // Output a simple confirmation message to the console
        Console.WriteLine("QR Code generated and saved as qr.png");
    }
}