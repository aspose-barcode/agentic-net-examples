// Title: Read QR Code and Log Confidence Value
// Description: Generates a QR code, reads it back, extracts the confidence level of the detection, and writes the value to a diagnostics log file.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition operations. It uses BarcodeGenerator to create a QR code image and BarCodeReader to decode it, retrieving BarCodeResult.Confidence. Developers working with barcode scanning often need to assess detection reliability, log diagnostic information, and handle temporary files. The key API classes shown are BarcodeGenerator, BarCodeReader, BarCodeResult, and related parameter classes, which are common in QR code processing scenarios.
/// Prompt: Retrieve BarCodeResult.Confidence after reading a QR code and log the enumeration to a diagnostics file.
/// Tags: qr code, barcode, confidence, diagnostics, logging, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR code, reading it, and logging the confidence level of the detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code image, reads it, extracts the confidence value,
    /// and writes the result to a diagnostics log file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the QR image and the diagnostics log
        string qrImagePath = Path.Combine(tempDir, "qr.png");
        string logPath = Path.Combine(tempDir, "diagnostics.log");

        // Generate a QR code image using BarcodeGenerator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size
            generator.Save(qrImagePath, BarCodeImageFormat.Png);
        }

        // Verify that the QR code image was created successfully
        if (!File.Exists(qrImagePath))
        {
            Console.WriteLine("QR code image not found: " + qrImagePath);
            return;
        }

        // Read the QR code and log each result's confidence value
        using (BarCodeReader reader = new BarCodeReader(qrImagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                string logEntry = $"Confidence: {result.Confidence}{Environment.NewLine}";
                File.AppendAllText(logPath, logEntry); // Append to diagnostics log
                Console.WriteLine(logEntry.Trim());   // Output to console for immediate feedback
            }
        }

        // Inform the user where the diagnostics file is located
        Console.WriteLine("Diagnostics written to: " + logPath);
    }
}