using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and verification of a Swiss QR payment barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Swiss QR code, reads it back, and validates the encoded data.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare Swiss QR payment data
        // ------------------------------------------------------------
        var swissQr = new SwissQRCodetext();
        swissQr.Bill.Creditor.Name = "John Doe";
        swissQr.Bill.Creditor.CountryCode = "CH";
        swissQr.Bill.Account = "CH9300762011623852957";
        swissQr.Bill.Amount = 199.95m;
        swissQr.Bill.Version = SwissQRBill.QrBillStandardVersion.V2_0;

        // ------------------------------------------------------------
        // 2. Build the expected codetext string for later comparison
        // ------------------------------------------------------------
        string expectedCodetext = swissQr.GetConstructedCodetext();

        // ------------------------------------------------------------
        // 3. Generate QR code image using ComplexBarcodeGenerator
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(swissQr))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated QR code as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // ------------------------------------------------------------
                // 4. Recognize the QR code from the generated image
                // ------------------------------------------------------------
                using (var reader = new BarCodeReader(ms, DecodeType.QR))
                {
                    var results = reader.ReadBarCodes();

                    // Ensure at least one barcode was detected
                    if (results.Length == 0)
                    {
                        Console.WriteLine("FAIL: No barcode detected.");
                        return;
                    }

                    var result = results[0];
                    string recognizedCodetext = result.CodeText;

                    // ------------------------------------------------------------
                    // 5. Verify that the recognized codetext matches the expected one
                    // ------------------------------------------------------------
                    if (recognizedCodetext == expectedCodetext)
                    {
                        Console.WriteLine("PASS: Recognized QR code matches expected codetext.");
                    }
                    else
                    {
                        Console.WriteLine("FAIL: Mismatch in QR code data.");
                        Console.WriteLine($"Expected: {expectedCodetext}");
                        Console.WriteLine($"Actual  : {recognizedCodetext}");
                    }

                    // ------------------------------------------------------------
                    // 6. Additional verification using ComplexCodetextReader
                    // ------------------------------------------------------------
                    var decoded = ComplexCodetextReader.TryDecodeSwissQR(recognizedCodetext);
                    if (decoded != null && decoded.GetConstructedCodetext() == expectedCodetext)
                    {
                        Console.WriteLine("PASS: ComplexCodetextReader successfully decoded the QR code.");
                    }
                    else
                    {
                        Console.WriteLine("FAIL: ComplexCodetextReader could not decode the QR code correctly.");
                    }
                }
            }
        }
    }
}