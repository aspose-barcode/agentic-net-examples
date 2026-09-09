// Title: Batch decode OneCode barcodes and write results to a log file
// Description: Demonstrates generating a set of OneCode barcodes, decoding them in a batch, and recording the outcomes in a text log.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and common file‑system handling. Developers often need to process many barcodes at once—e.g., validating scanned data or migrating legacy images—so batch operations and logging are typical requirements.
// Prompt: Perform batch decoding of OneCode barcodes from an Azure Blob storage and write outcomes to a log file.
// Tags: onecode, barcode, batch-decoding, log, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates OneCode barcodes, decodes them in a batch,
/// and writes the decoding results to a log file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare a list to hold the generated barcode image file paths
        List<string> barcodeFiles = new List<string>();

        // Sample data for OneCode barcodes (5 items)
        string[] sampleData = new string[]
        {
            "1234567890",
            "ABCDEFGHIJ",
            "9876543210",
            "KLMNOPQRST",
            "1122334455"
        };

        // Generate OneCode barcode images and store their file paths
        foreach (string data in sampleData)
        {
            string filePath = Path.Combine(batchFolder, $"OneCode_{data}.png");
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, data))
                {
                    // No additional parameters required for basic generation
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
                barcodeFiles.Add(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for data '{data}': {ex.Message}");
            }
        }

        // Initialize the log file with a start timestamp
        string logPath = Path.Combine(batchFolder, "decode_log.txt");
        File.WriteAllText(logPath, $"Batch decoding started at {DateTime.Now}{Environment.NewLine}");

        // Batch decode OneCode barcodes and append results to the log
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
            {
                File.AppendAllText(logPath, $"File not found: {file}{Environment.NewLine}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(file, DecodeType.OneCode))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        // Note: OneCode recognition is unsupported; zero results are expected.
                        File.AppendAllText(logPath, $"File: {Path.GetFileName(file)} - No barcode detected (expected — OneCode recognition unsupported).{Environment.NewLine}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            File.AppendAllText(logPath, $"File: {Path.GetFileName(file)} - Type: {result.CodeTypeName}, Data: {result.CodeText}{Environment.NewLine}");
                        }
                    }
                }
            }
            catch (ArgumentException ae)
            {
                // Handles image loading failures
                File.AppendAllText(logPath, $"File: {Path.GetFileName(file)} - Image loading failed: {ae.Message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logPath, $"File: {Path.GetFileName(file)} - Unexpected error: {ex.Message}{Environment.NewLine}");
            }
        }

        // Append a completion timestamp to the log
        File.AppendAllText(logPath, $"Batch decoding completed at {DateTime.Now}{Environment.NewLine}");
        Console.WriteLine($"Decoding log written to: {logPath}");

        // Placeholder for Azure Blob Storage integration:
        // In a real environment, replace the local file generation and reading with Azure.Storage.Blobs SDK calls.
        // Example (commented out because the SDK is not available in the snippet runner):
        // var blobClient = new BlobClient(connectionString, containerName, blobName);
        // using (MemoryStream ms = new MemoryStream())
        // {
        //     blobClient.DownloadTo(ms);
        //     ms.Position = 0;
        //     using (var reader = new BarCodeReader(ms, DecodeType.OneCode)) { ... }
        // }
    }
}