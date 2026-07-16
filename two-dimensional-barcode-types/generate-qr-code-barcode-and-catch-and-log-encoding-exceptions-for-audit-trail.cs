// Title: Generate QR Code and Log Encoding Exceptions
// Description: Demonstrates creating a QR Code barcode, handling potential encoding errors, and logging outcomes for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include generating QR codes for URLs or data payloads while capturing exceptions for compliance and audit trails. Developers often need to log successes and failures when integrating barcode creation into automated workflows.
// Prompt: Generate QR Code barcode and catch and log encoding exceptions for audit trail.
// Tags: qr code, barcode generation, exception handling, logging, aspose.barcode, encode types, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode, handles encoding exceptions,
/// and logs the results for audit tracking.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code, saves it as PNG,
    /// and records success or failure in an audit log.
    /// </summary>
    static void Main()
    {
        // Define output file names
        const string outputFile = "qr.png";
        const string logFile = "audit.log";

        // Text to encode in the QR Code
        string codeText = "https://example.com";

        try
        {
            // Initialize the barcode generator for QR Code with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Optional: set QR error correction level to Medium
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code image as PNG
                generator.Save(outputFile, BarCodeImageFormat.Png);
            }

            // Log successful generation with UTC timestamp
            LogMessage(logFile, $"[{DateTime.UtcNow:u}] QR code generated successfully: {outputFile}");
        }
        catch (BarCodeException ex)
        {
            // Log encoding-related exceptions specific to Aspose.BarCode
            LogMessage(logFile, $"[{DateTime.UtcNow:u}] BarCodeException: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Log any other unexpected exceptions
            LogMessage(logFile, $"[{DateTime.UtcNow:u}] Unexpected exception: {ex.Message}");
        }
    }

    /// <summary>
    /// Appends a log entry to the specified file. If logging fails, writes the message to the console.
    /// </summary>
    /// <param name="logPath">Path to the audit log file.</param>
    /// <param name="message">Message to log.</param>
    private static void LogMessage(string logPath, string message)
    {
        try
        {
            // Append the message with a newline to the log file
            File.AppendAllText(logPath, message + Environment.NewLine);
        }
        catch
        {
            // Fallback: output to console if file logging fails
            Console.WriteLine("Logging failed: " + message);
        }
    }
}