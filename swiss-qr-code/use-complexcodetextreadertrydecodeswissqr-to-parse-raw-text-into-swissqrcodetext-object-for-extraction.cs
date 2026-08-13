// Title: Decode SwissQR Codetext Using ComplexCodetextReader
// Description: Demonstrates creating a SwissQR bill codetext, obtaining its raw string, and decoding it back into a SwissQRCodetext object using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode ComplexBarcode category, showcasing how to work with SwissQR bill codetexts. It highlights key API classes such as SwissQRCodetext, ComplexCodetextReader, and related bill components. Developers often need to generate, serialize, and later parse SwissQR data for payment processing or QR code generation, making this pattern essential for financial and invoicing applications.
// Prompt: Use ComplexCodetextReader.TryDecodeSwissQR to parse raw text into a SwissQRCodetext object for extraction.
// Tags: swissqr, decoding, codetext, complexcodetextreader, swissqrcodetext

using System;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a SwissQR bill codetext, encodes it to a raw string,
/// and then decodes it back into a <see cref="SwissQRCodetext"/> object using
/// <see cref="ComplexCodetextReader.TryDecodeSwissQR"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs creation, encoding, and decoding of SwissQR codetext.
    /// </summary>
    static void Main()
    {
        // Initialize a SwissQR codetext object and set mandatory bill fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the raw codetext string that would be embedded in the QR code
        string rawCodetext = swissQr.GetConstructedCodetext();

        // Attempt to decode the raw codetext back into a SwissQRCodetext instance
        SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(rawCodetext);

        // Output the decoding result
        if (decoded != null)
        {
            Console.WriteLine("Decoded SwissQR Bill:");
            Console.WriteLine($"Account: {decoded.Bill.Account}");
            Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
            Console.WriteLine($"Creditor Country: {decoded.Bill.Creditor.CountryCode}");
            Console.WriteLine($"Amount: {decoded.Bill.Amount}");
            Console.WriteLine($"Version: {decoded.Bill.Version}");
        }
        else
        {
            Console.WriteLine("Failed to decode SwissQR codetext.");
        }
    }
}