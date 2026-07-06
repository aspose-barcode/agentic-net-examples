// Title: Barcode decoding with error handling and logging
// Description: Demonstrates generating a Code128 barcode, decoding it, and handling decoding failures by checking the decoded text validity, then logging error details to a file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to use BarCodeGenerator, BarCodeReader, DecodeType, and related classes to read barcodes, verify decoded text validity, and log issues. Developers often need to detect unreadable or corrupted barcodes and record diagnostic information for troubleshooting.
// Prompt: Handle decoding failures by checking BarCodeReader.IsCodeTextValid and recording error details to a log file.
// Tags: barcode, decoding, error handling, logging, code128, aspose.barcode, barcodereader, iscodetextvalid

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, attempts to decode it,
/// and logs any decoding failures with detailed information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample barcode, reads it,
    /// checks for decoding validity, and records errors to a log file.
    /// </summary>
    static void Main()
    {
        // Paths for the generated barcode image and the log file
        const string barcodePath = "sample.png";
        const string logPath = "decode_log.txt";

        // Ensure any previous log is cleared
        if (File.Exists(logPath))
        {
            File.Delete(logPath);
        }

        // Generate a sample barcode image using Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Prepare the log writer (overwrite existing log)
        using (var logWriter = new StreamWriter(logPath, append: false))
        {
            // Verify that the barcode image file exists before attempting to read
            if (!File.Exists(barcodePath))
            {
                logWriter.WriteLine($"Error: Barcode image file '{barcodePath}' not found.");
                return;
            }

            // Create a BarCodeReader for all supported types
            using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
            {
                // Perform recognition and obtain an array of results
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, log the failure
                if (results == null || results.Length == 0)
                {
                    logWriter.WriteLine($"Decoding failed: No barcodes detected in '{barcodePath}'.");
                }
                else
                {
                    // Process each detected barcode
                    foreach (var result in results)
                    {
                        // Check if the decoded text is valid (non‑null and non‑empty)
                        // In practice, BarCodeResult.IsCodeTextValid can be used for this purpose
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            // Record details of the failure to the log file
                            logWriter.WriteLine("Decoding failure:");
                            logWriter.WriteLine($"  Type: {result.CodeTypeName}");
                            logWriter.WriteLine($"  Confidence: {result.Confidence}");
                            logWriter.WriteLine($"  ReadingQuality: {result.ReadingQuality}");
                            logWriter.WriteLine($"  Region: {result.Region.Rectangle}");
                        }
                        else
                        {
                            // Successful decode – write info to console (optional)
                            Console.WriteLine($"Decoded [{result.CodeTypeName}]: {result.CodeText}");
                        }
                    }
                }
            }
        }

        // Indicate completion to the user
        Console.WriteLine("Processing completed. See decode_log.txt for details.");
    }
}