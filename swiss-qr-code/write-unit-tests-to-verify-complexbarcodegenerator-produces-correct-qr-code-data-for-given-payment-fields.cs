// Title: Verify ComplexBarcodeGenerator QR code generation for SwissQR payment data
// Description: Demonstrates unit-test style verification that the generated QR code matches the expected SwissQR codetext for minimal and full payment scenarios.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on QR code creation for payment standards such as SwissQR. It showcases the use of ComplexBarcodeGenerator, SwissQRCodetext, and BarCodeReader to encode and decode QR codes, a common task for developers implementing payment QR codes, invoicing, or financial data exchange.
// Prompt: Write unit tests to verify ComplexBarcodeGenerator produces correct QR code data for given payment fields.
// Tags: qr code, payment, swissqr, complexbarcode, generation, decoding, aspose.barcode, unit-test

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains example tests that generate SwissQR QR codes using ComplexBarcodeGenerator
/// and verify the encoded data by decoding the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs two verification scenarios: minimal and full SwissQR data.
    /// </summary>
    static void Main()
    {
        int passed = 0, failed = 0;

        // Helper to execute a test and capture pass/fail status
        void RunTest(string name, Action test)
        {
            try
            {
                test();
                Console.WriteLine($"PASS: {name}");
                passed++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FAIL: {name} - {ex.Message}");
                failed++;
            }
        }

        // ------------------------------------------------------------
        // Test 1: Minimal SwissQR fields
        // ------------------------------------------------------------
        RunTest("SwissQR Minimal", () =>
        {
            // Build minimal SwissQR payload
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = "John Doe";
            swissQr.Bill.Creditor.CountryCode = "CH";
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = 199.95m;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

            // Generate QR code with high error correction
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save QR image to memory stream
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Decode the QR code to verify its content
                    using (var reader = new BarCodeReader(ms, DecodeType.QR))
                    {
                        var results = reader.ReadBarCodes();
                        if (results.Length == 0)
                            throw new InvalidOperationException("No barcode detected.");

                        string decoded = results[0].CodeText;
                        string expected = swissQr.GetConstructedCodetext();

                        if (!string.Equals(decoded, expected, StringComparison.Ordinal))
                            throw new InvalidOperationException($"Decoded text does not match. Expected: {expected}, Got: {decoded}");
                    }
                }
            }
        });

        // ------------------------------------------------------------
        // Test 2: Full SwissQR fields
        // ------------------------------------------------------------
        RunTest("SwissQR Full", () =>
        {
            // Build full SwissQR payload with creditor, debtor, and additional data
            var swissQr = new SwissQRCodetext();
            swissQr.Bill.Creditor.Name = "Acme Corp";
            swissQr.Bill.Creditor.CountryCode = "CH";
            swissQr.Bill.Account = "CH9300762011623852957";
            swissQr.Bill.Amount = 1234.56m;
            swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;
            swissQr.Bill.Creditor.Street = "Musterstrasse 1";
            swissQr.Bill.Creditor.PostalCode = "8000";
            swissQr.Bill.Creditor.Town = "Zürich";
            swissQr.Bill.Debtor.Name = "John Smith";
            swissQr.Bill.Debtor.Street = "Example Ave 5";
            swissQr.Bill.Debtor.PostalCode = "3000";
            swissQr.Bill.Debtor.Town = "Bern";
            swissQr.Bill.Reference = "RF18539007547034";
            swissQr.Bill.UnstructuredMessage = "Invoice 2023-001";

            // Generate QR code with medium error correction
            using (var generator = new ComplexBarcodeGenerator(swissQr))
            {
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Parameters.Barcode.FilledBars = false;
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

                // Save QR image to memory stream
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0;

                    // Decode the QR code to verify its content
                    using (var reader = new BarCodeReader(ms, DecodeType.QR))
                    {
                        var results = reader.ReadBarCodes();
                        if (results.Length == 0)
                            throw new InvalidOperationException("No barcode detected.");

                        string decoded = results[0].CodeText;
                        string expected = swissQr.GetConstructedCodetext();

                        if (!string.Equals(decoded, expected, StringComparison.Ordinal))
                            throw new InvalidOperationException($"Decoded text does not match. Expected: {expected}, Got: {decoded}");
                    }
                }
            }
        });

        // Output summary of test results
        Console.WriteLine($"Summary: {passed} passed, {failed} failed.");
    }
}