using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a QR code generator with initial codetext.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "InitialData"))
            {
                // Set the text that will be displayed under the QR code to a custom URL.
                generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "https://example.com";

                // Optional: set error correction level.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code image.
                generator.Save("qr_custom_text.png");
            }
        }
    }
}