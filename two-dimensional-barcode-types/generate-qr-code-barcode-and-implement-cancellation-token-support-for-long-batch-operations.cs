using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeBatchDemo
{
    /// <summary>
    /// Demonstrates batch generation of QR codes with cancellation support.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a batch of QR codes and handles cancellation.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static async Task Main(string[] args)
        {
            // Define a list of sample QR code texts.
            var qrTexts = new List<string>
            {
                "https://example.com/1",
                "https://example.com/2",
                "https://example.com/3",
                "https://example.com/4",
                "https://example.com/5"
            };

            // Create a cancellation token source that will cancel after 3 seconds.
            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(3000);
                try
                {
                    // Generate the QR codes asynchronously, respecting the cancellation token.
                    await GenerateQrBatchAsync(qrTexts, cts.Token);
                    Console.WriteLine("Batch processing completed.");
                }
                catch (OperationCanceledException)
                {
                    // Handle the case where the operation was cancelled.
                    Console.WriteLine("Batch processing was cancelled.");
                }
            }
        }

        /// <summary>
        /// Generates QR code images for each text in the provided list.
        /// </summary>
        /// <param name="texts">The collection of strings to encode as QR codes.</param>
        /// <param name="token">Cancellation token to observe.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static async Task GenerateQrBatchAsync(List<string> texts, CancellationToken token)
        {
            // Ensure the output directory exists; create it if it does not.
            string outputDir = "QrOutput";
            if (!Directory.Exists(outputDir))
                Directory.CreateDirectory(outputDir);

            int index = 0;
            // Iterate over each text item to generate a corresponding QR code.
            foreach (var text in texts)
            {
                // Throw if cancellation has been requested before processing the next item.
                token.ThrowIfCancellationRequested();

                // Create a new barcode generator for a QR code with the current text.
                using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
                {
                    // Configure QR error correction level to Medium (Level M).
                    generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                    // Set the image resolution to 300 DPI.
                    generator.Parameters.Resolution = 300f;
                    // Build the file path for the output image.
                    string filePath = Path.Combine(outputDir, $"qr_{index + 1}.png");
                    // Save the generated QR code image to disk.
                    generator.Save(filePath);
                    Console.WriteLine($"Saved QR code {index + 1} to {filePath}");
                }

                index++;

                // Simulate a short processing delay, respecting cancellation.
                await Task.Delay(500, token);
            }
        }
    }
}