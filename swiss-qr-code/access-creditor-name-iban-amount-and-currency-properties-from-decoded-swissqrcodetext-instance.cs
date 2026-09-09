// Title: Decode Swiss QR Code and Access Creditor Details
// Description: Generates a Swiss QR Code bill, decodes it, and extracts creditor name, IBAN, amount, and currency.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition of Swiss QR Bill codes. It uses ComplexBarcodeGenerator to create a QR code, BarCodeReader with DecodeType.QR to read it, and ComplexCodetextReader to parse the SwissQRCodetext. Developers working with payment QR codes can learn how to encode bill data, decode it, and retrieve key financial fields such as creditor information, account (IBAN), amount, and currency.
// Prompt: Access creditor name, IBAN, amount, and currency properties from the decoded SwissQRCodetext instance.
// Tags: swissqr, barcode, generation, recognition, aspose.barcode, qr, bill, creditor, iban, amount, currency

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creating, decoding, and extracting data from a Swiss QR Code bill using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a Swiss QR Code, decodes it, and prints creditor details.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters (e.g., umlauts) are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Create a temporary folder for the demo files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQRDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "SwissQRBill.png");

        // Build a sample Swiss QR Code payload.
        SwissQRCodetext swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.25m;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the QR code image using the complex barcode generator.
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate the Swiss QR Code image.");
            return;
        }

        // Read and decode the QR code from the generated image.
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to parse the decoded text as a Swiss QR Code payload.
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    continue;
                }

                // Output the requested creditor and payment details.
                Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
                Console.WriteLine($"IBAN: {decoded.Bill.Account}");
                Console.WriteLine($"Amount: {decoded.Bill.Amount}");
                Console.WriteLine($"Currency: {decoded.Bill.Currency}");
            }
        }

        // Clean up temporary files created for the demo.
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup not critical for demo.
        }
    }
}