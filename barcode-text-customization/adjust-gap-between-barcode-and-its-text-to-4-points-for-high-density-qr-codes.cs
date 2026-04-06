using System;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Text to encode
            generator.CodeText = "HighDensityQR";

            // Adjust the gap between the barcode and its human‑readable text to 4 points
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;

            // Optional: ensure a high‑density QR by using automatic version selection and low error correction
            generator.Parameters.Barcode.QR.Version = QRVersion.Auto;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelL;

            // Save the generated barcode image
            generator.Save("high_density_qr.png");
        }
    }
}