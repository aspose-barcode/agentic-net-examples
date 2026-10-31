// Title: Adjust ThreadPool Settings for Aspose.BarCodeReader Example
// Description: This example shows how to set .NET ThreadPool minimum and maximum threads before using Aspose.BarCode's BarCodeReader to decode a generated Code128 barcode saved as PNG.
// Category-Description: Demonstrates basic Aspose.BarCode operations such as barcode generation with BarcodeGenerator and barcode recognition with BarCodeReader. It illustrates configuring the .NET ThreadPool, a common requirement when processing multiple barcode reads in parallel, and covers typical use cases like temporary file handling and cleanup. Developers looking for end‑to‑end barcode creation and reading samples will find this pattern useful.
// Prompt: Adjust .NET ThreadPool minimum threads to 2 and maximum threads to 8 before creating BarCodeReader instances.
// Tags: code128, barcode generation, barcode recognition, threadpool, png, aspose.barcode

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, adjusting ThreadPool settings,
/// and reading the barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a Code128 barcode and save it as a PNG file
        var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890");
        generator.Save(barcodePath, BarCodeImageFormat.Png);

        // Adjust ThreadPool settings before creating BarCodeReader instances
        int workerThreads, completionPortThreads;
        ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMinThreads(2, completionPortThreads);
        ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMaxThreads(8, completionPortThreads);

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Barcode image not found.");
            return;
        }

        // Read the barcode from the generated image using BarCodeReader
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}