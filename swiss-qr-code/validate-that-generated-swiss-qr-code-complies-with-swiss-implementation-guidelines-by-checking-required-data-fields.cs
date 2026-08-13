// Title: Generate and Validate Swiss QR Code (QR Bill)
// Description: Creates a Swiss QR Code (QR Bill) with required fields, saves it as PNG, then reads and validates the data against Swiss Implementation Guidelines.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates using ComplexBarcodeGenerator to create a Swiss QR Code, BarCodeReader to decode it, and ComplexCodetextReader for Swiss QR specific parsing. Typical use cases include invoicing, payment processing, and compliance checks where developers need to generate QR Bills and ensure they meet regulatory standards.
// Prompt: Validate that the generated Swiss QR Code complies with Swiss Implementation Guidelines by checking required data fields.
// Tags: swissqr, qrbill, barcode, generation, recognition, validation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a Swiss QR Code (QR Bill), saving it, reading it back,
/// and validating required fields according to Swiss Implementation Guidelines.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated Swiss QR code image.
        string outputPath = "SwissQR.png";

        // Create a Swiss QR codetext instance and populate the mandatory fields.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Generate the Swiss QR barcode image and save it as PNG.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate Swiss QR code image.");
            return;
        }

        // Read the QR code from the saved image file.
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No QR code detected in the image.");
                return;
            }

            // Extract the raw code text from the first detected QR code.
            string codeText = results[0].CodeText;

            // Decode the Swiss QR codetext into a strongly‑typed object.
            var decoded = ComplexCodetextReader.TryDecodeSwissQR(codeText);
            if (decoded == null)
            {
                Console.WriteLine("Failed to decode Swiss QR codetext.");
                return;
            }

            // Validate required fields according to Swiss Implementation Guidelines.
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(decoded.Bill.Creditor.Name))
            {
                Console.WriteLine("Creditor name is missing.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(decoded.Bill.Creditor.CountryCode))
            {
                Console.WriteLine("Creditor country code is missing.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(decoded.Bill.Account))
            {
                Console.WriteLine("Account (IBAN) is missing.");
                isValid = false;
            }

            if (decoded.Bill.Amount <= 0)
            {
                Console.WriteLine("Amount is missing or invalid.");
                isValid = false;
            }

            if (decoded.Bill.Version != SwissQRBill.QrBillStandardVersion.V2_0)
            {
                Console.WriteLine("Version is missing or not V2.0.");
                isValid = false;
            }

            // Output the validation result.
            Console.WriteLine(isValid
                ? "Swiss QR Code is valid per implementation guidelines."
                : "Swiss QR Code validation failed.");
        }
    }
}