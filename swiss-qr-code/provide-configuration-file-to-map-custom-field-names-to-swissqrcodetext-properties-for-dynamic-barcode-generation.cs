// Title: Generate Swiss QR Code from Config File
// Description: Demonstrates reading a simple key‑value configuration file and using its values to populate a SwissQRCodetext object for QR‑bill barcode generation.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of Aspose.BarCode.ComplexBarcode classes such as SwissQRCodetext, SwissQRBill, and ComplexBarcodeGenerator to create payment QR‑bills. Developers often need to map external data sources (e.g., configuration files, databases) to QR‑bill fields for dynamic barcode creation in invoicing or payment applications.
// Prompt: Provide a configuration file to map custom field names to SwissQRCodetext properties for dynamic barcode generation.
// Tags: barcode, swissqr, configuration, generation, aspose.barcode, complexbarcode, qr-bill

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads a temporary configuration file, maps its entries to a
/// <see cref="SwissQRCodetext"/> object, and generates a Swiss QR‑Bill barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the configuration parsing and barcode generation steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a temporary configuration file containing custom field mappings.
        // --------------------------------------------------------------------
        string configPath = Path.Combine(Path.GetTempPath(), "SwissQRConfig.txt");
        var sampleConfig = new List<string>
        {
            "CreditorName=John Doe",
            "CreditorCountry=CH",
            "Account=CH9300762011623852957",
            "Amount=199.95",
            "Currency=CHF",
            "Reference=210000000003139471430009017"
        };
        File.WriteAllLines(configPath, sampleConfig);

        // --------------------------------------------------------------------
        // 2. Load the configuration into a dictionary for easy lookup.
        // --------------------------------------------------------------------
        var config = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var line in File.ReadAllLines(configPath))
        {
            // Skip empty lines or lines without a key/value separator.
            if (string.IsNullOrWhiteSpace(line) || !line.Contains("=")) continue;

            var parts = line.Split(new[] { '=' }, 2);
            var key = parts[0].Trim();
            var value = parts[1].Trim();
            config[key] = value;
        }

        // --------------------------------------------------------------------
        // 3. Initialise the Swiss QR Code data structure and populate it from the config.
        // --------------------------------------------------------------------
        var swissQRCode = new SwissQRCodetext();
        swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        if (config.TryGetValue("CreditorName", out var creditorName))
            swissQRCode.Bill.Creditor.Name = creditorName;

        if (config.TryGetValue("CreditorCountry", out var creditorCountry))
            swissQRCode.Bill.Creditor.CountryCode = creditorCountry;

        if (config.TryGetValue("Account", out var account))
            swissQRCode.Bill.Account = account;

        if (config.TryGetValue("Amount", out var amountStr) && decimal.TryParse(amountStr, out var amount))
            swissQRCode.Bill.Amount = amount;

        if (config.TryGetValue("Currency", out var currency))
            swissQRCode.Bill.Currency = currency;

        if (config.TryGetValue("Reference", out var reference))
            swissQRCode.Bill.Reference = reference;

        // --------------------------------------------------------------------
        // 4. Generate the barcode image using ComplexBarcodeGenerator.
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQRBill.png");
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Set a higher X‑dimension for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 5. Inform the user where the image was saved.
        // --------------------------------------------------------------------
        Console.WriteLine($"Swiss QR Code generated at: {outputPath}");
    }
}