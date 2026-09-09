// Title: Batch Barcode Generation with Checksum Error Handling
// Description: Demonstrates generating multiple barcodes in a batch while handling checksum calculation errors and logging them.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, illustrating how to use BarcodeGenerator, EncodeTypes, and checksum settings to create barcode images. Typical use cases include bulk barcode creation for inventory, shipping, or labeling systems where developers need to manage optional and obligatory checksum options and capture any generation errors.
// Prompt: Implement error handling that catches and logs any checksum calculation errors during batch barcode generation.
// Tags: barcode, batch, checksum, error handling, aspose.barcode, png, generation

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a batch of barcodes, handling and logging checksum errors.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary folder, defines barcode items, generates each barcode,
    /// and logs any errors that occur during generation (including checksum calculation failures).
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);
        string logFile = Path.Combine(batchFolder, "error_log.txt");

        // Define batch items: symbology name, code text, checksum option
        var items = new List<(string Symbology, string CodeText, EnableChecksum ChecksumOption)>
        {
            ("Code39Extended", "CODE39", EnableChecksum.No),          // optional checksum disabled
            ("Code93Extended", "CODE93", EnableChecksum.No),          // obligatory checksum disabled (will cause error)
            ("Codabar", "-12345-", EnableChecksum.Yes),               // optional checksum enabled
            ("Code128", "CODE128", EnableChecksum.Yes),              // obligatory checksum enabled
            ("Code128", "CODE128", EnableChecksum.No)                // obligatory checksum disabled (will cause error)
        };

        // Process each barcode definition
        foreach (var item in items)
        {
            try
            {
                // Resolve symbology name to BaseEncodeType using reflection
                var field = typeof(EncodeTypes).GetField(item.Symbology);
                if (field == null)
                {
                    string msg = $"Unknown symbology: {item.Symbology}";
                    Console.WriteLine(msg);
                    File.AppendAllText(logFile, msg + Environment.NewLine);
                    continue; // Skip to next item
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Create barcode generator with the resolved type and provided text
                using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                {
                    // Apply the checksum option for this barcode
                    generator.Parameters.Barcode.IsChecksumEnabled = item.ChecksumOption;

                    // Build output file path and save the barcode image as PNG
                    string filePath = Path.Combine(batchFolder, $"{item.Symbology}_{item.CodeText}_{item.ChecksumOption}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Capture any generation errors (including checksum calculation failures) and log them
                string errorMsg = $"Error generating barcode for symbology '{item.Symbology}' with text '{item.CodeText}': {ex.Message}";
                Console.WriteLine(errorMsg);
                File.AppendAllText(logFile, errorMsg + Environment.NewLine);
            }
        }

        // Summarize batch processing results
        Console.WriteLine($"Batch processing completed. Files saved to: {batchFolder}");
        Console.WriteLine($"Error log (if any) saved to: {logFile}");
    }
}