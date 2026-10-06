// Title: UTF-8 QR Code Generation and Decoding Comparison
// Description: Generates a QR barcode containing UTF‑8 text, reads it using automatic encoding detection and manual UTF‑8 decoding, and verifies that both methods return identical text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, illustrating how to create QR codes (EncodeTypes.QR) with Unicode content and how to decode them using BarCodeReader. It demonstrates the use of BarcodeGenerator, BarCodeReader, DecodeType, and encoding settings (DetectEncoding) – common tasks for developers needing reliable multi‑language barcode handling.
// Prompt: Write a unit test confirming manual decoding using Encoding.UTF8 produces identical results to automatic detection for UTF8 barcodes.
// Tags: qr, utf-8, encoding, barcode, generation, recognition, unit-test, aspose.barcode

using System;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR code with UTF‑8 text and comparing automatic
/// versus manual decoding results using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR barcode, reads it with automatic and manual
    /// UTF‑8 decoding, and prints the verification outcome.
    /// </summary>
    static void Main()
    {
        const string text = "こんにちは世界"; // UTF-8 sample text
        string autoDecoded = null;
        string manualDecoded = null;

        // Generate a QR barcode containing the UTF-8 text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
            generator.SetCodeText(text, Encoding.UTF8);

            // Render the barcode to a bitmap image
            using (Aspose.Drawing.Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // ---------- Automatic encoding detection ----------
                using (var readerAuto = new BarCodeReader(barcodeImage, DecodeType.QR))
                {
                    readerAuto.BarcodeSettings.DetectEncoding = true; // let the reader detect encoding
                    foreach (BarCodeResult result in readerAuto.ReadBarCodes())
                    {
                        autoDecoded = result.CodeText; // decoded text with automatic detection
                        break; // only need the first result
                    }
                }

                // ---------- Manual decoding with explicit UTF-8 ----------
                using (var readerManual = new BarCodeReader(barcodeImage, DecodeType.QR))
                {
                    readerManual.BarcodeSettings.DetectEncoding = false; // disable auto‑detection
                    foreach (BarCodeResult result in readerManual.ReadBarCodes())
                    {
                        manualDecoded = result.GetCodeText(Encoding.UTF8); // decoded text using UTF-8
                        break; // only need the first result
                    }
                }
            }
        }

        // ---------- Result verification ----------
        if (autoDecoded == null || manualDecoded == null)
        {
            Console.WriteLine("FAILED: Could not read barcode.");
        }
        else if (autoDecoded == manualDecoded)
        {
            Console.WriteLine("PASSED: Automatic and manual decoding results match.");
        }
        else
        {
            Console.WriteLine("FAILED: Decoding results differ.");
            Console.WriteLine($"Automatic: {autoDecoded}");
            Console.WriteLine($"Manual   : {manualDecoded}");
        }
    }
}