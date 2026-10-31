// Title: Generate and Decode Swiss QR Code using Aspose.BarCode
// Description: Demonstrates creating a Swiss QR Code barcode from a JSON configuration, saving it as a PNG image, and then decoding it to verify the encoded data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on complex barcode types such as Swiss QR Codes. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and BarCodeReader classes for dynamic barcode creation, customization, and decoding—common tasks for financial applications and payment processing systems.
// Prompt: Provide a configuration file to map custom field names to SwissQRCodetext properties for dynamic barcode generation.
// Tags: barcode, swissqr, generation, decoding, png, aspose.barcode, complexbarcode, financial, payment

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Swiss QR Code barcode from configuration data,
/// saving it as an image, and decoding it to verify the contents.
/// </summary>
class Program
{
    /// <summary>
    /// Simple POCO representing the configuration fields required for a Swiss QR Code.
    /// </summary>
    class Config
    {
        public string CreditorName { get; set; }
        public string CreditorCountryCode { get; set; }
        public string DebtorName { get; set; }
        public string DebtorCountryCode { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
    }

    /// <summary>
    /// Entry point. Generates a Swiss QR Code, saves it, and then reads it back.
    /// </summary>
    static void Main()
    {
        // Determine the path for the JSON configuration file in the temp folder.
        string configPath = Path.Combine(Path.GetTempPath(), "SwissQRConfig.json");

        // If the config file does not exist, create a default one with sample data.
        if (!File.Exists(configPath))
        {
            var defaultConfig = new Config
            {
                CreditorName = "John Doe",
                CreditorCountryCode = "CH",
                DebtorName = "Jane Smith",
                DebtorCountryCode = "CH",
                Account = "CH9300762011623852957",
                Amount = 199.95m,
                Currency = "CHF",
                Reference = "210000000003139471430009017"
            };
            string json = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, json);
            Console.WriteLine($"Created default config at: {configPath}");
        }

        // Load configuration from the JSON file.
        Config cfg;
        try
        {
            string json = File.ReadAllText(configPath);
            cfg = JsonSerializer.Deserialize<Config>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        // Build Swiss QR Code text using the loaded configuration.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = cfg.Account;
        swissQr.Bill.Amount = cfg.Amount;
        swissQr.Bill.Currency = cfg.Currency;
        swissQr.Bill.Reference = cfg.Reference;
        swissQr.Bill.Creditor = new Address
        {
            Name = cfg.CreditorName,
            CountryCode = cfg.CreditorCountryCode
        };
        swissQr.Bill.Debtor = new Address
        {
            Name = cfg.DebtorName,
            CountryCode = cfg.DebtorCountryCode
        };

        // Save the generated barcode as a PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR.png");
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
        Console.WriteLine($"Swiss QR Code saved to: {outputPath}");

        // Read back the saved image and decode the QR code.
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded != null)
                {
                    Console.WriteLine("Decoded Swiss QR Code:");
                    Console.WriteLine($"  Account: {decoded.Bill.Account}");
                    Console.WriteLine($"  Amount: {decoded.Bill.Amount}");
                    Console.WriteLine($"  Currency: {decoded.Bill.Currency}");
                    Console.WriteLine($"  Creditor: {decoded.Bill.Creditor.Name}");
                    Console.WriteLine($"  Debtor: {decoded.Bill.Debtor.Name}");
                }
            }
        }
    }
}