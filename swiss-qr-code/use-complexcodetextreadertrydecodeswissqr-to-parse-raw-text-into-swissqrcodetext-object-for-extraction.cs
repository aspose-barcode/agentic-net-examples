using System;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Create a SwissQR codetext and populate required bill fields
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Account = "CH9300762011623852957"; // valid IBAN
        swissQr.Bill.Amount = 199.95m; // decimal amount with 'm' suffix
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // required version
        swissQr.Bill.Creditor.CountryCode = "CH"; // country code
        swissQr.Bill.Creditor.Name = "John Doe"; // mandatory creditor name

        // Construct the raw codetext string that would be embedded in the barcode
        string rawCodetext = swissQr.GetConstructedCodetext();

        // Decode the raw codetext back into a SwissQRCodetext object
        SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(rawCodetext);

        if (decoded == null)
        {
            Console.WriteLine("Decoding failed: result is null.");
            return;
        }

        // Extract and display some fields from the decoded object
        Console.WriteLine($"Account: {decoded.Bill.Account}");
        Console.WriteLine($"Amount: {decoded.Bill.Amount}");
        Console.WriteLine($"Version: {decoded.Bill.Version}");
        Console.WriteLine($"Creditor Country Code: {decoded.Bill.Creditor.CountryCode}");
        Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
    }
}