// Title: Decode Swiss QR Code Text Using ComplexCodetextReader
// Description: Demonstrates how to parse raw Swiss QR code text (SPC format) into a SwissQRCodetext object and extract billing details.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode decoding category, focusing on Swiss QR (QR‑IBAN) processing. It showcases the ComplexCodetextReader and SwissQRCodetext classes, which developers use to interpret QR‑IBAN payment strings, retrieve creditor and payment information, and integrate QR‑based payment data into financial applications. Typical use cases include validating QR‑IBAN data, generating payment receipts, and automating invoice processing.
// Prompt: Use ComplexCodetextReader.TryDecodeSwissQR to parse raw text into a SwissQRCodetext object for extraction.
// Tags: swissqr, barcode, decoding, complexcodetextreader, aspose.barcode

using System;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that decodes a raw Swiss QR code string into a <see cref="SwissQRCodetext"/> object
/// and prints selected billing information to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Parses a sample SPC‑formatted string, extracts the <see cref="SwissQRCodetext"/>
    /// and displays key fields such as creditor name, country code, IBAN, amount and version.
    /// </summary>
    static void Main()
    {
        // Sample encoded Swiss QR codetext in SPC format (lines separated by LF)
        string rawCodetext = "SPC\n0200\n1\nCH9300762011623852957\nJohn Doe\nCH\n1000\nCHF\nInvoice 123\n";

        // Attempt to decode the raw text into a SwissQRCodetext object using the ComplexCodetextReader API
        SwissQRCodetext swiss = ComplexCodetextReader.TryDecodeSwissQR(rawCodetext);

        // If decoding fails, inform the user and exit
        if (swiss == null)
        {
            Console.WriteLine("Failed to decode Swiss QR codetext.");
            return;
        }

        // Retrieve the Bill component which holds payment details
        var bill = swiss.Bill;

        // Output selected bill information to the console
        Console.WriteLine($"Creditor Name: {bill.Creditor.Name}");
        Console.WriteLine($"Creditor Country Code: {bill.Creditor.CountryCode}");
        Console.WriteLine($"Account (IBAN): {bill.Account}");
        Console.WriteLine($"Amount: {bill.Amount}");
        Console.WriteLine($"Version: {bill.Version}");
    }
}