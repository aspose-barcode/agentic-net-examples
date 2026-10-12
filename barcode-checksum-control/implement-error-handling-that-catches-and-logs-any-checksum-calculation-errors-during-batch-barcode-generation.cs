// Title: Batch barcode generation with checksum error handling
// Description: Demonstrates generating multiple barcodes in a batch, configuring checksum settings, and logging any errors that occur during generation.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat for creating barcode images. Typical use cases include bulk barcode creation for inventory, shipping, or labeling systems where developers need to manage checksum options and capture generation failures.
// Prompt: Implement error handling that catches and logs any checksum calculation errors during batch barcode generation.
// Tags: barcode, symbology, batch, checksum, error-handling, aspose.barcode, generation, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a batch of barcode images with various checksum configurations,
/// handling and logging any errors that occur during the process.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary batch folder,
    /// defines barcode items, generates images, and logs errors.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Path for the error log file
        string logPath = Path.Combine(batchFolder, "error_log.txt");

        // Define batch items: symbology name, code text, checksum setting
        var batchItems = new List<(string Symbology, string CodeText, EnableChecksum Checksum)>
        {
            ("Code39Extended", "CODE39", EnableChecksum.No),          // optional checksum disabled
            ("Code39Extended", "CODE39", EnableChecksum.Yes),         // optional checksum enabled
            ("Code128", "CODE128", EnableChecksum.Yes),               // obligatory checksum (must be Yes)
            ("Code93Extended", "CODE93", EnableChecksum.No),          // will cause exception (obligatory)
            ("Codabar", "-12345-", EnableChecksum.Default)            // default (no checksum)
        };

        // Process each barcode definition
        foreach (var item in batchItems)
        {
            try
            {
                // Resolve symbology name to BaseEncodeType via reflection
                FieldInfo field = typeof(EncodeTypes).GetField(item.Symbology);
                if (field == null)
                {
                    string msg = $"Unknown symbology: {item.Symbology}";
                    Console.WriteLine(msg);
                    File.AppendAllText(logPath, msg + Environment.NewLine);
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Create generator and configure checksum
                using (var generator = new BarcodeGenerator(encodeType, item.CodeText))
                {
                    generator.Parameters.Barcode.IsChecksumEnabled = item.Checksum;

                    // Save barcode image as PNG
                    string filePath = Path.Combine(batchFolder, $"{item.Symbology}_{item.CodeText}_{item.Checksum}.png");
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated: {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any generation errors, including checksum calculation failures
                string errorMsg = $"Error generating {item.Symbology} with text '{item.CodeText}': {ex.Message}";
                Console.WriteLine(errorMsg);
                File.AppendAllText(logPath, errorMsg + Environment.NewLine);
            }
        }

        // Summary output
        Console.WriteLine($"Batch processing completed. Files saved to: {batchFolder}");
        Console.WriteLine($"Error log (if any) saved to: {logPath}");
    }
}