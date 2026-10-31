// Title: Batch Generation of Swiss QR Code Images from CSV Invoices
// Description: Demonstrates how to read invoice data from a CSV file and generate Swiss QR Code barcodes as PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Code. It illustrates the use of ComplexBarcodeGenerator, SwissQRCodetext, and related address classes to create QR codes for payment slips. Developers handling financial documents, invoices, or batch barcode creation can adopt this pattern to automate image generation.
// Prompt: Create a batch process to generate Swiss QR Code images for invoices listed in a CSV file.
// Tags: swiss qr code, batch processing, png, complexbarcodegenerator, swissqrcodetext, address

using System;
using System.IO;
using System.Globalization;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates Swiss QR Code barcode images for a set of invoices read from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads invoice data, builds Swiss QR Code text, and saves PNG images.
    /// </summary>
    /// <param name="args">Optional command‑line argument specifying the CSV file path.</param>
    static void Main(string[] args)
    {
        // Determine CSV path (use argument if supplied, otherwise create a temporary sample file)
        string csvPath = args.Length > 0 ? args[0] : Path.Combine(Path.GetTempPath(), "invoices.csv");

        // If the CSV does not exist, create a small sample with up to 5 records
        if (!File.Exists(csvPath))
        {
            string[] sampleLines = new[]
            {
                "CreditorName,CreditorStreet,CreditorHouseNo,CreditorPostalCode,CreditorTown,CreditorCountryCode,Account,Amount,Currency,Reference,DebtorName,DebtorStreet,DebtorHouseNo,DebtorPostalCode,DebtorTown,DebtorCountryCode",
                "John Doe,Main Street,12,8000,Zurich,CH,CH9300762011623852957,199.95,CHF,210000000003139471430009017,Acme Corp,Second Street,5,3000,Bern,CH",
                "Alice Smith,First Ave,8,8200,Zurich,CH,CH9300762011623852957,250.00,CHF,210000000003139471430009018,Globex Ltd,Third Blvd,3,4000,Geneva,CH",
                "Bob Lee,Fourth Rd,20,8300,Zurich,CH,CH9300762011623852957,75.50,CHF,210000000003139471430009019,Initech,Five St,7,5000,Basel,CH"
            };
            File.WriteAllLines(csvPath, sampleLines);
        }

        // Read all lines from the CSV file
        string[] allLines = File.ReadAllLines(csvPath);
        if (allLines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data rows.");
            return;
        }

        // Create a unique output directory for the generated PNG files
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        int processedCount = 0;

        // Process each data row (skip header) – limit to 5 records for safety
        for (int i = 1; i < allLines.Length && processedCount < 5; i++)
        {
            string line = allLines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // skip empty lines

            // Split CSV line into fields
            string[] fields = line.Split(',');
            if (fields.Length < 16)
                continue; // insufficient data, skip this row

            // Trim whitespace from each field
            for (int j = 0; j < fields.Length; j++)
                fields[j] = fields[j].Trim();

            // Map CSV columns to meaningful variables
            string creditorName = fields[0];
            string creditorStreet = fields[1];
            string creditorHouseNo = fields[2];
            string creditorPostalCode = fields[3];
            string creditorTown = fields[4];
            string creditorCountry = fields[5];
            string account = fields[6];
            decimal amount = decimal.Parse(fields[7], CultureInfo.InvariantCulture);
            string currency = fields[8];
            string reference = fields[9];
            string debtorName = fields[10];
            string debtorStreet = fields[11];
            string debtorHouseNo = fields[12];
            string debtorPostalCode = fields[13];
            string debtorTown = fields[14];
            string debtorCountry = fields[15];

            // Build the Swiss QR Code codetext using the Aspose.BarCode model
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQr.Bill.Account = account;
            swissQr.Bill.Amount = amount;
            swissQr.Bill.Currency = currency;
            swissQr.Bill.Reference = reference;

            // Populate creditor address
            swissQr.Bill.Creditor = new Address
            {
                Name = creditorName,
                Street = creditorStreet,
                HouseNo = creditorHouseNo,
                PostalCode = creditorPostalCode,
                Town = creditorTown,
                CountryCode = creditorCountry
            };

            // Populate debtor address
            swissQr.Bill.Debtor = new Address
            {
                Name = debtorName,
                Street = debtorStreet,
                HouseNo = debtorHouseNo,
                PostalCode = debtorPostalCode,
                Town = debtorTown,
                CountryCode = debtorCountry
            };

            // Define output file path for the current invoice
            string outputPath = Path.Combine(outputDir, $"Invoice_{processedCount + 1}.png");

            // Generate the barcode image and save as PNG
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            processedCount++;
        }

        // Report the number of generated images and the output location
        Console.WriteLine($"Generated {processedCount} Swiss QR Code image(s) in: {outputDir}");
    }
}