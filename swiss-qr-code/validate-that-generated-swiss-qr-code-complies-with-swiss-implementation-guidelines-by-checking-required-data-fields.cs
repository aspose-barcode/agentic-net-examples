// Title: Generate and Validate Swiss QR Bill Barcode
// Description: Demonstrates creating a Swiss QR Code (QR‑Bill) image, saving it, and verifying that required fields comply with the Swiss Implementation Guidelines.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on Swiss QR Bill (QR‑Bill) creation using ComplexBarcodeGenerator and validation via BarCodeReader. Developers commonly use these APIs to produce compliant payment QR codes and to programmatically ensure that all mandatory data fields are present and correct.
// Prompt: Validate that the generated Swiss QR Code complies with Swiss Implementation Guidelines by checking required data fields.
// Tags: swiss qr, barcode generation, barcode recognition, qr, swiss-qr-bill, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Swiss QR Bill barcode, saves it as an image,
/// and validates the encoded data against the Swiss Implementation Guidelines.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR code, saves it, and performs validation.
    /// </summary>
    static void Main()
    {
        // Ensure Unicode characters (e.g., umlauts) are displayed correctly in the console.
        Console.OutputEncoding = Encoding.Unicode;

        // Prepare the output file path in the temporary folder.
        string outputPath = Path.Combine(Path.GetTempPath(), "SwissQRBill.png");

        // ------------------------------------------------------------
        // Create Swiss QR Code data (QR‑Bill)
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";

        // Populate creditor address.
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
        };

        // Populate debtor address.
        swissQr.Bill.Debtor = new Address
        {
            Name = "Muster AG",
            Street = "Musterstrasse",
            HouseNo = "1",
            PostalCode = "3030",
            Town = "Bern",
            CountryCode = "CH"
        };

        // ------------------------------------------------------------
        // Generate the barcode image
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Set visual parameters: module size and QR encoding mode.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated QR code as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate Swiss QR Code image.");
            return;
        }

        // ------------------------------------------------------------
        // Read and decode the barcode, then validate required fields
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.QR))
        {
            bool validationPassed = true;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Attempt to decode the Swiss QR Code text.
                SwissQRCodetext decoded = ComplexCodetextReader.TryDecodeSwissQR(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Unable to decode Swiss QR Code.");
                    validationPassed = false;
                    continue;
                }

                // Validate mandatory fields according to the Swiss guidelines.
                if (decoded.Bill.Version != SwissQRBill.QrBillStandardVersion.V2_0)
                {
                    Console.WriteLine("Invalid or missing Bill.Version.");
                    validationPassed = false;
                }

                if (string.IsNullOrWhiteSpace(decoded.Bill.Account))
                {
                    Console.WriteLine("Missing Bill.Account.");
                    validationPassed = false;
                }

                if (decoded.Bill.Amount <= 0)
                {
                    Console.WriteLine("Invalid Bill.Amount.");
                    validationPassed = false;
                }

                if (string.IsNullOrWhiteSpace(decoded.Bill.Currency))
                {
                    Console.WriteLine("Missing Bill.Currency.");
                    validationPassed = false;
                }

                if (decoded.Bill.Creditor == null || string.IsNullOrWhiteSpace(decoded.Bill.Creditor.Name))
                {
                    Console.WriteLine("Missing Creditor.Name.");
                    validationPassed = false;
                }

                if (decoded.Bill.Creditor == null || string.IsNullOrWhiteSpace(decoded.Bill.Creditor.CountryCode))
                {
                    Console.WriteLine("Missing Creditor.CountryCode.");
                    validationPassed = false;
                }

                if (decoded.Bill.Debtor == null || string.IsNullOrWhiteSpace(decoded.Bill.Debtor.Name))
                {
                    Console.WriteLine("Missing Debtor.Name.");
                    validationPassed = false;
                }

                // Output decoded values for reference.
                Console.WriteLine($"Version: {decoded.Bill.Version}");
                Console.WriteLine($"Account: {decoded.Bill.Account}");
                Console.WriteLine($"Amount: {decoded.Bill.Amount}");
                Console.WriteLine($"Currency: {decoded.Bill.Currency}");
                Console.WriteLine($"Reference: {decoded.Bill.Reference}");
                Console.WriteLine($"Creditor: {decoded.Bill.Creditor.Name}");
                Console.WriteLine($"Debtor: {decoded.Bill.Debtor.Name}");

                // Report overall validation result.
                if (validationPassed)
                {
                    Console.WriteLine("Swiss QR Code validation passed.");
                }
                else
                {
                    Console.WriteLine("Swiss QR Code validation failed.");
                }
            }
        }
    }
}