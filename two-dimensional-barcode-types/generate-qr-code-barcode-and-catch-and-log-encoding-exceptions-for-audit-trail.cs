// Title: Generate QR Code and Log Encoding Exceptions
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, handling encoding errors, and writing audit logs.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and exception handling. It showcases the BarcodeGenerator class, EncodeTypes, and QR encoding settings, which developers use to produce QR images and maintain audit trails for failures. Ideal for scenarios requiring reliable barcode output with error logging for compliance or debugging.
// Prompt: Generate QR Code barcode and catch and log encoding exceptions for audit trail.
// Tags: qr code, barcode generation, exception handling, logging, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation with exception handling and audit logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR Code, saves it as PNG, and logs any encoding errors.
    /// </summary>
    static void Main()
    {
        // Define paths for the output image and the audit log file in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_code.png");
        string logPath = Path.Combine(Path.GetTempPath(), "qr_audit_log.txt");

        try
        {
            // Create a BarcodeGenerator for QR encoding with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "こんにちは"))
            {
                // Enable exception throwing for invalid code text.
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Set QR encoding mode to binary to handle non‑ASCII characters.
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Save the generated QR code as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR code saved to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Build a log entry with UTC timestamp and error details.
            string logEntry = $"[{DateTime.UtcNow:u}] QR generation error: {ex.Message}{Environment.NewLine}";

            // Append the log entry to the audit log file.
            File.AppendAllText(logPath, logEntry);
            Console.WriteLine("An error occurred during QR code generation. Details logged.");
        }
    }
}