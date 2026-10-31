// Title: Barcode Generation with Retry Queue
// Description: Demonstrates generating barcodes using Aspose.BarCode with a retry mechanism that reprocesses failed tasks after a configurable delay.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, BaseEncodeType, and BarCodeImageFormat to create barcode images. It illustrates typical use cases such as batch processing, error handling, and retry logic for transient failures, which developers often need when integrating barcode creation into automated workflows.
// Prompt: Implement a retry queue that reprocesses failed barcode generation tasks after a configurable delay.
// Tags: barcode symbology, generation, retry, delay, aspose.barcode, code128, itf6, png

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Represents a single barcode generation task, including the text to encode,
/// the symbology type, and the output file path.
/// </summary>
class BarcodeTask
{
    /// <summary>Gets the text to be encoded in the barcode.</summary>
    public string Text { get; }

    /// <summary>Gets the barcode symbology (encode type) to use.</summary>
    public BaseEncodeType EncodeType { get; }

    /// <summary>Gets the full file path where the generated barcode image will be saved.</summary>
    public string OutputPath { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BarcodeTask"/> class.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <param name="encodeType">The barcode symbology.</param>
    /// <param name="outputPath">The destination file path for the image.</param>
    public BarcodeTask(string text, BaseEncodeType encodeType, string outputPath)
    {
        Text = text;
        EncodeType = encodeType;
        OutputPath = outputPath;
    }
}

/// <summary>
/// Entry point of the example. Generates barcodes with retry logic for failed tasks.
/// </summary>
class Program
{
    /// <summary>
    /// Main method that configures retry parameters, creates barcode tasks,
    /// and processes them with a configurable delay between attempts.
    /// </summary>
    static void Main()
    {
        // ----- Configuration -----
        int maxAttempts = 3;          // Maximum number of retry attempts per task
        int delayMilliseconds = 2000; // Delay between attempts (in milliseconds)

        // ----- Prepare a unique output folder -----
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeRetry_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // ----- Define barcode tasks (one valid, one intentionally invalid) -----
        var tasks = new List<BarcodeTask>
        {
            new BarcodeTask("VALID12345", EncodeTypes.Code128, Path.Combine(outputFolder, "valid_code128.png")),
            new BarcodeTask("12", EncodeTypes.ITF6, Path.Combine(outputFolder, "invalid_itf6.png")) // Invalid length for ITF6
        };

        // Initialize the list of pending tasks
        var pendingTasks = new List<BarcodeTask>(tasks);

        // ----- Process tasks with retry logic -----
        for (int attempt = 1; attempt <= maxAttempts && pendingTasks.Count > 0; attempt++)
        {
            Console.WriteLine($"Attempt {attempt} - processing {pendingTasks.Count} task(s).");
            var failedThisRound = new List<BarcodeTask>();

            foreach (var task in pendingTasks)
            {
                try
                {
                    // Create a barcode generator for the specific task
                    using (var generator = new BarcodeGenerator(task.EncodeType, task.Text))
                    {
                        // Throw an exception if the provided text does not meet the symbology requirements
                        generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = true;

                        // Set a modest XDimension for better visual quality
                        generator.Parameters.Barcode.XDimension.Pixels = 2f;

                        // Ensure the output directory exists
                        string dir = Path.GetDirectoryName(task.OutputPath);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }

                        // Save the generated barcode as a PNG image
                        generator.Save(task.OutputPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"Generated: {task.OutputPath}");
                    }
                }
                catch (Exception ex)
                {
                    // Capture any failure and schedule the task for a retry
                    Console.WriteLine($"Failed to generate '{task.Text}' (Attempt {attempt}): {ex.Message}");
                    failedThisRound.Add(task);
                }
            }

            // Update the pending list with tasks that failed this round
            pendingTasks = failedThisRound;

            // If there are still pending tasks and we have attempts left, wait before retrying
            if (pendingTasks.Count > 0 && attempt < maxAttempts)
            {
                Console.WriteLine($"Waiting {delayMilliseconds} ms before next retry...");
                Task.Delay(delayMilliseconds).Wait();
            }
        }

        // ----- Final reporting -----
        if (pendingTasks.Count == 0)
        {
            Console.WriteLine("All barcode tasks completed successfully.");
        }
        else
        {
            Console.WriteLine("Some barcode tasks could not be processed after maximum retries:");
            foreach (var task in pendingTasks)
            {
                Console.WriteLine($"- {task.Text}");
            }
        }
    }
}