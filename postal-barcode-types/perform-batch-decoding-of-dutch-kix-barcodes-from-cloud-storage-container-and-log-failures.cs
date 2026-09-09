// Title: Batch decode Dutch KIX barcodes from generated images
// Description: Demonstrates generating a set of Dutch KIX barcodes, decoding them in batch, and logging successes and failures.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader with a specific DecodeType to read them, and typical file handling for batch processing. Developers often need to process multiple barcode images from storage, detect errors, and record results, making this pattern useful for automated scanning workflows.
// Prompt: Perform batch decoding of Dutch KIX barcodes from a cloud storage container and log failures.
// Tags: dutch,kix,barcode,generation,recognition,batch,processing,logging

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates Dutch KIX barcodes, decodes them in batch,
/// and writes a detailed log of successes and failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary barcode images, attempts to decode each,
    /// and records the outcome to a log file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch processing
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define the path for the log file that will capture results
        string logPath = Path.Combine(batchFolder, "batch_decode_log.txt");

        // Sample data that will be encoded into Dutch KIX barcodes
        List<string> codeTexts = new List<string>
        {
            "123456ASPOSE",
            "ABCDEF1234",
            "KIXTEST01",
            "POST2023",
            "ZXCVBNM"
        };

        // Collection to store the full file paths of generated barcode images
        List<string> barcodeFiles = new List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images for each sample text
        // -----------------------------------------------------------------
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, $"DutchKIX_{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DutchKIX, text))
            {
                // Configure visual appearance
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Save the barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // -----------------------------------------------------------------
        // Batch decode the generated barcode images
        // -----------------------------------------------------------------
        foreach (string file in barcodeFiles)
        {
            try
            {
                // Specify the expected barcode type for decoding
                BaseDecodeType decodeType = DecodeType.DutchKIX;

                using (BarCodeReader reader = new BarCodeReader(file, decodeType))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        // No barcode detected – log as failure
                        string msg = $"FAILURE: {Path.GetFileName(file)} - No barcode detected.";
                        Console.WriteLine(msg);
                        File.AppendAllText(logPath, msg + Environment.NewLine);
                    }
                    else
                    {
                        // Log each successfully decoded barcode
                        foreach (BarCodeResult result in results)
                        {
                            string msg = $"SUCCESS: {Path.GetFileName(file)} - Type: {result.CodeTypeName}, Text: {result.CodeText}";
                            Console.WriteLine(msg);
                            File.AppendAllText(logPath, msg + Environment.NewLine);
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Image could not be loaded – log as failure
                string msg = $"FAILURE: {Path.GetFileName(file)} - Image loading failed. {ex.Message}";
                Console.WriteLine(msg);
                File.AppendAllText(logPath, msg + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Unexpected error – log as failure
                string msg = $"FAILURE: {Path.GetFileName(file)} - Unexpected error. {ex.Message}";
                Console.WriteLine(msg);
                File.AppendAllText(logPath, msg + Environment.NewLine);
            }
        }

        // Inform the user that processing is complete and where the log is stored
        Console.WriteLine($"Batch processing completed. Log saved to: {logPath}");
    }
}