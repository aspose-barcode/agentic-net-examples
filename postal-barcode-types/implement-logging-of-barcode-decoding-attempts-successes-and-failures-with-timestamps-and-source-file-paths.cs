// Title: Barcode Generation, Decoding, and Logging Example
// Description: Demonstrates creating barcode images, decoding them, and logging each attempt with timestamps and file paths.
// Category-Description: This example belongs to the Aspose.BarCode operations collection that covers barcode generation, recognition, and diagnostic logging. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and BarCodeImageFormat, illustrating typical use cases like creating sample barcodes, reading them back, and recording results for troubleshooting or audit trails. Developers working with barcode workflows often need to generate test images, validate decoding, and maintain logs for compliance or debugging.
// Prompt: Implement logging of barcode decoding attempts, successes, and failures with timestamps and source file paths.
// Tags: barcode, generation, decoding, logging, aspose.barcode, qrcode, code128, datamatrix

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, decoding, and logging using Aspose.BarCode.
/// </summary>
class Program
{
    // Path to the log file stored in the system temporary folder
    static readonly string LogFilePath = Path.Combine(Path.GetTempPath(), "BarcodeDecodeLog.txt");

    /// <summary>
    /// Entry point that creates sample barcodes, attempts to decode them, and logs each step.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare log file: delete any previous run's log to start fresh
        // --------------------------------------------------------------------
        if (File.Exists(LogFilePath))
            File.Delete(LogFilePath);

        // --------------------------------------------------------------------
        // Create a dedicated temporary folder for generated barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define sample barcodes to generate (symbology, text, output file name)
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType encodeType, string text, string fileName)>
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png")
        };

        var generatedFiles = new List<string>();

        // --------------------------------------------------------------------
        // Generate barcode images and log each creation
        // --------------------------------------------------------------------
        foreach (var (encodeType, text, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Set X-dimension for better visual quality
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
            Log($"Generated barcode: {filePath}");
        }

        // --------------------------------------------------------------------
        // Decode each generated barcode and log the outcome
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        for (int i = 0; i < generatedFiles.Count; i++)
        {
            string filePath = generatedFiles[i];
            Log($"Decoding attempt [{DateTime.Now:O}] - File: {filePath}");

            if (!File.Exists(filePath))
            {
                Log("Failure: File does not exist.");
                continue;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, decodeType))
                {
                    // Use default checksum validation
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

                    var results = reader.ReadBarCodes();
                    if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                    {
                        Log($"Success: Detected Type={results[0].CodeTypeName}, Data={results[0].CodeText}");
                    }
                    else
                    {
                        Log("Failure: No barcode detected or empty data.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Log($"Failure: ArgumentException - {ex.Message}");
            }
            catch (Exception ex)
            {
                Log($"Failure: Exception - {ex.GetType().Name} - {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up generated files and temporary folder (optional)
        // --------------------------------------------------------------------
        foreach (var file in generatedFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { Directory.Delete(tempFolder, true); } catch { }

        Log("Processing completed.");
    }

    /// <summary>
    /// Writes a timestamped message to both the console and the log file.
    /// </summary>
    /// <param name="message">The message to log.</param>
    static void Log(string message)
    {
        string timestamped = $"[{DateTime.Now:O}] {message}";
        Console.WriteLine(timestamped);
        try
        {
            File.AppendAllText(LogFilePath, timestamped + Environment.NewLine);
        }
        catch
        {
            // Suppress any logging errors to avoid crashing the program
        }
    }
}