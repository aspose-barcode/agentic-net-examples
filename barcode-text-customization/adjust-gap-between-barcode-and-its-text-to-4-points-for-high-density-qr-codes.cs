using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

class Program
{
    static void Main()
    {
        // Create a QR code generator with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HighDensityQR"))
        {
            // Set the gap between the barcode and its human‑readable text to 4 points
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;

            // Optionally, set a high error correction level for a denser QR code
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated QR code image
            generator.Save("HighDensityQR.png");
        }
    }
}