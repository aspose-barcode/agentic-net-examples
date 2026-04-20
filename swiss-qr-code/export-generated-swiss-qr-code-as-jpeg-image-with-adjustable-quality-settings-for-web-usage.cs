using System;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.Street = "Main Street 1";
        swissQr.Bill.Creditor.PostalCode = "8000";
        swissQr.Bill.Creditor.Town = "Zurich";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Save("SwissQR.png");
        }
    }
}