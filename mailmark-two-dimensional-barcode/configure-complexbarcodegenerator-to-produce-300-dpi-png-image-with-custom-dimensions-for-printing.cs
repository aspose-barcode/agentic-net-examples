using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        var swissQr = new SwissQRCodetext();

        // Creditor (mandatory fields)
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.Street = "Main Street 1";
        swissQr.Bill.Creditor.PostalCode = "8000";
        swissQr.Bill.Creditor.Town = "Zurich";
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Account (mandatory)
        swissQr.Bill.Account = "CH9300762011623852957";

        // Optional fields
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Resolution = 300f;
            generator.Parameters.ImageWidth.Pixels = 1200f;
            generator.Parameters.ImageHeight.Pixels = 800f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            generator.Save("SwissQR_300dpi.png");
        }
    }
}