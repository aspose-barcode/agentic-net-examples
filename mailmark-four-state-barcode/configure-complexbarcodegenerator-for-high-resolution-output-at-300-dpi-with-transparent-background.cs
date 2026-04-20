using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Creditor.Name = "John Doe";

        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Resolution = 300;
            generator.Parameters.BackColor = Color.Transparent;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Save("SwissQR.png");
        }
    }
}