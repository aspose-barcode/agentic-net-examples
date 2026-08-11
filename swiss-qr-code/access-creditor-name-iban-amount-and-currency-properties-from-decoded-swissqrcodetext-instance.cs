// Title: Extract Creditor Details from Swiss QR Code
// Description: Demonstrates decoding a Swiss QR barcode and retrieving creditor name, IBAN, amount, and currency.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode operations collection. It showcases how to use ComplexBarcodeGenerator to create a Swiss QR code, BarCodeReader to detect and read the barcode, and ComplexCodetextReader to decode the SwissQRCodetext. Developers working with financial QR codes (e.g., Swiss QR-bill) often need to generate, read, and extract payment data programmatically.
// Prompt: Access creditor name, IBAN, amount, and currency properties from the decoded SwissQRCodetext instance.
// Tags: swissqr, barcode, decoding, aspose.barcode, complexbarcode, qr, financial

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Swiss QR barcode, decodes it, and extracts creditor information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample Swiss QR barcode, reads it, decodes the text, and prints creditor details.
    /// </summary>
    static void Main()
    {
        // Generate a sample Swiss QR barcode image
        string imagePath = "SwissQR.png";
        GenerateSwissQRImage(imagePath);

        // Verify the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read the QR code from the image using a barcode reader that supports all types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();

            // Ensure at least one barcode was detected
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            // Assume the first result is the Swiss QR code and obtain its raw text
            string codeText = results[0].CodeText;
            if (string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine("Detected barcode has empty codetext.");
                return;
            }

            // Decode the Swiss QR codetext into a strongly‑typed object
            SwissQRCodetext swissQr = ComplexCodetextReader.TryDecodeSwissQR(codeText);
            if (swissQr == null)
            {
                Console.WriteLine("Failed to decode Swiss QR codetext.");
                return;
            }

            // Access required properties from the decoded object
            string creditorName = swissQr.Bill.Creditor.Name;
            string iban = swissQr.Bill.Account;
            decimal amount = swissQr.Bill.Amount;
            string currency = swissQr.Bill.Currency;

            // Output the extracted information
            Console.WriteLine($"Creditor Name: {creditorName}");
            Console.WriteLine($"IBAN: {iban}");
            Console.WriteLine($"Amount: {amount}");
            Console.WriteLine($"Currency: {currency}");
        }
    }

    // Generates a Swiss QR barcode image with known data
    static void GenerateSwissQRImage(string filePath)
    {
        // Create and populate SwissQRCodetext with sample payment details
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate and save the barcode image using the complex barcode generator
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Save(filePath);
        }
    }
}