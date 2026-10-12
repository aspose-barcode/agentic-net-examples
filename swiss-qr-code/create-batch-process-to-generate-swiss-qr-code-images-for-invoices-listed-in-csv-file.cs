// Title: Batch generation of Swiss QR Code barcodes for invoices from CSV
// Description: Demonstrates how to read invoice data from a CSV file and generate Swiss QR Code images for each invoice using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode creation with the SwissQRCodetext class. It showcases typical use cases such as batch processing, file I/O, and image output, which developers often need when automating invoice QR code generation for payment processing.
// Prompt: Create a batch process to generate Swiss QR Code images for invoices listed in a CSV file.
// Tags: swiss qr code, batch processing, image generation, png, aspose.barcode, complexbarcodegenerator, csv parsing

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch creation of Swiss QR Code barcodes from invoice data stored in a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads a CSV file, builds Swiss QR Code data for each invoice,
    /// and saves the generated barcode images as PNG files.
    /// </summary>
    static void Main()
    {
        // Prepare temporary directories for CSV input and PNG output
        string baseTemp = Path.Combine(Path.GetTempPath(), "SwissQRBatch_" + Guid.NewGuid().ToString("N"));
        string csvPath = Path.Combine(baseTemp, "invoices.csv");
        string outputDir = Path.Combine(baseTemp, "Output");
        Directory.CreateDirectory(baseTemp);
        Directory.CreateDirectory(outputDir);

        // Create a sample CSV file (header + three sample invoices)
        string[] csvLines = new[]
        {
            "InvoiceId,CreditorName,CreditorStreet,CreditorHouseNo,CreditorPostalCode,CreditorTown,CreditorCountryCode,Account,Amount,Currency,Reference,DebtorName,DebtorStreet,DebtorHouseNo,DebtorPostalCode,DebtorTown,DebtorCountryCode",
            "INV001,John Doe,Main Street,12,8000,Zurich,CH,CH9300762011623852957,199.95,CHF,210000000003139471430009017,Acme Corp,Second Street,5,3000,Bern,CH",
            "INV002,Jane Smith,High Road,8,8200,Zurich,CH,CH4431999123000889012,250.00,CHF,210000000003139471430009018,Globex,Third Avenue,3,4000,Geneva,CH",
            "INV003,Alpha Ltd,Market Plaza,1,8300,Zurich,CH,CH4431999123000889012,75.50,CHF,210000000003139471430009019,Delta Inc,Fourth Blvd,9,5000,Lausanne,CH"
        };
        File.WriteAllLines(csvPath, csvLines);

        // Read CSV lines into memory
        List<string> lines = new List<string>(File.ReadAllLines(csvPath));
        if (lines.Count <= 1)
        {
            Console.WriteLine("CSV file contains no data.");
            return;
        }

        // Parse header row to obtain column indices (simple approach)
        string[] headers = lines[0].Split(',');
        int idxInvoiceId = Array.IndexOf(headers, "InvoiceId");
        int idxCreditorName = Array.IndexOf(headers, "CreditorName");
        int idxCreditorStreet = Array.IndexOf(headers, "CreditorStreet");
        int idxCreditorHouseNo = Array.IndexOf(headers, "CreditorHouseNo");
        int idxCreditorPostalCode = Array.IndexOf(headers, "CreditorPostalCode");
        int idxCreditorTown = Array.IndexOf(headers, "CreditorTown");
        int idxCreditorCountryCode = Array.IndexOf(headers, "CreditorCountryCode");
        int idxAccount = Array.IndexOf(headers, "Account");
        int idxAmount = Array.IndexOf(headers, "Amount");
        int idxCurrency = Array.IndexOf(headers, "Currency");
        int idxReference = Array.IndexOf(headers, "Reference");
        int idxDebtorName = Array.IndexOf(headers, "DebtorName");
        int idxDebtorStreet = Array.IndexOf(headers, "DebtorStreet");
        int idxDebtorHouseNo = Array.IndexOf(headers, "DebtorHouseNo");
        int idxDebtorPostalCode = Array.IndexOf(headers, "DebtorPostalCode");
        int idxDebtorTown = Array.IndexOf(headers, "DebtorTown");
        int idxDebtorCountryCode = Array.IndexOf(headers, "DebtorCountryCode");

        // Process up to five invoices (safe batch size)
        int maxItems = Math.Min(5, lines.Count - 1);
        for (int i = 1; i <= maxItems; i++)
        {
            // Split current CSV line into fields
            string[] fields = lines[i].Split(',');

            // Extract and trim invoice data
            string invoiceId = fields[idxInvoiceId].Trim();
            string creditorName = fields[idxCreditorName].Trim();
            string creditorStreet = fields[idxCreditorStreet].Trim();
            string creditorHouseNo = fields[idxCreditorHouseNo].Trim();
            string creditorPostalCode = fields[idxCreditorPostalCode].Trim();
            string creditorTown = fields[idxCreditorTown].Trim();
            string creditorCountryCode = fields[idxCreditorCountryCode].Trim();
            string account = fields[idxAccount].Trim();
            decimal amount = decimal.Parse(fields[idxAmount].Trim());
            string currency = fields[idxCurrency].Trim();
            string reference = fields[idxReference].Trim();
            string debtorName = fields[idxDebtorName].Trim();
            string debtorStreet = fields[idxDebtorStreet].Trim();
            string debtorHouseNo = fields[idxDebtorHouseNo].Trim();
            string debtorPostalCode = fields[idxDebtorPostalCode].Trim();
            string debtorTown = fields[idxDebtorTown].Trim();
            string debtorCountryCode = fields[idxDebtorCountryCode].Trim();

            // Build Swiss QR Code codetext using Aspose.BarCode's SwissQRCodetext class
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
                CountryCode = creditorCountryCode
            };

            // Populate debtor address
            swissQr.Bill.Debtor = new Address
            {
                Name = debtorName,
                Street = debtorStreet,
                HouseNo = debtorHouseNo,
                PostalCode = debtorPostalCode,
                Town = debtorTown,
                CountryCode = debtorCountryCode
            };

            // Generate the barcode image and save as PNG
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                string outputPath = Path.Combine(outputDir, $"{invoiceId}.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated QR for {invoiceId} at {outputPath}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}