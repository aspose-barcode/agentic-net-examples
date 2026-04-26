using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Prepare Swiss QR code data
        var swissCodetext = new SwissQRCodetext();
        swissCodetext.Bill.Account = "CH9300762011623852957"; // valid IBAN
        swissCodetext.Bill.Amount = 199.95m;
        swissCodetext.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Creditor address (required fields)
        swissCodetext.Bill.Creditor.Name = "Creditor Name";
        swissCodetext.Bill.Creditor.Street = "Creditor Street 1";
        swissCodetext.Bill.Creditor.PostalCode = "8000";
        swissCodetext.Bill.Creditor.Town = "Zurich";
        swissCodetext.Bill.Creditor.CountryCode = "CH";

        // Optional debtor address (can be left empty)
        swissCodetext.Bill.Debtor.Name = "Debtor Name";
        swissCodetext.Bill.Debtor.Street = "Debtor Street 2";
        swissCodetext.Bill.Debtor.PostalCode = "3000";
        swissCodetext.Bill.Debtor.Town = "Bern";
        swissCodetext.Bill.Debtor.CountryCode = "CH";

        // Reference (optional but included for completeness)
        swissCodetext.Bill.Reference = "RF18539007547034";

        // Generate and save the Swiss QR barcode image
        const string outputFile = "SwissQR.png";
        using (var generator = new ComplexBarcodeGenerator(swissCodetext))
        {
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Verify that the file was created
        if (!File.Exists(outputFile))
        {
            Console.WriteLine("Failed to create the Swiss QR image.");
            return;
        }

        // Decode the barcode from the saved image
        using (var reader = new BarCodeReader(outputFile, DecodeType.AllSupportedTypes))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected in the image.");
                return;
            }

            foreach (var result in results)
            {
                var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode Swiss QR codetext.");
                    continue;
                }

                // Validate required fields according to Swiss Implementation Guidelines
                bool isValid = true;
                var bill = decoded.Bill;

                if (string.IsNullOrWhiteSpace(bill.Account) || !bill.Account.StartsWith("CH"))
                {
                    Console.WriteLine("Invalid or missing IBAN.");
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(bill.Creditor.Name) ||
                    string.IsNullOrWhiteSpace(bill.Creditor.Street) ||
                    string.IsNullOrWhiteSpace(bill.Creditor.PostalCode) ||
                    string.IsNullOrWhiteSpace(bill.Creditor.Town) ||
                    string.IsNullOrWhiteSpace(bill.Creditor.CountryCode))
                {
                    Console.WriteLine("Missing required creditor address fields.");
                    isValid = false;
                }

                if (bill.Version == 0)
                {
                    Console.WriteLine("Bill version is not set.");
                    isValid = false;
                }

                // Additional optional checks can be added here (e.g., amount > 0)

                Console.WriteLine(isValid
                    ? "Swiss QR Code complies with the implementation guidelines."
                    : "Swiss QR Code does NOT comply with the implementation guidelines.");
            }
        }
    }
}