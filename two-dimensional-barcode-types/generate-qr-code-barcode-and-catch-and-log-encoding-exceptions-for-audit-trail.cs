using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image and handling errors with audit logging.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the QR code.
        string codeText = "https://example.com";

        // Define file paths for the generated image and the audit log.
        string imagePath = "qr.png";
        string logPath = "audit.log";

        try
        {
            // Initialize the QR code generator with the specified data.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set the QR code error correction level (optional).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code image to the specified path.
                generator.Save(imagePath);
            }

            // Inform the user that the QR code was generated successfully.
            Console.WriteLine($"QR code generated successfully: {imagePath}");
        }
        catch (Exception ex)
        {
            // Build a detailed log entry with timestamp, error message, and stack trace.
            string logEntry = $"[{DateTime.UtcNow:O}] Error generating QR code: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";

            try
            {
                // Append the log entry to the audit log file.
                File.AppendAllText(logPath, logEntry);
            }
            catch
            {
                // If writing to the log fails, fall back to console output.
                Console.WriteLine("Failed to write to audit log.");
            }

            // Notify the user that an error occurred and refer them to the audit log.
            Console.WriteLine("An error occurred while generating the QR code. See audit log for details.");
        }
    }
}