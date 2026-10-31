using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare sample payment records
        var payments = new List<PaymentRecord>
        {
            new PaymentRecord { CreditorName = "Alice", Account = "CH9300762011623852957", Amount = 150.00m, Reference = "RF18539007547034" },
            new PaymentRecord { CreditorName = "Bob", Account = "CH9300762011623852957", Amount = 250.50m, Reference = "RF18539007547035" },
            new PaymentRecord { CreditorName = "Carol", Account = "CH9300762011623852957", Amount = 99.99m, Reference = "RF18539007547036" },
            new PaymentRecord { CreditorName = "Dave", Account = "CH9300762011623852957", Amount = 500.00m, Reference = "RF18539007547037" },
            new PaymentRecord { CreditorName = "Eve", Account = "CH9300762011623852957", Amount = 75.25m, Reference = "RF18539007547038" }
        };

        // Create a unique output folder
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Generating barcodes into: {outputFolder}");

        // Parallel generation
        Parallel.ForEach(payments, (payment, state, index) =>
        {
            // Build Swiss QR codetext
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQr.Bill.Account = payment.Account;
            swissQr.Bill.Amount = payment.Amount;
            swissQr.Bill.Currency = "CHF";
            swissQr.Bill.Reference = payment.Reference;
            swissQr.Bill.Creditor = new Address
            {
                Name = payment.CreditorName,
                CountryCode = "CH"
            };

            // Generate barcode
            string filePath = Path.Combine(outputFolder, $"SwissQR_{index + 1}.png");
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated: {Path.GetFileName(filePath)}");
        });

        Console.WriteLine("All barcodes generated.");
    }

    // Simple DTO for payment data
    class PaymentRecord
    {
        public string CreditorName { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
    }
}