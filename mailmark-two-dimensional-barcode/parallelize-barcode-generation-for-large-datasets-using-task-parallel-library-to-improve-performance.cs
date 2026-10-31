// Title: Parallel Barcode Generation Using TPL
// Description: Demonstrates generating multiple barcodes concurrently with the Task Parallel Library to improve throughput for large data sets.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and related parameters to create various symbologies. Typical use cases include batch processing of inventory codes, QR links, or any high‑volume barcode creation where performance matters. Developers often need to parallelize such workloads to reduce overall processing time.
// Prompt: Parallelize barcode generation for large datasets using Task Parallel Library to improve performance.
// Tags: barcode, symbology, generation, parallel, task parallel library, aspose.barcode, png, encodetypes

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates parallel generation of different barcode types using Aspose.BarCode and TPL.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates an output folder, defines barcode data, and runs generation tasks in parallel.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define a collection of barcode symbologies and the text to encode.
        var items = new List<(string Symbology, string Text)>
        {
            ("Code128", "ABC123"),
            ("QR", "https://example.com"),
            ("DataMatrix", "DM12345"),
            ("Aztec", "AztecTest"),
            ("PDF417", "PDF417Data")
        };

        // Launch a separate task for each barcode generation request.
        var tasks = new List<Task>();
        foreach (var item in items)
        {
            tasks.Add(Task.Run(() => GenerateBarcode(item.Symbology, item.Text, outputDir)));
        }

        // Wait for all barcode generation tasks to complete.
        Task.WaitAll(tasks.ToArray());

        Console.WriteLine($"Generated {items.Count} barcodes in: {outputDir}");
    }

    /// <summary>
    /// Generates a single barcode image based on the provided symbology and text, then saves it as PNG.
    /// </summary>
    /// <param name="symbologyName">Name of the barcode symbology (e.g., "QR", "Code128").</param>
    /// <param name="codeText">The text or data to encode in the barcode.</param>
    /// <param name="outputDir">Directory where the generated image will be saved.</param>
    static void GenerateBarcode(string symbologyName, string codeText, string outputDir)
    {
        // Resolve the EncodeTypes field that matches the symbology name.
        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }

        // Cast the field value to BaseEncodeType for the generator.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Build a safe file name using the symbology and a hash of the text.
        string safeFileName = $"{symbologyName}_{Math.Abs(codeText.GetHashCode()):X}.png";
        string filePath = Path.Combine(outputDir, safeFileName);

        // Create and configure the barcode generator, then save the image.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Resolution = 300f;               // High‑resolution output.
            generator.Parameters.Barcode.XDimension.Point = 2f; // Set module width.
            generator.Save(filePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Saved {symbologyName} barcode to {filePath}");
    }
}