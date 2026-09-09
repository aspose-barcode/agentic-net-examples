// Title: Barcode Generation with Retry Queue
// Description: Demonstrates generating multiple barcodes using Aspose.BarCode with a retry mechanism that reprocesses failed tasks after a configurable delay.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes of various symbologies, configure appearance, and handle transient failures using a retry queue. It highlights key API classes such as BarcodeGenerator, BaseEncodeType, and BarCodeImageFormat, which developers commonly use for batch barcode creation, automated reporting, and inventory systems.
// Prompt: Implement a retry queue that reprocesses failed barcode generation tasks after a configurable delay.
// Tags: barcode, symbology, generation, retry, queue, aspose.barcode, png, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch barcode generation with a configurable retry queue for handling failures.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO representing a single barcode generation task.
    /// </summary>
    class BarcodeTask
    {
        public string CodeText { get; set; }
        public BaseEncodeType EncodeType { get; set; }
        public string OutputPath { get; set; }
    }

    /// <summary>
    /// Entry point of the example. Configures retry parameters, creates sample tasks,
    /// and processes them with a retry loop that re-attempts failed generations after a delay.
    /// </summary>
    static void Main()
    {
        // Configuration: maximum number of attempts and delay between retries (in milliseconds)
        int maxAttempts = 3;
        int retryDelayMilliseconds = 2000;

        // Prepare sample barcode generation tasks
        var tasks = new List<BarcodeTask>();
        AddTask(tasks, "Code128", "ABC123", "barcode1.png");
        AddTask(tasks, "QR", "https://example.com", "barcode2.png");
        AddTask(tasks, "DataMatrix", "Sample DataMatrix", "barcode3.png");

        // Process tasks with retry logic
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            Console.WriteLine($"Attempt {attempt} processing {tasks.Count} task(s).");
            var failedTasks = new List<BarcodeTask>();

            foreach (var task in tasks)
            {
                try
                {
                    // Ensure the output directory exists
                    string directory = Path.GetDirectoryName(task.OutputPath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    // Generate the barcode using Aspose.BarCode
                    using (var generator = new BarcodeGenerator(task.EncodeType, task.CodeText))
                    {
                        // Basic appearance settings
                        generator.Parameters.Barcode.XDimension.Pixels = 3f;
                        generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                        generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                        // Save the barcode image as PNG
                        generator.Save(task.OutputPath, BarCodeImageFormat.Png);
                    }

                    Console.WriteLine($"Generated: {task.OutputPath}");
                }
                catch (Exception ex)
                {
                    // Capture any failure and add the task to the retry list
                    Console.WriteLine($"Failed to generate {task.OutputPath}: {ex.Message}");
                    failedTasks.Add(task);
                }
            }

            // If no failures, exit the retry loop
            if (failedTasks.Count == 0)
            {
                Console.WriteLine("All tasks completed successfully.");
                break;
            }

            // If there are remaining attempts, wait before retrying
            if (attempt < maxAttempts)
            {
                Console.WriteLine($"Retrying {failedTasks.Count} failed task(s) after delay.");
                Task.Delay(retryDelayMilliseconds).Wait();
                tasks = failedTasks; // Replace the task list with only the failed ones
            }
            else
            {
                Console.WriteLine("Maximum attempts reached. Some tasks could not be processed.");
            }
        }
    }

    /// <summary>
    /// Helper method that adds a barcode task to the list by resolving the symbology name via reflection.
    /// </summary>
    /// <param name="list">The collection to which the task will be added.</param>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text or data to encode.</param>
    /// <param name="outputPath">The file path where the generated image will be saved.</param>
    static void AddTask(List<BarcodeTask> list, string symbologyName, string codeText, string outputPath)
    {
        // Resolve the symbology field from EncodeTypes using reflection
        var field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}. Task skipped.");
            return;
        }

        // Retrieve the BaseEncodeType instance
        var encodeType = field.GetValue(null) as BaseEncodeType;
        if (encodeType == null)
        {
            Console.WriteLine($"Failed to obtain encode type for: {symbologyName}. Task skipped.");
            return;
        }

        // Add the configured task to the list
        list.Add(new BarcodeTask
        {
            CodeText = codeText,
            EncodeType = encodeType,
            OutputPath = outputPath
        });
    }
}