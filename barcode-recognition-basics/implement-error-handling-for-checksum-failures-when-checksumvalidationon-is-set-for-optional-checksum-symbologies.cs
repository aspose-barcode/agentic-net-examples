// Title: Checksum Validation Failure Handling for Optional Checksum Symbologies
// Description: Demonstrates how to detect and handle checksum validation failures when reading a Code39 barcode generated without a checksum.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on checksum validation for optional checksum symbologies such as Code39. It showcases the use of BarcodeGenerator, BarCodeReader, and related settings like IsChecksumEnabled and ChecksumValidation. Developers often need to ensure data integrity by enabling checksum validation during decoding and handling cases where the checksum is missing or incorrect.
// Prompt: Implement error handling for checksum failures when ChecksumValidation.On is set for optional checksum symbologies.
// Tags: barcode symbology, checksum validation, code39, generation, recognition, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code39 barcode without a checksum,
/// then attempts to read it with checksum validation enabled to demonstrate error handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, validates its existence,
    /// reads it with checksum validation turned on, and handles possible checksum failures.
    /// </summary>
    static void Main()
    {
        // Path where the generated barcode image will be saved
        string barcodePath = "code39.png";

        // ------------------------------------------------------------
        // Generate a Code39 barcode without an optional checksum
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            // Disable checksum generation for this optional checksum symbology
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Save the barcode image to the specified file
            generator.Save(barcodePath);
        }

        // ------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{barcodePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with checksum validation enabled
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code39))
        {
            // Turn on checksum validation for optional checksum symbologies
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Attempt to decode the barcode(s) in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no results are returned, checksum validation likely failed
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("Checksum validation failed: barcode could not be recognized.");
            }
            else
            {
                // Process each recognized barcode (unexpected when checksum is invalid)
                foreach (var result in results)
                {
                    Console.WriteLine($"BarCode Type: {result.CodeType}");
                    Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                    // For 1D barcodes, the detected checksum (if any) is available here
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"Detected Checksum: {result.Extended.OneD.CheckSum}");
                    }
                }
            }
        }
    }
}