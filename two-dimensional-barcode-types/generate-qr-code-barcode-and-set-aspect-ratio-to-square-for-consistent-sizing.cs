using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR Code using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code with a sample URL and saves it.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for QR code with the desired content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure QR code parameters: set aspect ratio to 1 to ensure a square shape.
            generator.Parameters.Barcode.QR.AspectRatio = 1f;

            // Persist the generated QR code image to a PNG file named "qr.png".
            generator.Save("qr.png");
        }

        // Inform the user that the QR code has been successfully created.
        Console.WriteLine("QR Code generated and saved as qr.png");
    }
}