// Title: Generate QR Code Barcodes with Cancellation Token Support
// Description: Demonstrates generating QR code images using Aspose.BarCode and handling cancellation for long-running batch operations.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.QR to create QR code images. Typical use cases include batch creation of barcodes for inventory, marketing, or authentication purposes, where developers often need to manage long-running processes and support graceful cancellation via CancellationToken.
// Prompt: Generate QR Code barcode and implement cancellation token support for long batch operations.
// Tags: qr code, barcode generation, cancellation token, batch processing, aspose.barcode, png output

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of generating QR code barcodes in batch with cancellation support.
/// </summary>
class Program
{
    /// <summary>
    /// Generates QR code images for each string in <paramref name="data"/> and saves them to <paramref name="outputFolder"/>.
    /// The method respects the provided <paramref name="token"/> to allow cooperative cancellation.
    /// </summary>
    /// <param name="data">List of text values to encode as QR codes.</param>
    /// <param name="outputFolder">Folder where generated PNG files will be saved.</param>
    /// <param name="token">Cancellation token to observe for cancellation requests.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task GenerateQrCodesAsync(List<string> data, string outputFolder, CancellationToken token)
    {
        int index = 0;

        // Process each text entry sequentially.
        foreach (var text in data)
        {
            // Throw if cancellation has been requested before starting the next iteration.
            token.ThrowIfCancellationRequested();

            // Build the output file path.
            string filePath = Path.Combine(outputFolder, $"qr_{index + 1}.png");

            // Create a QR code generator, configure it, and save the image.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                generator.CodeText = text;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR code: {filePath}");
            index++;

            // Simulate processing delay and allow cancellation during the wait.
            await Task.Delay(500, token);
        }
    }

    /// <summary>
    /// Entry point of the program. Sets up a temporary batch folder, sample data, and a cancellation token source,
    /// then invokes the QR code generation routine while demonstrating cancellation handling.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A task representing the asynchronous execution of the program.</returns>
    static async Task Main(string[] args)
    {
        // Create a dedicated temporary folder for this batch.
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Prepare sample data to encode.
        var data = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            data.Add($"Sample QR {i}");
        }

        // Set up a cancellation token source that will cancel after a short delay.
        using (var cts = new CancellationTokenSource())
        {
            // Schedule cancellation after 2 seconds to demonstrate support.
            var cancelTask = Task.Run(async () =>
            {
                await Task.Delay(2000);
                cts.Cancel();
                Console.WriteLine("Cancellation requested.");
            });

            try
            {
                // Run the batch generation with cancellation support.
                await GenerateQrCodesAsync(data, batchFolder, cts.Token);
                Console.WriteLine("Batch generation completed.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Batch generation was canceled.");
            }
        }

        // Enumerate and display the files that were actually created.
        var generatedFiles = Directory.GetFiles(batchFolder);
        Console.WriteLine($"Generated files count: {generatedFiles.Length}");
        foreach (var file in generatedFiles)
        {
            Console.WriteLine(file);
        }
    }
}