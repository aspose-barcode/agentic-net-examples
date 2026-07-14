// Title: Dynamic Swiss QR Code Generation Using Configuration Mapping
// Description: Demonstrates how to map custom configuration fields to SwissQRCodetext properties and generate a Swiss QR barcode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Aspose.BarCode.Generation.ComplexBarcodeGenerator together with SwissQRCodetext to create Swiss QR bills. Developers often need to populate QR code data dynamically from configuration files or user input, and this pattern illustrates typical mapping, validation, and image output steps.
// Prompt: Provide a configuration file to map custom field names to SwissQRCodetext properties for dynamic barcode generation.
// Tags: barcode, swissqr, configuration, dynamic, generation, aspose.barcode, complexbarcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads a dictionary of custom field names,
/// maps them to <see cref="SwissQRCodetext"/> properties, validates required data,
/// and generates a Swiss QR barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs configuration mapping,
    /// validation, and barcode generation.
    /// </summary>
    static void Main()
    {
        // Sample configuration mapping custom field names to values
        var config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "CreditorName", "John Doe" },
            { "CreditorCountryCode", "CH" },
            { "Account", "CH9300762011623852957" },
            { "Amount", "199.95" },
            { "Version", "V2_0" } // maps to SwissQRBill.QrBillStandardVersion.V2_0
        };

        // Create an empty SwissQRCodetext instance
        var swissQr = new SwissQRCodetext();

        // Apply configuration values to the corresponding SwissQRCodetext properties
        foreach (var kvp in config)
        {
            switch (kvp.Key)
            {
                case "CreditorName":
                    swissQr.Bill.Creditor.Name = kvp.Value;
                    break;
                case "CreditorCountryCode":
                    swissQr.Bill.Creditor.CountryCode = kvp.Value;
                    break;
                case "Account":
                    swissQr.Bill.Account = kvp.Value;
                    break;
                case "Amount":
                    // Parse amount string to decimal; throw if invalid
                    if (decimal.TryParse(kvp.Value, out var amount))
                        swissQr.Bill.Amount = amount;
                    else
                        throw new ArgumentException($"Invalid amount value: {kvp.Value}");
                    break;
                case "Version":
                    // Convert string to enum; fall back to default if parsing fails
                    if (Enum.TryParse<SwissQRBill.QrBillStandardVersion>(kvp.Value, out var version))
                        swissQr.Bill.Version = version;
                    else
                        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
                    break;
                default:
                    // Unknown field – ignore or handle as needed
                    break;
            }
        }

        // Verify that all mandatory fields have been populated
        if (string.IsNullOrWhiteSpace(swissQr.Bill.Creditor.Name) ||
            string.IsNullOrWhiteSpace(swissQr.Bill.Creditor.CountryCode) ||
            string.IsNullOrWhiteSpace(swissQr.Bill.Account) ||
            swissQr.Bill.Amount <= 0m)
        {
            Console.WriteLine("Missing required Swiss QR bill information.");
            return;
        }

        // Generate the Swiss QR barcode and save it as a PNG file
        const string outputPath = "SwissQR.png";
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Swiss QR barcode generated and saved to '{Path.GetFullPath(outputPath)}'.");
    }
}