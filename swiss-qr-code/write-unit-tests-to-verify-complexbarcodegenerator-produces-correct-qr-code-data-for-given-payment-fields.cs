// Title: Generate and Verify Swiss QR Code using ComplexBarcodeGenerator
// Description: Demonstrates creating a Swiss QR bill barcode, saving it as PNG, and verifying the encoded data by reading it back.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the ComplexBarcodeGenerator and BarCodeReader classes for QR code creation and validation. Typical use cases include payment QR codes, data matrix generation, and automated testing of barcode content. Developers often need to generate barcodes, customize parameters, and ensure correctness via decoding.
// Prompt: Write unit tests to verify ComplexBarcodeGenerator produces correct QR code data for given payment fields.
// Tags: swissqr, qr, barcode, generation, recognition, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating Swiss QR bill barcodes, saving them, and verifying the encoded data using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes a series of QR code generation tests and reports the results.
    /// </summary>
    static void Main()
    {
        // Define a collection of test cases, each with a name and a delegate that runs the test.
        var tests = new List<(string Name, Func<bool> Test)>
        {
            ("Test1_SimplePayment", () => TestSwissQR(
                "SimplePayment",
                qr =>
                {
                    // Configure a simple Swiss QR bill with creditor and debtor details.
                    qr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
                    qr.Bill.Account = "CH4431999123000889012";
                    qr.Bill.Amount = 1000.25m;
                    qr.Bill.Currency = "CHF";
                    qr.Bill.Reference = "210000000003139471430009017";
                    qr.Bill.Creditor = new Address
                    {
                        Name = "Muster & Söhne",
                        Street = "Musterstrasse",
                        HouseNo = "12b",
                        PostalCode = "8200",
                        Town = "Zürich",
                        CountryCode = "CH"
                    };
                    qr.Bill.Debtor = new Address
                    {
                        Name = "Muster AG",
                        Street = "Musterstrasse",
                        HouseNo = "1",
                        PostalCode = "3030",
                        Town = "Bern",
                        CountryCode = "CH"
                    };
                })),
            ("Test2_AnotherPayment", () => TestSwissQR(
                "AnotherPayment",
                qr =>
                {
                    // Configure a second Swiss QR bill with different payment details.
                    qr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
                    qr.Bill.Account = "CH9300762011623852957";
                    qr.Bill.Amount = 199.95m;
                    qr.Bill.Currency = "CHF";
                    qr.Bill.Reference = "210000000000000000000000001";
                    qr.Bill.Creditor = new Address
                    {
                        Name = "John Doe",
                        Street = "Main Street",
                        HouseNo = "10",
                        PostalCode = "8000",
                        Town = "Zurich",
                        CountryCode = "CH"
                    };
                    qr.Bill.Debtor = new Address
                    {
                        Name = "Acme Corp",
                        Street = "Industrial Road",
                        HouseNo = "5",
                        PostalCode = "3000",
                        Town = "Bern",
                        CountryCode = "CH"
                    };
                }))
        };

        int passed = 0;

        // Execute each test and count the passed ones.
        foreach (var (name, test) in tests)
        {
            try
            {
                bool result = test();
                Console.WriteLine($"{name}: {(result ? "PASS" : "FAIL")}");
                if (result) passed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{name}: EXCEPTION - {ex.Message}");
            }
        }

        // Summarize the test run.
        Console.WriteLine($"TOTAL: {passed}/{tests.Count} tests passed.");
    }

    /// <summary>
    /// Generates a Swiss QR code using the provided configuration, saves it to a memory stream,
    /// reads it back, and verifies that the decoded text matches the expected codetext.
    /// </summary>
    /// <param name="testName">Identifier for the test (unused but kept for signature compatibility).</param>
    /// <param name="configure">Action that configures the <see cref="SwissQRCodetext"/> instance.</param>
    /// <returns>True if the decoded codetext matches the expected value; otherwise, false.</returns>
    static bool TestSwissQR(string testName, Action<SwissQRCodetext> configure)
    {
        // Create and configure the Swiss QR code data.
        var swissQRCode = new SwissQRCodetext();
        configure(swissQRCode);
        string expectedCodetext = swissQRCode.GetConstructedCodetext();

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(swissQRCode))
        {
            // Set barcode rendering parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Decode the barcode from the memory stream.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    if (results == null || results.Length == 0)
                        return false; // No barcode detected.

                    string actualCodetext = results[0].CodeText;
                    // Compare the decoded text with the expected codetext.
                    return string.Equals(actualCodetext, expectedCodetext, StringComparison.Ordinal);
                }
            }
        }
    }
}