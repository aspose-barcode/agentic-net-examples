// Title: Generate QR Code and Log Encoding Exceptions
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as a PNG file, and handling any encoding errors by logging them for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeException to produce QR Code images, configure error‑correction levels, and implement robust exception handling. Developers working on automated barcode creation, reporting, or audit trails commonly use these APIs to ensure reliable output and traceability.
// Prompt: Generate QR Code barcode and catch and log encoding exceptions for audit trail.
// Tags: qr, barcode, generation, exception, logging, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR Code barcode and logs any encoding exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates QR Code, saves to file, and logs exceptions.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the text to encode and the output image path
        string codeText = "Sample QR Code";
        string outputPath = "qr.png";

        try
        {
            // Initialize the QR Code generator with the appropriate symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Assign the data to be encoded
                generator.CodeText = codeText;

                // Optional: configure the QR Code error‑correction level (Level M)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Render and save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR code generated and saved to '{outputPath}'.");
            }
        }
        // Capture specific barcode generation errors
        catch (BarCodeException ex)
        {
            LogException(ex);
        }
        // Capture any other unexpected errors
        catch (Exception ex)
        {
            LogException(ex);
        }
    }

    /// <summary>
    /// Writes exception details to the console and appends them to an audit log file.
    /// </summary>
    /// <param name="ex">The exception to log.</param>
    static void LogException(Exception ex)
    {
        string logPath = "audit.log";
        string message = $"{DateTime.UtcNow:u} - Exception: {ex.GetType().Name} - {ex.Message}{Environment.NewLine}";
        Console.WriteLine(message);
        try
        {
            // Append the formatted message to the audit log
            File.AppendAllText(logPath, message);
        }
        catch
        {
            // Suppress any failures while writing to the log to avoid secondary errors
        }
    }
}