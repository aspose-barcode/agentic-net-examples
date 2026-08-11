// Title: Batch decode OneCode barcodes and write results to a log file
// Description: Demonstrates how to read multiple OneCode barcodes using Aspose.BarCode, handling up to five images, and write the decoding outcomes to a timestamped log file.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating batch processing of images, use of BarCodeReader with DecodeType.OneCode, and logging. Developers working with bulk barcode scanning, Azure Blob storage integration, or automated reporting can adapt this pattern for their solutions.
// Prompt: Perform batch decoding of OneCode barcodes from an Azure Blob storage and write outcomes to a log file.
// Tags: onecode, barcode, decoding, batch, log, aspose.barcode, azure blob, image processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch decoding of OneCode barcodes and logging the results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs decoding and logging.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // -----------------------------------------------------------------
        // NOTE: In a real environment you would download images from Azure
        // Blob Storage using Azure.Storage.Blobs SDK. The SDK is not
        // available in the snippet runner, so this example uses a local
        // folder ("Barcodes") as a stand‑in for the blob container.
        // -----------------------------------------------------------------

        // Define the folder that simulates the Azure Blob container
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        // Define the path of the log file that will receive decoding results
        string logFile = Path.Combine(Directory.GetCurrentDirectory(), "OneCodeDecodeLog.txt");

        // Ensure the input folder exists
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
        }

        // -----------------------------------------------------------------
        // Create a few sample OneCode barcode images (OneCode requires a
        // numeric CodeText of length 20, 25, 29 or 31). This makes the example
        // self‑contained and runnable without external files.
        // -----------------------------------------------------------------
        CreateSampleBarcodes(inputFolder);

        // Clear any previous log content
        if (File.Exists(logFile))
        {
            File.Delete(logFile);
        }

        // Retrieve up to 5 PNG files from the folder
        string[] imageFiles = Directory.GetFiles(inputFolder, "*.png");
        int maxFiles = Math.Min(imageFiles.Length, 5);

        // Process each image file
        for (int i = 0; i < maxFiles; i++)
        {
            string imagePath = imageFiles[i];
            try
            {
                // Initialize reader for OneCode symbology
                using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.OneCode))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcode was detected, log the information
                    if (results.Length == 0)
                    {
                        AppendLog(logFile, $"[{Path.GetFileName(imagePath)}] No barcode detected.");
                        continue;
                    }

                    // Log each detected barcode's type and text
                    foreach (BarCodeResult result in results)
                    {
                        string line = $"[{Path.GetFileName(imagePath)}] Type: {result.CodeTypeName}, Text: {result.CodeText}";
                        AppendLog(logFile, line);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while processing the image
                AppendLog(logFile, $"[{Path.GetFileName(imagePath)}] Error: {ex.Message}");
            }
        }

        Console.WriteLine($"Decoding completed. Log written to: {logFile}");
    }

    // Helper to append a line to the log file with a timestamp
    private static void AppendLog(string logPath, string message)
    {
        string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {message}{Environment.NewLine}";
        File.AppendAllText(logPath, entry);
    }

    // Generates a few OneCode barcode images in the specified folder
    private static void CreateSampleBarcodes(string folder)
    {
        // Sample numeric code texts of valid OneCode lengths (20 digits)
        string[] sampleTexts = new[]
        {
            "12345678901234567890",
            "98765432109876543210",
            "11111111111111111111"
        };

        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(folder, $"OneCode_{i + 1}.png");
            try
            {
                // EncodeTypes.OneCode exists for OneCode generation
                using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.OneCode, sampleTexts[i]))
                {
                    // Optional: adjust barcode appearance
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.Padding.Left.Point = 5f;
                    generator.Parameters.Barcode.Padding.Right.Point = 5f;

                    // Save as PNG
                    generator.Save(filePath, BarCodeImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                // If OneCode generation is not supported, log the issue.
                AppendLog(Path.Combine(folder, "generation_errors.log"), $"Failed to generate {filePath}: {ex.Message}");
            }
        }
    }
}