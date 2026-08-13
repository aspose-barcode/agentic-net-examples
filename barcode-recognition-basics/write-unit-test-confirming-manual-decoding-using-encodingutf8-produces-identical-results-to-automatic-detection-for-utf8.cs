// Title: UTF-8 QR Barcode Encoding and Decoding Comparison
// Description: Demonstrates generating a QR barcode with UTF-8 text and verifying that automatic encoding detection matches manual UTF-8 decoding.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating QR codes, BarCodeReader for decoding, and the DetectEncoding setting for automatic Unicode handling. Developers working with multilingual data often need to ensure that generated barcodes preserve character encoding and that decoding yields the original text, making this pattern a common requirement in internationalized applications.
// Prompt: Write a unit test confirming manual decoding using Encoding.UTF8 produces identical results to automatic detection for UTF8 barcodes.
// Tags: qr,utf-8,encoding,barcode,generation,recognition,unit-test

using System;
using System.IO;
using System.Text;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a QR barcode containing UTF-8 text and validates that automatic encoding detection
/// yields the same result as manual UTF-8 decoding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, automatic detection, manual decoding,
    /// and prints the verification outcome.
    /// </summary>
    static void Main()
    {
        // Sample UTF-8 text (Cyrillic characters)
        const string originalText = "Привет мир";

        // Create a QR barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Encode the text using UTF-8 (adds BOM if needed)
            generator.SetCodeText(originalText, Encoding.UTF8);

            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // ---------- Automatic detection ----------
                using (var readerAuto = new BarCodeReader(ms, DecodeType.QR))
                {
                    // Enable automatic detection of Unicode encoding
                    readerAuto.BarcodeSettings.DetectEncoding = true;

                    // Read the first barcode found
                    var resultAuto = readerAuto.ReadBarCodes().FirstOrDefault();
                    if (resultAuto == null)
                    {
                        Console.WriteLine("Automatic detection failed: no barcode found.");
                        return;
                    }

                    // Retrieve the automatically decoded text
                    string autoDecoded = resultAuto.CodeText;

                    // Reset stream for the second read
                    ms.Position = 0;

                    // ---------- Manual decoding ----------
                    using (var readerManual = new BarCodeReader(ms, DecodeType.QR))
                    {
                        // Disable automatic detection to force manual decoding
                        readerManual.BarcodeSettings.DetectEncoding = false;

                        // Read the first barcode found
                        var resultManual = readerManual.ReadBarCodes().FirstOrDefault();
                        if (resultManual == null)
                        {
                            Console.WriteLine("Manual decoding failed: no barcode found.");
                            return;
                        }

                        // Manually decode using UTF-8
                        string manualDecoded = resultManual.GetCodeText(Encoding.UTF8);

                        // Verify that both methods produce the same result and match the original text
                        bool isSuccess = autoDecoded == manualDecoded && autoDecoded == originalText;

                        // Output the results
                        Console.WriteLine($"Original text : {originalText}");
                        Console.WriteLine($"Auto decoded  : {autoDecoded}");
                        Console.WriteLine($"Manual decoded: {manualDecoded}");
                        Console.WriteLine(isSuccess
                            ? "Test passed: automatic detection matches manual UTF-8 decoding."
                            : "Test failed: results differ.");
                    }
                }
            }
        }
    }
}