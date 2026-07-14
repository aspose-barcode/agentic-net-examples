// Title: Decode Swiss QR Bill and extract creditor details
// Description: Demonstrates generating a Swiss QR code, decoding it, and accessing creditor name, IBAN, amount, and currency from the decoded SwissQRCodetext.
// Category-Description: This example belongs to the Aspose.BarCode Swiss QR Bill processing category. It showcases the use of BarcodeGenerator, BarCodeReader, and ComplexCodetextReader to create, read, and parse Swiss QR codes. Developers working with financial QR codes can learn how to encode bill data, generate PNG images, and retrieve structured payment information programmatically.
// Prompt: Access creditor name, IBAN, amount, and currency properties from the decoded SwissQRCodetext instance.
// Tags: swissqr, qr, barcode generation, barcode recognition, png, aspose.barcode, financial, payment

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Swiss QR code, decodes it, and extracts key payment fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Swiss QR barcode, reads it back, and prints creditor details.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Build the Swiss QR bill data model with required fields.
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // ------------------------------------------------------------
        // 2. Construct the encoded text that will be embedded in the QR code.
        // ------------------------------------------------------------
        string encodedText = swissQr.GetConstructedCodetext();

        // ------------------------------------------------------------
        // 3. Generate a QR barcode image (PNG) containing the Swiss QR text.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, encodedText))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // ------------------------------------------------------------
                // 4. Read and decode the barcode image from the memory stream.
                // ------------------------------------------------------------
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();

                    // ------------------------------------------------------------
                    // 5. Iterate over decoded results and extract Swiss QR bill fields.
                    // ------------------------------------------------------------
                    foreach (var result in results)
                    {
                        // Attempt to parse the raw code text as a Swiss QR bill.
                        var decodedSwiss = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                        if (decodedSwiss != null)
                        {
                            // Access required properties from the decoded object.
                            string creditorName = decodedSwiss.Bill.Creditor.Name;
                            string iban = decodedSwiss.Bill.Account;
                            decimal amount = decodedSwiss.Bill.Amount;
                            string currency = decodedSwiss.Bill.Currency;

                            // Output the extracted values.
                            Console.WriteLine($"Creditor Name: {creditorName}");
                            Console.WriteLine($"IBAN: {iban}");
                            Console.WriteLine($"Amount: {amount}");
                            Console.WriteLine($"Currency: {currency}");
                        }
                        else
                        {
                            Console.WriteLine("Failed to decode Swiss QR codetext.");
                        }
                    }
                }
            }
        }
    }
}