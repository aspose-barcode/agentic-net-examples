// Title: Generate and Decode Swiss QR Code Barcode with Aspose.BarCode
// Description: Demonstrates creating a Swiss QR Code for a payment bill, saving it as an image, then reading and extracting creditor details, IBAN, amount, and currency.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and QR code decoding category. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and BarCodeReader to produce a Swiss QR Bill (QR‑IBAN) image and subsequently decode it. Typical scenarios include automating payment QR code creation for invoices and extracting payment data from scanned QR codes. Developers often need these APIs to integrate Swiss QR payment processing into desktop or web applications.
// Prompt: Access creditor name, IBAN, amount, and currency properties from the decoded SwissQRCodetext instance.
// Tags: swissqr,qr code,barcode generation,barcode decoding,aspose.barcode,complexbarcode,swissqrcodetext,payment

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Swiss QR Code image, decodes it, and prints payment details.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Swiss QR Code, reads it back, and outputs creditor information.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters (e.g., umlauts) are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Create a unique temporary directory to store the generated QR image.
        string tempDir = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "SwissQRBill.png");

        // ------------------------------------------------------------
        // Build the Swiss QR Code data model (payment bill information)
        // ------------------------------------------------------------
        var swissQRCode = new SwissQRCodetext();
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

        // ------------------------------------------------------------
        // Generate the barcode image from the Swiss QR Code data
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Set the module size (pixel dimension) for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Read and decode the generated QR Code image
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to decode the QR text into a strongly‑typed SwissQRCodetext object.
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                    continue; // Skip if decoding failed.

                // Output the requested payment details.
                Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
                Console.WriteLine($"IBAN: {decoded.Bill.Account}");
                Console.WriteLine($"Amount: {decoded.Bill.Amount}");
                Console.WriteLine($"Currency: {decoded.Bill.Currency}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program exit.
        }
    }
}