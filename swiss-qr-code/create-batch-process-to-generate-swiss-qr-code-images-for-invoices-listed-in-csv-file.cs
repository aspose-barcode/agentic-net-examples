using System;
using System.IO;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define input CSV and output folder
        string inputCsv = "invoices.csv";
        string outputFolder = "output";

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Seed a sample CSV if it does not exist
        if (!File.Exists(inputCsv))
        {
            string[] sampleLines = new[]
            {
                "InvoiceNumber,Amount,Reference",
                "1001,199.95,RF1234567890",
                "1002,250.00,RF0987654321",
                "1003,75.50,RF1122334455",
                "1004,120.00,RF5566778899",
                "1005,99.99,RF6677889900"
            };
            File.WriteAllLines(inputCsv, sampleLines);
        }

        // Read all lines from the CSV
        string[] lines = File.ReadAllLines(inputCsv);
        if (lines.Length <= 1)
        {
            Console.WriteLine("No invoice data found in the CSV file.");
            return;
        }

        // Process each invoice (skip header)
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] parts = line.Split(',');
            if (parts.Length < 3)
                continue; // Invalid line, skip

            string invoiceNumber = parts[0].Trim();
            if (!decimal.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
                continue; // Invalid amount, skip

            string reference = parts[2].Trim();

            // Create Swiss QR code text and populate bill data
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Account = "CH9300762011623852957"; // known valid IBAN
            swissQr.Bill.Amount = amount;
            swissQr.Bill.Currency = "CHF";
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQr.Bill.Reference = reference;
            swissQr.Bill.BillInformation = $"Invoice {invoiceNumber}";
            swissQr.Bill.Creditor = new Address();
            swissQr.Bill.Creditor.CountryCode = "CH";

            // Generate and save the QR code image
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                string outputPath = Path.Combine(outputFolder, $"Invoice_{invoiceNumber}.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine("Swiss QR code generation completed.");
    }
}