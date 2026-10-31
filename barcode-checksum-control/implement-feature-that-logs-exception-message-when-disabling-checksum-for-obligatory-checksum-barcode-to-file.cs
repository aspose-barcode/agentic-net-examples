// Title: Disable checksum handling for Code11 barcode and log exception
// Description: Demonstrates generating a Code11 barcode, attempting to disable its mandatory checksum, catching the resulting exception, and writing the error to a log file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on checksum management. It showcases the use of BarcodeGenerator, EncodeTypes, and related parameter classes to create barcodes, handle obligatory checksum settings, and implement robust error handling. Developers working with barcode symbologies that require checksums often need to validate configuration changes and log issues for diagnostics.
// Prompt: Implement a feature that logs the exception message when disabling checksum for an obligatory‑checksum barcode to a file.
// Tags: code11, checksum, exception handling, logging, aspose.barcode, barcode generation, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation with Aspose.BarCode, attempts to disable a mandatory checksum,
/// captures the exception, and logs it to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code11 barcode, tries to turn off its required checksum,
    /// and records any exception to a log file.
    /// </summary>
    static void Main()
    {
        // Prepare temporary directory and file paths
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeChecksumDemo");
        Directory.CreateDirectory(tempDir);
        string logFile = Path.Combine(tempDir, "checksum_log.txt");
        string barcodePath = Path.Combine(tempDir, "code11.png");

        // --------------------------------------------------------------------
        // Create a barcode with obligatory checksum (Code11) and save it
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            // Set visual parameter: X-dimension in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save a correct barcode (checksum enabled by default)
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Attempt to disable checksum for the same barcode and capture any exception
        // --------------------------------------------------------------------
        try
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Disable checksum (should throw for obligatory checksum symbology)
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Generate barcode image (expected to fail)
                using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
                {
                    // If generation unexpectedly succeeds, save the image
                    string disabledPath = Path.Combine(tempDir, "code11_no_checksum.png");
                    image.Save(disabledPath, Aspose.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
        catch (Exception ex)
        {
            // Log exception message with timestamp to the log file
            string logEntry = $"{DateTime.Now:u} - Exception disabling checksum: {ex.Message}{Environment.NewLine}";
            File.AppendAllText(logFile, logEntry);
        }

        // Output locations for reference
        Console.WriteLine($"Barcode saved to: {barcodePath}");
        Console.WriteLine($"Log file written to: {logFile}");
    }
}