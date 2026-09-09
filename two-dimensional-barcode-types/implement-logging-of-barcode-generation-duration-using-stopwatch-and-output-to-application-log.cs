// Title: Generate Code128 barcode and log generation duration
// Description: This example creates a Code128 barcode image, saves it as PNG, and records the time taken for generation.
// Category-Description: Demonstrates Aspose.BarCode generation operations, focusing on the BarcodeGenerator class, EncodeTypes enumeration, and BarCodeImageFormat settings. Typical scenarios include creating barcodes for product labeling, inventory tracking, or shipping documents, where developers need to produce images quickly and log performance metrics. This example belongs to the barcode creation and performance logging category of Aspose.BarCode samples.
// Prompt: Implement logging of barcode generation duration using Stopwatch and output to application log.
// Tags: barcode, code128, generation, png, logging, stopwatch, aspose.barcode, performance

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it as PNG, and logging the generation duration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, measures execution time, and writes a log entry.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for the barcode image and log file.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build full paths for the barcode image and the log file.
        string outputPath = Path.Combine(outputDir, "barcode.png");
        string logPath = Path.Combine(outputDir, "log.txt");
        string codeText = "1234567890";

        // Start measuring the barcode generation time.
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        // Generate the barcode using Code128 symbology and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Stop the timer and calculate elapsed time.
        stopwatch.Stop();
        TimeSpan duration = stopwatch.Elapsed;

        // Prepare a log entry with the duration and output location.
        string logMessage = $"Generated barcode '{codeText}' in {duration.TotalMilliseconds} ms. Saved to: {outputPath}{Environment.NewLine}";

        // Output the log entry to console and append it to the log file.
        Console.WriteLine(logMessage);
        File.AppendAllText(logPath, logMessage);
    }
}