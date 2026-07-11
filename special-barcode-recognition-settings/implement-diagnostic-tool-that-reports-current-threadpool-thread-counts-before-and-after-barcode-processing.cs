// Title: ThreadPool Diagnostic for Barcode Generation and Recognition
// Description: Demonstrates how to capture ThreadPool thread counts before and after generating and reading a Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to generate barcodes in various formats (e.g., PNG) and subsequently validate them, while also monitoring resource usage such as ThreadPool threads in high‑throughput applications.
// Prompt: Implement a diagnostic tool that reports current ThreadPool thread counts before and after barcode processing.
// Tags: code128, generation, recognition, png, threadpool, diagnostics

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a diagnostic demonstration of ThreadPool usage during barcode generation and recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode, reads it back, and reports ThreadPool thread counts before and after processing.
    /// </summary>
    static void Main()
    {
        // Capture ThreadPool thread counts before any barcode operation
        ThreadPool.GetAvailableThreads(out int workerThreadsBefore, out int completionPortsBefore);
        Console.WriteLine($"ThreadPool available worker threads before: {workerThreadsBefore}");
        Console.WriteLine($"ThreadPool available completion port threads before: {completionPortsBefore}");

        // Define a temporary file path for the generated barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "barcode.png");

        // Generate a simple Code128 barcode and save it as a PNG image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Initialize a barcode reader to decode the previously generated image
        using (BarCodeReader reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            // Iterate through all detected barcodes (expected one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                Console.WriteLine($"Detected barcode text: {result.CodeText}");
            }
        }

        // Capture ThreadPool thread counts after barcode generation and recognition
        ThreadPool.GetAvailableThreads(out int workerThreadsAfter, out int completionPortsAfter);
        Console.WriteLine($"ThreadPool available worker threads after: {workerThreadsAfter}");
        Console.WriteLine($"ThreadPool available completion port threads after: {completionPortsAfter}");

        // Clean up the temporary barcode image file
        if (File.Exists(tempFile))
        {
            try
            {
                File.Delete(tempFile);
            }
            catch
            {
                // Suppress any exceptions during cleanup to avoid interrupting the diagnostic flow
            }
        }
    }
}