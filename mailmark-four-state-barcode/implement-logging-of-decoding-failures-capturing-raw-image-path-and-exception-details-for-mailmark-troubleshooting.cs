// Title: Mailmark barcode decoding with failure logging
// Description: Demonstrates how to decode Mailmark barcodes from images and log any decoding failures for troubleshooting.
// Category-Description: This example belongs to the Aspose.BarCode barcode decoding category, focusing on Mailmark symbology using BarCodeReader and ComplexCodetextReader. It shows typical use cases such as batch processing of images, handling missing files, empty results, and capturing detailed error information. Developers working with postal Mailmark codes often need robust logging to diagnose decoding issues in production environments.
// Prompt: Implement logging of decoding failures, capturing raw image path and exception details for Mailmark troubleshooting.
// Tags: mailmark, decoding, logging, console, barcodereader, complexcodetextreader, exception

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that reads Mailmark barcodes from a set of image files,
/// outputs successful decodings to the console, and logs any failures
/// (missing files, no detection, empty code text, or exceptions) for later analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Iterates over predefined image paths,
    /// attempts to decode Mailmark barcodes, and records any issues to a log file.
    /// </summary>
    static void Main()
    {
        // Define sample image paths (replace with actual paths as needed)
        string[] imagePaths = { "mailmark1.png", "mailmark2.png" };
        string logPath = "decode_failures.log";

        // Initialize the log file: create it if missing or clear existing content
        using (var logWriter = new StreamWriter(logPath, false))
        {
            logWriter.WriteLine($"Log started at {DateTime.Now}");
        }

        // Process each image file individually
        foreach (string imagePath in imagePaths)
        {
            // Verify that the image file exists before attempting to read it
            if (!File.Exists(imagePath))
            {
                LogFailure("File not found", imagePath);
                continue;
            }

            try
            {
                // Create a BarCodeReader configured for Mailmark decoding
                using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
                {
                    // Attempt to read all barcodes present in the image
                    var results = reader.ReadBarCodes();

                    // If no barcodes were detected, log the failure and move to the next image
                    if (results.Length == 0)
                    {
                        LogFailure("No barcode detected", imagePath);
                        continue;
                    }

                    // Iterate over each detected barcode result
                    foreach (var result in results)
                    {
                        // Ensure the detected barcode contains a non‑empty CodeText
                        if (string.IsNullOrEmpty(result.CodeText))
                        {
                            LogFailure("Empty CodeText", imagePath);
                            continue;
                        }

                        // Attempt to decode the Mailmark-specific codetext
                        var mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (mailmark == null)
                        {
                            // Decoding failed – log the raw CodeText for troubleshooting
                            LogFailure($"Failed to decode Mailmark codetext. Raw CodeText: {result.CodeText}", imagePath);
                        }
                        else
                        {
                            // Successful decode – output key fields to the console
                            Console.WriteLine($"Decoded Mailmark from '{imagePath}': ItemID={mailmark.ItemID}, DestinationPostCodePlusDPS='{mailmark.DestinationPostCodePlusDPS}'");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Capture any unexpected exceptions and log detailed information
                LogFailure($"Exception: {ex.GetType().Name}: {ex.Message}", imagePath, ex);
            }
        }
    }

    /// <summary>
    /// Writes a failure message to the log file, optionally including exception details.
    /// </summary>
    /// <param name="message">Human‑readable description of the failure.</param>
    /// <param name="imagePath">Path to the image that caused the failure.</param>
    /// <param name="ex">Optional exception object providing stack trace information.</param>
    static void LogFailure(string message, string imagePath, Exception ex = null)
    {
        string logPath = "decode_failures.log";
        using (var logWriter = new StreamWriter(logPath, true))
        {
            // Log timestamp, message, and image path
            logWriter.WriteLine($"{DateTime.Now}: {message} - Image: {imagePath}");
            // If an exception is provided, log its stack trace for deeper analysis
            if (ex != null)
            {
                logWriter.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }
    }
}