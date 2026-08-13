// Title: Retrieve QR Code Confidence and Log to Diagnostics File
// Description: Demonstrates generating a QR code, reading it, extracting the BarCodeResult.Confidence enumeration, and writing the value to a diagnostics log.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category, showcasing how to use BarcodeGenerator, BarCodeReader, and BarCodeResult classes. Typical use cases include validating barcode quality, logging confidence levels for diagnostics, and integrating barcode verification into automated workflows. Developers often need to capture confidence metrics to assess scan reliability and troubleshoot scanning issues.
// Prompt: Retrieve BarCodeResult.Confidence after reading a QR code and log the enumeration to a diagnostics file.
// Tags: qr, confidence, barcode, reading, generation, diagnostics, logfile, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a QR code, reads it back, extracts the confidence level,
/// and logs the result to a diagnostics file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code image, reads the barcode, logs the confidence enumeration,
    /// and provides console feedback.
    /// </summary>
    static void Main()
    {
        // Define file paths for the QR image and the diagnostics log.
        string imagePath = "qr.png";
        string logPath = "diagnostics.txt";

        // Clean up any previous run artifacts to ensure a fresh start.
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
        if (File.Exists(logPath))
        {
            File.Delete(logPath);
        }

        // Generate a QR code containing sample text and save it to disk.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            generator.Save(imagePath);
        }

        // Verify that the QR code image was successfully created before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create QR code image.");
            return;
        }

        // Initialize a barcode reader for QR codes and process the generated image.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the confidence enumeration from the read result.
                BarCodeConfidence confidence = result.Confidence;

                // Build a log entry string containing the confidence value.
                string logEntry = $"BarCode Confidence: {confidence}";

                // Append the log entry to the diagnostics file.
                File.AppendAllText(logPath, logEntry + Environment.NewLine);

                // Output the log entry to the console for immediate visibility.
                Console.WriteLine(logEntry);
            }
        }

        // If no barcode was detected, write a warning message to the diagnostics file and console.
        if (!File.Exists(logPath) || new FileInfo(logPath).Length == 0)
        {
            string warning = "No barcode detected or confidence could not be retrieved.";
            File.AppendAllText(logPath, warning + Environment.NewLine);
            Console.WriteLine(warning);
        }
    }
}