// Title: Parallel generation of Swiss QR Code barcodes for multiple payments
// Description: Demonstrates how to create Swiss QR Code barcodes concurrently using Aspose.BarCode's ComplexBarcodeGenerator, improving throughput for batch payment processing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Swiss QR (QR‑Bill) creation. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and parallel processing to efficiently produce PNG images for payment records—common in financial and invoicing applications where bulk QR‑Bill generation is required.
// Prompt: Implement parallel generation of Swiss QR Code barcodes for multiple payment records to boost performance.
// Tags: barcode symbology, generation, png, swissqr, complexbarcodegenerator, parallel

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;

/// <summary>
/// Demonstrates parallel generation of Swiss QR Code barcodes for a collection of payment records.
/// </summary>
class Program
{
    // Simple model representing a payment record required for Swiss QR generation
    class PaymentRecord
    {
        public string CreditorName { get; set; }
        public string CountryCode { get; set; }   // ISO country code, e.g., "CH"
        public string Account { get; set; }       // Valid IBAN
        public decimal Amount { get; set; }       // Payment amount
    }

    /// <summary>
    /// Entry point that creates sample payment data, generates Swiss QR Code barcodes in parallel, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare a small set of sample payment records (safe size for CI)
        var records = new List<PaymentRecord>
        {
            new PaymentRecord
            {
                CreditorName = "John Doe",
                CountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 199.95m
            },
            new PaymentRecord
            {
                CreditorName = "Alice Smith",
                CountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 50.00m
            },
            new PaymentRecord
            {
                CreditorName = "Bob Müller",
                CountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 123.45m
            },
            new PaymentRecord
            {
                CreditorName = "Carol Lee",
                CountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 75.20m
            },
            new PaymentRecord
            {
                CreditorName = "David Zhang",
                CountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 300.00m
            }
        };

        // Ensure output directory exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR_Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Parallel generation of Swiss QR Code barcodes
        Parallel.ForEach(records, (record, state, index) =>
        {
            // Build Swiss QR codetext based on the current payment record
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = record.CreditorName;
            swissQr.Bill.Creditor.CountryCode = record.CountryCode;
            swissQr.Bill.Account = record.Account;
            swissQr.Bill.Amount = record.Amount;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            // Optional: set currency (default is CHF)
            swissQr.Bill.Currency = "CHF";

            // Create a unique file name for the barcode image
            string fileName = $"SwissQR_{index + 1}_{record.CreditorName.Replace(' ', '_')}.png";
            string filePath = Path.Combine(outputDir, fileName);

            // Generate the barcode and save it directly to the file system
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // ComplexBarcodeGenerator handles image creation internally
                generator.Save(filePath);
            }

            Console.WriteLine($"Generated: {filePath}");
        });

        Console.WriteLine("All Swiss QR Code barcodes have been generated.");
    }
}