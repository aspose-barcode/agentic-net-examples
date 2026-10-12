// Title: Verify automatic UTF-8 detection for QR codes with multilingual text
// Description: Demonstrates a simple test that generates a QR code containing English, Chinese, and Arabic characters, then validates Aspose.BarCode's automatic UTF-8 detection during recognition.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to work with BarcodeGenerator, BarCodeReader, and related settings. It is useful for developers needing to ensure correct encoding handling for multi‑language data in QR codes, a common requirement in international applications and data exchange scenarios.
// Prompt: Create a unit test verifying automatic UTF8 detection works for a generated QR code containing multilingual text.
// Tags: qr, utf8, detection, encoding, barcode, generation, recognition, unit-test, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains a self‑contained test that generates a QR code with multilingual content,
/// then checks the behavior of automatic UTF‑8 detection during barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the test program. Generates a QR code, reads it with different
    /// DetectEncoding settings, and reports the results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder and file path for the generated image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Utf8DetectTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "multilingual_qr.png");

        // --------------------------------------------------------------
        // Define multilingual text (English, Chinese, Arabic) to encode
        // --------------------------------------------------------------
        string originalText = "Hello 世界 مرحبا";

        // --------------------------------------------------------------
        // Generate a QR code using UTF‑8 encoding and save it as PNG
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.SetCodeText(originalText, Encoding.UTF8);
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        bool testPassed = true;

        // --------------------------------------------------------------
        // Read the QR code with DetectEncoding = true (automatic UTF‑8 detection)
        // --------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = true;
            foreach (var result in reader.ReadBarCodes())
            {
                if (result.CodeText != originalText)
                {
                    Console.WriteLine("FAILED: DetectEncoding true returned incorrect text.");
                    Console.WriteLine($"Expected: {originalText}");
                    Console.WriteLine($"Actual:   {result.CodeText}");
                    testPassed = false;
                }
                else
                {
                    Console.WriteLine("PASS: DetectEncoding true correctly decoded the text.");
                }
            }
        }

        // --------------------------------------------------------------
        // Read the QR code with DetectEncoding = false (raw byte interpretation)
        // --------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = false;
            foreach (var result in reader.ReadBarCodes())
            {
                if (result.CodeText == originalText)
                {
                    Console.WriteLine("FAILED: DetectEncoding false should not match original text.");
                    testPassed = false;
                }
                else
                {
                    Console.WriteLine("PASS: DetectEncoding false returned different (raw) text as expected.");
                }
            }
        }

        // --------------------------------------------------------------
        // Output overall test result
        // --------------------------------------------------------------
        Console.WriteLine(testPassed ? "ALL TESTS PASSED." : "ONE OR MORE TESTS FAILED.");

        // --------------------------------------------------------------
        // Cleanup temporary files and folder (optional)
        // --------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}