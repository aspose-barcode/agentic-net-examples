using System;
using Aspose.BarCode.ComplexBarcode;

namespace SwissQRDecodeExample
{
    class Program
    {
        static void Main()
        {
            // Create a SwissQR codetext with mandatory fields
            var original = new SwissQRCodetext();
            original.Bill.Creditor.Name = "John Doe";
            original.Bill.Creditor.CountryCode = "CH";
            original.Bill.Account = "CH9300762011623852957";
            original.Bill.Amount = 199.95m;
            original.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Get the raw codetext string
            string rawCodetext = original.GetConstructedCodetext();

            // Decode the raw codetext back into a SwissQRCodetext object
            SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(rawCodetext);

            if (decoded != null)
            {
                Console.WriteLine("Decoded Swiss QR Bill:");
                Console.WriteLine($"Creditor Name: {decoded.Bill.Creditor.Name}");
                Console.WriteLine($"Creditor Country: {decoded.Bill.Creditor.CountryCode}");
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
}