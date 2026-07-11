// Title: Asynchronous barcode generation example
// Description: Demonstrates generating multiple barcodes concurrently using async/await, suitable for high‑throughput web scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the BarcodeGenerator class with Code128 symbology, image sizing, and styling. Developers often need to create barcodes on demand in web services, batch processes, or APIs, requiring efficient asynchronous I/O and parallel execution. The snippet illustrates typical usage patterns for high‑volume barcode creation.
// Prompt: Implement asynchronous barcode generation for high‑throughput web requests using async/await pattern efficiently.
// Tags: barcode, code128, async, await, generation, png, aspose.barcode, high-throughput

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates asynchronous generation of Code128 barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that initiates asynchronous barcode generation.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Generate a small batch of barcodes asynchronously.
        await GenerateBarcodesAsync();
    }

    // Asynchronously generates barcodes for a set of sample texts.
    private static async Task GenerateBarcodesAsync()
    {
        // Sample data – in a real high‑throughput scenario this could come from a request queue.
        var samples = new List<string>
        {
            "ABC123",
            "DEF456",
            "GHI789",
            "JKL012",
            "MNO345"
        };

        var tasks = new List<Task>();

        // Create a task for each sample text.
        foreach (var text in samples)
        {
            tasks.Add(Task.Run(async () =>
            {
                // Create and configure the generator for Code128.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
                {
                    // Use interpolation mode for automatic sizing.
                    generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    // Optional styling: set barcode and background colors.
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.DarkBlue;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Generate the bitmap image.
                    using (var bitmap = generator.GenerateBarCodeImage())
                    {
                        var fileName = $"barcode_{text}.png";
                        // Save the bitmap asynchronously.
                        await SaveBitmapAsync(bitmap, fileName);
                        Console.WriteLine($"Saved {fileName}");
                    }
                }
            }));
        }

        // Await all generation tasks to ensure completion.
        await Task.WhenAll(tasks);
    }

    // Saves a bitmap to a file using asynchronous file I/O.
    private static async Task SaveBitmapAsync(Bitmap bitmap, string path)
    {
        // Ensure the target directory exists.
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Write the image to a file stream asynchronously.
        using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
        {
            // Bitmap.Save writes synchronously to the provided stream.
            bitmap.Save(stream, ImageFormat.Png);
            await stream.FlushAsync();
        }
    }
}