// Title: Hide Code 39 checksum digit and verify barcode data
// Description: Demonstrates how to generate a Code 39 barcode with the checksum digit hidden and then read it back to confirm the checksum is not present.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to configure BarcodeGenerator parameters to disable checksum generation for Code 39 (using EncodeTypes.Code39FullASCII) and how to use BarCodeReader with checksum validation enabled to read the barcode. Developers working with one‑dimensional symbologies often need to hide or ignore checksum digits while still being able to validate scanned data.
// Prompt: Configure BarcodeParameters to hide the checksum digit for Code 39 and verify the data excludes it.
// Tags: code39, checksum, hide-checksum, barcode-generation, barcode-recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code 39 barcode without a checksum,
/// reads it back, and displays the extracted data and checksum information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, and cleans up.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "code39.png");

        // ------------------------------------------------------------
        // Generate a Code 39 barcode with checksum generation disabled
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "CODE39"))
        {
            // Turn off checksum generation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Save the generated barcode as a PNG image
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode back and attempt checksum validation
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.Code39FullASCII))
        {
            // Enable checksum validation (will be ignored because no checksum was generated)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (only one expected)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Read CodeText: {result.CodeText}");
                Console.WriteLine($"Extracted CheckSum: {result.Extended.OneD.CheckSum}");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary barcode image file
        // ------------------------------------------------------------
        if (File.Exists(tempPath))
        {
            File.Delete(tempPath);
        }
    }
}