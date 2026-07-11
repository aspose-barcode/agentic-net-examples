// Title: Batch decode Dutch KIX barcodes from a local folder
// Description: Demonstrates how to decode multiple Dutch KIX barcodes stored in a folder and log any failures.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the use of BarCodeReader, DecodeType, and QualitySettings for high‑performance batch decoding. Typical use cases include processing large image collections from cloud or on‑premise storage, extracting barcode data, and handling error reporting. Developers often need to iterate over files, apply specific symbology settings, and capture decoding results for further processing.
// Prompt: Perform batch decoding of Dutch KIX barcodes from a cloud storage container and log failures.
// Tags: barcode symbology, decoding, dutch kix, batch processing, aspose.barcode, barcodereader, qualitysettings

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads Dutch KIX barcodes from a set of image files,
/// outputs successful decodings to the console, and logs any failures to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs batch decoding and logs failures.
    /// </summary>
    static void Main()
    {
        // Folder containing barcode images (replace with actual path or use sample images)
        string imagesFolder = "Barcodes";

        // Path to the log file where failure details will be written
        string logFilePath = "failures.log";

        // Ensure the log file starts empty
        if (File.Exists(logFilePath))
        {
            File.Delete(logFilePath);
        }

        // Verify that the images folder exists before proceeding
        if (!Directory.Exists(imagesFolder))
        {
            Console.WriteLine($"Images folder not found: {imagesFolder}");
            return;
        }

        // Retrieve all image files in the folder (no recursion)
        string[] imageFiles = Directory.GetFiles(imagesFolder, "*.*", SearchOption.TopDirectoryOnly);
        // Limit processing to a maximum of 10 files for safe execution
        int maxSamples = Math.Min(10, imageFiles.Length);

        int processed = 0;
        int successes = 0;
        int failures = 0;

        // Iterate over each selected image file
        for (int i = 0; i < maxSamples; i++)
        {
            string filePath = imageFiles[i];

            // Verify the file still exists (it could have been removed externally)
            if (!File.Exists(filePath))
            {
                LogFailure(logFilePath, filePath, "File does not exist.");
                failures++;
                continue;
            }

            // Initialize BarCodeReader for Dutch KIX symbology
            using (var reader = new BarCodeReader(filePath, DecodeType.DutchKIX))
            {
                // Apply a high‑performance quality preset to speed up processing
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Attempt to read all barcodes in the image
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    // No barcode detected – log the failure
                    LogFailure(logFilePath, filePath, "No barcode detected.");
                    failures++;
                }
                else
                {
                    // Output each successful decode to the console
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                    successes++;
                }
            }

            processed++;
        }

        // Write a summary of the batch operation
        Console.WriteLine();
        Console.WriteLine($"Processed: {processed}, Successes: {successes}, Failures: {failures}");
        Console.WriteLine($"Failure details logged to: {logFilePath}");
    }

    /// <summary>
    /// Appends a failure entry to the specified log file. If logging fails, writes the message to the console.
    /// </summary>
    /// <param name="logPath">Full path to the log file.</param>
    /// <param name="fileName">Name or path of the image file that caused the failure.</param>
    /// <param name="reason">Human‑readable reason for the failure.</param>
    static void LogFailure(string logPath, string fileName, string reason)
    {
        string message = $"{DateTime.Now:u} | File: {fileName} | Reason: {reason}";
        try
        {
            File.AppendAllText(logPath, message + Environment.NewLine);
        }
        catch
        {
            // Fallback: output the failure message to the console if file logging fails
            Console.WriteLine("Logging failed: " + message);
        }
    }
}