using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates reading invoice data from a CSV file and generating Swiss QR Code images for each invoice.
/// </summary>
class Program
{
    /// <summary>
    /// Simple invoice data model.
    /// </summary>
    class Invoice
    {
        public string InvoiceNumber { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string CreditorName { get; set; }
    }

    /// <summary>
    /// Application entry point. Reads invoices, creates output folder, and generates QR code images.
    /// </summary>
    static void Main()
    {
        // Input CSV file containing invoice data
        string inputCsv = "invoices.csv";

        // Folder where generated QR code images will be saved
        string outputFolder = "SwissQRImages";

        // Ensure the output directory exists; create it if missing
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // If the CSV file does not exist, create a small sample file for demonstration purposes
        if (!File.Exists(inputCsv))
        {
            var sampleLines = new[]
            {
                "InvoiceNumber,Account,Amount,CreditorName",
                "INV001,CH9300762011623852957,199.95,John Doe",
                "INV002,CH9300762011623852958,250.00,Acme Corp",
                "INV003,CH9300762011623852959,75.50,Global Ltd"
            };
            File.WriteAllLines(inputCsv, sampleLines);
            Console.WriteLine($"Sample CSV created at '{inputCsv}'.");
        }

        // Read invoices from the CSV file into a list
        List<Invoice> invoices = new List<Invoice>();
        try
        {
            using (var reader = new StreamReader(inputCsv))
            {
                bool isHeader = true; // Skip the first line (header)
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue; // Ignore empty lines

                    if (isHeader)
                    {
                        isHeader = false;
                        continue; // Skip header line
                    }

                    // Split CSV line into fields
                    string[] parts = line.Split(',');
                    if (parts.Length < 4)
                        continue; // Skip malformed lines

                    // Create an Invoice object from the parsed fields
                    invoices.Add(new Invoice
                    {
                        InvoiceNumber = parts[0].Trim(),
                        Account = parts[1].Trim(),
                        Amount = decimal.Parse(parts[2].Trim()),
                        CreditorName = parts[3].Trim()
                    });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV: {ex.Message}");
            return;
        }

        // Process each invoice and generate a Swiss QR Code image
        foreach (var inv in invoices)
        {
            try
            {
                // Build Swiss QR code data structure
                var swissQr = new SwissQRCodetext();
                swissQr.Bill.Creditor.Name = inv.CreditorName;
                swissQr.Bill.Creditor.CountryCode = "CH";
                swissQr.Bill.Account = inv.Account;
                swissQr.Bill.Amount = inv.Amount;
                swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

                // Determine output file path for the QR code image
                string outputPath = Path.Combine(outputFolder, $"{inv.InvoiceNumber}.png");

                // Generate and save the QR code image
                using (var generator = new ComplexBarcodeGenerator(swissQr))
                {
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated QR for invoice {inv.InvoiceNumber} -> {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate QR for invoice {inv.InvoiceNumber}: {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}