// Title: Asynchronous Barcode Generation Example
// Description: Demonstrates generating multiple barcodes concurrently using Aspose.BarCode with async/await, suitable for high‑throughput web scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create various barcode symbologies (Code128, QR, DataMatrix, Aztec, Pdf417) using the BarcodeGenerator class. It illustrates typical use cases such as batch processing and web‑service endpoints where developers need to generate barcodes efficiently and asynchronously. The code leverages EncodeTypes reflection, BaseEncodeType, and BarCodeImageFormat to produce PNG images.
// Prompt: Implement asynchronous barcode generation for high‑throughput web requests using async/await pattern efficiently.
// Tags: barcode, symbology, generation, async, png, aspose.barcode, encode-types

using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an entry point that generates a set of barcodes asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously creates barcode images for a predefined list of requests.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Create a dedicated temporary folder for output files.
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Define sample request data: each tuple contains a symbology name and the text to encode.
        var requests = new List<(string Symbology, string CodeText)>
        {
            ("Code128", "ABC123456"),
            ("QR", "https://example.com"),
            ("DataMatrix", "DM12345"),
            ("Aztec", "AztecSample"),
            ("Pdf417", "PDF417 Sample Text")
        };

        // Prepare a collection of tasks, one per barcode generation request.
        var tasks = new List<Task>();
        int index = 1;
        foreach (var req in requests)
        {
            // Build a unique file path for each generated barcode image.
            string filePath = Path.Combine(outputFolder, $"barcode_{index}.png");
            // Queue the asynchronous generation operation.
            tasks.Add(GenerateBarcodeAsync(req.Symbology, req.CodeText, filePath));
            index++;
        }

        // Await completion of all barcode generation tasks.
        await Task.WhenAll(tasks);
        Console.WriteLine("All barcodes generated.");
    }

    /// <summary>
    /// Generates a single barcode image asynchronously based on the provided symbology and text.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <param name="codeText">The text or data to encode in the barcode.</param>
    /// <param name="outputPath">The file system path where the PNG image will be saved.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    static async Task GenerateBarcodeAsync(string symbologyName, string codeText, string outputPath)
    {
        // Resolve the symbology name to a BaseEncodeType value using reflection.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        var encodeType = (BaseEncodeType)field.GetValue(null);

        // Run the potentially CPU‑bound barcode generation on a background thread.
        await Task.Run(() =>
        {
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Adjust a simple parameter (X‑dimension) for demonstration purposes.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        });

        Console.WriteLine($"Generated {symbologyName} barcode at {outputPath}");
    }
}