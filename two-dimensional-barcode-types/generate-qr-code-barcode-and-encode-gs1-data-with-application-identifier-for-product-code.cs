using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // GS1 QR code with product code (GTIN) Application Identifier (01)
            const string gs1ProductCode = "(01)12345678901231";
            const string outputFile = "gs1qr.png";

            // Create a QR code generator with GS1 QR symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1QR, gs1ProductCode))
            {
                // Set high error correction level for better readability
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated barcode image
                generator.Save(outputFile);
            }

            Console.WriteLine($"GS1 QR code saved to '{outputFile}'.");
        }
    }
}