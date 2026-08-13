// Title: Asynchronous Barcode Generation with Controlled Parallelism
// Description: Demonstrates generating multiple barcodes concurrently using async/await and a semaphore to limit parallelism, saving each as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class together with EncodeTypes to create various symbologies. Typical use cases include high‑throughput web services that need to produce barcode images on demand. Developers often need to manage resources efficiently, handle unknown symbologies, and control concurrency when processing large batches.
// Prompt: Implement asynchronous barcode generation for high‑throughput web requests using async/await pattern efficiently.
// Tags: barcode, symbology, async, parallelism, generation, aspose.barcode, png

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides methods to resolve barcode symbologies, generate barcode images asynchronously,
/// and process batches of barcode requests with controlled parallelism.
/// </summary>
class Program
{
    /// <summary>
    /// Resolves a symbology name (e.g., "Code128") to the corresponding <see cref="BaseEncodeType"/> using reflection.
    /// Returns <c>null</c> if the symbology is not found.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology.</param>
    /// <returns>The matching <see cref="BaseEncodeType"/>, or <c>null</c> if unknown.</returns>
    private static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        // Look up the static field in EncodeTypes that matches the provided name.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}. Skipping.");
            return null;
        }
        return (BaseEncodeType)field.GetValue(null);
    }

    /// <summary>
    /// Asynchronously generates a single barcode image and saves it to the specified path.
    /// </summary>
    /// <param name="encodeType">The barcode symbology to use.</param>
    /// <param name="codeText">The text or data to encode.</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private static Task GenerateBarcodeAsync(BaseEncodeType encodeType, string codeText, string outputPath)
    {
        return Task.Run(() =>
        {
            // Ensure the target directory exists before saving.
            string directory = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create and configure the barcode generator.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example of setting a barcode parameter (optional).
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });
    }

    /// <summary>
    /// Processes a batch of barcode generation requests concurrently, limiting the number of simultaneous operations.
    /// </summary>
    /// <param name="requests">
    /// A collection of tuples containing the symbology name, code text, and desired output file name.
    /// </param>
    /// <param name="maxDegreeOfParallelism">Maximum number of concurrent barcode generation tasks.</param>
    /// <returns>A <see cref="Task"/> that completes when all requests have been processed.</returns>
    private static async Task ProcessBatchAsync(IEnumerable<(string Symbology, string CodeText, string FileName)> requests, int maxDegreeOfParallelism)
    {
        // Semaphore limits the number of parallel tasks.
        using (var semaphore = new SemaphoreSlim(maxDegreeOfParallelism))
        {
            var tasks = new List<Task>();

            foreach (var request in requests)
            {
                // Resolve the symbology to an EncodeType; skip if unknown.
                BaseEncodeType encodeType = ResolveEncodeType(request.Symbology);
                if (encodeType == null)
                {
                    continue;
                }

                // Wait for an available slot before starting a new task.
                await semaphore.WaitAsync();

                // Start the generation task and ensure the semaphore is released afterwards.
                Task task = GenerateBarcodeAsync(encodeType, request.CodeText, Path.Combine("Barcodes", request.FileName))
                    .ContinueWith(t => semaphore.Release());

                tasks.Add(task);
            }

            // Await completion of all generation tasks.
            await Task.WhenAll(tasks);
        }
    }

    /// <summary>
    /// Application entry point. Creates sample barcode requests and processes them asynchronously.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous execution.</returns>
    static async Task Main(string[] args)
    {
        // Sample barcode requests (symbology, code text, output file name).
        var requests = new List<(string Symbology, string CodeText, string FileName)>
        {
            ("Code128", "123ABC", "code128_1.png"),
            ("QR", "https://example.com", "qr_1.png"),
            ("Code39", "CODE39", "code39_1.png"),
            ("DataMatrix", "DM12345", "datamatrix_1.png"),
            ("Aztec", "AztecDemo", "aztec_1.png")
        };

        // Process the batch with a maximum of 3 concurrent operations.
        await ProcessBatchAsync(requests, maxDegreeOfParallelism: 3);

        Console.WriteLine("Barcode generation completed.");
    }
}