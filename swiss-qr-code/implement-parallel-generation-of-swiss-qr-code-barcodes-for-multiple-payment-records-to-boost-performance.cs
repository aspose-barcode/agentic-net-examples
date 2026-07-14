// Title: Parallel generation of Swiss QR Code barcodes for payment records
// Description: Demonstrates creating Swiss QR Code barcodes for multiple payment records using Aspose.BarCode in parallel to improve performance.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Codes. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related classes to encode payment information into QR bills, a common requirement for Swiss financial applications. Developers can adapt this pattern for batch processing of payment data and high‑throughput barcode creation.
// Prompt: Implement parallel generation of Swiss QR Code barcodes for multiple payment records to boost performance.
// Tags: swiss qr code, barcode generation, parallel processing, aspnet, aspose.barcode, complexbarcodegenerator

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates parallel generation of Swiss QR Code barcodes for multiple payment records.
/// </summary>
class Program
{
    /// <summary>
    /// Simple payment record model containing the data required for a Swiss QR bill.
    /// </summary>
    class PaymentRecord
    {
        public string CreditorName { get; set; }
        public string CreditorCountryCode { get; set; }
        public string AccountIban { get; set; }
        public decimal Amount { get; set; }
    }

    /// <summary>
    /// Entry point that creates sample payment records, generates Swiss QR Code barcodes in parallel, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare a small set of sample payment records.
        var payments = new List<PaymentRecord>
        {
            new PaymentRecord { CreditorName = "John Doe", CreditorCountryCode = "CH", AccountIban = "CH9300762011623852957", Amount = 199.95m },
            new PaymentRecord { CreditorName = "John Doe", CreditorCountryCode = "CH", AccountIban = "CH9300762011623852957", Amount = 250.00m },
            new PaymentRecord { CreditorName = "John Doe", CreditorCountryCode = "CH", AccountIban = "CH9300762011623852957", Amount = 75.50m },
            new PaymentRecord { CreditorName = "John Doe", CreditorCountryCode = "CH", AccountIban = "CH9300762011623852957", Amount = 120.00m },
            new PaymentRecord { CreditorName = "John Doe", CreditorCountryCode = "CH", AccountIban = "CH9300762011623852957", Amount = 99.99m }
        };

        // Ensure the output directory exists.
        string outputDir = "SwissQR_Output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Generate Swiss QR Code barcodes in parallel, one per payment record.
        Parallel.ForEach(
            payments,
            new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
            (payment, state, index) =>
            {
                // Build the Swiss QR code text using the payment data.
                var swissQr = new SwissQRCodetext();
                swissQr.Bill.Creditor.Name = payment.CreditorName;
                swissQr.Bill.Creditor.CountryCode = payment.CreditorCountryCode;
                swissQr.Bill.Account = payment.AccountIban;
                swissQr.Bill.Amount = payment.Amount;
                swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

                // Create a generator for the current record and save the barcode as PNG.
                using (var generator = new ComplexBarcodeGenerator(swissQr))
                {
                    string filePath = Path.Combine(outputDir, $"SwissQR_{index + 1}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated Swiss QR barcode for record {index + 1}");
            });

        Console.WriteLine("All Swiss QR barcodes have been generated.");
    }
}