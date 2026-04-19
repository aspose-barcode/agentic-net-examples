using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set high error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Make the background transparent so the barcode can be overlaid on any colored background
            generator.Parameters.BackColor = Color.Transparent;

            // Save the barcode image as PNG (supports transparency)
            generator.Save("qr_transparent.png");
        }

        Console.WriteLine("QR Code with transparent background saved as 'qr_transparent.png'.");
    }
}