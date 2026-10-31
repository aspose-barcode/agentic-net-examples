// Title: Batch Barcode Generation with Checksum Handling and Logging
// Description: Demonstrates generating multiple barcodes in a temporary batch folder, applying optional checksum settings, and logging successes and errors.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating how to use BarcodeGenerator, EncodeTypes, and related parameters to create barcodes of various symbologies. Typical use cases include bulk barcode creation for inventory, shipping labels, or product catalogs, where developers need to manage checksum options and capture generation results. The snippet shows common patterns for error handling, logging, and temporary file management.
// Prompt: Implement error handling that catches and logs any checksum calculation errors during batch barcode generation.
// Tags: barcode symbology, batch generation, checksum handling, error logging, aspose.barcode, png output

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a batch of barcodes with optional checksum settings,
/// logs the outcome of each generation, and demonstrates error handling for checksum issues.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary batch folder, defines barcode items,
    /// generates each barcode, and records success or error messages to a log file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define the path for the batch log file
        string logFile = Path.Combine(batchFolder, "batch_log.txt");

        // Define batch items: symbology name, code text, optional checksum setting (null = default)
        var batchItems = new List<(string Symbology, string CodeText, EnableChecksum? ChecksumSetting)>
        {
            ("Code39Extended", "CODE39", EnableChecksum.No),          // optional checksum disabled
            ("Code93Extended", "CODE93", EnableChecksum.No),          // obligatory checksum disabled -> error
            ("Codabar", "-12345-", EnableChecksum.Yes),               // optional checksum enabled
            ("Code128", "CODE128", null),                             // default (checksum mandatory)
            ("Interleaved2of5", "1234567890", EnableChecksum.No)     // optional checksum disabled
        };

        // Process each barcode definition in the batch
        foreach (var item in batchItems)
        {
            // Resolve symbology name to BaseEncodeType via reflection
            var field = typeof(EncodeTypes).GetField(item.Symbology);
            if (field == null)
            {
                // Log unknown symbology and skip to the next item
                string msg = $"Unknown symbology: {item.Symbology}";
                Console.WriteLine(msg);
                File.AppendAllText(logFile, $"{DateTime.Now}: {msg}{Environment.NewLine}");
                continue;
            }

            // Cast the reflected field value to the required encode type
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            // Build a unique output file name for the generated barcode image
            string outputFile = Path.Combine(batchFolder, $"{item.Symbology}_{Guid.NewGuid().ToString("N")}.png");

            try
            {
                // Initialize the barcode generator with the resolved symbology and code text
                using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                {
                    // Apply checksum setting if it was provided for this item
                    if (item.ChecksumSetting.HasValue)
                    {
                        generator.Parameters.Barcode.IsChecksumEnabled = item.ChecksumSetting.Value;
                    }

                    // Save the generated barcode image as PNG
                    generator.Save(outputFile, BarCodeImageFormat.Png);
                }

                // Log successful generation
                string successMsg = $"Generated {item.Symbology} to {outputFile}";
                Console.WriteLine(successMsg);
                File.AppendAllText(logFile, $"{DateTime.Now}: {successMsg}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                // Capture and log any errors, including checksum calculation failures
                string errorMsg = $"Error generating {item.Symbology}: {ex.Message}";
                Console.WriteLine(errorMsg);
                File.AppendAllText(logFile, $"{DateTime.Now}: {errorMsg}{Environment.NewLine}");
            }
        }

        // Indicate batch processing completion and provide the log file location
        Console.WriteLine($"Batch processing completed. Log file: {logFile}");
    }
}