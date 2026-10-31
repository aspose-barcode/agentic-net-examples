// Title: Generate and Verify Swiss QR Code using ComplexBarcodeGenerator
// Description: Demonstrates creating a Swiss QR Code with payment information, generating a PNG barcode, and verifying the encoded fields by decoding the barcode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator, SwissQRCodetext, and ComplexCodetextReader. Typical use cases include generating QR codes for payment bills and validating their content programmatically. Developers often need to create, render, and read complex barcodes such as Swiss QR for financial transactions.
// Prompt: Write unit tests to verify ComplexBarcodeGenerator produces correct QR code data for given payment fields.
// Tags: swiss-qr, complex-barcode, generation, verification, png, aspose.barcode, aspose.barcode.complexbarcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the Swiss QR Code generation and verification example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Swiss QR Code barcode, reads it back, and checks that key payment fields match the original data.
    /// </summary>
    static void Main()
    {
        // Prepare Swiss QR Code data with payment details
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
        swissQr.Bill.Account = "CH4431999123000889012";
        swissQr.Bill.Amount = 1000.25m;
        swissQr.Bill.Currency = "CHF";
        swissQr.Bill.Reference = "210000000003139471430009017";
        swissQr.Bill.Creditor = new Address
        {
            Name = "Muster & Söhne",
            Street = "Musterstrasse",
            HouseNo = "12b",
            PostalCode = "8200",
            Town = "Zürich",
            CountryCode = "CH"
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

        // Retrieve the constructed codetext for later verification
        string originalCodetext = swissQr.GetConstructedCodetext();

        bool testPassed = false;
        string message = string.Empty;

        try
        {
            // Generate the barcode into a memory stream (PNG format)
            using (var ms = new MemoryStream())
            {
                using (var generator = new ComplexBarcodeGenerator(swissQr))
                {
                    // Optional: set module size (X dimension) for better readability
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Reset stream position before reading
                ms.Position = 0;

                // Decode the generated barcode from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        message = "No barcode detected.";
                    }
                    else
                    {
                        // Attempt to decode Swiss QR specific codetext
                        var decoded = ComplexCodetextReader.TryDecodeSwissQR(results[0].CodeText);
                        if (decoded == null)
                        {
                            message = "Failed to decode Swiss QR codetext.";
                        }
                        else
                        {
                            // Compare key fields between original and decoded data
                            bool fieldsMatch =
                                decoded.Bill.Account == swissQr.Bill.Account &&
                                decoded.Bill.Amount == swissQr.Bill.Amount &&
                                decoded.Bill.Currency == swissQr.Bill.Currency &&
                                decoded.Bill.Reference == swissQr.Bill.Reference &&
                                decoded.Bill.Creditor.Name == swissQr.Bill.Creditor.Name &&
                                decoded.Bill.Debtor.Name == swissQr.Bill.Debtor.Name;

                            testPassed = fieldsMatch;
                            message = fieldsMatch ? "All fields match." : "Field mismatch detected.";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            message = $"Exception: {ex.Message}";
        }

        // Output test result
        Console.WriteLine($"Test {(testPassed ? "PASSED" : "FAILED")}: {message}");
    }
}