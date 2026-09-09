// Title: Barcode Generation, Decoding, and Logging Example
// Description: This example creates several barcodes, attempts to decode them, and records each attempt, success, or failure with timestamps and source file paths.
// Category-Description: Demonstrates core Aspose.BarCode operations including barcode generation (BarcodeGenerator) and recognition (BarCodeReader). Typical scenarios involve creating barcodes for inventory, tickets, or QR links and later validating them in batch processes. Developers often need to log decoding attempts for audit trails or troubleshooting, using the API classes EncodeTypes, DecodeType, BarCodeResult, and file I/O.
// Prompt: Implement logging of barcode decoding attempts, successes, and failures with timestamps and source file paths.
// Tags: barcode, generation, decoding, logging, code128, qr, datamatrix, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, decoding, and logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them, and writes a log file with timestamps and file paths.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images and the log file
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the decoding log file
        string logFile = Path.Combine(tempFolder, "decode_log.txt");

        // Collect paths of generated barcode images
        List<string> barcodeFiles = new List<string>();

        // Generate a Code128 barcode and store its file path
        GenerateBarcode(EncodeTypes.Code128, "SampleCode128", Path.Combine(tempFolder, "code128.png"));
        barcodeFiles.Add(Path.Combine(tempFolder, "code128.png"));

        // Generate a QR code and store its file path
        GenerateBarcode(EncodeTypes.QR, "https://example.com", Path.Combine(tempFolder, "qr.png"));
        barcodeFiles.Add(Path.Combine(tempFolder, "qr.png"));

        // Generate a DataMatrix barcode and store its file path
        GenerateBarcode(EncodeTypes.DataMatrix, "DM12345", Path.Combine(tempFolder, "datamatrix.png"));
        barcodeFiles.Add(Path.Combine(tempFolder, "datamatrix.png"));

        // Iterate over each barcode file, attempt decoding, and log the outcome
        foreach (string filePath in barcodeFiles)
        {
            // Log the start of a decoding attempt
            LogAttempt(logFile, filePath);

            // Verify that the file exists before trying to read it
            if (!File.Exists(filePath))
            {
                LogResult(logFile, filePath, false, "File does not exist.");
                continue;
            }

            // Use BarCodeReader to decode the image; support all recognized types
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                try
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Determine success based on presence of a result with non‑empty text
                    bool success = results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText);
                    string message = success
                        ? $"Success. Detected: {results[0].CodeTypeName}, Text: {results[0].CodeText}"
                        : "Failure. No barcode detected.";

                    // Log the decoding result
                    LogResult(logFile, filePath, success, message);
                }
                catch (Exception ex)
                {
                    // Log any exception that occurs during decoding
                    LogResult(logFile, filePath, false, $"Exception: {ex.Message}");
                }
            }
        }

        // Inform the user where the log file was written
        Console.WriteLine("Decoding log written to: " + logFile);
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The data to encode.</param>
    /// <param name="outputPath">File path where the PNG image will be saved.</param>
    static void GenerateBarcode(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set a simple X-dimension for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Writes a log entry indicating that a decoding attempt is starting.
    /// </summary>
    /// <param name="logFile">Path to the log file.</param>
    /// <param name="filePath">Path of the barcode image being decoded.</param>
    static void LogAttempt(string logFile, string filePath)
    {
        string entry = $"[{DateTime.Now:O}] Attempting to decode: {filePath}";
        File.AppendAllText(logFile, entry + Environment.NewLine);
    }

    /// <summary>
    /// Writes a log entry with the result of a decoding operation.
    /// </summary>
    /// <param name="logFile">Path to the log file.</param>
    /// <param name="filePath">Path of the barcode image that was decoded.</param>
    /// <param name="success">Indicates whether decoding succeeded.</param>
    /// <param name="message">Additional information about the result.</param>
    static void LogResult(string logFile, string filePath, bool success, string message)
    {
        string status = success ? "SUCCESS" : "FAILURE";
        string entry = $"[{DateTime.Now:O}] {status} - {filePath} - {message}";
        File.AppendAllText(logFile, entry + Environment.NewLine);
    }
}