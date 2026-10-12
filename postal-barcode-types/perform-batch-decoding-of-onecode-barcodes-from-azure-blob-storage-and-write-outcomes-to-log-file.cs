// Title: Batch decode OneCode barcodes and write results to a log file
// Description: Demonstrates generating a set of OneCode barcodes, decoding them in a batch, and recording the outcomes in a text log. Useful for validating OneCode recognition in automated pipelines.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator for OneCode creation and BarCodeReader for OneCode recognition. Typical use cases include bulk validation of barcode data, automated quality checks, and integration tests where developers need to generate, read, and log multiple barcodes efficiently.
// Prompt: Perform batch decoding of OneCode barcodes from an Azure Blob storage and write outcomes to a log file.
// Tags: onecode, barcode, batch, decode, log, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a temporary set of OneCode barcode images,
/// decodes each image using <see cref="BarCodeReader"/>, and writes a detailed
/// log of the decoding results to a text file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the generation, batch decoding,
    /// and logging workflow for OneCode barcodes.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a unique temporary folder for generated barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "OneCodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // 2. Define sample OneCode codetexts (valid lengths and second digit 0‑4).
        // --------------------------------------------------------------------
        List<string> codeTexts = new List<string>
        {
            "12345678901234567890",               // 20 digits, second digit '2'
            "1034567890123456789012",             // 22 digits (invalid length) – will be skipped
            "1204567890123456789012345",          // 25 digits, second digit '2'
            "13045678901234567890123456789",      // 29 digits, second digit '3'
            "14045678901234567890123456789012"    // 31 digits, second digit '4'
        };

        // --------------------------------------------------------------------
        // 3. Generate barcode images for the valid codetexts.
        // --------------------------------------------------------------------
        List<string> imageFiles = new List<string>();
        foreach (string text in codeTexts)
        {
            // Validate length (20, 25, 29, 31) and ensure the second digit is between 0‑4.
            if ((text.Length == 20 || text.Length == 25 || text.Length == 29 || text.Length == 31) &&
                text.Length > 1 && text[1] >= '0' && text[1] <= '4')
            {
                string filePath = Path.Combine(tempFolder, $"OneCode_{text.Length}.png");
                try
                {
                    using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, text))
                    {
                        generator.Save(filePath, BarCodeImageFormat.Png);
                    }
                    imageFiles.Add(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate barcode for text '{text}': {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Skipping invalid OneCode codetext: {text}");
            }
        }

        // --------------------------------------------------------------------
        // 4. Prepare the log file that will capture decoding results.
        // --------------------------------------------------------------------
        string logPath = Path.Combine(tempFolder, "OneCodeDecodeLog.txt");
        File.WriteAllText(logPath, $"OneCode batch decode started at {DateTime.Now}\n");

        // --------------------------------------------------------------------
        // 5. Decode each generated image using BarCodeReader with DecodeType.OneCode.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.OneCode;
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                File.AppendAllText(logPath, $"File not found: {imagePath}\n");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(imagePath, decodeType))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        File.AppendAllText(logPath,
                            $"No barcode detected in '{Path.GetFileName(imagePath)}' (expected – OneCode recognition unsupported).\n");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            File.AppendAllText(logPath,
                                $"Detected in '{Path.GetFileName(imagePath)}': Type={result.CodeTypeName}, Text={result.CodeText}\n");
                        }
                    }
                }
            }
            catch (ArgumentException ae)
            {
                File.AppendAllText(logPath,
                    $"Image loading failed for '{Path.GetFileName(imagePath)}': {ae.Message}\n");
            }
            catch (Exception ex)
            {
                File.AppendAllText(logPath,
                    $"Error processing '{Path.GetFileName(imagePath)}': {ex.Message}\n");
            }
        }

        // --------------------------------------------------------------------
        // 6. Finalize the log and inform the user where it is stored.
        // --------------------------------------------------------------------
        File.AppendAllText(logPath, $"OneCode batch decode completed at {DateTime.Now}\n");
        Console.WriteLine($"Log written to: {logPath}");
    }
}