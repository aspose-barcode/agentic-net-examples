// Title: Read Swiss QR Code Images and Export Payment Details to JSON
// Description: Demonstrates reading QR code images from a temporary folder, decoding Swiss QR payment information, and writing the extracted data to a JSON file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It shows how to generate Swiss QR Code barcodes with ComplexBarcodeGenerator, read them using BarCodeReader, and map the decoded SwissQRCodetext to a custom data model. Developers working with payment QR codes can use these APIs to automate data extraction and integration with downstream systems.
// Prompt: Create a console app that reads QR code images from a folder and writes payment details to JSON.
// Tags: qr code, payment, json, aspose.barcode, barcoderecognition, barcodegeneration, swissqr, complexbarcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Represents the payment information extracted from a Swiss QR Code.
/// </summary>
class PaymentDetail
{
    public string Version { get; set; } = string.Empty;
    public string Account { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Reference { get; set; } = string.Empty;
    public string CreditorName { get; set; } = string.Empty;
    public string CreditorStreet { get; set; } = string.Empty;
    public string CreditorHouseNo { get; set; } = string.Empty;
    public string CreditorPostalCode { get; set; } = string.Empty;
    public string CreditorTown { get; set; } = string.Empty;
    public string CreditorCountryCode { get; set; } = string.Empty;
    public string DebtorName { get; set; } = string.Empty;
    public string DebtorStreet { get; set; } = string.Empty;
    public string DebtorHouseNo { get; set; } = string.Empty;
    public string DebtorPostalCode { get; set; } = string.Empty;
    public string DebtorTown { get; set; } = string.Empty;
    public string DebtorCountryCode { get; set; } = string.Empty;
}

/// <summary>
/// Console application that generates sample Swiss QR Code images, reads them,
/// extracts payment details, and writes the results to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a dedicated temporary folder for generated QR code images.
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "SwissQR_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);

        // --------------------------------------------------------------------
        // 2. Define a valid Swiss IBAN that will be used for all sample bills.
        // --------------------------------------------------------------------
        const string validIban = "CH9300762011623852957";

        // --------------------------------------------------------------------
        // 3. Generate three sample Swiss QR Code images and store their paths.
        // --------------------------------------------------------------------
        List<string> generatedFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            SwissQRCodetext swissCode = new SwissQRCodetext();
            swissCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissCode.Bill.Account = validIban;
            swissCode.Bill.Amount = 1000.00m + i;
            swissCode.Bill.Currency = "CHF";
            swissCode.Bill.Reference = $"21000000000313947143000901{i:D2}";
            swissCode.Bill.Creditor = new Address
            {
                Name = $"Creditor {i}",
                Street = "Main Street",
                HouseNo = $"{i}A",
                PostalCode = "8000",
                Town = "Zurich",
                CountryCode = "CH"
            };
            swissCode.Bill.Debtor = new Address
            {
                Name = $"Debtor {i}",
                Street = "Second Street",
                HouseNo = $"{i}B",
                PostalCode = "3000",
                Town = "Bern",
                CountryCode = "CH"
            };

            // Use ComplexBarcodeGenerator to create the QR code image.
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissCode))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

                string filePath = Path.Combine(inputFolder, $"SwissQR_{i}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                generatedFiles.Add(filePath);
            }
        }

        // --------------------------------------------------------------------
        // 4. Read each generated QR code image, decode the Swiss QR payload,
        //    and map it to the PaymentDetail model.
        // --------------------------------------------------------------------
        List<PaymentDetail> paymentDetails = new List<PaymentDetail>();
        foreach (string file in generatedFiles)
        {
            if (!File.Exists(file))
                continue;

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.QR))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                        if (decoded == null)
                            continue;

                        PaymentDetail detail = new PaymentDetail
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
                        paymentDetails.Add(detail);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read '{file}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // 5. Serialize the collected payment details to a formatted JSON file.
        // --------------------------------------------------------------------
        string jsonOutput = JsonSerializer.Serialize(paymentDetails, new JsonSerializerOptions { WriteIndented = true });
        string jsonPath = Path.Combine(inputFolder, "paymentDetails.json");
        File.WriteAllText(jsonPath, jsonOutput);

        // --------------------------------------------------------------------
        // 6. Inform the user about the processing result.
        // --------------------------------------------------------------------
        Console.WriteLine($"Processed {paymentDetails.Count} QR code(s). JSON written to: {jsonPath}");
    }
}