// Title: Checksum Validation Demo for Optional Checksum Symbology (Code39)
// Description: Demonstrates generating a Code39 barcode without a checksum and then reading it with checksum validation enabled, showing how validation failures are handled.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them, focusing on optional checksum symbologies. Developers often need to enable checksum validation to ensure data integrity; this snippet illustrates typical error‑handling patterns when validation fails.
// Prompt: Implement error handling for checksum failures when ChecksumValidation.On is set for optional checksum symbologies.
// Tags: barcode symbology, checksum validation, code39, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation without a checksum and subsequent reading with checksum validation enabled,
/// handling cases where validation fails for optional checksum symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code39 barcode, attempts to read it with checksum validation,
    /// and reports success or validation failure.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: create a temporary folder to store the generated barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string barcodePath = Path.Combine(tempFolder, "code39_no_checksum.png");

        // --------------------------------------------------------------
        // Generation: create a Code39 barcode with checksum disabled
        // --------------------------------------------------------------
        try
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
            {
                // Disable checksum generation for Code39 (optional checksum)
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                // Do not show checksum in human‑readable text (it is absent)
                generator.Parameters.Barcode.ChecksumAlwaysShow = false;

                // Save the barcode image to the temporary location
                generator.Save(barcodePath);
                Console.WriteLine($"Barcode saved to: {barcodePath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during barcode generation: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------
        // Recognition: read the barcode with checksum validation turned ON
        // --------------------------------------------------------------
        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code39))
            {
                // Enable checksum validation – for optional checksum symbologies this will
                // cause the reader to reject barcodes whose checksum does not match.
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    // No results indicates that checksum validation failed (or barcode not found)
                    Console.WriteLine("Checksum validation failed: barcode was ignored.");
                }
                else
                {
                    // Successful decode – output decoded text and symbology type
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"Decoded text: {result.CodeText}");
                        Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during barcode reading: {ex.Message}");
        }
        finally
        {
            // --------------------------------------------------------------
            // Cleanup: delete temporary barcode image and folder
            // --------------------------------------------------------------
            if (File.Exists(barcodePath))
            {
                try { File.Delete(barcodePath); } catch { /* ignore cleanup errors */ }
            }
            try { Directory.Delete(tempFolder, true); } catch { /* ignore cleanup errors */ }
        }
    }
}