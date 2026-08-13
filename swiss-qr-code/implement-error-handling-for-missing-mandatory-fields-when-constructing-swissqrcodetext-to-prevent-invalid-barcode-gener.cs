// Title: Generate Swiss QR Code barcode with mandatory field validation
// Description: This example creates a Swiss QR Code (QR‑Bill) barcode, validates required bill fields, and saves the image as PNG.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation for Swiss QR (QR‑Bill) using ComplexBarcodeGenerator. Shows how to populate SwissQRCodetext, perform custom validation of mandatory fields, and export the barcode image. Useful for developers implementing payment QR codes, invoicing, or financial document automation.
// Prompt: Implement error handling for missing mandatory fields when constructing SwissQRCodetext to prevent invalid barcode generation.
// Tags: swissqr, barcode generation, validation, png, aspose.barcode, complexbarcodegenerator, qr‑bill

using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that builds a Swiss QR Code (QR‑Bill) barcode, validates required data, and saves it as an image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Constructs the SwissQRCodetext, validates mandatory fields, and generates a PNG barcode.
    /// </summary>
    static void Main()
    {
        // Create SwissQR codetext and populate mandatory fields
        var swissQr = new SwissQRCodetext();

        // Set creditor address (mandatory Name and CountryCode)
        swissQr.Bill.Creditor = new Address();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";

        // Set mandatory bill data: IBAN account, amount, and QR‑Bill version
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Optional: additional creditor address fields can be set here
        // swissQr.Bill.Creditor.Street = "Main Street 1";
        // swissQr.Bill.Creditor.PostalCode = "8000";
        // swissQr.Bill.Creditor.Town = "Zurich";

        // Validate that all mandatory fields are present before barcode generation
        try
        {
            ValidateSwissQR(swissQr);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            return;
        }

        // Generate the barcode and save it as a PNG file
        try
        {
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Save("SwissQR.png");
                Console.WriteLine("SwissQR barcode image saved as SwissQR.png");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Barcode generation failed: {ex.Message}");
        }
    }

    // Validates mandatory fields for SwissQR codetext
    static void ValidateSwissQR(SwissQRCodetext codetext)
    {
        if (codetext == null)
            throw new ArgumentNullException(nameof(codetext));

        var bill = codetext.Bill;
        if (bill == null)
            throw new ArgumentException("Bill data is missing.");

        // Creditor must be provided
        if (bill.Creditor == null)
            throw new ArgumentException("Creditor information is missing.");

        if (string.IsNullOrWhiteSpace(bill.Creditor.Name))
            throw new ArgumentException("Creditor name is mandatory.");

        if (string.IsNullOrWhiteSpace(bill.Creditor.CountryCode))
            throw new ArgumentException("Creditor country code is mandatory.");

        if (string.IsNullOrWhiteSpace(bill.Account))
            throw new ArgumentException("Account (IBAN) is mandatory.");

        if (bill.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        if (bill.Version == 0)
            throw new ArgumentException("Bill version is mandatory.");
    }
}