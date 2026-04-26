using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

public class Program
{
    public static void Main()
    {
        var swissQr = new SwissQRCodetext();

        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Creditor.Name = "John Doe";

        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            string outputPath = "SwissQR.png";
            generator.Save(outputPath);
            Console.WriteLine($"Swiss QR Code saved to {Path.GetFullPath(outputPath)}");
        }
    }
}