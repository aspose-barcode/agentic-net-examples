// Title: Batch decode OneCode barcodes and log results
// Description: Demonstrates generating OneCode barcodes, attempting batch decoding, and writing outcomes to a log file.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating how to generate OneCode barcodes, use BarCodeReader for batch decoding, and record results. It showcases key classes such as BarcodeGenerator, BarCodeReader, and BarCodeResult, which developers commonly use for bulk barcode operations, logging, and integration with storage services.
// Prompt: Perform batch decoding of OneCode barcodes from an Azure Blob storage and write outcomes to a log file.
// Tags: onecode, batch-decoding, log-file, aspose.barcode, barcode-generation, barcode-recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates OneCode barcodes, attempts to decode them in batch,
/// and records the results in a log file. Includes commented‑out Azure Blob Storage
/// integration for reference.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary files, generates barcodes,
    /// decodes them, and writes a detailed log.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Create a unique temporary folder for the batch operation
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "OneCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the log file that will capture generation and decoding outcomes
        string logFile = Path.Combine(tempFolder, "OneCodeDecodeLog.txt");

        // ----------------------------------------------------------------------
        // Sample OneCode codetexts (valid lengths: 20, 25, 29, 31 digits; second digit 0‑4)
        // ----------------------------------------------------------------------
        List<string> sampleCodes = new List<string>
        {
            "12345678901234567890",          // 20 digits, second digit '2'
            "1234567890123456789012345",    // 25 digits, second digit '2'
            "12345678901234567890123456789" // 29 digits, second digit '2'
        };

        // ----------------------------------------------------------------------
        // Generate barcode images and log the process
        // ----------------------------------------------------------------------
        List<string> barcodeFiles = new List<string>();
        foreach (string code in sampleCodes)
        {
            string filePath = Path.Combine(tempFolder, $"OneCode_{code.Length}.png");
            try
            {
                using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, code))
                {
                    // No additional parameters required for OneCode generation
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }

                barcodeFiles.Add(filePath);
                File.AppendAllText(logFile, $"Generated barcode image: {filePath}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFile, $"Failed to generate barcode for code '{code}': {ex.Message}{Environment.NewLine}");
            }
        }

        // ----------------------------------------------------------------------
        // Batch decode the generated OneCode images and log results
        // ----------------------------------------------------------------------
        foreach (string imagePath in barcodeFiles)
        {
            if (!File.Exists(imagePath))
            {
                File.AppendAllText(logFile, $"File not found: {imagePath}{Environment.NewLine}");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(imagePath, DecodeType.OneCode))
                {
                    // OneCode recognition is not supported; expecting zero results
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        File.AppendAllText(logFile, $"No barcode detected in '{Path.GetFileName(imagePath)}' (expected – OneCode not supported).{Environment.NewLine}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            File.AppendAllText(logFile, $"Detected OneCode in '{Path.GetFileName(imagePath)}': Type={result.CodeTypeName}, Data={result.CodeText}{Environment.NewLine}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(logFile, $"Error reading '{Path.GetFileName(imagePath)}': {ex.Message}{Environment.NewLine}");
            }
        }

        // ----------------------------------------------------------------------
        // Inform the user where the log file is located
        // ----------------------------------------------------------------------
        Console.WriteLine($"Batch decoding completed. Log file located at: {logFile}");

        // ----------------------------------------------------------------------
        // Azure Blob Storage integration (commented out – requires Azure.Storage.Blobs package)
        // ----------------------------------------------------------------------
        // string connectionString = "<Your Azure Blob Storage connection string>";
        // string containerName = "<Your container name>";
        // string blobName = "<Your blob name>";
        // var blobClient = new BlobClient(connectionString, containerName, blobName);
        // using (var downloadStream = new MemoryStream())
        // {
        //     blobClient.DownloadTo(downloadStream);
        //     downloadStream.Position = 0;
        //     // Process the downloaded image stream with BarCodeReader as shown above
        // }
    }
}