using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with retry logic.
/// </summary>
class Program
{
    // Configuration constants
    private const int MaxRetryAttempts = 3;          // maximum number of retries per task
    private const int RetryDelayMilliseconds = 2000; // delay before each retry batch

    // Simple DTO for a barcode generation request
    class BarcodeTask
    {
        public BaseEncodeType Symbology { get; set; }
        public string CodeText { get; set; }
        public string OutputPath { get; set; }
        public int Attempt { get; set; } = 0;
    }

    /// <summary>
    /// Application entry point. Creates barcode tasks, processes them,
    /// and retries failed tasks up to a configured limit.
    /// </summary>
    static async Task Main()
    {
        // Sample tasks (in a real scenario these could come from a database, file, etc.)
        var tasks = new List<BarcodeTask>
        {
            new BarcodeTask { Symbology = EncodeTypes.Code128, CodeText = "VALID123", OutputPath = "code128_1.png" },
            new BarcodeTask { Symbology = EncodeTypes.EAN13,   CodeText = "1234567890128", OutputPath = "ean13.png" }, // valid checksum
            new BarcodeTask { Symbology = EncodeTypes.EAN13,   CodeText = "1234567890123", OutputPath = "ean13_invalid.png" } // invalid checksum – will fail
        };

        // Queue that holds tasks needing a retry
        var retryQueue = new List<BarcodeTask>();

        // First pass: attempt each task once
        foreach (var task in tasks)
        {
            bool success = await TryGenerateAsync(task);
            if (!success)
            {
                // Increment attempt count and queue for retry if limit not reached
                task.Attempt++;
                if (task.Attempt <= MaxRetryAttempts)
                {
                    retryQueue.Add(task);
                }
                else
                {
                    Console.WriteLine($"Task gave up after {MaxRetryAttempts} attempts: {task.CodeText}");
                }
            }
        }

        // Process retries in batches until the queue is empty or attempts are exhausted
        while (retryQueue.Count > 0)
        {
            Console.WriteLine($"Waiting {RetryDelayMilliseconds} ms before retrying {retryQueue.Count} failed task(s)...");
            await Task.Delay(RetryDelayMilliseconds);

            // Capture current batch and clear the queue for new failures
            var currentBatch = new List<BarcodeTask>(retryQueue);
            retryQueue.Clear();

            foreach (var task in currentBatch)
            {
                bool success = await TryGenerateAsync(task);
                if (!success)
                {
                    // Increment attempt count and re‑queue if attempts remain
                    task.Attempt++;
                    if (task.Attempt <= MaxRetryAttempts)
                    {
                        retryQueue.Add(task);
                    }
                    else
                    {
                        Console.WriteLine($"Task gave up after {MaxRetryAttempts} attempts: {task.CodeText}");
                    }
                }
            }
        }

        Console.WriteLine("All processing completed.");
    }

    // Attempts to generate a barcode; returns true on success, false on failure.
    private static async Task<bool> TryGenerateAsync(BarcodeTask task)
    {
        try
        {
            // Ensure the output directory exists
            string directory = Path.GetDirectoryName(task.OutputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Generate and save the barcode image
            using (var generator = new BarcodeGenerator(task.Symbology, task.CodeText))
            {
                // Example of setting a simple property (optional)
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Save(task.OutputPath);
            }

            Console.WriteLine($"Generated barcode: {task.OutputPath}");
            return true;
        }
        catch (Exception ex)
        {
            // Log the error; in a real app you might log to a file or monitoring system
            Console.WriteLine($"Error generating barcode (Attempt {task.Attempt + 1}): {ex.Message}");
            // Simulate asynchronous work (e.g., logging) to keep the method async
            await Task.Yield();
            return false;
        }
    }
}