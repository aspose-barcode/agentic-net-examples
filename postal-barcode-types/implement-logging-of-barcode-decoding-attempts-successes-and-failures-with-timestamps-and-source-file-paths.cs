// Title: Barcode generation, decoding, and logging example
// Description: Demonstrates creating sample barcodes (Code128, QR, DataMatrix), decoding them, and logging each attempt with timestamps and file paths.
// Category-Description: This example belongs to the Aspose.BarCode operations category covering barcode generation and recognition. It showcases the use of BarcodeGenerator, BarCodeReader, EncodeTypes, and DecodeType classes to create and read various symbologies, while logging outcomes for audit or debugging purposes. Developers often need such patterns for batch processing, validation, and traceability of barcode workflows.
// Prompt: Implement logging of barcode decoding attempts, successes, and failures with timestamps and source file paths.
// Tags: barcode, generation, recognition, logging, codetype, decode, encode, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, decoding, and logging using Aspose.BarCode.
/// </summary>
class Program
{
    // Path to the folder that will hold sample barcode images
    private const string BarcodeFolder = "Barcodes";

    // Path to the log file
    private const string LogFile = "barcode_log.txt";

    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them, and logs results.
    /// </summary>
    static void Main()
    {
        // Ensure a clean log file at the start of each run
        if (File.Exists(LogFile))
        {
            File.Delete(LogFile);
        }

        // Create the folder for sample images if it does not exist
        if (!Directory.Exists(BarcodeFolder))
        {
            Directory.CreateDirectory(BarcodeFolder);
        }

        // Generate a few sample barcodes (Code128, QR, DataMatrix)
        GenerateSampleBarcodes();

        // Process each PNG image in the folder
        string[] files = Directory.GetFiles(BarcodeFolder, "*.png");
        foreach (string filePath in files)
        {
            // Log the start of a decoding attempt
            LogAttempt(filePath);

            // Verify the file still exists before attempting to read
            if (!File.Exists(filePath))
            {
                LogMessage($"File not found: {filePath}");
                continue;
            }

            // Use AllSupportedTypes to detect any barcode present in the image
            using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                try
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // No barcodes detected
                    if (results.Length == 0)
                    {
                        LogMessage("Result: Failure – No barcode detected.");
                    }
                    else
                    {
                        // Iterate through all detected barcodes
                        foreach (BarCodeResult result in results)
                        {
                            if (!string.IsNullOrEmpty(result.CodeText))
                            {
                                LogMessage($"Result: Success – Type: {result.CodeTypeName}, Text: {result.CodeText}");
                            }
                            else
                            {
                                LogMessage($"Result: Failure – Detected type {result.CodeTypeName} but no code text.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log any exception that occurs during decoding
                    LogMessage($"Result: Failure – Exception: {ex.Message}");
                }
            }
        }

        // Indicate completion to the user
        Console.WriteLine("Barcode processing completed. See log file for details.");
    }

    // Generates sample barcode images (Code128, QR, DataMatrix) in the BarcodeFolder
    private static void GenerateSampleBarcodes()
    {
        // Code128 example
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            string path = Path.Combine(BarcodeFolder, "code128.png");
            generator.Save(path, BarCodeImageFormat.Png);
        }

        // QR code example
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            string path = Path.Combine(BarcodeFolder, "qr.png");
            generator.Save(path, BarCodeImageFormat.Png);
        }

        // DataMatrix example
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "DataMatrix123"))
        {
            string path = Path.Combine(BarcodeFolder, "datamatrix.png");
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }

    // Logs the start of a decoding attempt with timestamp and file path
    private static void LogAttempt(string filePath)
    {
        string entry = $"{DateTime.Now:O} | Attempt: {filePath}{Environment.NewLine}";
        File.AppendAllText(LogFile, entry);
    }

    // Appends a generic message to the log with timestamp
    private static void LogMessage(string message)
    {
        string entry = $"{DateTime.Now:O} | {message}{Environment.NewLine}";
        File.AppendAllText(LogFile, entry);
    }
}