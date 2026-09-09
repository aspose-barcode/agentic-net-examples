// Title: Mailmark 4-State Barcode Generation and Decoding with Failure Logging
// Description: Demonstrates generating a Mailmark 4‑state barcode, saving it as PNG, then attempting to decode it while logging any decoding failures, including image path and exception details.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, BarCodeReader, and DecodeType.Mailmark. Developers commonly need to create Mailmark barcodes for postal services, read them from images, and capture detailed logs when decoding fails for troubleshooting purposes. The pattern illustrated here is useful for batch processing and automated CI pipelines.
// Prompt: Implement logging of decoding failures, capturing raw image path and exception details for Mailmark troubleshooting.
// Tags: mailmark, barcode, generation, recognition, logging, complexbarcode, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates a Mailmark 4‑state barcode, attempts to decode it, and logs any failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode image,
    /// reads the barcode, and writes detailed failure information to a log file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the generated image and the log file
        string imagePath = Path.Combine(tempFolder, "Mailmark4State.png");
        string logPath = Path.Combine(tempFolder, "decode_log.txt");

        // Configure the Mailmark codetext (4‑state format)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image with a specific X‑dimension
        var generator = new ComplexBarcodeGenerator(mailmark);
        generator.Parameters.Barcode.XDimension.Pixels = 4f;
        generator.Save(imagePath, BarCodeImageFormat.Png);

        // Attempt to read the barcode and log any failures
        try
        {
            using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
            {
                var results = reader.ReadBarCodes();

                // No results returned – log the failure
                if (results == null || results.Length == 0)
                {
                    LogFailure(logPath, imagePath, "No barcode detected.");
                }
                else
                {
                    // Process each detected barcode
                    foreach (var result in results)
                    {
                        var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (decoded != null)
                        {
                            Console.WriteLine($"Decoded Mailmark ItemID: {decoded.ItemID}");
                        }
                        else
                        {
                            // Parsing of the codetext failed – log the issue
                            LogFailure(logPath, imagePath, "Failed to parse Mailmark codetext.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Unexpected exception while reading – log full details
            LogFailure(logPath, imagePath, ex);
        }

        Console.WriteLine($"Log written to: {logPath}");
    }

    /// <summary>
    /// Writes a simple error message to the log file and echoes it to the console.
    /// </summary>
    /// <param name="logFile">Path to the log file.</param>
    /// <param name="imagePath">Path to the image that caused the error.</param>
    /// <param name="message">Human‑readable error description.</param>
    static void LogFailure(string logFile, string imagePath, string message)
    {
        string entry = $"{DateTime.UtcNow:u} | Image: {imagePath} | Error: {message}{Environment.NewLine}";
        File.AppendAllText(logFile, entry);
        Console.WriteLine(message);
    }

    /// <summary>
    /// Writes exception details to the log file and echoes a short message to the console.
    /// </summary>
    /// <param name="logFile">Path to the log file.</param>
    /// <param name="imagePath">Path to the image that caused the exception.</param>
    /// <param name="ex">The caught exception.</param>
    static void LogFailure(string logFile, string imagePath, Exception ex)
    {
        string entry = $"{DateTime.UtcNow:u} | Image: {imagePath} | Exception: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
        File.AppendAllText(logFile, entry);
        Console.WriteLine($"Exception: {ex.Message}");
    }
}