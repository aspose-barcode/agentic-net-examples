using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a QR code generator with sample codetext
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleData"))
            {
                // Set multilingual display text for the QR code
                generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "こんにちは世界 🌍";

                // Set error correction level (optional)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code image
                generator.Save("qr_multilingual.png");
            }
        }
    }
}