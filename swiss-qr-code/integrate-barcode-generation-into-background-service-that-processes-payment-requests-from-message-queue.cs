// Title: Generate Swiss QR Code for Payment Requests in a Background Service
// Description: Demonstrates generating a Swiss QR Code barcode for a payment request using Aspose.BarCode and saving it as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex QR code creation (Swiss QR Bill). It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related parameter settings to produce QR codes for financial documents. Developers working with payment processing, invoicing, or QR‑based payment standards commonly use these APIs to embed payment data in a scannable format.
// Prompt: Integrate barcode generation into a background service that processes payment requests from a message queue.
// Tags: swiss qr, barcode generation, payment, qr code, aspose.barcode, complexbarcodegenerator, png, background service

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate Swiss QR Code barcodes for payment requests
/// and save them as PNG images. Intended for use in background services that
/// process messages from a queue.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Simulates a message queue, builds the QR
    /// code payload, generates the barcode image, and writes the file to disk.
    /// </summary>
    static void Main()
    {
        // Simulated payment request queue with a single item
        var paymentRequests = new List<PaymentRequest>
        {
            new PaymentRequest
            {
                Account = "CH4431999123000889012",
                Amount = 1000.25m,
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
            }
        };

        // Create a temporary output directory for the generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        int index = 0;
        foreach (var request in paymentRequests)
        {
            // Build Swiss QR Code codetext from the payment request data
            var swissQRCode = new SwissQRCodetext();
            swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQRCode.Bill.Account = request.Account;
            swissQRCode.Bill.Amount = request.Amount;
            swissQRCode.Bill.Currency = request.Currency;
            swissQRCode.Bill.Reference = request.Reference;
            swissQRCode.Bill.Creditor = request.Creditor;
            swissQRCode.Bill.Debtor = request.Debtor;

            // Generate the barcode image using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(swissQRCode))
            {
                // Configure visual parameters: module size and QR encoding mode
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                // Save the generated QR code as a PNG file
                string filePath = Path.Combine(outputDir, $"SwissQR_{index}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated Swiss QR Code saved to: {filePath}");
            }

            index++;
        }

        Console.WriteLine("Processing completed.");
    }
}

/// <summary>
/// Represents a payment request containing all data required for a Swiss QR Bill.
/// </summary>
class PaymentRequest
{
    public string Account { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Reference { get; set; }
    public Address Creditor { get; set; }
    public Address Debtor { get; set; }
}