using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        byte[] pngData = GenerateSwissQrCode();

        const string outputPath = "SwissQR.png";
        using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            fileStream.Write(pngData, 0, pngData.Length);
        }

        Console.WriteLine($"Swiss QR Code image saved to '{outputPath}'.");
    }

    static byte[] GenerateSwissQrCode()
    {
        var swissQr = new SwissQRCodetext();

        // Bill information
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Creditor (mandatory fields)
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.Street = "Main Street 1";
        swissQr.Bill.Creditor.PostalCode = "8000";
        swissQr.Bill.Creditor.Town = "Zurich";
        swissQr.Bill.Creditor.CountryCode = "CH";

        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQr))
        using (MemoryStream memoryStream = new MemoryStream())
        {
            generator.Save(memoryStream, BarCodeImageFormat.Png);
            return memoryStream.ToArray();
        }
    }
}