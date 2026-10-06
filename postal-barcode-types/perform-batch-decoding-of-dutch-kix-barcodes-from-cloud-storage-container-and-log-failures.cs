// Title: Batch decode Dutch KIX barcodes from generated images
// Description: Generates a set of Dutch KIX barcode images, decodes them in a batch operation, and records any failures to a log file.
// Category-Description: This example demonstrates Aspose.BarCode's generation and recognition APIs for Dutch KIX symbology. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader with a specific DecodeType for batch decoding, and common patterns for error handling and logging. Developers working on bulk barcode processing, automated verification, or cloud‑based image pipelines will find these techniques useful.
// Prompt: Perform batch decoding of Dutch KIX barcodes from a cloud storage container and log failures.
// Tags: dutchkix, barcode, generation, recognition, batch, logging, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation and decoding of Dutch KIX barcodes,
/// logging any decoding failures for later analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, decodes them,
    /// and writes a detailed log of successes and failures.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare a log file for failures and start timestamp
        string logPath = Path.Combine(batchFolder, "decode_log.txt");
        File.WriteAllText(logPath, $"Batch decode started at {DateTime.Now}{Environment.NewLine}");

        // Sample data for Dutch KIX barcodes
        List<string> sampleTexts = new List<string>
        {
            "123456ASPOSE",
            "ABCDEF1234",
            "ZXCVBNM987",
            "KIXTEST01",
            "POSTCODE9"
        };

        // Generate barcode images from the sample data
        List<string> generatedFiles = new List<string>();
        foreach (string text in sampleTexts)
        {
            string filePath = Path.Combine(batchFolder, $"{text}.png");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.DutchKIX, text))
            {
                gen.Parameters.Barcode.XDimension.Pixels = 4;
                gen.Parameters.Barcode.BarHeight.Pixels = 50;
                gen.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // Resolve DecodeType for Dutch KIX via reflection (required for BarCodeReader)
        BaseDecodeType dutchKixDecode = ResolveDecodeType("DutchKIX");
        if (dutchKixDecode == null)
        {
            Console.WriteLine("Failed to resolve DecodeType for DutchKIX. Exiting.");
            return;
        }

        // Batch decode each generated file and log any issues
        foreach (string file in generatedFiles)
        {
            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, dutchKixDecode))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        string msg = $"No barcode detected in file: {Path.GetFileName(file)}";
                        Console.WriteLine(msg);
                        AppendLog(logPath, msg);
                    }
                    else
                    {
                        foreach (BarCodeResult result in results)
                        {
                            string info = $"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}";
                            Console.WriteLine(info);
                        }
                    }
                }
            }
            catch (ArgumentException ex) // Image loading failed or unsupported format
            {
                string err = $"Failed to process file {Path.GetFileName(file)}: {ex.Message}";
                Console.WriteLine(err);
                AppendLog(logPath, err);
            }
            catch (Exception ex) // Any other unexpected error
            {
                string err = $"Unexpected error for file {Path.GetFileName(file)}: {ex.Message}";
                Console.WriteLine(err);
                AppendLog(logPath, err);
            }
        }

        Console.WriteLine($"Batch decoding completed. Log written to: {logPath}");

        // Cleanup: delete temporary files and folder (optional)
        // Commented out to allow inspection of generated files after run
        // foreach (string f in generatedFiles) File.Delete(f);
        // File.Delete(logPath);
        // Directory.Delete(batchFolder);
    }

    /// <summary>
    /// Resolves a DecodeType field by name using reflection.
    /// Returns null if the symbology name is not found.
    /// </summary>
    /// <param name="symbologyName">The name of the DecodeType field (e.g., "DutchKIX").</param>
    /// <returns>The corresponding BaseDecodeType instance or null.</returns>
    static BaseDecodeType ResolveDecodeType(string symbologyName)
    {
        var field = typeof(DecodeType).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown decode symbology: {symbologyName}");
            return null;
        }
        return (BaseDecodeType)field.GetValue(null);
    }

    /// <summary>
    /// Appends a timestamped message to the specified log file.
    /// Errors during logging are silently ignored to keep the batch process alive.
    /// </summary>
    /// <param name="logFile">Path to the log file.</param>
    /// <param name="message">Message to append.</param>
    static void AppendLog(string logFile, string message)
    {
        try
        {
            File.AppendAllText(logFile, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
        catch
        {
            // Suppress any logging errors to avoid crashing the batch process
        }
    }
}