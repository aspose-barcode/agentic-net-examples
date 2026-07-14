// Title: Generate Swiss QR Code Images from CSV Invoices
// Description: Demonstrates batch creation of Swiss QR Code barcodes for invoice data read from a CSV file and saves them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Bill. It showcases using the SwissQRCodetext class and ComplexBarcodeGenerator to encode payment information into QR codes, a common requirement for financial applications and invoicing systems. Developers often need to automate QR code creation for multiple records, handling CSV input and image output.
// Prompt: Create a batch process to generate Swiss QR Code images for invoices listed in a CSV file.
// Tags: swiss qr code, batch processing, png, aspose.barcode, complexbarcodegenerator, csv

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Batch processor that reads invoice data from a CSV file and generates Swiss QR Code images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Reads invoices, creates QR codes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Path to the input CSV file containing invoice records
        string csvPath = "invoices.csv";

        // Directory where generated QR code images will be stored
        string outputFolder = "output";

        // Ensure the output directory exists; create it if necessary
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the CSV file is missing, create a sample file with example invoices
        if (!File.Exists(csvPath))
        {
            var sampleLines = new List<string>
            {
                "Account,CreditorName,CountryCode,Amount,BillInformation",
                "CH9300762011623852957,John Doe,CH,199.95,Invoice 001",
                "CH9300762011623852957,Acme Corp,CH,350.00,Invoice 002",
                "CH9300762011623852957,Global Ltd,CH,1200.50,Invoice 003"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read all lines from the CSV file
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("No invoice data found in the CSV file.");
            return;
        }

        // Process each invoice line, skipping the header row.
        // Limit processing to a maximum of 5 items for safety in this example.
        int maxItems = Math.Min(lines.Length - 1, 5);
        for (int i = 1; i <= maxItems; i++)
        {
            string line = lines[i];

            // Simple CSV split (assumes no commas inside fields)
            string[] parts = line.Split(',');

            // Validate that the line contains all required columns
            if (parts.Length < 5)
            {
                Console.WriteLine($"Skipping line {i + 1}: insufficient columns.");
                continue;
            }

            // Map CSV columns to local variables
            string account = parts[0].Trim();
            string creditorName = parts[1].Trim();
            string countryCode = parts[2].Trim();

            // Parse the amount; skip the line if parsing fails
            if (!decimal.TryParse(parts[3].Trim(), out decimal amount))
            {
                Console.WriteLine($"Skipping line {i + 1}: invalid amount.");
                continue;
            }

            string billInfo = parts[4].Trim();

            // Build the Swiss QR bill data structure
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Account = account;
            swissQr.Bill.Creditor.Name = creditorName;
            swissQr.Bill.Creditor.CountryCode = countryCode;
            swissQr.Bill.Amount = amount;
            swissQr.Bill.BillInformation = billInfo;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Define the output file path for the generated QR code image
            string outputPath = Path.Combine(outputFolder, $"invoice_{i}.png");

            // Generate and save the QR code image using ComplexBarcodeGenerator
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Set barcode and background colors (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the QR code as a PNG file
                generator.Save(outputPath);
            }

            Console.WriteLine($"Generated QR code for invoice {i} at: {outputPath}");
        }

        Console.WriteLine("Batch processing completed.");
    }
}