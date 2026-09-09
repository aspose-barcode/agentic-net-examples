// Title: ThreadPool Diagnostic for Barcode Generation and Reading
// Description: Demonstrates how to capture ThreadPool thread counts before and after generating and reading a barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating a Code128 barcode image and BarCodeReader for decoding it. Developers often need to monitor resource usage such as ThreadPool threads when processing barcodes in high‑throughput or asynchronous scenarios, making this pattern useful for performance diagnostics.
// Prompt: Implement a diagnostic tool that reports current ThreadPool thread counts before and after barcode processing.
// Tags: barcode, code128, generation, recognition, threadpool, diagnostics, aspose.barcode, png

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a simple diagnostic example that reports ThreadPool thread counts
/// before and after barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Writes the current number of available worker and completion‑port threads to the console.
    /// </summary>
    /// <param name="stage">A label indicating the point in the workflow (e.g., "Before processing").</param>
    static void ReportThreadPool(string stage)
    {
        // Retrieve the counts of available ThreadPool threads.
        ThreadPool.GetAvailableThreads(out int workerThreads, out int completionPortThreads);
        Console.WriteLine($"{stage}: Available worker threads = {workerThreads}, completion port threads = {completionPortThreads}");
    }

    /// <summary>
    /// Executes the barcode generation, reading, and ThreadPool diagnostics.
    /// </summary>
    static void Main()
    {
        // Report initial ThreadPool status before any barcode work.
        ReportThreadPool("Before processing");

        // Define a temporary file path for the generated barcode image.
        string tempFile = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // -------------------------------------------------
        // Generate a Code128 barcode image and save it as PNG.
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Report ThreadPool status after barcode generation.
        ReportThreadPool("After generation");

        // -------------------------------------------------
        // Read (decode) the previously generated barcode image.
        // -------------------------------------------------
        using (var reader = new BarCodeReader(tempFile, DecodeType.Code128))
        {
            reader.ReadBarCodes();
        }

        // Report ThreadPool status after barcode reading.
        ReportThreadPool("After reading");

        // -------------------------------------------------
        // Clean up: delete the temporary barcode image file.
        // -------------------------------------------------
        if (File.Exists(tempFile))
        {
            try
            {
                File.Delete(tempFile);
            }
            catch
            {
                // Suppress any exceptions during cleanup to avoid interrupting the diagnostic flow.
            }
        }
    }
}