// Title: Code128 Checksum Handling Example
// Description: Demonstrates how disabling the checksum on a mandatory‑checksum symbology (Code 128) throws an exception, and how to generate a barcode with checksum enabled.
// Prompt: Implement exception handling for disabling checksum on an obligatory‑checksum symbology like Code 128.
// Tags: barcode symbology, checksum, code128, exception handling, aspnet barcodes, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows exception handling when attempting to disable the checksum
/// on a symbology (Code 128) that requires a checksum, and then generates a valid barcode
/// with the checksum enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare output directory
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Define file paths for the two barcode images
        // ------------------------------------------------------------
        // Expected to fail because checksum is disabled on Code 128
        string disabledPath = Path.Combine(outputDir, "code128_disabled.png");
        // Expected to succeed with checksum enabled (default behavior)
        string enabledPath = Path.Combine(outputDir, "code128_enabled.png");

        // ------------------------------------------------------------
        // Attempt to generate Code 128 barcode with checksum disabled
        // ------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Disable checksum for an obligatory‑checksum symbology
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                generator.Save(disabledPath);
                Console.WriteLine($"Barcode saved (checksum disabled) to: {disabledPath}");
            }
        }
        catch (Exception ex)
        {
            // Expected exception: checksum cannot be disabled for Code 128
            Console.WriteLine($"Exception while disabling checksum: {ex.Message}");
        }

        // ------------------------------------------------------------
        // Generate Code 128 barcode with checksum enabled (default)
        // ------------------------------------------------------------
        try
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Ensure checksum is enabled
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(enabledPath);
                Console.WriteLine($"Barcode saved (checksum enabled) to: {enabledPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception while generating barcode with checksum: {ex.Message}");
        }
    }
}