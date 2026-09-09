// Title: Swiss QR Code Generation with Mandatory Field Validation
// Description: Demonstrates creating a Swiss QR bill barcode, validating required fields, and handling errors for missing mandatory data.
// Category-Description: This example belongs to the Aspose.BarCode Swiss QR bill generation category. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and related API classes to produce QR bill images. Typical scenarios include generating payment QR codes for Swiss invoices while ensuring all mandatory fields are present to avoid invalid barcodes. Developers often need to validate bill data before barcode creation, making this pattern essential for reliable financial applications.
/// Prompt: Implement error handling for missing mandatory fields when constructing SwissQRCodetext to prevent invalid barcode generation.
/// Tags: barcode, swissqr, validation, error-handling, aspose.barcode, qr, image, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates Swiss QR bill barcode generation with validation of mandatory fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Swiss QR code, first showing validation failure
    /// when a mandatory field is missing, then producing a valid QR code image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for the generated PNG file.
        string outputDir = Path.Combine(Path.GetTempPath(), "SwissQRDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "SwissQRBill.png");

        // --------------------------------------------------------------------
        // Example 1: Attempt to create a QR code with a missing mandatory field (Creditor.Name)
        // --------------------------------------------------------------------
        try
        {
            var invalidQr = new SwissQRCodetext();
            invalidQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            invalidQr.Bill.Account = "CH4431999123000889012";
            invalidQr.Bill.Amount = 1000.25m;
            invalidQr.Bill.Currency = "CHF";

            // Creditor.Name is intentionally omitted to trigger validation.
            invalidQr.Bill.Creditor = new Address
            {
                CountryCode = "CH",
                Street = "Musterstrasse",
                HouseNo = "12b",
                PostalCode = "8200",
                Town = "Zürich"
            };

            // Validate the QR code data; should throw an ArgumentException.
            ValidateSwissQR(invalidQr);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error (expected): {ex.Message}");
        }

        // --------------------------------------------------------------------
        // Example 2: Create a valid Swiss QR code and save it as a PNG image
        // --------------------------------------------------------------------
        try
        {
            var validQr = new SwissQRCodetext();
            validQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            validQr.Bill.Account = "CH4431999123000889012";
            validQr.Bill.Amount = 1000.25m;
            validQr.Bill.Currency = "CHF";
            validQr.Bill.Reference = "210000000003139471430009017";

            // Populate creditor (mandatory fields included).
            validQr.Bill.Creditor = new Address
            {
                Name = "Muster & Söhne",
                Street = "Musterstrasse",
                HouseNo = "12b",
                PostalCode = "8200",
                Town = "Zürich",
                CountryCode = "CH"
            };

            // Populate debtor (optional but commonly used).
            validQr.Bill.Debtor = new Address
            {
                Name = "Muster AG",
                Street = "Musterstrasse",
                HouseNo = "1",
                PostalCode = "3030",
                Town = "Bern",
                CountryCode = "CH"
            };

            // Ensure all mandatory fields are present before generation.
            ValidateSwissQR(validQr);

            // Generate the QR code image using ComplexBarcodeGenerator.
            using (var generator = new ComplexBarcodeGenerator(validQr))
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
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates that all mandatory fields of a SwissQRCodetext instance are set.
    /// Throws <see cref="ArgumentException"/> if any required data is missing or invalid.
    /// </summary>
    /// <param name="qr">The SwissQRCodetext object to validate.</param>
    static void ValidateSwissQR(SwissQRCodetext qr)
    {
        if (qr == null) throw new ArgumentException("SwissQRCodetext instance is null.");

        var bill = qr.Bill;
        if (bill == null) throw new ArgumentException("Bill information is missing.");

        // Creditor name is mandatory.
        if (string.IsNullOrWhiteSpace(bill.Creditor?.Name))
            throw new ArgumentException("Creditor.Name is mandatory and missing.");

        // Creditor country code is mandatory.
        if (string.IsNullOrWhiteSpace(bill.Creditor?.CountryCode))
            throw new ArgumentException("Creditor.CountryCode is mandatory and missing.");

        // IBAN (account) is mandatory.
        if (string.IsNullOrWhiteSpace(bill.Account))
            throw new ArgumentException("Bill.Account (IBAN) is mandatory and missing.");

        // Amount must be greater than zero.
        if (bill.Amount <= 0)
            throw new ArgumentException("Bill.Amount must be greater than zero.");

        // Version must be set (non-zero enum value).
        if (bill.Version == 0)
            throw new ArgumentException("Bill.Version is mandatory and not set.");
    }
}