using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;

/// <summary>
/// Demonstrates creation, encoding, and decoding of a Swiss QR bill using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Swiss QR code, saves it to a memory stream, then reads and decodes it.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Build the Swiss QR bill codetext with required fields.
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";               // Creditor's name
        swissQr.Bill.Creditor.CountryCode = "CH";             // Creditor's country (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";       // IBAN account number
        swissQr.Bill.Amount = 199.95m;                         // Payment amount
        swissQr.Bill.Currency = "CHF";                        // Currency (mandatory)
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // ------------------------------------------------------------
        // 2. Encode the codetext into a QR barcode image stored in memory.
        // ------------------------------------------------------------
        using (var ms = new MemoryStream())
        {
            // Generate the QR code and write it as PNG into the memory stream.
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading.
            ms.Position = 0;

            // ------------------------------------------------------------
            // 3. Decode the QR barcode from the memory stream.
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Iterate over all detected barcodes (should be only one).
                foreach (var result in reader.ReadBarCodes())
                {
                    // Attempt to parse the complex Swiss QR codetext.
                    var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                    if (decoded != null)
                    {
                        // Output decoded bill details to the console.
                        Console.WriteLine("Creditor Name: " + decoded.Bill.Creditor.Name);
                        Console.WriteLine("IBAN: " + decoded.Bill.Account);
                        Console.WriteLine("Amount: " + decoded.Bill.Amount);
                        Console.WriteLine("Currency: " + decoded.Bill.Currency);
                    }
                    else
                    {
                        // Inform the user if decoding failed.
                        Console.WriteLine("Failed to decode SwissQR codetext.");
                    }
                }
            }
        }
    }
}