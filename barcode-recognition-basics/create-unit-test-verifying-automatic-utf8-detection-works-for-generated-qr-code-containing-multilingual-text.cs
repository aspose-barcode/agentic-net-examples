// Title: Verify automatic UTF-8 detection for QR code with multilingual text
// Description: This example generates a QR code containing Arabic and Japanese characters, saves it as PNG, and tests whether the Aspose.BarCode reader automatically detects UTF-8 encoding.
// Category-Description: Demonstrates Aspose.BarCode QR code generation and recognition focusing on encoding detection. It uses BarcodeGenerator, BarCodeReader, and related settings to show typical use cases where developers need to handle multilingual data and verify automatic charset detection in QR codes.
// Prompt: Create a unit test verifying automatic UTF8 detection works for a generated QR code containing multilingual text.
// Tags: qr code, utf8 detection, multilingual, barcode generation, barcode recognition, aspose.barcode, unit test

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a QR code with multilingual text and verifies automatic UTF-8 detection during recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR code, saves it, and checks detection with DetectEncoding enabled and disabled.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder and file path for the generated QR code image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Utf8DetectTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "multilingual_qr.png");

        // Multilingual text containing Arabic and Japanese characters
        string multilingualText = "بالقمة Aspose こんにちは";

        // Generate a QR code using UTF-8 encoding and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.SetCodeText(multilingualText, Encoding.UTF8);
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the QR code image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("FAILED: QR code image was not created.");
            return;
        }

        // ---------- Test with DetectEncoding = true ----------
        bool successDetectTrue = false;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Enable automatic encoding detection
            reader.BarcodeSettings.DetectEncoding = true;
            BarCodeResult[] results = reader.ReadBarCodes();

            // Success if a result is returned and the decoded text is not empty
            successDetectTrue = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine($"DetectEncoding true: {(successDetectTrue ? "Success" : "Failure")}");
        }

        // ---------- Test with DetectEncoding = false ----------
        bool successDetectFalse = false;
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Disable automatic encoding detection
            reader.BarcodeSettings.DetectEncoding = false;
            BarCodeResult[] results = reader.ReadBarCodes();

            // Success if a result is returned and the decoded text is not empty
            successDetectFalse = results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
            Console.WriteLine($"DetectEncoding false: {(successDetectFalse ? "Success" : "Failure")}");
        }

        // ---------- Summary ----------
        if (successDetectTrue && !successDetectFalse)
        {
            Console.WriteLine("TEST PASSED: Automatic UTF8 detection works as expected.");
        }
        else
        {
            Console.WriteLine("TEST FAILED: Unexpected detection results.");
        }
    }
}