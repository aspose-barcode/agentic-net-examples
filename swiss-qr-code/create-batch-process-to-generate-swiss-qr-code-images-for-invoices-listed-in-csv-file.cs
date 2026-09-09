// Title: Batch generation of Swiss QR Code images from invoice CSV
// Description: Demonstrates how to read invoice data from a CSV file and create Swiss QR Code barcodes for each invoice, saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Codes. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related address classes to build QR bill data, a common requirement for Swiss payment processing systems. Developers often need to automate QR code creation for large numbers of invoices, making this pattern useful for batch processing scenarios.
// Prompt: Create a batch process to generate Swiss QR Code images for invoices listed in a CSV file.
// Tags: swiss qr code, barcode generation, csv, batch processing, aspose.barcode, complexbarcodegenerator, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Provides a console application that reads invoice information from a CSV file
/// and generates a Swiss QR Code image for each invoice using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Handles CSV input, creates Swiss QR Code data,
    /// and saves each barcode as a PNG file in an output directory.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument may be a path to a CSV file.</param>
    static void Main(string[] args)
    {
        // Determine CSV file path: use argument if valid, otherwise create a temporary sample CSV.
        string csvPath;
        if (args.Length > 0 && File.Exists(args[0]))
        {
            csvPath = args[0];
        }
        else
        {
            // Create a temporary folder for sample data.
            string tempFolder = Path.Combine(Path.GetTempPath(), "SwissQRBatch_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);
            csvPath = Path.Combine(tempFolder, "invoices.csv");
            CreateSampleCsv(csvPath);
            Console.WriteLine($"Sample CSV created at: {csvPath}");
        }

        // Prepare output folder for generated QR code images.
        string outputFolder = Path.Combine(Path.GetDirectoryName(csvPath) ?? Directory.GetCurrentDirectory(), "SwissQRImages");
        Directory.CreateDirectory(outputFolder);

        // Read all lines from the CSV file.
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Console.WriteLine("CSV file contains no data.");
            return;
        }

        // Process each data line (skip header at index 0).
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
                continue; // Skip empty lines.

            string[] fields = line.Split(',');

            // Basic validation: ensure the line has the expected number of columns.
            if (fields.Length < 17)
            {
                Console.WriteLine($"Skipping line {i + 1}: insufficient columns.");
                continue;
            }

            // Map CSV columns to variables (order matches the sample CSV header).
            string invoiceNumber = fields[0].Trim();
            string account = fields[1].Trim();
            string amountStr = fields[2].Trim();
            string currency = fields[3].Trim();
            string reference = fields[4].Trim();

            // Creditor address fields.
            string credName = fields[5].Trim();
            string credStreet = fields[6].Trim();
            string credHouseNo = fields[7].Trim();
            string credPostalCode = fields[8].Trim();
            string credTown = fields[9].Trim();
            string credCountryCode = fields[10].Trim();

            // Debtor address fields.
            string debtName = fields[11].Trim();
            string debtStreet = fields[12].Trim();
            string debtHouseNo = fields[13].Trim();
            string debtPostalCode = fields[14].Trim();
            string debtTown = fields[15].Trim();
            string debtCountryCode = fields[16].Trim();

            // Parse amount; skip line if invalid.
            if (!decimal.TryParse(amountStr, out decimal amount))
            {
                Console.WriteLine($"Skipping line {i + 1}: invalid amount.");
                continue;
            }

            // Build Swiss QR Code codetext using Aspose.BarCode classes.
            SwissQRCodetext swissQRCode = new SwissQRCodetext
            {
                Bill =
                {
                    Version = SwissQRBill.QrBillStandardVersion.V2_0,
                    Account = account,
                    Amount = amount,
                    Currency = currency,
                    Reference = reference,
                    Creditor = new Address
                    {
                        Name = credName,
                        Street = credStreet,
                        HouseNo = credHouseNo,
                        PostalCode = credPostalCode,
                        Town = credTown,
                        CountryCode = credCountryCode
                    },
                    Debtor = new Address
                    {
                        Name = debtName,
                        Street = debtStreet,
                        HouseNo = debtHouseNo,
                        PostalCode = debtPostalCode,
                        Town = debtTown,
                        CountryCode = debtCountryCode
                    }
                }
            };

            // Generate barcode image and save as PNG.
            string imagePath = Path.Combine(outputFolder, $"{invoiceNumber}_SwissQR.png");
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQRCode))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size.
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR for invoice {invoiceNumber} at {imagePath}");
        }

        Console.WriteLine("Batch processing completed.");
    }

    /// <summary>
    /// Creates a sample CSV file with a header and three example invoice records.
    /// </summary>
    /// <param name="path">Full file path where the CSV will be written.</param>
    private static void CreateSampleCsv(string path)
    {
        var lines = new List<string>
        {
            "InvoiceNumber,Account,Amount,Currency,Reference,CreditorName,CreditorStreet,CreditorHouseNo,CreditorPostalCode,CreditorTown,CreditorCountryCode,DebtorName,DebtorStreet,DebtorHouseNo,DebtorPostalCode,DebtorTown,DebtorCountryCode",
            "INV001,CH9300762011623852957,199.95,CHF,210000000003139471430009017,John Doe,Main Street,12,8000,Zürich,CH,Acme Corp,Second Street,5,3000,Bern,CH",
            "INV002,CH9300762011623852957,250.00,CHF,210000000003139471430009018,Jane Smith,High Road,8,4000,Basel,CH,Widget Ltd,Low Avenue,3,5000,Lausanne,CH",
            "INV003,CH9300762011623852957,75.50,CHF,210000000003139471430009019,Alpha AG,Market Plaza,1,6000,Genève,CH,Beta GmbH,Industrial Way,9,7000,St. Gallen,CH"
        };
        File.WriteAllLines(path, lines);
    }
}