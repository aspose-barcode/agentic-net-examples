using System;
using System.IO;
using System.Text;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.Unicode;

        var swiss = new SwissQRCodetext();
        swiss.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swiss.Bill.Account = "CH9300762011623852957";
        swiss.Bill.Amount = 199.95m;
        swiss.Bill.Currency = "CHF";
        swiss.Bill.Creditor = new Address
        {
            Name = "John Doe",
            CountryCode = "CH"
        };

        string imagePath = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N") + ".png");
        using (var generator = new ComplexBarcodeGenerator(swiss))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null) continue;

                Console.WriteLine("Creditor Name: " + decoded.Bill.Creditor.Name);
                Console.WriteLine("IBAN: " + decoded.Bill.Account);
                Console.WriteLine("Amount: " + decoded.Bill.Amount);
                Console.WriteLine("Currency: " + decoded.Bill.Currency);
            }
        }

        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
        }
        catch
        {
        }
    }
}