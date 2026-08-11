// Title: Adjust .NET ThreadPool settings and read a generated Code128 barcode
// Description: This example generates a Code128 barcode image, configures the .NET ThreadPool limits, then reads the barcode using Aspose.BarCode.
// Category-Description: Demonstrates basic Aspose.BarCode operations including barcode generation (BarcodeGenerator) and recognition (BarCodeReader) with thread pool tuning. Useful for developers needing to control concurrency while processing barcodes in high‑throughput scenarios. Covers common use cases such as creating PNG images and decoding all supported symbologies.
// Prompt: Adjust .NET ThreadPool minimum threads to 2 and maximum threads to 8 before creating BarCodeReader instances.
// Tags: code128, barcode-generation, barcode-recognition, png, threadpool, aspose.barcode

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode, configuring ThreadPool limits, and reading the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, sets ThreadPool thread counts, reads the barcode, and cleans up.
    /// </summary>
    static void Main()
    {
        // Define the temporary file path for the generated barcode image
        string barcodePath = "sample_barcode.png";

        // Generate a simple Code128 barcode and save it as a PNG file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Adjust ThreadPool settings: set minimum to 2 worker threads and maximum to 8 worker threads
        int workerThreads, completionPortThreads;
        ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMinThreads(2, completionPortThreads);
        ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMaxThreads(8, completionPortThreads);

        // Read the barcode using BarCodeReader with all supported decode types
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected Text: {result.CodeText}");
            }
        }

        // Clean up the temporary barcode image file
        if (File.Exists(barcodePath))
        {
            try
            {
                File.Delete(barcodePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete temporary file: {ex.Message}");
            }
        }
    }
}