// Title: QR Code Generation, Reading, and Confidence Logging
// Description: Generates a QR code image, reads it back, extracts the confidence level, and writes the value to a diagnostics file.
// Prompt: Retrieve BarCodeResult.Confidence after reading a QR code and log the enumeration to a diagnostics file.
// Tags: qr, barcode, confidence, diagnostics, generation, recognition, aspose

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a QR code, read it, retrieve the confidence level,
/// and log the result to a diagnostics file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it, and logs the confidence.
    /// </summary>
    static void Main()
    {
        const string imagePath = "qr.png";
        const string diagnosticsPath = "diagnostics.txt";

        // ------------------------------------------------------------
        // Generate a QR code image and save it to disk.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            // Set a moderate error correction level (optional).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code as a PNG file.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the QR code image was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Log an error message if the image file is missing.
            File.WriteAllText(diagnosticsPath, "Error: QR code image was not created." + Environment.NewLine);
            return;
        }

        // ------------------------------------------------------------
        // Read the QR code from the image and retrieve the confidence level.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Build a log entry containing the confidence enumeration value.
                string logEntry = $"Confidence: {result.Confidence}";

                // Append the confidence information to the diagnostics file.
                File.AppendAllText(diagnosticsPath, logEntry + Environment.NewLine);
            }
        }
    }
}