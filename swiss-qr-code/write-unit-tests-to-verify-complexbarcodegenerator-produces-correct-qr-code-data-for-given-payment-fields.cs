// Title: Generate and Verify Swiss QR Code using ComplexBarcodeGenerator
// Description: Demonstrates creating a Swiss QR bill barcode with Aspose.BarCode, saving it to a PNG stream, and validating the encoded data by reading it back.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the ComplexBarcodeGenerator, SwissQRCodetext, and BarCodeReader classes for QR code creation, error correction configuration, and data verification—common tasks for developers implementing payment QR codes or other structured data barcodes.
// Prompt: Write unit tests to verify ComplexBarcodeGenerator produces correct QR code data for given payment fields.
// Tags: qr, swiss, barcode, generation, recognition, complexbarcode, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Swiss QR code and verifying its content using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that runs the Swiss QR code verification test.
    /// </summary>
    static void Main()
    {
        // Execute the test that generates a Swiss QR code and validates its data.
        RunSwissQrTest();
    }

    static void RunSwissQrTest()
    {
        // Define expected payment data for the Swiss QR bill.
        const string creditorName = "John Doe";
        const string creditorCountryCode = "CH";
        const string account = "CH9300762011623852957";
        const decimal amount = 199.95m;
        const SwissQRBill.QrBillStandardVersion version = SwissQRBill.QrBillStandardVersion.V2_0;

        // Build the Swiss QR codetext using the provided payment details.
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = creditorName;
        swissQr.Bill.Creditor.CountryCode = creditorCountryCode;
        swissQr.Bill.Account = account;
        swissQr.Bill.Amount = amount;
        swissQr.Bill.Version = version;

        // Construct the expected plain codetext string from the SwissQRCodetext object.
        string expectedCodeText = swissQr.GetConstructedCodetext();

        // Generate the QR barcode image and write it to a memory stream.
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            // Optional: set a high error correction level for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reading.

                // Read the barcode back from the memory stream to verify its content.
                using (var reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    var results = reader.ReadBarCodes();

                    // Ensure at least one barcode was detected.
                    if (results.Length == 0)
                    {
                        Console.WriteLine("FAILED: No barcode detected.");
                        return;
                    }

                    // Retrieve the decoded codetext from the first result.
                    string actualCodeText = results[0].CodeText;

                    // Compare the generated codetext with the expected codetext.
                    if (actualCodeText == expectedCodeText)
                    {
                        Console.WriteLine("PASSED: Generated QR code matches expected codetext.");
                    }
                    else
                    {
                        Console.WriteLine("FAILED: Mismatch in QR code data.");
                        Console.WriteLine($"Expected: {expectedCodeText}");
                        Console.WriteLine($"Actual  : {actualCodeText}");
                    }
                }
            }
        }
    }
}