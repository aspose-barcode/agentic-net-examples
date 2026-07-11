// Title: Barcode Generation and Decoding with Logging
// Description: Generates a Code128 barcode image, then attempts to decode it while logging each attempt, success, and failure with timestamps and file paths.
// Category-Description: This example belongs to the Aspose.BarCode operations collection that demonstrates barcode generation and recognition. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include inventory labeling, document processing, and automated scanning systems where developers need to generate barcodes programmatically and verify them by reading image files.
// Prompt: Implement logging of barcode decoding attempts, successes, and failures with timestamps and source file paths.
// Tags: barcode, code128, generation, decoding, logging, aspose.barcode, image, console

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, decoding, and logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a sample barcode, attempts decoding on a list of files, and logs outcomes.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a directory and generate a sample barcode image (Code128)
        // --------------------------------------------------------------------
        string sampleDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(sampleDir);
        string samplePath = Path.Combine(sampleDir, "sample.png");

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Save the generated barcode as a PNG file
            generator.Save(samplePath, BarCodeImageFormat.Png);
        }

        // ---------------------------------------------------------------
        // Define the list of files to decode (includes a non‑existent file)
        // ---------------------------------------------------------------
        var filesToDecode = new List<string>
        {
            samplePath,
            Path.Combine(sampleDir, "missing.png")
        };

        // ---------------------------------------------------------------
        // Iterate over each file, attempt decoding, and log the result
        // ---------------------------------------------------------------
        foreach (var filePath in filesToDecode)
        {
            Log($"Attempting to decode barcode in file: {filePath}");

            // Verify that the file exists before trying to read it
            if (!File.Exists(filePath))
            {
                Log("File does not exist. Decoding failed.");
                continue;
            }

            try
            {
                // Initialize the barcode reader for all supported symbologies
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes found in the image
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Log("No barcode detected. Decoding failed.");
                    }
                    else
                    {
                        // Log details for each detected barcode
                        foreach (var result in results)
                        {
                            Log($"Success: Type={result.CodeTypeName}, Text={result.CodeText}, Confidence={result.Confidence}, ReadingQuality={result.ReadingQuality}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions during decoding
                Log($"Exception during decoding: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Writes a timestamped log message to the console.
    /// </summary>
    /// <param name="message">The message to log.</param>
    static void Log(string message)
    {
        Console.WriteLine($"{DateTime.Now:O} - {message}");
    }
}