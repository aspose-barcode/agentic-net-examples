// Title: Barcode Generation, Decoding, and Logging Demo
// Description: Demonstrates generating sample barcodes, decoding them, and logging each attempt with timestamps and file paths.
// Category-Description: This example belongs to the Aspose.BarCode barcode processing category, showcasing how to use BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and handling checksum validation. Typical use cases include batch barcode generation, automated scanning verification, and audit logging. Developers often need to log decoding attempts to trace issues in production environments.
// Prompt: Implement logging of barcode decoding attempts, successes, and failures with timestamps and source file paths.
// Tags: barcode, generation, decoding, logging, aspose.barcode, checksumvalidation, png, tempfolder

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating barcodes, decoding them, and logging the process.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates sample barcodes, attempts to decode each,
    /// and records the outcome in a log file with timestamps and file paths.
    /// </summary>
    static void Main()
    {
        // Prepare a log file in the system temporary folder.
        string logFile = Path.Combine(Path.GetTempPath(), "BarcodeDecodeLog.txt");
        File.WriteAllText(logFile, $"Log started at {DateTime.Now:O}{Environment.NewLine}");

        // Create a dedicated temporary folder for the generated barcode images.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeLogDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define a collection of sample barcodes to generate.
        var samples = new List<(BaseEncodeType encodeType, string text, string fileName)>
        {
            (EncodeTypes.Code128, "SampleCode128", "code128.png"),
            (EncodeTypes.QR, "SampleQR", "qr.png"),
            (EncodeTypes.DataMatrix, "SampleDM", "datamatrix.png")
        };

        // Generate barcode images and save them to the temporary folder.
        foreach (var (encodeType, text, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Set a modest X-dimension for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Decode each generated barcode and log the attempt, success, or failure.
        foreach (var (_, _, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            string timestamp = DateTime.Now.ToString("o");

            // Log the decoding attempt.
            File.AppendAllText(logFile, $"{timestamp} Attempt decoding: {filePath}{Environment.NewLine}");

            // Verify that the image file exists before attempting to read it.
            if (!File.Exists(filePath))
            {
                File.AppendAllText(logFile, $"{timestamp} Failure: File does not exist.{Environment.NewLine}");
                continue;
            }

            try
            {
                // Initialize the barcode reader for all supported types.
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Enable checksum validation (default behavior).
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Perform the decoding operation.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Determine if a valid result was obtained.
                    bool success = results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);

                    if (success)
                    {
                        // Log successful detection with type and decoded text.
                        File.AppendAllText(logFile, $"{timestamp} Success: Detected type {results[0].CodeTypeName}, Text \"{results[0].CodeText}\"{Environment.NewLine}");
                    }
                    else
                    {
                        // Log failure when no barcode is detected or result is empty.
                        File.AppendAllText(logFile, $"{timestamp} Failure: No barcode detected or empty result.{Environment.NewLine}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Log failures related to image loading or invalid arguments.
                File.AppendAllText(logFile, $"{timestamp} Failure: Image loading failed - {ex.Message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors.
                File.AppendAllText(logFile, $"{timestamp} Failure: Unexpected error - {ex.Message}{Environment.NewLine}");
            }
        }

        // Inform the user where the log file is located.
        Console.WriteLine($"Decoding log written to: {logFile}");
    }
}