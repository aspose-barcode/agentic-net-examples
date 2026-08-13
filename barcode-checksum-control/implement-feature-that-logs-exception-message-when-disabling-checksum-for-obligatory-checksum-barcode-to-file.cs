// Title: Disabling checksum on a mandatory‑checksum barcode and logging the exception
// Description: Demonstrates how attempting to disable the checksum for a Code128 barcode throws an exception, and logs the error to a file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on barcode symbology configuration and error handling. It uses BarcodeGenerator and related parameter classes to illustrate typical use cases where developers need to validate barcode settings and capture configuration errors. The snippet shows how to log exceptions for troubleshooting in automated pipelines.
// Prompt: Implement a feature that logs the exception message when disabling checksum for an obligatory‑checksum barcode to a file.
// Tags: code128, checksum, exception-logging, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that attempts to disable the checksum for a Code128 barcode,
/// catches the resulting exception, and writes the error details to a log file.
/// </summary>
class Program
{
    /// <summary>
    /// Main entry point. Generates a barcode, handles configuration errors, and logs them.
    /// </summary>
    static void Main()
    {
        // Path to the log file where exception messages will be recorded
        string logFilePath = "checksum_error_log.txt";

        // Sample barcode text (Code128 requires a checksum)
        string codeText = "123456";

        // Attempt to generate a Code128 barcode with checksum disabled
        try
        {
            // Create a barcode generator for Code128 with the provided text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Disable checksum – this is not allowed for Code128 and will throw an exception
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // If no exception occurs (unexpected), save the barcode image
                generator.Save("code128.png");
            }
        }
        catch (Exception ex)
        {
            // Build a log entry with timestamp and exception message
            string logEntry = $"{DateTime.Now:u} - Exception: {ex.Message}{Environment.NewLine}";

            // Append the log entry to the designated log file
            File.AppendAllText(logFilePath, logEntry);

            // Write error information to the console for immediate feedback
            Console.WriteLine("An error occurred while disabling checksum:");
            Console.WriteLine(ex.Message);
        }

        // Indicate program completion
        Console.WriteLine("Program finished. Check the log file for details if an error occurred.");
    }
}