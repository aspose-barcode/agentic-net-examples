// Title: Hide checksum digit for Code 39 barcode and verify data
// Description: Demonstrates how to generate a Code 39 barcode without displaying the checksum digit and then verifies that the decoded data does not contain a checksum.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to configure barcode parameters (e.g., disabling checksum calculation and hiding the checksum in human‑readable text) and BarCodeReader to decode and validate the result. Typical use cases include creating barcodes for systems that do not require checksum display and confirming that the encoded data matches expectations. Developers often work with EncodeTypes, BarCodeImageFormat, DecodeType, and checksum‑related settings when handling 1D symbologies.
// Prompt: Configure BarcodeParameters to hide the checksum digit for Code 39 and verify the data excludes it.
// Tags: code39, checksum, hide-checksum, barcode-generation, barcode-recognition, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code 39 barcode with the checksum hidden, then reads it back to confirm the checksum is absent.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode image, decodes it, and validates that no checksum digit is present.
    /// </summary>
    static void Main()
    {
        // Prepare temporary file paths
        string tempDir = Path.GetTempPath();
        string barcodePath = Path.Combine(tempDir, "code39.png");

        // ------------------------------------------------------------
        // Generate Code 39 barcode without checksum and without showing it
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE"))
        {
            // Disable checksum calculation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Ensure checksum is not displayed in the human‑readable text
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode and verify that no checksum digit is present
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code39FullASCII))
        {
            // Use default checksum validation (no validation for optional checksum)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum from barcode (if any): {result.Extended.OneD.CheckSum ?? "None"}");

                // Verify that the decoded text matches the original string without a checksum digit
                if (result.CodeText == "CODE")
                {
                    Console.WriteLine("Verification passed: No checksum digit present.");
                }
                else
                {
                    Console.WriteLine("Verification failed: Unexpected checksum digit.");
                }
            }
        }

        // Clean up the generated barcode image file
        if (File.Exists(barcodePath))
        {
            File.Delete(barcodePath);
        }
    }
}