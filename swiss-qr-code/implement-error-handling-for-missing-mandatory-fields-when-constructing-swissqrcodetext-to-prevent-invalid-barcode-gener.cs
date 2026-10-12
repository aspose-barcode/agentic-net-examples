// Title: Swiss QR Code Validation and Generation Example
// Description: Demonstrates how to validate mandatory fields of a Swiss QR Code before generating a barcode and how to create a valid QR barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Swiss QR Bill (QR‑IBAN) creation. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and related API classes for validating bill data, handling errors, and producing PNG images. Developers working with payment QR codes can reference this pattern for ensuring data integrity before barcode rendering.
// Prompt: Implement error handling for missing mandatory fields when constructing SwissQRCodetext to prevent invalid barcode generation.
// Tags: swissqr, barcode, validation, generation, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates validation of Swiss QR Code data and generation of a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Validates that all mandatory fields of a Swiss QR Code are populated.
    /// Throws descriptive exceptions when validation fails.
    /// </summary>
    /// <param name="qr">The SwissQRCodetext instance to validate.</param>
    static void ValidateSwissQR(SwissQRCodetext qr)
    {
        if (qr == null) throw new ArgumentNullException(nameof(qr));

        var bill = qr.Bill;
        if (bill == null) throw new ArgumentException("Bill is null.");

        // Creditor address validation
        if (bill.Creditor == null) throw new ArgumentException("Creditor address is null.");
        if (string.IsNullOrWhiteSpace(bill.Creditor.Name))
            throw new ArgumentException("Creditor Name is mandatory.");
        if (string.IsNullOrWhiteSpace(bill.Creditor.CountryCode))
            throw new ArgumentException("Creditor CountryCode is mandatory.");

        // Account validation
        if (string.IsNullOrWhiteSpace(bill.Account))
            throw new ArgumentException("Account is mandatory.");

        // Amount validation
        if (bill.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        // Version validation
        if (!Enum.IsDefined(typeof(SwissQRBill.QrBillStandardVersion), bill.Version))
            throw new ArgumentException("Bill.Version is not set or invalid.");
    }

    /// <summary>
    /// Entry point of the example. Executes two scenarios: one with missing mandatory fields (expected failure) and one with a fully populated bill (successful generation).
    /// </summary>
    static void Main()
    {
        // Example 1: Attempt to generate with a missing mandatory field (Creditor Name)
        try
        {
            var incomplete = new SwissQRCodetext();
            incomplete.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            incomplete.Bill.Account = "CH9300762011623852957";
            incomplete.Bill.Amount = 199.95m;
            incomplete.Bill.Creditor = new Address
            {
                // Name is intentionally omitted to trigger validation
                CountryCode = "CH"
            };

            // Perform validation – should throw
            ValidateSwissQR(incomplete);

            // If validation unexpectedly passes, attempt barcode generation (should not happen)
            using (var gen = new ComplexBarcodeGenerator(incomplete))
            {
                gen.Save("unused.png", BarCodeImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validation failed as expected: {ex.Message}");
        }

        // Example 2: Properly constructed Swiss QR Code – successful generation
        try
        {
            var swissQRCode = new SwissQRCodetext();
            swissQRCode.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQRCode.Bill.Account = "CH4431999123000889012";
            swissQRCode.Bill.Amount = 1000.25m;
            swissQRCode.Bill.Currency = "CHF";
            swissQRCode.Bill.Reference = "210000000003139471430009017";
            swissQRCode.Bill.Creditor = new Address
            {
                Name = "Muster & Söhne",
                Street = "Musterstrasse",
                HouseNo = "12b",
                PostalCode = "8200",
                Town = "Zürich",
                CountryCode = "CH"
            };
            swissQRCode.Bill.Debtor = new Address
            {
                Name = "Muster AG",
                Street = "Musterstrasse",
                HouseNo = "1",
                PostalCode = "3030",
                Town = "Bern",
                CountryCode = "CH"
            };

            // Validate the fully populated bill
            ValidateSwissQR(swissQRCode);

            // Define output path in the temporary folder
            string outputPath = Path.Combine(Path.GetTempPath(), "SwissQR.png");

            // Generate the barcode with custom parameters
            using (var generator = new ComplexBarcodeGenerator(swissQRCode))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Swiss QR Code generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during barcode generation: {ex.Message}");
        }
    }
}