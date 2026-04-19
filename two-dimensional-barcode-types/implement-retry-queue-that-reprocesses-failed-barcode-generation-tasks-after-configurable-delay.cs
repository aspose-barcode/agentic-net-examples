using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeRetryDemo
{
    // Represents a single barcode generation request.
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

    class Program
    {
        // Configurable retry settings.
        private const int MaxRetryAttempts = 3;
        private const int RetryDelayMilliseconds = 500; // 0.5 second delay between retries.

        static async Task Main()
        {
            // Sample tasks – some are intentionally invalid to trigger failures.
            var tasks = new List<BarcodeTask>
            {
                new BarcodeTask(EncodeTypes.Code128, "VALID123", "code128_valid.png"),
                new BarcodeTask(EncodeTypes.Code128, "INVALID@@@", "code128_invalid.png"), // will fail
                new BarcodeTask(EncodeTypes.QR, "https://example.com", "qr_valid.png")
            };

            // Queue for tasks that need to be retried.
            var retryQueue = new Queue<(BarcodeTask task, int attempt)>();

            // Initial processing.
            foreach (var task in tasks)
            {
                try
                {
                    GenerateAndSaveBarcode(task);
                    Console.WriteLine($"Generated: {task.OutputPath}");
                }
                catch (BarCodeException ex)
                {
                    Console.WriteLine($"Failed (will retry): {task.OutputPath} – {ex.Message}");
                    retryQueue.Enqueue((task, 1));
                }
            }

            // Process retries with delay.
            while (retryQueue.Count > 0)
            {
                var (task, attempt) = retryQueue.Dequeue();

                // Wait before retrying.
                await Task.Delay(RetryDelayMilliseconds);

                try
                {
                    GenerateAndSaveBarcode(task);
                    Console.WriteLine($"Retry succeeded: {task.OutputPath}");
                }
                catch (BarCodeException ex)
                {
                    Console.WriteLine($"Retry {attempt} failed: {task.OutputPath} – {ex.Message}");
                    if (attempt < MaxRetryAttempts)
                    {
                        // Enqueue for another attempt.
                        retryQueue.Enqueue((task, attempt + 1));
                    }
                    else
                    {
                        Console.WriteLine($"Giving up on: {task.OutputPath}");
                    }
                }
            }

            Console.WriteLine("Processing complete.");
        }

        // Generates a barcode image and saves it to the specified path.
        private static void GenerateAndSaveBarcode(BarcodeTask task)
        {
            // Ensure the output directory exists.
            var directory = Path.GetDirectoryName(task.OutputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Use a using block for the disposable BarcodeGenerator.
            using (var generator = new BarcodeGenerator(task.EncodeType, task.CodeText))
            {
                // Example of setting a property – bar color.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

                // Save directly; this method handles image generation internally.
                generator.Save(task.OutputPath, BarCodeImageFormat.Png);
            }
        }
    }
}