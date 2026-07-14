// Title: Batch Generation of Swiss QR Code Images from CSV Invoices
// Description: Generates Swiss QR Code barcodes for each invoice listed in a CSV file and saves them as PNG images.
// Category-Description: This example demonstrates batch processing using Aspose.BarCode to create Swiss QR Code (QR‑Bill) images. It utilizes the ComplexBarcodeGenerator and SwissQRCodetext classes to encode creditor, account, amount, and reference data. Typical use cases include automating invoice QR‑Bill creation for Swiss payment standards, where developers need to read data sources (e.g., CSV) and produce barcode images in bulk.
// Prompt: Create a batch process to generate Swiss QR Code images for invoices listed in a CSV file.
// Tags: swiss qr code, barcode generation, csv processing, aspose.barcode, png output, batch processing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates batch generation of Swiss QR Code images from invoice data stored in a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads invoice records, builds Swiss QR codetext, and saves PNG barcode images.
    /// </summary>
    static void Main()
    {
        // Define the input CSV file path; create a sample file if it does not exist.
        string inputCsv = "invoices.csv";
        if (!File.Exists(inputCsv))
        {
            var sampleLines = new List<string>
            {
                "CreditorName,CountryCode,Account,Amount,Reference",
                "John Doe,CH,CH9300762011623852957,199.95,RF18539007547034",
                "Acme Corp,CH,CH5604835012345678009,2500.00,RF18000012345678",
                "Global Ltd,CH,CH3709000000304442225,123.45,RF19000098765432",
                "Tech Solutions,CH,CH6209000000001234567,999.99,RF20000123456789",
                "Finance AG,CH,CH9300762011623852957,75.00,RF21000111223344"
            };
            File.WriteAllLines(inputCsv, sampleLines);
        }

        // Ensure the output directory exists for the generated QR images.
        string outputDir = "SwissQRImages";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Read all lines from the CSV file.
        string[] allLines = File.ReadAllLines(inputCsv);
        if (allLines.Length <= 1)
        {
            Console.WriteLine("No invoice data found.");
            return;
        }

        // Iterate over each invoice record, skipping the header line.
        for (int i = 1; i < allLines.Length; i++)
        {
            string line = allLines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines.

            // Split the CSV line into its constituent fields.
            string[] parts = line.Split(',');
            if (parts.Length < 5)
            {
                Console.WriteLine($"Skipping malformed line {i + 1}.");
                continue;
            }

            // Trim and assign each field to a variable.
            string creditorName = parts[0].Trim();
            string countryCode = parts[1].Trim();
            string account = parts[2].Trim();
            string amountStr = parts[3].Trim();
            string reference = parts[4].Trim();

            // Parse the amount; if invalid, skip this record.
            if (!decimal.TryParse(amountStr, out decimal amount))
            {
                Console.WriteLine($"Invalid amount on line {i + 1}, skipping.");
                continue;
            }

            // Build the Swiss QR codetext using the parsed data.
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = creditorName;
            swissQr.Bill.Creditor.CountryCode = countryCode;
            swissQr.Bill.Account = account;
            swissQr.Bill.Amount = amount;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            if (!string.IsNullOrEmpty(reference))
                swissQr.Bill.Reference = reference; // Optional reference.

            // Define the output file name for this invoice's QR code.
            string outputFile = Path.Combine(outputDir, $"Invoice_{i}.png");

            // Generate the QR code image and save it as PNG.
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Example: set module size (X dimension) if desired.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Save(outputFile, BarCodeImageFormat.Png);
            }
        }

        Console.WriteLine("Swiss QR code generation completed.");
    }
}