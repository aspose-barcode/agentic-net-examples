// Title: Read QR Code and Log Confidence to Diagnostics File
// Description: Demonstrates reading a QR code with Aspose.BarCode, extracting the Confidence value, and writing the details to a diagnostics text file.
// Category-Description: This example belongs to the Aspose.BarCode reading and diagnostics category. It shows how to use BarcodeGenerator to create a QR code, BarCodeReader to decode it, and how to access BarCodeResult properties such as Confidence and ReadingQuality. Developers working with barcode scanning and quality assessment can use these APIs to log scan results for troubleshooting or analytics.
// Prompt: Retrieve BarCodeResult.Confidence after reading a QR code and log the enumeration to a diagnostics file.
// Tags: qr code, barcode reading, confidence, diagnostics, aspose.barcode, barcodegeneration, barcoderecognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, reads it back, and logs detailed scan information,
/// including the confidence enumeration, to a diagnostics file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, reads it, extracts confidence data, and writes diagnostics.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary folder and file paths for the QR image and diagnostics.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string qrImagePath = Path.Combine(tempFolder, "sample_qr.png");
        string diagnosticsPath = Path.Combine(tempFolder, "diagnostics.txt");

        // --------------------------------------------------------------------
        // Generate a QR code image using BarcodeGenerator.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.BarCode Demo"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size.
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the QR code image and retrieve the Confidence value for each result.
        // --------------------------------------------------------------------
        if (File.Exists(qrImagePath))
        {
            using (BarCodeReader reader = new BarCodeReader(qrImagePath, DecodeType.QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Build a log entry containing all relevant result information.
                    string logEntry = $"File: {qrImagePath}{Environment.NewLine}" +
                                      $"CodeType: {result.CodeTypeName}{Environment.NewLine}" +
                                      $"CodeText: {result.CodeText}{Environment.NewLine}" +
                                      $"Confidence: {result.Confidence}{Environment.NewLine}" +
                                      $"ReadingQuality: {result.ReadingQuality}{Environment.NewLine}" +
                                      $"---{Environment.NewLine}";

                    // Append the entry to the diagnostics file.
                    File.AppendAllText(diagnosticsPath, logEntry);
                }
            }
        }
        else
        {
            // Log an error if the QR image could not be found.
            File.WriteAllText(diagnosticsPath, $"QR image not found at {qrImagePath}{Environment.NewLine}");
        }

        // --------------------------------------------------------------------
        // Inform the user where the diagnostics file has been written.
        // --------------------------------------------------------------------
        Console.WriteLine($"Diagnostics written to: {diagnosticsPath}");
    }
}