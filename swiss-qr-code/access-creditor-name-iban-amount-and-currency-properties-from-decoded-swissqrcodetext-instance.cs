using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Path to the SwissQR barcode image
        string imagePath = "SwissQR.png";

        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Read the barcode from the image using all supported types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();

            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Take the first detected barcode
            string encodedText = results[0].CodeText;

            // Decode the SwissQR codetext
            SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(encodedText);

            if (decoded == null)
            {
                Console.WriteLine("Failed to decode SwissQR codetext.");
                return;
            }

            // Access required properties
            string creditorName = decoded.Bill.Creditor?.Name ?? "N/A";
            string iban = decoded.Bill.Account ?? "N/A";
            decimal amount = decoded.Bill.Amount;
            string currency = decoded.Bill.Currency ?? "N/A";

            // Output the values
            Console.WriteLine($"Creditor Name: {creditorName}");
            Console.WriteLine($"IBAN: {iban}");
            Console.WriteLine($"Amount: {amount}");
            Console.WriteLine($"Currency: {currency}");
        }
    }
}