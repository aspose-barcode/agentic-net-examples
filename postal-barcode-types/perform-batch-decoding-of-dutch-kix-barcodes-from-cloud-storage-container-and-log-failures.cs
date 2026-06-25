using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch decoding of Dutch KIX barcodes from a list of image files,
/// logging any failures to a text file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes a predefined list of image files,
    /// attempts to decode Dutch KIX barcodes, outputs results to the console,
    /// and logs any failures.
    /// </summary>
    static void Main()
    {
        // Define a sample list of image files.
        // In a real scenario these would be retrieved from cloud storage.
        List<string> imageFiles = new List<string>
        {
            "kix1.png",
            "kix2.png",
            "kix3.png",
            "kix4.png",
            "kix5.png"
        };

        // Path to the log file that will capture any decoding failures.
        string logPath = "decode_log.txt";

        // Ensure the log file starts empty for this run.
        File.WriteAllText(logPath, string.Empty);

        // Iterate over each image file and attempt barcode decoding.
        foreach (string filePath in imageFiles)
        {
            try
            {
                // Verify that the file exists before attempting to read it.
                if (!File.Exists(filePath))
                {
                    LogFailure(logPath, filePath, "File does not exist.");
                    continue;
                }

                // Load the image into a bitmap and create a barcode reader for Dutch KIX.
                using (var bitmap = new Bitmap(filePath))
                using (var reader = new BarCodeReader(bitmap, DecodeType.DutchKIX))
                {
                    // Optional: enforce checksum validation if desired.
                    // reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    // Perform the barcode reading operation.
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // If no barcodes were found, log the failure.
                    if (results.Length == 0)
                    {
                        LogFailure(logPath, filePath, "No Dutch KIX barcode detected.");
                        continue;
                    }

                    // Output each detected barcode's type and text to the console.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"File: {filePath} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions that occur during processing.
                LogFailure(logPath, filePath, $"Exception: {ex.Message}");
            }
        }

        // Inform the user that processing is complete.
        Console.WriteLine("Batch decoding completed. See decode_log.txt for any failures.");
    }

    /// <summary>
    /// Writes a failure entry to both the console and the specified log file.
    /// </summary>
    /// <param name="logPath">Path to the log file.</param>
    /// <param name="filePath">Path of the image file that caused the failure.</param>
    /// <param name="message">Description of the failure.</param>
    static void LogFailure(string logPath, string filePath, string message)
    {
        // Format the log entry with a timestamp.
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] File: {filePath} - {message}";
        Console.WriteLine(logEntry);
        // Append the entry to the log file.
        using (var writer = new StreamWriter(logPath, true))
        {
            writer.WriteLine(logEntry);
        }
    }
}