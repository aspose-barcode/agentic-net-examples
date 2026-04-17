using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a QR Code generator with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Disable automatic sizing
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set fixed image dimensions (e.g., 300x300 points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Optional: set QR error correction level
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the barcode image to a file
            generator.Save("qr.png");
        }
    }
}