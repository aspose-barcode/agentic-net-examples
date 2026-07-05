// Title: Disable checksum on mandatory-checksum barcode and log exception
// Description: Demonstrates attempting to disable checksum for an EAN‑13 barcode where checksum is required, catching the resulting exception, and writing the error message to a log file.
// Prompt: Implement a feature that logs the exception message when disabling checksum for an obligatory‑checksum barcode to a file.
// Tags: barcode, ean13, checksum, exception handling, file logging, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that tries to disable the checksum on an EAN‑13 barcode (where checksum is mandatory),
/// catches the resulting exception, and logs the exception message to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define output file paths
        string barcodePath = Path.Combine(Environment.CurrentDirectory, "ean13_no_checksum.png");
        string logPath = Path.Combine(Environment.CurrentDirectory, "checksum_error_log.txt");

        try
        {
            // Create a barcode generator for EAN‑13 (checksum is mandatory)
            using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
            {
                // Attempt to disable checksum – this should raise an exception
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Save the barcode (this line will not be reached if an exception is thrown)
                generator.Save(barcodePath);
            }

            // If no exception occurs, write a success message to the log file
            File.WriteAllText(logPath, "No exception was thrown when disabling checksum.");
        }
        catch (Exception ex)
        {
            // Write the exception details to the log file
            using (var writer = new StreamWriter(logPath, false))
            {
                writer.WriteLine("Exception while disabling checksum:");
                writer.WriteLine(ex.Message);
            }
        }
    }
}