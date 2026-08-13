// Title: Generate Swiss QR Code barcode from JSON configuration
// Description: Demonstrates how to map custom JSON fields to SwissQRCodetext properties and generate a Swiss QR barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Swiss QR Bill (QR‑IBAN) creation. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and related classes to build a QR‑code compliant with Swiss payment standards. Developers often need to dynamically populate bill data from configuration files or databases and render the barcode for printing or digital distribution.
// Prompt: Provide a configuration file to map custom field names to SwissQRCodetext properties for dynamic barcode generation.
// Tags: swissqr, barcode, complexbarcode, json, configuration, aspnet, aspose.barcode, qr-bill, payment

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode;

/// <summary>
/// Example program that reads a JSON configuration, maps its fields to a <see cref="SwissQRCodetext"/> instance,
/// and generates a Swiss QR barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Sample configuration JSON that maps custom field names to SwissQR bill values.
        // In a real scenario this could be read from an external file.
        string configJson = @"
        {
            ""CreditorName"": ""John Doe"",
            ""CreditorCountryCode"": ""CH"",
            ""Account"": ""CH9300762011623852957"",
            ""Amount"": ""199.95"",
            ""Version"": ""V2_0"",
            ""BillInformation"": ""Invoice 12345"",
            ""Currency"": ""CHF""
        }";

        // Parse the JSON into a dictionary.
        Dictionary<string, string> config;
        try
        {
            config = JsonSerializer.Deserialize<Dictionary<string, string>>(configJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse configuration: {ex.Message}");
            return;
        }

        // Create a SwissQRCodetext instance and populate its Bill property using the configuration.
        var swissQr = new SwissQRCodetext();

        // Map custom fields to the corresponding SwissQR bill properties.
        // Mandatory fields: Creditor.Name, Creditor.CountryCode, Account, Amount, Version.
        if (config.TryGetValue("CreditorName", out string creditorName))
        {
            swissQr.Bill.Creditor.Name = creditorName;
        }
        else
        {
            Console.WriteLine("CreditorName is required.");
            return;
        }

        if (config.TryGetValue("CreditorCountryCode", out string creditorCountry))
        {
            swissQr.Bill.Creditor.CountryCode = creditorCountry;
        }
        else
        {
            Console.WriteLine("CreditorCountryCode is required.");
            return;
        }

        if (config.TryGetValue("Account", out string account))
        {
            swissQr.Bill.Account = account;
        }
        else
        {
            Console.WriteLine("Account is required.");
            return;
        }

        if (config.TryGetValue("Amount", out string amountStr) && decimal.TryParse(amountStr, out decimal amount))
        {
            swissQr.Bill.Amount = amount;
        }
        else
        {
            Console.WriteLine("Amount is required and must be a valid decimal.");
            return;
        }

        if (config.TryGetValue("Version", out string versionStr))
        {
            // Map version string to the enum value.
            if (Enum.TryParse<SwissQRBill.QrBillStandardVersion>(versionStr, out var version))
            {
                swissQr.Bill.Version = version;
            }
            else
            {
                Console.WriteLine($"Invalid Version value: {versionStr}");
                return;
            }
        }
        else
        {
            Console.WriteLine("Version is required.");
            return;
        }

        // Optional fields.
        if (config.TryGetValue("BillInformation", out string billInfo))
        {
            swissQr.Bill.BillInformation = billInfo;
        }

        if (config.TryGetValue("Currency", out string currency))
        {
            swissQr.Bill.Currency = currency;
        }

        // Generate the Swiss QR barcode image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");
        try
        {
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Save(outputPath);
            }
            Console.WriteLine($"Swiss QR barcode generated at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate barcode: {ex.Message}");
        }
    }
}