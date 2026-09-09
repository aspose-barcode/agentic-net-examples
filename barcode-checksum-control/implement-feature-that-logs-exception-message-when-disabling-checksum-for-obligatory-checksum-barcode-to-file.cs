// Title: Disable checksum on obligatory-checksum barcode and log exception
// Description: Demonstrates attempting to disable the checksum for a Code128 barcode that requires a checksum, catching the resulting exception, and writing the error message to a temporary log file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on checksum manipulation and error handling. It showcases the use of BarcodeGenerator, EncodeTypes, and generator parameters to control checksum behavior, a common task when developers need to validate or customize barcode output. Typical use cases include validating barcode specifications, handling unsupported configurations, and logging errors for diagnostics.
// Prompt: Implement a feature that logs the exception message when disabling checksum for an obligatory‑checksum barcode to a file.
// Tags: barcode symbology, checksum, exception handling, logging, code128, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that attempts to disable the checksum on a Code128 barcode,
/// captures the resulting exception, and logs the error message to a temporary file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the barcode generation attempt
    /// and handles any exceptions by writing them to a log file.
    /// </summary>
    static void Main()
    {
        // Define the path for the log file in the system's temporary folder
        string logFile = Path.Combine(Path.GetTempPath(), "checksum_log.txt");

        // Remove any existing log file to start with a clean slate
        if (File.Exists(logFile))
        {
            File.Delete(logFile);
        }

        // Initialize a barcode generator for Code128, which requires a checksum
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            try
            {
                // Attempt to disable the checksum; this operation is invalid for Code128
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

                // Generate the barcode image to force processing of the parameters
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the generated image to a temporary PNG file (optional)
                    string imagePath = Path.Combine(Path.GetTempPath(), "code128.png");
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        bitmap.Save(fs, Aspose.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
            catch (Exception ex)
            {
                // Append the exception message to the log file for later review
                File.AppendAllText(logFile, ex.Message + Environment.NewLine);
                Console.WriteLine("Exception logged: " + ex.Message);
            }
        }

        // Inform the user that the program has finished and provide the log location
        Console.WriteLine("Program completed. Log file: " + logFile);
    }
}