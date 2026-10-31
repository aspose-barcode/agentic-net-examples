// Title: Swiss QR Code Generation with Validation of Mandatory Fields
// Description: Demonstrates creating a Swiss QR Code barcode, validating required fields, and handling errors before generating the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Swiss QR Codes. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and validation logic to ensure required bill information is present. Developers working with payment QR codes often need to verify mandatory fields like creditor name, IBAN, and amount before rendering the barcode.
// Prompt: Implement error handling for missing mandatory fields when constructing SwissQRCodetext to prevent invalid barcode generation.
// Tags: barcode, swissqr, validation, error-handling, aspnet, aspose.barcode, complexbarcode, qr, payment

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Swiss QR Code, validates mandatory bill fields,
/// and generates a PNG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Builds a SwissQRCodetext, validates it,
    /// fills missing data, and generates the barcode image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");

        // Initialize SwissQRCodetext with an intentionally missing mandatory field (Creditor Name).
        SwissQRCodetext swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Creditor = new Address
        {
            // Name is omitted to trigger validation error.
            CountryCode = "CH",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich"
        };
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // First validation attempt – expected to fail due to missing Creditor Name.
        try
        {
            ValidateSwissQR(swissQr);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
        }

        // Populate the missing mandatory field.
        swissQr.Bill.Creditor.Name = "John Doe";

        // Re-validate and generate the barcode if validation succeeds.
        try
        {
            ValidateSwissQR(swissQr);
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                // Set barcode module size (X dimension) to 4 points.
                generator.Parameters.Barcode.XDimension.Point = 4f;
                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
            Console.WriteLine($"Swiss QR Code generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to generate barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Validates the provided SwissQRCodetext instance, ensuring all mandatory fields are present.
    /// Throws <see cref="ArgumentException"/> when a validation rule is violated.
    /// </summary>
    /// <param name="qr">The SwissQRCodetext to validate.</param>
    static void ValidateSwissQR(SwissQRCodetext qr)
    {
        if (qr == null)
            throw new ArgumentException("SwissQRCodetext instance is null.");

        if (qr.Bill == null)
            throw new ArgumentException("Bill information is missing.");

        if (qr.Bill.Creditor == null)
            throw new ArgumentException("Creditor address is missing.");

        if (string.IsNullOrWhiteSpace(qr.Bill.Creditor.Name))
            throw new ArgumentException("Creditor Name is mandatory.");

        if (string.IsNullOrWhiteSpace(qr.Bill.Creditor.CountryCode))
            throw new ArgumentException("Creditor CountryCode is mandatory.");

        if (string.IsNullOrWhiteSpace(qr.Bill.Account))
            throw new ArgumentException("Account (IBAN) is mandatory.");

        if (qr.Bill.Amount <= 0)
            throw new ArgumentException("Amount must be a positive value.");

        if (!Enum.IsDefined(typeof(SwissQRBill.QrBillStandardVersion), qr.Bill.Version))
            throw new ArgumentException("Bill Version is mandatory and must be a valid enum value.");
    }
}