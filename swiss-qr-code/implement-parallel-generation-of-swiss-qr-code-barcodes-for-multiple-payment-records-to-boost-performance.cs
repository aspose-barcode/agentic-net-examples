// Title: Parallel Generation of Swiss QR Code Barcodes for Payment Records
// Description: Demonstrates how to generate Swiss QR Code barcodes for multiple payment records in parallel, improving throughput.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Codes used for Swiss payment standards. It showcases the ComplexBarcodeGenerator, SwissQRCodetext, and related address classes. Developers creating bulk payment documents, invoices, or banking applications often need to generate many QR codes efficiently, and this pattern illustrates typical usage and performance optimization with parallel processing.
// Prompt: Implement parallel generation of Swiss QR Code barcodes for multiple payment records to boost performance.
// Tags: swiss qr code, barcode generation, parallel processing, aspose.barcode, complexbarcode, payment, invoice, png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates Swiss QR Code barcodes for a collection of payment records using parallel processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates sample payment data, generates QR codes in parallel,
    /// and saves the resulting PNG images to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for output
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Sample payment records (small batch for demonstration)
        var payments = new List<PaymentRecord>
        {
            new PaymentRecord
            {
                CreditorName = "Alice Smith",
                CreditorStreet = "Main Street",
                CreditorHouseNo = "10",
                CreditorPostalCode = "8000",
                CreditorTown = "Zurich",
                CreditorCountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 150.00m,
                Currency = "CHF",
                Reference = "2100000000000000000000001"
            },
            new PaymentRecord
            {
                CreditorName = "Bob Müller",
                CreditorStreet = "Bahnhofstrasse",
                CreditorHouseNo = "5A",
                CreditorPostalCode = "3000",
                CreditorTown = "Bern",
                CreditorCountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 250.75m,
                Currency = "CHF",
                Reference = "2100000000000000000000002"
            },
            new PaymentRecord
            {
                CreditorName = "Carol Dupont",
                CreditorStreet = "Rue de la Paix",
                CreditorHouseNo = "12",
                CreditorPostalCode = "1200",
                CreditorTown = "Geneva",
                CreditorCountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 99.99m,
                Currency = "CHF",
                Reference = "2100000000000000000000003"
            },
            new PaymentRecord
            {
                CreditorName = "David Rossi",
                CreditorStreet = "Limmatquai",
                CreditorHouseNo = "3",
                CreditorPostalCode = "8001",
                CreditorTown = "Zurich",
                CreditorCountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 500.00m,
                Currency = "CHF",
                Reference = "2100000000000000000000004"
            },
            new PaymentRecord
            {
                CreditorName = "Eva Keller",
                CreditorStreet = "Marktgasse",
                CreditorHouseNo = "7B",
                CreditorPostalCode = "6000",
                CreditorTown = "Luzern",
                CreditorCountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 75.50m,
                Currency = "CHF",
                Reference = "2100000000000000000000005"
            }
        };

        // Parallel generation of Swiss QR Code barcodes
        Parallel.ForEach(payments, (payment, state, index) =>
        {
            // Build Swiss QR Code codetext for the current payment
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQr.Bill.Account = payment.Account;
            swissQr.Bill.Amount = payment.Amount;
            swissQr.Bill.Currency = payment.Currency;
            swissQr.Bill.Reference = payment.Reference;
            swissQr.Bill.Creditor = new Address
            {
                Name = payment.CreditorName,
                Street = payment.CreditorStreet,
                HouseNo = payment.CreditorHouseNo,
                PostalCode = payment.CreditorPostalCode,
                Town = payment.CreditorTown,
                CountryCode = payment.CreditorCountryCode
            };
            // Optional debtor (left empty for this demo)

            // Generate barcode using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Set visual parameters
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save each barcode as a PNG file
                string filePath = Path.Combine(outputFolder, $"SwissQR_{index + 1}.png");
                generator.Save(filePath);
                Console.WriteLine($"Generated: {filePath}");
            }
        });

        Console.WriteLine("All barcodes generated.");
    }

    // Simple DTO for payment information
    class PaymentRecord
    {
        public string CreditorName { get; set; }
        public string CreditorStreet { get; set; }
        public string CreditorHouseNo { get; set; }
        public string CreditorPostalCode { get; set; }
        public string CreditorTown { get; set; }
        public string CreditorCountryCode { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
    }
}