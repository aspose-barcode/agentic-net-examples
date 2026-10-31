// Title: Batch decode Dutch KIX barcodes and log failures
// Description: Demonstrates generating a set of Dutch KIX barcode images, decoding them in batch, and recording any failures to a log file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator for image creation, BarCodeReader for decoding, and file I/O for logging. Typical use cases include bulk verification of barcode data stored in cloud containers or local directories, where developers need to process many images efficiently and capture errors for later analysis.
// Prompt: Perform batch decoding of Dutch KIX barcodes from a cloud storage container and log failures.
// Tags: dutch kix, barcode, batch decoding, error logging, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation and decoding of Dutch KIX barcodes,
/// logging any decoding failures to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample Dutch KIX barcodes,
    /// decodes them, and writes failure details to a log.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Sample data to encode as Dutch KIX barcodes
        List<string> codeTexts = new List<string>
        {
            "123456ASPOSE",
            "ABCDEF1234",
            "9876543210",
            "KIXTEST01",
            "POSTCODE9"
        };

        // Generate barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, $"DutchKIX_{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DutchKIX, text))
            {
                // Configure barcode appearance
                generator.Parameters.Barcode.XDimension.Pixels = 4;
                generator.Parameters.Barcode.BarHeight.Pixels = 50;
                // Save as PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Prepare the log file that will store any decoding failures
        string logPath = Path.Combine(batchFolder, "failures.log");
        if (File.Exists(logPath))
            File.Delete(logPath);

        // Set the decode type to Dutch KIX
        BaseDecodeType decodeType = DecodeType.DutchKIX;
        int successCount = 0;
        int failureCount = 0;

        // Process each generated barcode image
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                LogFailure(logPath, $"File not found: {file}");
                failureCount++;
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, decodeType))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        // No barcode detected in the image
                        LogFailure(logPath, $"No barcode detected in file: {file}");
                        failureCount++;
                    }
                    else
                    {
                        // Output each detected barcode
                        foreach (BarCodeResult result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(file)} | Detected: {result.CodeTypeName} | Text: {result.CodeText}");
                        }
                        successCount++;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle image loading failures or unsupported formats
                LogFailure(logPath, $"ArgumentException for file {file}: {ex.Message}");
                failureCount++;
            }
            catch (Exception ex)
            {
                // Log any other unexpected errors
                LogFailure(logPath, $"Exception for file {file}: {ex.Message}");
                failureCount++;
            }
        }

        // Summarize batch processing results
        Console.WriteLine($"Batch decoding completed. Success: {successCount}, Failures: {failureCount}");
        Console.WriteLine($"Failure log saved to: {logPath}");
    }

    /// <summary>
    /// Appends a failure message with a UTC timestamp to the specified log file.
    /// </summary>
    /// <param name="logPath">Path to the log file.</param>
    /// <param name="message">Failure description.</param>
    static void LogFailure(string logPath, string message)
    {
        File.AppendAllText(logPath, $"{DateTime.UtcNow:u} - {message}{Environment.NewLine}");
    }
}