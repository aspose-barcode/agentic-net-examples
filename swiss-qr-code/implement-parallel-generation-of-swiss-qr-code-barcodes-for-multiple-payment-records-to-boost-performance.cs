using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare a small set of payment records
        var payments = new List<PaymentRecord>
        {
            new PaymentRecord { Amount = 150.00m, Reference = "RF18539007547034", Currency = "CHF" },
            new PaymentRecord { Amount = 250.50m, Reference = "RF76123456789012", Currency = "CHF" },
            new PaymentRecord { Amount = 99.99m,  Reference = "RF00123456789012", Currency = "CHF" },
            new PaymentRecord { Amount = 500.00m, Reference = "RF12345678901234", Currency = "CHF" },
            new PaymentRecord { Amount = 75.25m,  Reference = "RF98765432109876", Currency = "CHF" }
        };

        // Ensure output directory exists
        string outputDir = "SwissQR_Output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Parallel generation of Swiss QR Code barcodes
        Parallel.ForEach(payments, (payment, state, index) =>
        {
            try
            {
                // Create and populate SwissQRCodetext
                var swissQr = new SwissQRCodetext();
                swissQr.Bill.Account = "CH9300762011623852957"; // known valid IBAN
                swissQr.Bill.Amount = payment.Amount;
                swissQr.Bill.Currency = payment.Currency;
                swissQr.Bill.Reference = payment.Reference;
                swissQr.Bill.Creditor.CountryCode = "CH";
                swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

                // Generate barcode image and save to file
                string filePath = Path.Combine(outputDir, $"SwissQR_{index + 1}.png");
                using (var generator = new ComplexBarcodeGenerator(swissQr))
                {
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating barcode for record {index + 1}: {ex.Message}");
            }
        });

        Console.WriteLine("Swiss QR Code generation completed.");
    }

    // Simple DTO for payment information
    class PaymentRecord
    {
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
    }
}