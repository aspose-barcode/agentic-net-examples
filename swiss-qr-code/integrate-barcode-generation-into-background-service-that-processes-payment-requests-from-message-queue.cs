// Title: Generate Swiss QR Code barcodes for payment requests in a background service
// Description: Demonstrates how to create Swiss QR Code barcodes from payment data and save them as PNG files, suitable for batch processing in a service.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode creation using the ComplexBarcodeGenerator and SwissQRCodetext classes. It shows typical use cases such as encoding payment information into QR codes for electronic invoicing. Developers working with financial data often need to generate QR codes in bulk, customize dimensions, and output them in common image formats.
// Prompt: Integrate barcode generation into a background service that processes payment requests from a message queue.
// Tags: swiss-qr-code, barcode-generation, png, complexbarcodegenerator, swissqrcodetext

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Swiss QR Code barcodes for a list of payment requests
/// and saves each barcode as a PNG image in a temporary output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates sample payment data, builds Swiss QR Code
    /// payloads, generates barcodes, and writes the images to disk.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary output folder for the generated barcode images
        string outputFolder = Path.Combine(Path.GetTempPath(), "Payments_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Barcodes will be saved to: {outputFolder}");

        // Sample payment requests that would normally be read from a message queue
        var payments = new List<PaymentRequest>
        {
            new PaymentRequest
            {
                Account = "CH4431999123000889012",
                Amount = 150.75m,
                Currency = "CHF",
                Reference = "210000000003139471430009017",
                Creditor = new Address
                {
                    Name = "Muster & Söhne",
                    Street = "Musterstrasse",
                    HouseNo = "12b",
                    PostalCode = "8200",
                    Town = "Zürich",
                    CountryCode = "CH"
                },
                Debtor = new Address
                {
                    Name = "Muster AG",
                    Street = "Musterstrasse",
                    HouseNo = "1",
                    PostalCode = "3030",
                    Town = "Bern",
                    CountryCode = "CH"
                }
            },
            new PaymentRequest
            {
                Account = "CH9300762011623852957",
                Amount = 2500.00m,
                Currency = "CHF",
                Reference = "210000000004567890123456789",
                Creditor = new Address
                {
                    Name = "Alpha Corp",
                    Street = "Hauptstrasse",
                    HouseNo = "5",
                    PostalCode = "8000",
                    Town = "Zürich",
                    CountryCode = "CH"
                },
                Debtor = new Address
                {
                    Name = "Beta Ltd",
                    Street = "Nebenweg",
                    HouseNo = "9",
                    PostalCode = "4000",
                    Town = "Basel",
                    CountryCode = "CH"
                }
            },
            new PaymentRequest
            {
                Account = "CH5604835012345678009",
                Amount = 99.99m,
                Currency = "CHF",
                Reference = "210000000005987654321098765",
                Creditor = new Address
                {
                    Name = "Gamma Services",
                    Street = "Industriestrasse",
                    HouseNo = "22",
                    PostalCode = "6000",
                    Town = "Luzern",
                    CountryCode = "CH"
                },
                Debtor = new Address
                {
                    Name = "Delta GmbH",
                    Street = "Marktweg",
                    HouseNo = "3A",
                    PostalCode = "5000",
                    Town = "Aarau",
                    CountryCode = "CH"
                }
            }
        };

        // Iterate over each payment request and generate a corresponding barcode
        for (int i = 0; i < payments.Count; i++)
        {
            var request = payments[i];
            try
            {
                // Build the Swiss QR Code payload using the payment data
                SwissQRCodetext swissCode = new SwissQRCodetext();
                swissCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
                swissCode.Bill.Account = request.Account;
                swissCode.Bill.Amount = request.Amount;
                swissCode.Bill.Currency = request.Currency;
                swissCode.Bill.Reference = request.Reference;
                swissCode.Bill.Creditor = request.Creditor;
                swissCode.Bill.Debtor = request.Debtor;

                // Generate the barcode image with desired parameters
                using (var generator = new ComplexBarcodeGenerator(swissCode))
                {
                    generator.Parameters.Barcode.XDimension.Pixels = 4;               // Set module size
                    generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;   // Use ECI encoding for UTF-8 support
                    generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                    // Define the output file path and save the PNG image
                    string filePath = Path.Combine(outputFolder, $"Payment_{i + 1}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for payment {i + 1}: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation
                Console.WriteLine($"Error processing payment {i + 1}: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }

    // Simple DTO for payment data
    class PaymentRequest
    {
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public Address Creditor { get; set; }
        public Address Debtor { get; set; }
    }
}