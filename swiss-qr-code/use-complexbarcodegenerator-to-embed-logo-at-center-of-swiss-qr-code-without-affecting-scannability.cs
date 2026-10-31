using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Prepare output directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create Swiss QR Code codetext
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";
        swissQr.Bill.Creditor = new Address
        {
            Name = "John Doe",
            Street = "Main St",
            HouseNo = "1",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };
        swissQr.Bill.Debtor = new Address
        {
            Name = "Jane Smith",
            Street = "Second St",
            HouseNo = "2",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate Swiss QR Code with ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Generate barcode image
            using (var barcodeBmp = generator.GenerateBarCodeImage())
            {
                // Create a simple logo bitmap (red square)
                using (var logoBmp = new Bitmap(100, 100))
                {
                    using (var gLogo = Graphics.FromImage(logoBmp))
                    {
                        gLogo.Clear(Color.Red);
                    }

                    // Compute centered position
                    int x = (barcodeBmp.Width - logoBmp.Width) / 2;
                    int y = (barcodeBmp.Height - logoBmp.Height) / 2;

                    // Draw logo onto barcode
                    using (var gBarcode = Graphics.FromImage(barcodeBmp))
                    {
                        gBarcode.DrawImage(logoBmp, x, y, logoBmp.Width, logoBmp.Height);
                    }

                    // Save final image
                    string finalPath = Path.Combine(outputDir, "SwissQR_WithLogo.png");
                    barcodeBmp.Save(finalPath, ImageFormat.Png);
                    Console.WriteLine($"Swiss QR Code with logo saved to: {finalPath}");
                }
            }
        }
    }
}