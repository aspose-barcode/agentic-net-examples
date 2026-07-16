// Title: Barcode Generation with Retry Queue
// Description: Demonstrates generating barcodes using Aspose.BarCode with a retry mechanism for failed tasks.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class together with EncodeTypes to create barcode images. It illustrates typical use cases such as batch processing, error handling, and implementing a retry queue for transient failures. Developers working with barcode creation, image output, and robust task processing will find this pattern useful.
// Prompt: Implement a retry queue that reprocesses failed barcode generation tasks after a configurable delay.
// Tags: barcode, symbology, generation, retry, async, png, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Sample program that generates barcodes and retries failed tasks using a configurable delay.
/// </summary>
class Program
{
    // Configuration constants
    private const int MaxRetryCount = 2;               // Maximum number of retry attempts per task
    private const int RetryDelayMilliseconds = 500;    // Delay between retries in milliseconds

    /// <summary>
    /// Simple data structure representing a barcode generation task.
    /// </summary>
    private class BarcodeTask
    {
        public string SymbologyName { get; set; }   // e.g., "Code128"
        public string CodeText { get; set; }        // Text to encode into the barcode
        public string OutputPath { get; set; }      // Destination file path for the generated image
    }

    /// <summary>
    /// Entry point of the program. Prepares sample tasks and processes each with retry logic.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define a collection of sample barcode tasks, including intentional failures
        var tasks = new List<BarcodeTask>
        {
            new BarcodeTask { SymbologyName = "Code128", CodeText = "ABC123", OutputPath = "code128_1.png" },
            new BarcodeTask { SymbologyName = "QRCode", CodeText = "https://example.com", OutputPath = "qr_1.png" },
            // Invalid symbology to trigger a failure
            new BarcodeTask { SymbologyName = "InvalidSymbology", CodeText = "FAIL", OutputPath = "invalid.png" },
            // Valid symbology but empty code text (may cause an exception)
            new BarcodeTask { SymbologyName = "Code128", CodeText = "", OutputPath = "code128_empty.png" }
        };

        // Process each task sequentially, awaiting completion before moving to the next
        foreach (var task in tasks)
        {
            await ProcessBarcodeTaskAsync(task);
        }

        Console.WriteLine("All tasks processed.");
    }

    /// <summary>
    /// Generates a barcode for the specified task, retrying on failure up to <see cref="MaxRetryCount"/>.
    /// </summary>
    /// <param name="task">The barcode generation task to process.</param>
    private static async Task ProcessBarcodeTaskAsync(BarcodeTask task)
    {
        int attempt = 0;

        // Retry loop: continue until success or retry limit reached
        while (attempt <= MaxRetryCount)
        {
            try
            {
                // Resolve the symbology name to a BaseEncodeType enum value using reflection
                var field = typeof(EncodeTypes).GetField(task.SymbologyName, BindingFlags.Public | BindingFlags.Static);
                if (field == null)
                    throw new ArgumentException($"Unknown symbology: {task.SymbologyName}");

                var encodeType = (BaseEncodeType)field.GetValue(null);

                // Ensure the output directory exists
                var directory = Path.GetDirectoryName(task.OutputPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // Create the barcode generator, configure parameters, and save the image
                using (var generator = new BarcodeGenerator(encodeType))
                {
                    generator.CodeText = task.CodeText;
                    // Example of setting a simple parameter (optional)
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Save(task.OutputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Successfully generated: {task.OutputPath}");
                break; // Exit loop on success
            }
            catch (Exception ex) when (ex is BarCodeException || ex is ArgumentException)
            {
                attempt++;

                if (attempt > MaxRetryCount)
                {
                    // Exhausted retries – log failure
                    Console.WriteLine($"Failed to generate {task.OutputPath} after {MaxRetryCount} retries. Error: {ex.Message}");
                    break;
                }
                else
                {
                    // Log retry attempt and wait before next try
                    Console.WriteLine($"Attempt {attempt} failed for {task.OutputPath}. Retrying in {RetryDelayMilliseconds} ms. Error: {ex.Message}");
                    await Task.Delay(RetryDelayMilliseconds);
                }
            }
        }
    }
}