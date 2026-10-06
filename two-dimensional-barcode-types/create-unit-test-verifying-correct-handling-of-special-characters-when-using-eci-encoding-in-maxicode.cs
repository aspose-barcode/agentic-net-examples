// Title: MaxiCode ECI Encoding Special Characters Unit Test
// Description: Demonstrates generating a MaxiCode barcode with ECI UTF-8 encoding for Greek characters and verifies correct decoding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on MaxiCode symbology with ECI support. It showcases the use of BarcodeGenerator, BarCodeReader, and related parameter classes to handle non‑ASCII data. Developers often need to ensure that special characters are preserved when encoding and decoding barcodes in international applications.
// Prompt: Create unit test verifying correct handling of special characters when using ECI encoding in MaxiCode.
// Tags: maxicode, eci, encoding, special characters, barcode generation, barcode recognition, png, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode using ECI UTF‑8 encoding,
/// reads it back, and validates that special characters are preserved.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, decodes it, compares the result,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare test data: Greek characters to test ECI handling
        // ------------------------------------------------------------
        string originalText = "ΑΒΓΔΕ"; // Greek characters
        string tempPath = Path.Combine(Path.GetTempPath(), "maxicode_eci_test.png");

        // ------------------------------------------------------------
        // Ensure any previous file is removed to start with a clean state
        // ------------------------------------------------------------
        if (File.Exists(tempPath))
        {
            try { File.Delete(tempPath); } catch { }
        }

        // ------------------------------------------------------------
        // Generate MaxiCode with ECI (UTF‑8) encoding
        // ------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, originalText))
            {
                generator.Parameters.Barcode.MaxiCode.EncodeMode = MaxiCodeEncodeMode.ECI;
                generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;
                generator.Save(tempPath, BarCodeImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception during generation - {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Verify that the barcode image file was created
        // ------------------------------------------------------------
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("FAILED: Barcode image was not created.");
            return;
        }

        // ------------------------------------------------------------
        // Read back the barcode and extract the decoded text
        // ------------------------------------------------------------
        string decodedText = null;
        try
        {
            using (var reader = new BarCodeReader(tempPath, DecodeType.MaxiCode))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    decodedText = result.CodeText;
                    break; // Only need the first result
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"FAILED: Exception during reading - {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Compare the decoded text with the original input
        // ------------------------------------------------------------
        if (decodedText == originalText)
        {
            Console.WriteLine("PASSED: ECI encoding handled special characters correctly.");
        }
        else
        {
            Console.WriteLine($"FAILED: Decoded text does not match. Expected '{originalText}', got '{decodedText ?? "null"}'.");
        }

        // ------------------------------------------------------------
        // Cleanup temporary file
        // ------------------------------------------------------------
        try { File.Delete(tempPath); } catch { }
    }
}