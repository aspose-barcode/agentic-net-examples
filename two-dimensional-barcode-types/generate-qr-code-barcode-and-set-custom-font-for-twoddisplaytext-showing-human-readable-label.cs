using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a QR Code generator with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                // Set QR error correction level to high
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Set custom font for the human‑readable text (TwoDDisplayText)
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
                generator.Parameters.Barcode.CodeTextParameters.Font.Style = FontStyle.Bold;

                // Set the text that will be displayed instead of the codetext in the QR image
                generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "My QR Label";

                // Optionally adjust image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the barcode image
                generator.Save("qr_custom.png");
            }
        }
    }
}