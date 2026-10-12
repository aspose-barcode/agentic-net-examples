// Title: Generate QR Code and Log Encoding Exceptions
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, intentionally causing an encoding error, and logging the exception for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and error handling. It showcases the use of BarcodeGenerator, EncodeTypes, and QR-specific parameters such as QREncodeMode and ThrowExceptionWhenCodeTextIncorrect. Developers often need to capture generation failures to maintain audit trails or troubleshoot invalid input data.
// Prompt: Generate QR Code barcode and catch and log encoding exceptions for audit trail.
// Tags: qr, barcode, generation, exception handling, logging, aspose.barcode, qrcode, binary mode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR Code generation with forced encoding error and logs exceptions for audit.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR Code, forces an encoding exception, and writes details to a log file.
    /// </summary>
    static void Main()
    {
        // Define output directory in the temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");
        Directory.CreateDirectory(outputDir);

        // Paths for the generated barcode image and the audit log file
        string barcodePath = Path.Combine(outputDir, "qr_invalid_binary.png");
        string logPath = Path.Combine(outputDir, "audit.log");

        try
        {
            // Initialize the barcode generator for QR Code with Unicode text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "テスト"))
            {
                // Configure generator to throw an exception when the code text is invalid for the selected mode
                generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                // Set QR encoding mode to Binary, which will cause an exception for Unicode characters
                generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Binary;

                // Attempt to save the barcode image; this will trigger the exception
                generator.Save(barcodePath, BarCodeImageFormat.Png);
                Console.WriteLine($"QR code generated successfully: {barcodePath}");
            }
        }
        catch (Exception ex)
        {
            // Build a timestamped error message
            string message = $"[{DateTime.UtcNow:u}] QR generation error: {ex.Message}{Environment.NewLine}";
            Console.WriteLine(message);

            // Attempt to append the error details to the audit log; suppress any logging failures
            try
            {
                File.AppendAllText(logPath, message);
            }
            catch
            {
                // Intentionally ignore logging errors to avoid secondary failures
            }
        }
    }
}