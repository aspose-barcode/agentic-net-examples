using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation and decoding of a Mailmark barcode,
/// logging successes and failures to a text file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a sample Mailmark image, attempts to decode it (and a non‑existent file),
    /// and logs the outcomes.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a sample Mailmark barcode image and save it to a temporary file.
        // --------------------------------------------------------------------
        string sampleImagePath = Path.Combine(Path.GetTempPath(), "mailmark_sample.png");
        CreateSampleMailmarkImage(sampleImagePath);

        // --------------------------------------------------------------------
        // Define the list of image paths to process.
        // Includes a valid sample and a deliberately missing file to show error handling.
        // --------------------------------------------------------------------
        string[] imagePaths = new string[]
        {
            sampleImagePath,
            Path.Combine(Path.GetTempPath(), "nonexistent_image.png")
        };

        // --------------------------------------------------------------------
        // Set up the log file path and clear any previous log content.
        // --------------------------------------------------------------------
        string logFile = Path.Combine(Path.GetTempPath(), "MailmarkDecodeLog.txt");
        if (File.Exists(logFile))
            File.Delete(logFile);

        // --------------------------------------------------------------------
        // Process each image path.
        // --------------------------------------------------------------------
        foreach (string imagePath in imagePaths)
        {
            // Skip processing if the file does not exist and log the failure.
            if (!File.Exists(imagePath))
            {
                LogFailure(logFile, imagePath, "File does not exist.");
                continue;
            }

            try
            {
                // Open a barcode reader for the current image, targeting Mailmark barcodes.
                using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
                {
                    // Attempt to read all barcodes from the image.
                    var results = reader.ReadBarCodes();

                    // If no barcodes were detected, log the failure.
                    if (results.Length == 0)
                    {
                        LogFailure(logFile, imagePath, "No barcode detected.");
                        continue;
                    }

                    // Iterate over each detected barcode result.
                    foreach (var result in results)
                    {
                        // Try to decode the Mailmark codetext using the ComplexCodetextReader.
                        var mailmark = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                        if (mailmark != null)
                        {
                            // Successful decode – output details to the console.
                            Console.WriteLine($"Successfully decoded Mailmark from '{imagePath}':");
                            Console.WriteLine($"  Format: {mailmark.Format}");
                            Console.WriteLine($"  VersionID: {mailmark.VersionID}");
                            Console.WriteLine($"  Class: {mailmark.Class}");
                            Console.WriteLine($"  SupplychainID: {mailmark.SupplychainID}");
                            Console.WriteLine($"  ItemID: {mailmark.ItemID}");
                            Console.WriteLine($"  DestinationPostCodePlusDPS: {mailmark.DestinationPostCodePlusDPS}");
                        }
                        else
                        {
                            // Decoding failed – log the issue.
                            LogFailure(logFile, imagePath, "Failed to decode Mailmark codetext.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions that occur during processing.
                LogFailure(logFile, imagePath, $"Exception: {ex.GetType().Name} - {ex.Message}");
            }
        }

        // Inform the user that logging is complete.
        Console.WriteLine($"Logging completed. See log file at: {logFile}");
    }

    /// <summary>
    /// Generates a sample Mailmark barcode image and writes it to the specified path.
    /// </summary>
    /// <param name="outputPath">File path where the generated PNG image will be saved.</param>
    static void CreateSampleMailmarkImage(string outputPath)
    {
        // Configure the Mailmark codetext with sample data.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // 4‑state format
            VersionID = 1,
            Class = "0",              // Test class
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Use ComplexBarcodeGenerator to create the barcode image.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode to a memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Write the memory stream contents to the output file.
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    ms.CopyTo(fileStream);
                }
            }
        }
    }

    /// <summary>
    /// Appends a failure entry to the log file and writes a brief message to the console.
    /// </summary>
    /// <param name="logFilePath">Path to the log file.</param>
    /// <param name="imagePath">Path of the image that caused the failure.</param>
    /// <param name="message">Description of the failure.</param>
    static void LogFailure(string logFilePath, string imagePath, string message)
    {
        // Build a timestamped log entry.
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Image: {imagePath} - {message}{Environment.NewLine}";

        // Append the entry to the log file.
        File.AppendAllText(logFilePath, logEntry);

        // Output a concise failure message to the console.
        Console.WriteLine($"Failure: {logEntry.TrimEnd()}");
    }
}