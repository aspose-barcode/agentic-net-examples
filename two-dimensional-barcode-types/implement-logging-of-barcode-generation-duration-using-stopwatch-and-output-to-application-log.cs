// Title: Generate Code128 barcode and log generation duration
// Description: This example creates a Code128 barcode image, measures how long the generation takes, and records the duration in a log file.
// Category-Description: Demonstrates Aspose.BarCode generation operations, focusing on the BarcodeGenerator class, EncodeTypes, and BarCodeImageFormat. Typical scenarios include creating barcodes for product labeling, inventory tracking, or receipts while monitoring performance. Developers often need to log generation times for diagnostics or auditing, making this pattern useful across many barcode‑related projects.
// Prompt: Implement logging of barcode generation duration using Stopwatch and output to application log.
// Tags: barcode, code128, generation, performance, logging, aspose.barcode, stopwatch, png

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates barcode generation with performance logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, measures generation time, and writes a log entry.
    /// </summary>
    static void Main()
    {
        // Prepare output directory and file paths
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);
        string barcodePath = Path.Combine(outputDir, "barcode.png");
        string logPath = Path.Combine(outputDir, "generation.log");

        // Define barcode data and symbology
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Start measuring generation duration
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Generate the barcode and save it as a PNG image
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Stop the timer after generation completes
        sw.Stop();

        // Compose a log entry with timestamp and elapsed time
        string logEntry = $"[{DateTime.Now:O}] Barcode generated in {sw.Elapsed.TotalMilliseconds} ms. File: {barcodePath}{Environment.NewLine}";
        File.AppendAllText(logPath, logEntry);

        // Output paths and timing information to the console
        Console.WriteLine("Barcode generated at: " + barcodePath);
        Console.WriteLine("Generation time (ms): " + sw.Elapsed.TotalMilliseconds);
        Console.WriteLine("Log written to: " + logPath);
    }
}