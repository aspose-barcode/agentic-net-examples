// Title: Barcode generation with retry queue and configurable delay
// Description: Demonstrates generating multiple barcodes, retrying failed tasks up to a maximum number of attempts with a configurable delay between retries.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create images. It illustrates typical use cases such as batch processing, error handling, and retry mechanisms for developers needing robust barcode creation in automated workflows.
// Prompt: Implement a retry queue that reprocesses failed barcode generation tasks after a configurable delay.
// Tags: barcode generation, retry queue, delay, code128, qr, datamatrix, aspose.barcode, aspose.barcode.generation, png

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with a retry mechanism for failed tasks.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Processes a list of barcode tasks, retrying failures up to a configurable number of attempts with a delay.
    /// </summary>
    /// <param name="args">Command‑line arguments; optionally the delay in milliseconds for retries.</param>
    static async Task Main(string[] args)
    {
        // Default configuration
        int maxAttempts = 3;
        int delayMilliseconds = 1000;

        // Allow overriding the delay via the first command‑line argument
        if (args.Length >= 1 && int.TryParse(args[0], out int parsedDelay) && parsedDelay > 0)
        {
            delayMilliseconds = parsedDelay;
        }

        // Define barcode generation tasks
        var tasks = new List<BarcodeTask>
        {
            new BarcodeTask(EncodeTypes.Code128, "ABC123", Path.Combine(Path.GetTempPath(), "code128.png")),
            new BarcodeTask(EncodeTypes.QR, "https://example.com", Path.Combine(Path.GetTempPath(), "qr.png")),
            // Intentionally invalid data to demonstrate retry handling
            new BarcodeTask(EncodeTypes.DataMatrix, "Invalid@@@", Path.Combine(Path.GetTempPath(), "datamatrix.png"))
        };

        // Copy tasks to a mutable list that tracks pending work
        var pending = new List<BarcodeTask>(tasks);

        // Retry loop: attempt up to maxAttempts while there are pending tasks
        for (int attempt = 1; attempt <= maxAttempts && pending.Count > 0; attempt++)
        {
            Console.WriteLine($"Attempt {attempt} - processing {pending.Count} task(s).");
            var nextRound = new List<BarcodeTask>();

            // Process each pending task
            foreach (var task in pending)
            {
                try
                {
                    GenerateBarcode(task);
                    Console.WriteLine($"Generated: {task.OutputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed: {task.OutputPath} - {ex.Message}");
                    // Queue for retry if we have attempts left
                    if (attempt < maxAttempts)
                    {
                        nextRound.Add(task);
                    }
                }
            }

            // Prepare for the next retry round
            pending = nextRound;

            // Wait before the next attempt if there are still tasks to retry
            if (pending.Count > 0 && attempt < maxAttempts)
            {
                Console.WriteLine($"Waiting {delayMilliseconds} ms before next retry.");
                await Task.Delay(delayMilliseconds);
            }
        }

        // Report final outcome
        if (pending.Count > 0)
        {
            Console.WriteLine("Some tasks could not be processed after maximum retries:");
            foreach (var task in pending)
            {
                Console.WriteLine($" - {task.OutputPath}");
            }
        }
        else
        {
            Console.WriteLine("All barcode tasks completed successfully.");
        }
    }

    /// <summary>
    /// Generates a barcode image for the specified task using Aspose.BarCode.
    /// </summary>
    /// <param name="task">The barcode task containing type, text, and output path.</param>
    static void GenerateBarcode(BarcodeTask task)
    {
        using (var generator = new BarcodeGenerator(task.EncodeType, task.CodeText))
        {
            // Example configuration: set X‑dimension and enforce strict text validation
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

            // Save the generated barcode as PNG
            generator.Save(task.OutputPath, BarCodeImageFormat.Png);
        }
    }
}

/// <summary>
/// Represents a single barcode generation request.
/// </summary>
class BarcodeTask
{
    public BaseEncodeType EncodeType { get; }
    public string CodeText { get; }
    public string OutputPath { get; }

    public BarcodeTask(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        EncodeType = encodeType ?? throw new ArgumentNullException(nameof(encodeType));
        CodeText = codeText ?? throw new ArgumentNullException(nameof(codeText));
        OutputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));
    }
}