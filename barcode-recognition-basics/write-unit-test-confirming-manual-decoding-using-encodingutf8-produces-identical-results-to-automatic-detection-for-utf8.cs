// Title: UTF-8 Barcode Decoding Comparison Test
// Description: Demonstrates generating a QR code with UTF‑8 text and verifies that manual decoding using Encoding.UTF8 yields the same result as automatic encoding detection.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create a QR code, control text encoding, and read it back. It highlights the use of BarcodeGenerator for encoding, BarCodeReader for detection, and the interplay between automatic encoding detection and manual UTF‑8 decoding—common tasks when handling multilingual barcodes in .NET applications.
// Prompt: Write a unit test confirming manual decoding using Encoding.UTF8 produces identical results to automatic detection for UTF8 barcodes.
// Tags: qr, utf-8, encoding, barcode, generation, recognition, unit-test, aspose.barcode

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a QR code with UTF‑8 text, reads it back using both automatic and manual decoding,
/// and validates that the decoded values are identical.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, recognition, comparison, and cleanup steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder and file path for the barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Utf8BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "utf8_qr.png");

        // --------------------------------------------------------------
        // Define the Unicode text that will be encoded into the QR code.
        // --------------------------------------------------------------
        string unicodeText = "Aspose常に先を行";

        // --------------------------------------------------------------
        // Generate a QR code using UTF‑8 encoding (manual encoding).
        // --------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 8;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;
            generator.SetCodeText(unicodeText, Encoding.UTF8);
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = unicodeText;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // --------------------------------------------------------------
        // Read the barcode with automatic encoding detection enabled.
        // --------------------------------------------------------------
        string autoDecoded = null;
        using (BarCodeReader readerAuto = new BarCodeReader(imagePath, DecodeType.QR))
        {
            readerAuto.BarcodeSettings.DetectEncoding = true;
            foreach (BarCodeResult result in readerAuto.ReadBarCodes())
            {
                autoDecoded = result.CodeText;
                break; // Only one barcode is expected.
            }
        }

        // --------------------------------------------------------------
        // Read the same barcode with automatic detection disabled and decode manually using UTF‑8.
        // --------------------------------------------------------------
        string manualDecoded = null;
        using (BarCodeReader readerManual = new BarCodeReader(imagePath, DecodeType.QR))
        {
            readerManual.BarcodeSettings.DetectEncoding = false;
            foreach (BarCodeResult result in readerManual.ReadBarCodes())
            {
                manualDecoded = result.GetCodeText(Encoding.UTF8);
                break; // Only one barcode is expected.
            }
        }

        // --------------------------------------------------------------
        // Compare the two decoded strings and output the test result.
        // --------------------------------------------------------------
        if (autoDecoded == null || manualDecoded == null)
        {
            Console.WriteLine("FAILED: Could not read barcode.");
        }
        else if (autoDecoded == manualDecoded)
        {
            Console.WriteLine("PASS: Manual UTF8 decoding matches automatic detection.");
        }
        else
        {
            Console.WriteLine("FAIL: Mismatch between manual and automatic decoding.");
            Console.WriteLine($"Automatic: {autoDecoded}");
            Console.WriteLine($"Manual   : {manualDecoded}");
        }

        // --------------------------------------------------------------
        // Clean up temporary files and directories.
        // --------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup failures are non‑critical for the test outcome.
        }
    }
}