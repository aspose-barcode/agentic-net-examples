// Title: Adjust QR Code Text Gap for High‑Density QR Barcodes
// Description: Demonstrates how to set a 4‑point gap between a high‑density QR code and its human‑readable text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR code customization. It showcases the use of BarcodeGenerator, EncodeTypes, QRErrorLevel, and CodeTextParameters to control visual layout. Developers often need to fine‑tune text positioning for readability and aesthetic requirements in printed or digital media.
// Prompt: Adjust the gap between barcode and its text to 4 points for high‑density QR codes.
// Tags: qr, gap adjustment, png, barcodegenerator, codetextparameters, qrcodes, aspnet

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates adjusting the space between a QR code and its human‑readable text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a high‑density QR code with a 4‑point text gap and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Initialize a QR code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "High density QR example"))
        {
            // Set QR error correction level to high (Level H) for higher density
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Adjust the gap between the barcode and its human‑readable text to 4 points
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;

            // Save the generated barcode image as PNG
            generator.Save("HighDensityQR.png");
        }

        // Inform the user that the QR code has been generated
        Console.WriteLine("QR code generated and saved as HighDensityQR.png");
    }
}