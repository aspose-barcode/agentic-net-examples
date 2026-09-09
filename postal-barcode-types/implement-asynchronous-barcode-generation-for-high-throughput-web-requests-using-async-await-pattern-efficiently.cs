// Title: Asynchronous Barcode Generation Example
// Description: Demonstrates generating multiple barcode images concurrently using Aspose.BarCode with async/await.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and related parameter classes to create barcodes on demand. Typical use cases include high‑throughput web services that need to produce barcode images quickly for invoices, tickets, or product labeling. Developers often need to handle many requests in parallel, manage temporary storage, and select appropriate symbologies.
// Prompt: Implement asynchronous barcode generation for high‑throughput web requests using async/await pattern efficiently.
// Tags: barcode, symbology, generation, async, png, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates asynchronous generation of barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, defines sample barcode requests,
    /// launches parallel generation tasks, and waits for all to complete.
    /// </summary>
    static async Task Main()
    {
        // Create a unique temporary folder for generated barcodes
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define a set of sample barcode requests (symbology name, code text)
        var requests = new List<(string Symbology, string Text)>
        {
            ("Code128", "ABC123456"),
            ("QR", "https://example.com"),
            ("DataMatrix", "DataMatrixSample"),
            ("Aztec", "AztecCode")
        };

        // Collect tasks for each barcode generation request
        var tasks = new List<Task>();

        foreach (var req in requests)
        {
            // Build a unique file name for each barcode image
            string fileName = $"{req.Symbology}_{Guid.NewGuid().ToString("N")}.png";
            string outputPath = Path.Combine(outputFolder, fileName);

            // Queue asynchronous generation
            tasks.Add(GenerateBarcodeAsync(req.Symbology, req.Text, outputPath));
        }

        // Await completion of all generation tasks
        await Task.WhenAll(tasks);

        Console.WriteLine($"Generated {tasks.Count} barcode images in: {outputFolder}");
    }

    /// <summary>
    /// Generates a barcode image asynchronously based on the provided symbology and text.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text or data to encode in the barcode.</param>
    /// <param name="outputPath">The file system path where the PNG image will be saved.</param>
    private static async Task GenerateBarcodeAsync(string symbologyName, string codeText, string outputPath)
    {
        // Resolve symbology name to BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Perform the actual barcode generation on a background thread
        await Task.Run(() =>
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example of setting a barcode parameter (X-dimension)
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });
    }
}