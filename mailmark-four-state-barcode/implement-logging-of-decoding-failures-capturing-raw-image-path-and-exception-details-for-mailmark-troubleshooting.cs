// Title: Mailmark 4-State Barcode Generation and Decoding with Failure Logging
// Description: Demonstrates generating a Mailmark 4‑state barcode, decoding it, and logging any decoding failures for troubleshooting.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of ComplexBarcodeGenerator for creating Mailmark barcodes and BarCodeReader for decoding them. Developers working with postal or logistics solutions often need to generate Mailmark symbols, read them from images, and capture detailed error information when decoding fails. The code illustrates typical API classes (MailmarkCodetext, ComplexBarcodeGenerator, BarCodeReader) and common patterns for handling success and failure scenarios.
/// Prompt: Implement logging of decoding failures, capturing raw image path and exception details for Mailmark troubleshooting.
// Tags: mailmark, barcode, generation, recognition, logging, complexbarcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates Mailmark 4‑state barcode generation, decoding, and logging of failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, attempts to decode it,
    /// and logs any decoding failures to a file for later analysis.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the barcode image and log file.
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the generated image and the failure log.
        string imagePath = Path.Combine(tempFolder, "Mailmark4State.png");
        string logPath = Path.Combine(tempFolder, "decode_failures.log");

        // Configure Mailmark 4‑state barcode parameters.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Attempt to read the generated barcode and handle possible failures.
        try
        {
            BaseDecodeType decodeType = DecodeType.Mailmark;
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                BarCodeResult[] results = reader.ReadBarCodes();

                if (results.Length == 0)
                {
                    // No barcode detected – treat as a failure and log it.
                    LogFailure(imagePath, new Exception("No barcode detected"));
                }
                else
                {
                    // Output successful decoding results.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exception that occurs during the decoding process.
            LogFailure(imagePath, ex);
        }

        // Inform the user where the log file is located.
        Console.WriteLine($"Log file written to: {logPath}");
    }

    /// <summary>
    /// Appends a detailed entry to the failure log, including timestamp, image path, and exception information.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image that failed to decode.</param>
    /// <param name="ex">Exception that was thrown during decoding.</param>
    static void LogFailure(string imagePath, Exception ex)
    {
        // Resolve the log file path based on the image's directory.
        string logPath = Path.Combine(Path.GetDirectoryName(imagePath) ?? "", "decode_failures.log");

        // Build a formatted log entry.
        string entry = $"[{DateTime.Now}] Decoding failure for image: {imagePath}{Environment.NewLine}" +
                       $"Exception: {ex.GetType().FullName} - {ex.Message}{Environment.NewLine}{Environment.NewLine}";

        // Append the entry to the log file.
        File.AppendAllText(logPath, entry);
    }
}