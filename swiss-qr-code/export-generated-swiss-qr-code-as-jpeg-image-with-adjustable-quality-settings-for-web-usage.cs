using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare output path
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "SwissQR.jpeg");

        // Create Swiss QR Code data
        var swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = "210000000003139471430009017";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate barcode
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Adjust size and quality for web usage
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Resolution = 72f;          // lower DPI reduces file size
            generator.Parameters.UseAntiAlias = false;    // disable anti-aliasing for smaller output

            // Save as JPEG
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        Console.WriteLine($"Swiss QR Code saved to: {outputPath}");
    }
}