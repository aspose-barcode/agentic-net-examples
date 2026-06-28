using System;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creating, encoding, and decoding a Swiss QR Bill using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a SwissQRCodetext instance and populate required fields
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";                 // Creditor's name
        swissQr.Bill.Creditor.CountryCode = "CH";               // ISO country code (Switzerland)
        swissQr.Bill.Account = "CH9300762011623852957";          // IBAN account number
        swissQr.Bill.Amount = 199.95m;                           // Invoice amount
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // QR bill version

        // ------------------------------------------------------------
        // 2. Generate the raw codetext string from the populated object
        // ------------------------------------------------------------
        string rawText = swissQr.GetConstructedCodetext();

        // ------------------------------------------------------------
        // 3. Decode the raw codetext back into a SwissQRCodetext object
        // ------------------------------------------------------------
        SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(rawText);

        // ------------------------------------------------------------
        // 4. Output the decoded information or an error message
        // ------------------------------------------------------------
        if (decoded != null)
        {
            Console.WriteLine("Decoded Swiss QR Bill:");
            Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
            Console.WriteLine($"Country Code: {decoded.Bill.Creditor.CountryCode}");
            Console.WriteLine($"Account: {decoded.Bill.Account}");
            Console.WriteLine($"Amount: {decoded.Bill.Amount}");
            Console.WriteLine($"Version: {decoded.Bill.Version}");
        }
        else
        {
            Console.WriteLine("Failed to decode Swiss QR codetext.");
        }
    }
}