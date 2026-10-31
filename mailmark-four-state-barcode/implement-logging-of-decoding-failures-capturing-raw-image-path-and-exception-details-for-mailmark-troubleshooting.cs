// Title: Mailmark 4‑State Barcode Generation and Decoding with Failure Logging
// Description: Demonstrates generating a Mailmark 4‑State barcode, decoding it, and logging any decoding failures, including missing files and parsing errors.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating Mailmark barcodes, BarCodeReader for decoding, and handling DecodeType.Mailmark. Developers often need to generate Mailmark barcodes for logistics, read them from images, and capture detailed logs when decoding fails for troubleshooting.
// Prompt: Implement logging of decoding failures, capturing raw image path and exception details for Mailmark troubleshooting.
// Tags: mailmark, barcode, generation, decoding, logging, aspnet, aspose.barcode, complexbarcode, error-handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates Mailmark barcode generation, decoding, and failure logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a Mailmark barcode, attempts decoding, and logs any failures.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the log file that will capture decoding failures
        string logPath = Path.Combine(tempFolder, "decode_failures.log");

        // Generate a Mailmark 4‑State barcode image
        string imagePath = Path.Combine(tempFolder, "Mailmark4State.png");
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Use ComplexBarcodeGenerator to create the barcode image
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Attempt to read the generated barcode and log any issues
        TryDecodeImage(imagePath, logPath);

        // Attempt to read a non‑existent image to demonstrate exception logging
        string missingPath = Path.Combine(tempFolder, "nonexistent.png");
        TryDecodeImage(missingPath, logPath);

        Console.WriteLine($"Demo completed. Log file: {logPath}");
    }

    /// <summary>
    /// Tries to decode a barcode from the specified image path, logging failures or missing results.
    /// </summary>
    /// <param name="path">Path to the image file containing the barcode.</param>
    /// <param name="logPath">Path to the log file where failures are recorded.</param>
    static void TryDecodeImage(string path, string logPath)
    {
        try
        {
            // Verify that the image file exists before attempting to read
            if (!File.Exists(path))
                throw new FileNotFoundException("Image file not found.", path);

            // Initialize the barcode reader for Mailmark symbology
            using (var reader = new BarCodeReader(path, DecodeType.Mailmark))
            {
                var results = reader.ReadBarCodes();

                // No barcodes detected – log this condition
                if (results.Length == 0)
                {
                    LogNoResult(logPath, path);
                }
                else
                {
                    // Process each detected barcode
                    foreach (var result in results)
                    {
                        var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (decoded != null)
                        {
                            Console.WriteLine($"Decoded Mailmark from '{path}':");
                            Console.WriteLine($"  ItemID: {decoded.ItemID}");
                            Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                        }
                        else
                        {
                            Console.WriteLine($"Barcode read but could not parse Mailmark codetext in '{path}'.");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any exception that occurs during decoding
            LogFailure(logPath, path, ex);
        }
    }

    /// <summary>
    /// Writes detailed information about a decoding failure to the log file.
    /// </summary>
    /// <param name="logPath">Path to the log file.</param>
    /// <param name="imagePath">Path to the image that caused the failure.</param>
    /// <param name="ex">Exception thrown during decoding.</param>
    static void LogFailure(string logPath, string imagePath, Exception ex)
    {
        string message = $"{DateTime.Now}: Failed to decode image '{imagePath}'. Exception: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
        File.AppendAllText(logPath, message);
    }

    /// <summary>
    /// Logs a message indicating that no barcode was detected in the specified image.
    /// </summary>
    /// <param name="logPath">Path to the log file.</param>
    /// <param name="imagePath">Path to the image with no detectable barcode.</param>
    static void LogNoResult(string logPath, string imagePath)
    {
        string message = $"{DateTime.Now}: No barcode detected in image '{imagePath}'.{Environment.NewLine}";
        File.AppendAllText(logPath, message);
    }
}