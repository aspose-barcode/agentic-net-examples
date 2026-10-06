// Title: Read Swiss QR Codes and Export Payment Details to JSON
// Description: Demonstrates how to generate sample Swiss QR Code images, read them using Aspose.BarCode, extract payment information, and serialize the data to a JSON file.
// Category-Description: This example belongs to the Aspose.BarCode QR code processing collection, illustrating barcode generation, recognition, and data extraction using the ComplexBarcode and BarCodeReader classes. Typical use cases include batch processing of payment QR codes, converting barcode data into structured formats, and integrating barcode workflows into .NET applications. Developers often need to generate QR codes, read them from files, and transform the extracted payload into JSON, XML, or database records.
// Prompt: Create a console app that reads QR code images from a folder and writes payment details to JSON.
// Tags: qr, barcode, reading, json, aspose.barcode, complexbarcode, payment

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates batch generation, reading, and processing of Swiss QR Code payment data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates sample QR codes, reads them,
    /// extracts payment details, and writes the results to a JSON file.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images and output JSON.
        string tempFolder = Path.Combine(Path.GetTempPath(), "QRBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample Swiss QR Code images and collect their file paths.
        List<string> imageFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(tempFolder, $"SwissQR_{i}.png");
            GenerateSampleSwissQR(filePath, i);
            imageFiles.Add(filePath);
        }

        // Read each QR code image, decode the Swiss QR payload, and map it to PaymentInfo objects.
        List<PaymentInfo> payments = new List<PaymentInfo>();
        foreach (string file in imageFiles)
        {
            if (!File.Exists(file))
                continue;

            try
            {
                // Initialize the barcode reader for QR codes.
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.QR))
                {
                    // Iterate through all detected barcodes in the image.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Attempt to decode the Swiss QR specific codetext.
                        SwissQRCodetext swiss = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                        if (swiss != null)
                        {
                            // Populate a PaymentInfo instance with extracted fields.
                            payments.Add(new PaymentInfo
                            {
                                Version = swiss.Bill.Version.ToString(),
                                Account = swiss.Bill.Account,
                                Amount = swiss.Bill.Amount,
                                Currency = swiss.Bill.Currency,
                                Reference = swiss.Bill.Reference,
                                CreditorName = swiss.Bill.Creditor?.Name,
                                DebtorName = swiss.Bill.Debtor?.Name
                            });
                        }
                    }
                }
            }
            catch (ArgumentException)
            {
                // Log unreadable or unsupported files and continue processing.
                Console.WriteLine($"Skipping unreadable file: {file}");
            }
        }

        // Serialize the collected payment details to a formatted JSON string.
        string jsonOutput = JsonSerializer.Serialize(payments, new JsonSerializerOptions { WriteIndented = true });
        string jsonPath = Path.Combine(tempFolder, "payments.json");
        File.WriteAllText(jsonPath, jsonOutput);

        // Inform the user about the processing result.
        Console.WriteLine($"Processed {payments.Count} payment(s). JSON written to: {jsonPath}");
    }

    /// <summary>
    /// Generates a sample Swiss QR Code image with predefined payment data.
    /// </summary>
    /// <param name="path">File path where the PNG image will be saved.</param>
    /// <param name="index">Index used to vary the sample data.</param>
    static void GenerateSampleSwissQR(string path, int index)
    {
        // Build the Swiss QR payload with varying amounts and references.
        SwissQRCodetext swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQRCode.Bill.Account = "CH4431999123000889012";
        swissQRCode.Bill.Amount = 1000.00m + index;
        swissQRCode.Bill.Currency = "CHF";
        swissQRCode.Bill.Reference = $"REF{index:D4}";
        swissQRCode.Bill.Creditor = new Address
        {
            Name = $"Creditor {index}",
            Street = "Main Street",
            HouseNo = "1",
            PostalCode = "8000",
            Town = "Zurich",
            CountryCode = "CH"
        };
        swissQRCode.Bill.Debtor = new Address
        {
            Name = $"Debtor {index}",
            Street = "Second Street",
            HouseNo = "2",
            PostalCode = "3000",
            Town = "Bern",
            CountryCode = "CH"
        };

        // Generate the QR code image using the ComplexBarcodeGenerator.
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Save(path);
        }
    }
}

/// <summary>
/// Simple DTO representing extracted payment information from a Swiss QR Code.
/// </summary>
class PaymentInfo
{
    public string Version { get; set; }
    public string Account { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Reference { get; set; }
    public string CreditorName { get; set; }
    public string DebtorName { get; set; }
}