// Title: Generate and Decode Swiss QR Codes to JSON
// Description: Demonstrates creating sample Swiss QR Code images, decoding them, and exporting payment details to a JSON file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating Swiss QR Codes and BarCodeReader for extracting payment information. Developers working with payment QR codes can learn how to generate test data, read QR content, and serialize results to JSON, covering common scenarios such as batch processing and data integration.
// Prompt: Create a console app that reads QR code images from a folder and writes payment details to JSON.
// Tags: qr code, swissqr, barcode generation, barcode recognition, json, aspose.barcode, payment

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating Swiss QR Code images, decoding them, and writing payment details to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary folder to store generated QR code images.
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "SwissQRBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        // List to keep track of generated image file paths.
        List<string> imageFiles = new List<string>();

        // --------------------------------------------------------------------
        // 2. Generate sample Swiss QR Code images (3 samples) using ComplexBarcodeGenerator.
        // --------------------------------------------------------------------
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"SwissQR_{i}.png");
            SwissQRCodetext swiss = new SwissQRCodetext();
            swiss.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swiss.Bill.Account = "CH4431999123000889012";
            swiss.Bill.Amount = 100m * i; // 100, 200, 300
            swiss.Bill.Currency = "CHF";
            swiss.Bill.Reference = $"21000000000{i}39471430009017";
            swiss.Bill.Creditor = new Address
            {
                Name = "Creditor " + i,
                Street = "Main Street",
                HouseNo = "1" + i,
                PostalCode = "8000",
                Town = "Zurich",
                CountryCode = "CH"
            };
            swiss.Bill.Debtor = new Address
            {
                Name = "Debtor " + i,
                Street = "Second Street",
                HouseNo = "2" + i,
                PostalCode = "3000",
                Town = "Bern",
                CountryCode = "CH"
            };

            // Configure generator parameters and save the QR code image.
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swiss))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            imageFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // 3. Prepare a list to hold decoded payment information.
        // --------------------------------------------------------------------
        List<PaymentInfo> payments = new List<PaymentInfo>();

        // --------------------------------------------------------------------
        // 4. Read each generated QR code, decode it, and extract payment details.
        // --------------------------------------------------------------------
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                    if (decoded == null)
                        continue;

                    // Map decoded fields to the DTO.
                    PaymentInfo info = new PaymentInfo
                    {
                        Version = decoded.Bill.Version.ToString(),
                        Account = decoded.Bill.Account,
                        Amount = decoded.Bill.Amount,
                        Currency = decoded.Bill.Currency,
                        Reference = decoded.Bill.Reference,
                        CreditorName = decoded.Bill.Creditor?.Name,
                        CreditorStreet = decoded.Bill.Creditor?.Street,
                        CreditorHouseNo = decoded.Bill.Creditor?.HouseNo,
                        CreditorPostalCode = decoded.Bill.Creditor?.PostalCode,
                        CreditorTown = decoded.Bill.Creditor?.Town,
                        CreditorCountryCode = decoded.Bill.Creditor?.CountryCode,
                        DebtorName = decoded.Bill.Debtor?.Name,
                        DebtorStreet = decoded.Bill.Debtor?.Street,
                        DebtorHouseNo = decoded.Bill.Debtor?.HouseNo,
                        DebtorPostalCode = decoded.Bill.Debtor?.PostalCode,
                        DebtorTown = decoded.Bill.Debtor?.Town,
                        DebtorCountryCode = decoded.Bill.Debtor?.CountryCode
                    };

                    payments.Add(info);
                }
            }
        }

        // --------------------------------------------------------------------
        // 5. Serialize the collected payment details to a formatted JSON file.
        // --------------------------------------------------------------------
        string jsonOutput = JsonSerializer.Serialize(payments, new JsonSerializerOptions { WriteIndented = true });
        string jsonPath = Path.Combine(inputFolder, "payments.json");
        File.WriteAllText(jsonPath, jsonOutput);

        Console.WriteLine($"Processed {payments.Count} QR codes. JSON written to: {jsonPath}");
    }

    // Simple DTO for JSON serialization
    class PaymentInfo
    {
        public string Version { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public string CreditorName { get; set; }
        public string CreditorStreet { get; set; }
        public string CreditorHouseNo { get; set; }
        public string CreditorPostalCode { get; set; }
        public string CreditorTown { get; set; }
        public string CreditorCountryCode { get; set; }
        public string DebtorName { get; set; }
        public string DebtorStreet { get; set; }
        public string DebtorHouseNo { get; set; }
        public string DebtorPostalCode { get; set; }
        public string DebtorTown { get; set; }
        public string DebtorCountryCode { get; set; }
    }
}