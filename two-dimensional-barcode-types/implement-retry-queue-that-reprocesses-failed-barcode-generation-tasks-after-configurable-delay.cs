// Title: Barcode Generation with Retry Queue
// Description: Demonstrates how to generate barcodes using Aspose.BarCode with a retry mechanism that reprocesses failed tasks after a configurable delay.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and error handling patterns. Developers often need to batch‑process barcode creation and handle transient failures, such as unsupported symbologies or I/O issues. The sample illustrates typical retry logic, configurable delays, and maximum attempt limits useful in automated pipelines.
// Prompt: Implement a retry queue that reprocesses failed barcode generation tasks after a configurable delay.
// Tags: barcode, symbology, generation, retry, delay, aspose.barcode, encode-types, exception-handling

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Represents a single barcode generation task with symbology, data, and output location.
/// </summary>
class BarcodeTask
{
    public string SymbologyName { get; }
    public string CodeText { get; }
    public string OutputPath { get; }

    public BarcodeTask(string symbologyName, string codeText, string outputPath)
    {
        SymbologyName = symbologyName;
        CodeText = codeText;
        OutputPath = outputPath;
    }
}

class Program
{
    // Configurable delay in milliseconds between retries
    const int RetryDelayMs = 2000;

    // Maximum number of attempts per task
    const int MaxAttempts = 3;

    /// <summary>
    /// Entry point of the program that processes a list of barcode tasks with retry logic.
    /// </summary>
    static async Task Main(string[] args)
    {
        // Define a collection of sample barcode generation tasks
        var tasks = new List<BarcodeTask>
        {
            new BarcodeTask("Code128", "123ABC", Path.Combine(Path.GetTempPath(), "barcode1.png")),
            new BarcodeTask("QR", "https://example.com", Path.Combine(Path.GetTempPath(), "barcode2.png")),
            // This task uses an invalid symbology name and will trigger the error handling path
            new BarcodeTask("InvalidSymbology", "Test", Path.Combine(Path.GetTempPath(), "barcode3.png"))
        };

        // Process each task sequentially, awaiting completion before moving to the next
        foreach (var task in tasks)
        {
            await ProcessTaskAsync(task);
        }

        Console.WriteLine("All tasks processed.");
    }

    /// <summary>
    /// Generates a barcode for the specified task, applying retry logic on failure.
    /// </summary>
    /// <param name="task">The barcode generation task to process.</param>
    static async Task ProcessTaskAsync(BarcodeTask task)
    {
        // Resolve the symbology name to a BaseEncodeType enum value using reflection
        var field = typeof(EncodeTypes).GetField(task.SymbologyName);
        if (field == null)
        {
            Console.WriteLine($"[Error] Unknown symbology: {task.SymbologyName}. Skipping task.");
            return;
        }

        var encodeType = (BaseEncodeType)field.GetValue(null);
        int attempt = 0;
        bool success = false;

        // Retry loop: continue until success or maximum attempts reached
        while (attempt < MaxAttempts && !success)
        {
            attempt++;
            try
            {
                // Create a barcode generator with the resolved symbology and provided data
                using (var generator = new BarcodeGenerator(encodeType, task.CodeText))
                {
                    // Optional: set a barcode parameter (e.g., X-dimension)
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    // Save the generated barcode image to the specified path
                    generator.Save(task.OutputPath);
                }

                Console.WriteLine($"[Success] Generated barcode '{task.OutputPath}' on attempt {attempt}.");
                success = true;
            }
            catch (BarCodeException ex)
            {
                // Handle known barcode generation errors and schedule a retry if attempts remain
                Console.WriteLine($"[Warning] Attempt {attempt} failed for '{task.OutputPath}': {ex.Message}");
                if (attempt < MaxAttempts)
                {
                    Console.WriteLine($"Waiting {RetryDelayMs} ms before retry...");
                    await Task.Delay(RetryDelayMs);
                }
            }
            catch (Exception ex)
            {
                // Unexpected errors are logged and not retried
                Console.WriteLine($"[Error] Unexpected error on attempt {attempt} for '{task.OutputPath}': {ex.Message}");
                break;
            }
        }

        // Log final failure if all attempts were exhausted without success
        if (!success)
        {
            Console.WriteLine($"[Failure] Could not generate barcode for '{task.OutputPath}' after {MaxAttempts} attempts.");
        }
    }
}