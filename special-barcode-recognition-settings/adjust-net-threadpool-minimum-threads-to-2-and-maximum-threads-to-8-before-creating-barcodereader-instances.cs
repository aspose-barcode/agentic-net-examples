// Title: Adjust .NET ThreadPool Settings and Read a Code128 Barcode with Aspose.BarCode
// Description: Demonstrates how to configure the .NET ThreadPool minimum and maximum thread counts before generating and reading a Code128 barcode using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image and BarCodeReader to decode it. Developers working with barcode automation often need to tune ThreadPool settings for optimal performance when processing many images concurrently. The key API classes demonstrated are BarcodeGenerator, BarCodeReader, EncodeTypes, DecodeType, and BarCodeImageFormat.
// Prompt: Adjust .NET ThreadPool minimum threads to 2 and maximum threads to 8 before creating BarCodeReader instances.
// Tags: code128, barcode generation, barcode recognition, threadpool, aspose.barcode, png

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates adjusting ThreadPool settings and using Aspose.BarCode to generate and read a Code128 barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures ThreadPool, creates a temporary barcode image,
    /// reads the barcode, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Configure .NET ThreadPool limits (min 2, max 8 worker threads)
        // ------------------------------------------------------------
        int workerThreads, completionPortThreads;
        ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMaxThreads(8, completionPortThreads);
        ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMinThreads(2, completionPortThreads);

        // ------------------------------------------------------------
        // 2. Prepare a temporary folder and file path for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // ------------------------------------------------------------
        // 3. Generate a Code128 barcode and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 4. Verify that the barcode image was created successfully
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // 5. Read the barcode using BarCodeReader with the appropriate decode type
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // 6. Clean up temporary files and directory
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}