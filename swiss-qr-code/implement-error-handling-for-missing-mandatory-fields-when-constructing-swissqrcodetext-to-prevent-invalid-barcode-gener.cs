// Title: Generate Swiss QR Code barcode with validation
// Description: Demonstrates creating a Swiss QR Code barcode, validating mandatory fields, and saving it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Swiss QR Bill (QR‑Bill) creation. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and related API classes to build a valid QR‑Bill, a common requirement for Swiss financial documents. Developers often need to ensure mandatory fields are set before generating the barcode to avoid errors.
// Prompt: Implement error handling for missing mandatory fields when constructing SwissQRCodetext to prevent invalid barcode generation.
// Tags: swissqr, qr-bill, barcode generation, validation, aspnet, aspnet-core, aspnet-barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Swiss QR Code barcode with mandatory field validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds SwissQRCodetext, validates required fields, and saves the barcode as PNG.
    /// </summary>
    static void Main()
    {
        // Create a new SwissQRCodetext instance to hold QR‑Bill data
        var swissQr = new SwissQRCodetext();

        // Populate mandatory fields required for a valid QR‑Bill
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Optional: add additional information such as invoice reference
        swissQr.Bill.BillInformation = "Invoice 12345";

        // Validate that all required fields are present and correct
        try
        {
            ValidateSwissQr(swissQr);
        }
        catch (ArgumentException ex)
        {
            // Output validation error and abort execution
            Console.WriteLine($"Validation error: {ex.Message}");
            return;
        }

        // Generate the Swiss QR barcode using the validated codetext
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Configure generator to throw if the codetext is incorrect (defensive programming)
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Determine output file path in the current working directory
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "SwissQR.png");

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"SwissQR barcode saved to: {outputPath}");
        }
    }

    // Validates mandatory fields of SwissQRCodetext; throws ArgumentException if any are missing or invalid.
    static void ValidateSwissQr(SwissQRCodetext codetext)
    {
        if (codetext == null)
            throw new ArgumentException("SwissQRCodetext instance cannot be null.");

        var bill = codetext.Bill;
        if (bill == null)
            throw new ArgumentException("Bill data cannot be null.");

        if (string.IsNullOrWhiteSpace(bill.Creditor?.Name))
            throw new ArgumentException("Creditor name is mandatory.");

        if (string.IsNullOrWhiteSpace(bill.Creditor?.CountryCode))
            throw new ArgumentException("Creditor country code is mandatory.");

        if (string.IsNullOrWhiteSpace(bill.Account))
            throw new ArgumentException("Account (IBAN) is mandatory.");

        if (bill.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        // Version is an enum; ensure it has a defined value
        if (!Enum.IsDefined(typeof(SwissQRBill.QrBillStandardVersion), bill.Version))
            throw new ArgumentException("Invalid SwissQR bill version.");
    }
}