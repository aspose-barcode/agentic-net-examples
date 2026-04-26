using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    // Generates a Swiss QR Code PNG and returns it as a byte array.
    // Required fields: Creditor Name, Creditor CountryCode, Account (IBAN), Amount, Version.
    public static byte[] CreateSwissQrCode(string creditorName, string creditorCountryCode, string account, decimal amount)
    {
        // Build the Swiss QR codetext with mandatory bill data.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = creditorName;
        swissQr.Bill.Creditor.CountryCode = creditorCountryCode;
        swissQr.Bill.Account = account;
        swissQr.Bill.Amount = amount;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the barcode image and save it to a memory stream as PNG.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
    }

    static void Main()
    {
        // Sample data (valid IBAN and creditor details).
        string name = "John Doe";
        string countryCode = "CH";
        string iban = "CH9300762011623852957";
        decimal amount = 199.95m;

        byte[] pngBytes = CreateSwissQrCode(name, countryCode, iban, amount);
        Console.WriteLine($"Generated Swiss QR Code PNG byte array length: {pngBytes.Length}");
        // Optionally write to a file for verification (commented out to avoid I/O in the runner).
        // File.WriteAllBytes("SwissQR.png", pngBytes);
    }
}