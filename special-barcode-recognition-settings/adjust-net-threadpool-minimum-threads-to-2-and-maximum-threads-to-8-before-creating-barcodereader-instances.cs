// Title: Adjust .NET ThreadPool settings for barcode reading
// Description: Demonstrates how to set ThreadPool minimum and maximum threads before generating and reading a barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode .NET barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating a Code128 barcode and BarCodeReader for decoding it, while configuring ThreadPool limits to optimize multithreaded performance. Developers often need to adjust thread pool settings when processing many images concurrently in high‑throughput applications.
// Prompt: Adjust .NET ThreadPool minimum threads to 2 and maximum threads to 8 before creating BarCodeReader instances.
// Tags: barcode symbology, generation, recognition, code128, threadpool, aspnet, aspose.barcode

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates adjusting .NET ThreadPool settings and using Aspose.BarCode to generate and read a Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures ThreadPool limits, creates a barcode image, reads it, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Adjust ThreadPool settings before any barcode operations are performed
        // --------------------------------------------------------------------
        ThreadPool.GetMinThreads(out int minWorker, out int minIOC);
        ThreadPool.SetMinThreads(2, minIOC); // Set minimum worker threads to 2
        ThreadPool.GetMaxThreads(out int maxWorker, out int maxIOC);
        ThreadPool.SetMaxThreads(8, maxIOC); // Set maximum worker threads to 8

        // -------------------------------------------------
        // Generate a sample barcode image using Code128 symbology
        // -------------------------------------------------
        string imagePath = "sample_barcode.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Verify that the barcode image was successfully created
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Read the barcode from the generated image using BarCodeReader
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }

        // -------------------------------------------------
        // Clean up the sample image file (optional)
        // -------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}