// Title: Generate Swiss QR Code Barcodes for Payment Requests
// Description: Demonstrates generating Swiss QR Code barcodes from a collection of payment requests and saving each barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of ComplexBarcodeGenerator and SwissQRCodetext to create QR codes for financial documents. Developers often need to produce QR codes for payment processing, invoicing, or banking applications; this snippet illustrates typical setup, parameter configuration, and image output using Aspose.BarCode APIs.
// Prompt: Integrate barcode generation into a background service that processes payment requests from a message queue.
// Tags: swissqr, barcode, generation, png, aspose.barcode, complexbarcodegenerator, payment, qr, eci, eciencoding, imageoutput

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Entry point for the console application that generates Swiss QR Code barcodes
/// for a predefined list of payment requests and writes the images to a temporary folder.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates the creation of QR code barcodes for each payment request.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Simulated payment requests – in a real background service these
        // would be read from a message queue or other asynchronous source.
        // ------------------------------------------------------------
        var payments = new List<PaymentRequest>
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
            },
            new PaymentRequest
            {
                Account = "CH9300762011623852957",
                Amount = 250.00m,
                Currency = "CHF",
                Reference = "210000000004567890123456789",
                Creditor = new Address
                {
                    Name = "Alpha Corp",
                    Street = "Alphastrasse",
                    HouseNo = "5",
                    PostalCode = "8000",
                    Town = "Zürich",
                    CountryCode = "CH"
                },
                Debtor = new Address
                {
                    Name = "Beta Ltd",
                    Street = "Betastraße",
                    HouseNo = "9",
                    PostalCode = "4000",
                    Town = "Basel",
                    CountryCode = "CH"
                }
            },
            new PaymentRequest
            {
                Account = "CH5604835012345678009",
                Amount = 75.50m,
                Currency = "CHF",
                Reference = "210000000005987654321098765",
                Creditor = new Address
                {
                    Name = "Gamma GmbH",
                    Street = "Gammastraße",
                    HouseNo = "3A",
                    PostalCode = "6000",
                    Town = "Luzern",
                    CountryCode = "CH"
                },
                Debtor = new Address
                {
                    Name = "Delta Inc",
                    Street = "Deltaplatz",
                    HouseNo = "2",
                    PostalCode = "3000",
                    Town = "Bern",
                    CountryCode = "CH"
                }
            }
        };

        // ------------------------------------------------------------
        // Create a dedicated temporary output folder for the generated PNG files.
        // ------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        int index = 1;
        foreach (var payment in payments)
        {
            // ------------------------------------------------------------
            // Build Swiss QR Code data structure from the current payment request.
            // ------------------------------------------------------------
            var swissQRCode = new SwissQRCodetext();
            swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQRCode.Bill.Account = payment.Account;
            swissQRCode.Bill.Amount = payment.Amount;
            swissQRCode.Bill.Currency = payment.Currency;
            swissQRCode.Bill.Reference = payment.Reference;
            swissQRCode.Bill.Creditor = payment.Creditor;
            swissQRCode.Bill.Debtor = payment.Debtor;

            // ------------------------------------------------------------
            // Generate the barcode image using ComplexBarcodeGenerator.
            // ------------------------------------------------------------
            using (var generator = new ComplexBarcodeGenerator(swissQRCode))
            {
                // Configure visual appearance and QR encoding settings.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode as a PNG file.
                string filePath = Path.Combine(outputFolder, $"SwissQR_{index}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode {index}: {filePath}");
            }

            index++;
        }

        Console.WriteLine("All barcodes generated.");
    }
}

/// <summary>
/// Represents a payment request containing all necessary data for QR code generation.
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