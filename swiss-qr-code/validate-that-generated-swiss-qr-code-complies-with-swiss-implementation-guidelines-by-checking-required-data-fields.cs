using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation, reading, and validation of a Swiss QR Code using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Swiss QR Code, reads it back, and validates required fields.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare Swiss QR code data according to the Swiss Implementation Guidelines
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Account = "CH9300762011623852957"; // Valid IBAN
        swissQr.Bill.Amount = 199.95m;                 // Amount (decimal with 'm' suffix)
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0; // Required version
        swissQr.Bill.Creditor.Name = "John Doe";      // Creditor name (mandatory)
        swissQr.Bill.Creditor.CountryCode = "CH";     // Creditor country code (mandatory)

        // ------------------------------------------------------------
        // 2. Generate the Swiss QR barcode image into a memory stream
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        using (var ms = new MemoryStream())
        {
            // Save the barcode as PNG into the memory stream
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for subsequent reading

            // ------------------------------------------------------------
            // 3. Recognize the barcode from the generated image
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                var results = reader.ReadBarCodes();

                // If no QR code was detected, inform the user and exit
                if (results.Length == 0)
                {
                    Console.WriteLine("No QR code detected in the generated image.");
                    return;
                }

                // Process each detected QR code (should be only one in this scenario)
                foreach (var result in results)
                {
                    // ------------------------------------------------------------
                    // 4. Decode the complex Swiss QR codetext
                    // ------------------------------------------------------------
                    var decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                    if (decoded == null)
                    {
                        Console.WriteLine("Failed to decode Swiss QR codetext.");
                        continue;
                    }

                    // ------------------------------------------------------------
                    // 5. Validate required fields of the decoded Swiss QR bill
                    // ------------------------------------------------------------
                    bool valid = true;

                    // Validate IBAN (Account)
                    if (string.IsNullOrWhiteSpace(decoded.Bill.Account))
                    {
                        Console.WriteLine("Missing or empty Account (IBAN).");
                        valid = false;
                    }

                    // Validate amount (must be greater than zero)
                    if (decoded.Bill.Amount <= 0)
                    {
                        Console.WriteLine("Amount must be greater than zero.");
                        valid = false;
                    }

                    // Validate version (must be V2_0)
                    if (decoded.Bill.Version != SwissQRBill.QrBillStandardVersion.V2_0)
                    {
                        Console.WriteLine($"Invalid version: {decoded.Bill.Version}");
                        valid = false;
                    }

                    // Validate creditor name
                    if (decoded.Bill.Creditor == null ||
                        string.IsNullOrWhiteSpace(decoded.Bill.Creditor.Name))
                    {
                        Console.WriteLine("Creditor name is missing.");
                        valid = false;
                    }

                    // Validate creditor country code
                    if (decoded.Bill.Creditor == null ||
                        string.IsNullOrWhiteSpace(decoded.Bill.Creditor.CountryCode))
                    {
                        Console.WriteLine("Creditor country code is missing.");
                        valid = false;
                    }

                    // Output validation result
                    Console.WriteLine(valid
                        ? "Swiss QR Code validation passed: all required fields are present."
                        : "Swiss QR Code validation failed: some required fields are missing or invalid.");
                }
            }
        }
    }
}