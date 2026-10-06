// Title: Disable checksum for Code128 and log exception
// Description: Demonstrates disabling checksum on a barcode that requires it and logging the resulting exception to a file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on checksum handling. It shows how to configure the BarcodeGenerator, trigger an error when an obligatory checksum is disabled, and capture the exception using standard .NET I/O. Developers working with barcode symbologies that enforce checksums can use this pattern to log errors for diagnostics.
// Prompt: Implement a feature that logs the exception message when disabling checksum for an obligatory‑checksum barcode to a file.
// Tags: code128, checksum, exception-logging, barcode-generation, aspose.barcode, file-io

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that attempts to disable the checksum for a Code128 barcode,
/// catches the resulting exception, and logs the error message to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Sets up logging, generates a barcode with checksum disabled,
    /// and records any exception that occurs.
    /// </summary>
    static void Main()
    {
        // Define the path for the log file in the current working directory.
        string logFile = Path.Combine(Directory.GetCurrentDirectory(), "checksum_error.log");

        // Remove any existing log file to ensure a clean start.
        if (File.Exists(logFile))
            File.Delete(logFile);

        try
        {
            // Create a barcode generator for Code128 with the data "ABC123".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
            {
                // Attempt to disable the checksum, which is mandatory for this symbology.
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Generate the barcode image. An exception is expected before this point.
                using (Bitmap bmp = generator.GenerateBarCodeImage())
                {
                    // Image generation succeeded (unlikely in this scenario).
                }
            }
        }
        catch (Exception ex)
        {
            // Append the exception message to the log file.
            File.AppendAllText(logFile, ex.Message + Environment.NewLine);

            // Inform the user where the exception was logged.
            Console.WriteLine("Exception logged to: " + logFile);
        }
    }
}