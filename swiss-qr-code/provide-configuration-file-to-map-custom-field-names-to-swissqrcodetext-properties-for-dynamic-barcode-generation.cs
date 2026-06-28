using System;
using System.Collections.Generic;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Swiss QR barcode using Aspose.BarCode with
/// dynamic field mapping from a dictionary of input values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Builds a <see cref="SwissQRCodetext"/>
    /// instance from custom input data, validates required fields, and generates
    /// a Swiss QR barcode image saved to disk.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare sample input data using custom field names.
        // ------------------------------------------------------------
        var inputData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "CreditorName", "John Doe" },
            { "CountryCode", "CH" },
            { "Account", "CH9300762011623852957" },
            { "Amount", "199.95" },
            { "Version", "V2_0" } // corresponds to SwissQRBill.QrBillStandardVersion.V2_0
        };

        // ------------------------------------------------------------
        // 2. Define mapping from custom field names to actions that set
        //    properties on a SwissQRCodetext instance.
        // ------------------------------------------------------------
        var fieldMappings = new Dictionary<string, Action<SwissQRCodetext, string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "CreditorName", (s, v) => s.Bill.Creditor.Name = v },
            { "CountryCode", (s, v) => s.Bill.Creditor.CountryCode = v },
            { "Account", (s, v) => s.Bill.Account = v },
            {
                "Amount",
                (s, v) =>
                {
                    // Parse amount using invariant culture; report if invalid.
                    if (decimal.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out var amt))
                        s.Bill.Amount = amt;
                    else
                        Console.WriteLine($"Invalid amount value: {v}");
                }
            },
            {
                "Version",
                (s, v) =>
                {
                    // Convert enum name (e.g., "V2_0") to SwissQRBill.QrBillStandardVersion.
                    if (Enum.TryParse<SwissQRBill.QrBillStandardVersion>(v, out var ver))
                        s.Bill.Version = ver;
                    else
                        Console.WriteLine($"Invalid version value: {v}");
                }
            }
        };

        // ------------------------------------------------------------
        // 3. Create a SwissQRCodetext object and populate it using the mapping.
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();

        foreach (var kvp in inputData)
        {
            if (fieldMappings.TryGetValue(kvp.Key, out var setter))
            {
                try
                {
                    // Apply the mapped setter action.
                    setter(swissQr, kvp.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error setting field '{kvp.Key}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"No mapping defined for field '{kvp.Key}'.");
            }
        }

        // ------------------------------------------------------------
        // 4. Validate that all mandatory fields are present and correct.
        // ------------------------------------------------------------
        if (string.IsNullOrWhiteSpace(swissQr.Bill.Creditor.Name) ||
            string.IsNullOrWhiteSpace(swissQr.Bill.Creditor.CountryCode) ||
            string.IsNullOrWhiteSpace(swissQr.Bill.Account) ||
            swissQr.Bill.Amount <= 0 ||
            swissQr.Bill.Version == 0)
        {
            Console.WriteLine("Missing required SwissQR fields. Barcode generation aborted.");
            return;
        }

        // ------------------------------------------------------------
        // 5. Generate the Swiss QR barcode and save it to a PNG file.
        // ------------------------------------------------------------
        const string outputPath = "SwissQR.png";

        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            try
            {
                generator.Save(outputPath);
                Console.WriteLine($"Swiss QR barcode saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode: {ex.Message}");
            }
        }
    }
}