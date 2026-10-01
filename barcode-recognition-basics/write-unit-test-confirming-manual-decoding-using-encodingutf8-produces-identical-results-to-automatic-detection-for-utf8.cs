// Title: UTF-8 QR Code Generation and Decoding Comparison
// Description: Demonstrates generating a QR barcode with UTF‑8 encoding and verifying that manual decoding using Encoding.UTF8 yields the same text as automatic encoding detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a QR code with explicit ECI UTF‑8 encoding, and BarCodeReader to read the barcode both with automatic encoding detection and with manual UTF‑8 decoding. Developers working with multilingual data often need to ensure that automatic detection and manual decoding produce identical results, making this pattern useful for unit testing and validation scenarios.
// Prompt: Write a unit test confirming manual decoding using Encoding.UTF8 produces identical results to automatic detection for UTF8 barcodes.
// Tags: qr code,utf-8,encoding,barcode generation,barcode recognition,aspose.barcode,unit test

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a QR barcode encoded in UTF‑8, reads it using both automatic detection and manual decoding,
/// and compares the results to demonstrate that they are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, reading, comparison, and cleanup.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeUtf8Test_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "utf8_qr.png");

        // ------------------------------------------------------------
        // Define the original text containing Unicode characters
        // ------------------------------------------------------------
        string originalText = "Привет мир";

        // ------------------------------------------------------------
        // Generate a QR barcode with explicit UTF‑8 (ECI) encoding
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, originalText))
        {
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.ECI;
            generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Automatic detection (default DetectEncoding = true)
        // ------------------------------------------------------------
        string autoDecoded = null;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = true;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0)
                autoDecoded = results[0].CodeText;
        }

        // ------------------------------------------------------------
        // Manual decoding using UTF‑8 (DetectEncoding = false)
        // ------------------------------------------------------------
        string manualDecoded = null;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            reader.BarcodeSettings.DetectEncoding = false;
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length > 0 && results[0].CodeBytes != null)
                manualDecoded = Encoding.UTF8.GetString(results[0].CodeBytes);
        }

        // ------------------------------------------------------------
        // Compare the two results for equality
        // ------------------------------------------------------------
        bool identical = string.Equals(autoDecoded, manualDecoded, StringComparison.Ordinal);
        Console.WriteLine("Original text : " + originalText);
        Console.WriteLine("Auto decoded  : " + (autoDecoded ?? "null"));
        Console.WriteLine("Manual decoded: " + (manualDecoded ?? "null"));
        Console.WriteLine("Results are identical: " + (identical ? "YES" : "NO"));

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test outcome
        }
    }
}