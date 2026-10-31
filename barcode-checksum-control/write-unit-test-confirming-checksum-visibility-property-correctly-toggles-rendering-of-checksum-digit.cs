// Title: Checksum Visibility Toggle Test for Code128 Barcodes
// Description: Demonstrates how to generate Code128 barcodes with and without the checksum digit displayed and verifies the effect using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use the BarcodeGenerator.Parameters.Barcode.ChecksumAlwaysShow property to control checksum rendering, and how to read back the barcode with BarCodeReader. Developers working with barcode validation, quality control, or custom rendering often need to toggle checksum visibility to meet specification requirements.
// Prompt: Write a unit test confirming the checksum visibility property correctly toggles rendering of the checksum digit.
// Tags: code128, checksum, barcode generation, barcode recognition, aspose.barcode, unit test, checksumvisibility

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Code128 barcodes with and without a visible checksum,
/// then reads them back to confirm the checksum visibility toggles the rendered digit.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates two barcodes, reads them, and validates that the checksum digit
    /// appears only when the <c>ChecksumAlwaysShow</c> property is set to true.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for test images
        string tempDir = Path.Combine(Path.GetTempPath(), "ChecksumTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Base data to encode
        string codeText = "12345";

        // Paths for the generated images
        string pathNoShow = Path.Combine(tempDir, "code_no_show.png");
        string pathShow = Path.Combine(tempDir, "code_show.png");

        // Generate barcode without showing checksum
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            gen.Parameters.Barcode.ChecksumAlwaysShow = false; // hide checksum
            gen.Save(pathNoShow, BarCodeImageFormat.Png);
        }

        // Generate barcode with checksum shown
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            gen.Parameters.Barcode.ChecksumAlwaysShow = true; // show checksum
            gen.Save(pathShow, BarCodeImageFormat.Png);
        }

        string decodedNoShow = null;
        string decodedShow = null;

        // Read barcode without checksum shown
        using (BarCodeReader reader = new BarCodeReader(pathNoShow, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                decodedNoShow = result.CodeText;
                break; // only need the first result
            }
        }

        // Read barcode with checksum shown
        using (BarCodeReader reader = new BarCodeReader(pathShow, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                decodedShow = result.CodeText;
                break; // only need the first result
            }
        }

        // Verify that the checksum digit is present only in the second barcode
        bool testPassed = false;
        if (decodedNoShow != null && decodedShow != null)
        {
            testPassed = decodedShow.Length > decodedNoShow.Length && decodedShow.StartsWith(decodedNoShow);
        }

        // Output test result
        if (testPassed)
        {
            Console.WriteLine("PASSED: Checksum visibility toggles rendering of checksum digit.");
        }
        else
        {
            Console.WriteLine("FAILED: Checksum visibility did not affect rendered checksum digit.");
            Console.WriteLine($"Decoded without checksum: {decodedNoShow ?? "null"}");
            Console.WriteLine($"Decoded with checksum: {decodedShow ?? "null"}");
        }
    }
}