// Title: Validate Swiss QR Code against Implementation Guidelines
// Description: Demonstrates creating a Swiss QR Code, validating required fields per Swiss guidelines, generating the barcode image, and verifying the encoded data.
// Category-Description: This example belongs to the Aspose.BarCode Swiss QR Code generation and validation category. It showcases the use of SwissQRCodetext, ComplexBarcodeGenerator, and ComplexCodetextReader classes to create, encode, and decode Swiss QR Bills. Developers commonly need to generate compliant QR codes for payments and verify that all mandatory fields are correctly embedded, making this pattern essential for financial and invoicing applications.
// Prompt: Validate that the generated Swiss QR Code complies with Swiss Implementation Guidelines by checking required data fields.
// Tags: swiss qr, barcode, validation, generation, aspose.barcode, swissqr, payment

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates, validates, generates, and decodes a Swiss QR Code
/// according to the Swiss Implementation Guidelines.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs field validation, barcode generation,
    /// and round‑trip verification of the Swiss QR Code data.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Build the Swiss QR code data structure and populate required fields
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // ------------------------------------------------------------
        // 2. Validate required fields according to Swiss Implementation Guidelines
        // ------------------------------------------------------------
        if (string.IsNullOrWhiteSpace(swissQr.Bill.Creditor.Name))
            throw new ArgumentException("Creditor name is required.");
        if (string.IsNullOrWhiteSpace(swissQr.Bill.Creditor.CountryCode))
            throw new ArgumentException("Creditor country code is required.");
        if (string.IsNullOrWhiteSpace(swissQr.Bill.Account))
            throw new ArgumentException("Account (IBAN) is required.");
        if (swissQr.Bill.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");
        if (swissQr.Bill.Version != SwissQRBill.QrBillStandardVersion.V2_0)
            throw new ArgumentException("Bill version must be V2_0.");

        // ------------------------------------------------------------
        // 3. Generate the Swiss QR barcode image and save it to disk
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Save the barcode image for visual verification (optional)
            generator.Save("SwissQR.png");

            // ------------------------------------------------------------
            // 4. Retrieve the constructed codetext for decoding
            // ------------------------------------------------------------
            string constructedCodetext = swissQr.GetConstructedCodetext();

            // ------------------------------------------------------------
            // 5. Decode the codetext back into a SwissQRCodetext object
            // ------------------------------------------------------------
            SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(constructedCodetext);
            if (decoded == null)
            {
                Console.WriteLine("Failed to decode the generated Swiss QR codetext.");
                return;
            }

            // ------------------------------------------------------------
            // 6. Verify that decoded fields match the original input data
            // ------------------------------------------------------------
            bool isValid = decoded.Bill.Creditor.Name == swissQr.Bill.Creditor.Name &&
                           decoded.Bill.Creditor.CountryCode == swissQr.Bill.Creditor.CountryCode &&
                           decoded.Bill.Account == swissQr.Bill.Account &&
                           decoded.Bill.Amount == swissQr.Bill.Amount &&
                           decoded.Bill.Version == swissQr.Bill.Version;

            Console.WriteLine(isValid
                ? "Swiss QR Code validation succeeded: all required fields are present and correct."
                : "Swiss QR Code validation failed: decoded fields do not match the original data.");
        }
    }
}