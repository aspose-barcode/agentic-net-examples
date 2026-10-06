// Title: Generate Swiss QR Code with Custom Field Mapping
// Description: Demonstrates creating a Swiss QR Code barcode using Aspose.BarCode by loading a JSON configuration that maps custom field names to SwissQRCodetext properties, populating the data, and saving the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Swiss QR (QR‑Bill) creation. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and related classes to produce payment QR codes. Developers often need to map external data sources to barcode fields dynamically, and this pattern illustrates loading a configuration file, applying mappings, and generating the barcode image.
// Prompt: Provide a configuration file to map custom field names to SwissQRCodetext properties for dynamic barcode generation.
// Tags: swissqr, barcode, complexbarcode, json, configuration, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR Code barcode with dynamic field mapping via a JSON configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Swiss QR Code image based on sample data and a configuration file that maps custom field names to barcode properties.
    /// </summary>
    static void Main()
    {
        // Define temporary paths for the configuration file and the generated barcode image
        string configPath = Path.Combine(Path.GetTempPath(), "SwissQRConfig.json");
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQRGenerated.png");

        // Create a sample configuration file if it does not already exist
        if (!File.Exists(configPath))
        {
            var sampleConfig = new Dictionary<string, string>
            {
                { "CreditorName", "Bill.Creditor.Name" },
                { "CreditorCountry", "Bill.Creditor.CountryCode" },
                { "Account", "Bill.Account" },
                { "Amount", "Bill.Amount" },
                { "Currency", "Bill.Currency" },
                { "Reference", "Bill.Reference" }
            };
            string json = JsonSerializer.Serialize(sampleConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
        }

        // Sample data that corresponds to the custom field names defined in the configuration
        var fieldValues = new Dictionary<string, string>
        {
            { "CreditorName", "John Doe" },
            { "CreditorCountry", "CH" },
            { "Account", "CH9300762011623852957" },
            { "Amount", "199.95" },
            { "Currency", "CHF" },
            { "Reference", "210000000003139471430009017" }
        };

        // Load the JSON configuration that maps custom field names to SwissQRCodetext property paths
        string configJson = File.ReadAllText(configPath);
        var configMap = JsonSerializer.Deserialize<Dictionary<string, string>>(configJson);

        // Initialize a Swiss QR codetext object and set the QR‑Bill version
        SwissQRCodetext swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Populate the SwissQRCodetext instance using the mapping defined in the configuration file
        foreach (var kvp in configMap)
        {
            string customField = kvp.Key;
            string propertyPath = kvp.Value;

            // Skip fields for which no value is provided in the sample data
            if (!fieldValues.TryGetValue(customField, out string value))
                continue;

            SetProperty(swissQr, propertyPath, value);
        }

        // Generate the barcode image using ComplexBarcodeGenerator and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Swiss QR Code generated at: {outputPath}");
    }

    /// <summary>
    /// Sets a property on the SwissQRCodetext instance based on a dot‑separated property path.
    /// </summary>
    /// <param name="codetext">The SwissQRCodetext object to modify.</param>
    /// <param name="propertyPath">The property path (e.g., "Bill.Creditor.Name").</param>
    /// <param name="value">The string value to assign.</param>
    static void SetProperty(SwissQRCodetext codetext, string propertyPath, string value)
    {
        switch (propertyPath)
        {
            case "Bill.Creditor.Name":
                codetext.Bill.Creditor.Name = value;
                break;
            case "Bill.Creditor.CountryCode":
                codetext.Bill.Creditor.CountryCode = value;
                break;
            case "Bill.Account":
                codetext.Bill.Account = value;
                break;
            case "Bill.Amount":
                if (decimal.TryParse(value, out decimal amount))
                    codetext.Bill.Amount = amount;
                break;
            case "Bill.Currency":
                codetext.Bill.Currency = value;
                break;
            case "Bill.Reference":
                codetext.Bill.Reference = value;
                break;
            // Additional mappings can be added here as needed
            default:
                // Unknown property path; ignore or log as appropriate
                break;
        }
    }
}