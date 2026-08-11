// Title: Generate Swiss QR Code images for invoices from CSV
// Description: Demonstrates batch processing of invoice data stored in a CSV file to create Swiss QR Code barcodes saved as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Bill. It shows how to use the ComplexBarcodeGenerator with SwissQRCodetext, configure bill details, and save the resulting QR code images. Developers working with financial documents, invoicing, or payment QR codes can use this pattern to automate barcode creation for multiple records.
// Prompt: Create a batch process to generate Swiss QR Code images for invoices listed in a CSV file.
// Tags: swiss qr code, barcode generation, csv processing, aspnet.barcode, complexbarcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of Swiss QR Code barcodes for invoices read from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that reads invoice data, builds Swiss QR code content, and saves PNG images.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define input CSV and output folder paths
        string csvPath = "invoices.csv";
        string outputFolder = "output";

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If CSV does not exist, create a sample file with a few invoices
        if (!File.Exists(csvPath))
        {
            string[] sampleLines =
            {
                "InvoiceNumber,CreditorName,Amount,Reference,BillInformation",
                "1001,John Doe,199.95,RF1234567890,Invoice 1001",
                "1002,Acme Corp,250.00,RF0987654321,Invoice 1002",
                "1003,Jane Smith,75.50,RF1122334455,Invoice 1003"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read all lines from CSV (skip header)
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no invoice data.");
            return;
        }

        // Process each invoice line
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines

            // Split CSV line into fields
            string[] parts = line.Split(',');
            if (parts.Length < 5)
            {
                Console.WriteLine($"Skipping malformed line {i + 1}: {line}");
                continue;
            }

            // Extract and trim individual fields
            string invoiceNumber = parts[0].Trim();
            string creditorName = parts[1].Trim();
            string amountStr = parts[2].Trim();
            string reference = parts[3].Trim();
            string billInfo = parts[4].Trim();

            // Parse amount value
            if (!decimal.TryParse(amountStr, out decimal amount))
            {
                Console.WriteLine($"Invalid amount on line {i + 1}: {amountStr}");
                continue;
            }

            // Build Swiss QR code data
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = creditorName;
            swissQr.Bill.Creditor.CountryCode = "CH";
            // Use a known valid IBAN for all invoices
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = amount;
            swissQr.Bill.Currency = "CHF";
            swissQr.Bill.Reference = reference;
            swissQr.Bill.BillInformation = billInfo;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Generate and save the QR code image
            string outputPath = Path.Combine(outputFolder, $"Invoice_{invoiceNumber}.png");
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR code for invoice {invoiceNumber} at {outputPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}