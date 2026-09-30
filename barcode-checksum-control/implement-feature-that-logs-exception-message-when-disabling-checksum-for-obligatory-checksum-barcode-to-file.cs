// Title: Demonstrate exception logging when disabling mandatory checksum for Code128 barcode
// Description: This example attempts to turn off the checksum for a Code128 barcode, which is required, catches the resulting exception, and logs the error message to a file.
// Category-Description: Shows how to work with Aspose.BarCode generation APIs, specifically handling mandatory checksum constraints, using BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Typical use cases include validating barcode settings, capturing generation errors, and persisting logs for diagnostics. Developers often need to ensure proper error handling when configuring barcode parameters.
// Prompt: Implement a feature that logs the exception message when disabling checksum for an obligatory‑checksum barcode to a file.
// Tags: code128, checksum, exception logging, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation demo that logs errors when disabling a mandatory checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with checksum disabled, captures the exception, and writes the message to a log file.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the barcode image and log file
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // File paths for the generated barcode image and the error log
        string barcodePath = Path.Combine(tempDir, "code128.png");
        string logPath = Path.Combine(tempDir, "checksum_error.log");

        // Initialize a Code128 barcode generator; checksum is mandatory for this symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Attempt to disable the checksum – this will trigger an exception during generation
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            try
            {
                // Save the barcode image (generation occurs here)
                generator.Save(barcodePath, BarCodeImageFormat.Png);
                Console.WriteLine("Barcode generated successfully.");
            }
            catch (Exception ex)
            {
                // Append the exception message with a timestamp to the log file
                File.AppendAllText(logPath, $"[{DateTime.Now}] {ex.Message}{Environment.NewLine}");
                Console.WriteLine("Exception occurred while disabling checksum. Logged to file.");
            }
        }

        // Inform the user where the log file can be found
        Console.WriteLine($"Log file location: {logPath}");
    }
}