using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Create Swiss QR bill data
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = "John Doe";
            swissQr.Bill.Creditor.CountryCode = "CH";
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = 199.95m;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Initialize ComplexBarcodeGenerator with the codetext
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Set high error correction level for QR code
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated Swiss QR code image
                generator.Save("SwissQR.png");
            }

            Console.WriteLine("Swiss QR code generated with high error correction level.");
        }
    }
}