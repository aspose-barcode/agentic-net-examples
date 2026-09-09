// Title: Adjust .NET ThreadPool settings for barcode generation and recognition
// Description: Demonstrates how to set the ThreadPool minimum and maximum threads before creating Aspose.BarCode BarcodeGenerator and BarCodeReader instances.
// Category-Description: This example belongs to the Aspose.BarCode threading and performance category, illustrating the use of ThreadPool configuration together with barcode generation (BarcodeGenerator) and recognition (BarCodeReader). Developers often need to tune thread pool limits to optimize parallel barcode processing in high‑throughput applications.
// Prompt: Adjust .NET ThreadPool minimum threads to 2 and maximum threads to 8 before creating BarCodeReader instances.
// Tags: qr, barcode generation, barcode recognition, png, barcodegenerator, barcodereader

using System;
using System.IO;
using System.Threading;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates adjusting .NET ThreadPool settings and using Aspose.BarCode to generate and read a QR code.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures ThreadPool, creates a temporary QR barcode image, reads it, and cleans up.
    /// </summary>
    static void Main()
    {
        // Adjust ThreadPool settings: set minimum worker threads to 2 and maximum to 8
        int workerThreads, completionPortThreads;
        ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMinThreads(2, completionPortThreads);
        ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
        ThreadPool.SetMaxThreads(8, completionPortThreads);

        // Prepare a temporary file path for the generated barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "barcode_" + Guid.NewGuid().ToString("N") + ".png");

        // Generate a QR barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(tempFile, BarCodeImageFormat.Png);
        }

        // Read the generated barcode using BarCodeReader
        BaseDecodeType decodeType = DecodeType.QR;
        using (BarCodeReader reader = new BarCodeReader(tempFile, decodeType))
        {
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }

        // Delete the temporary barcode image file
        if (File.Exists(tempFile))
        {
            File.Delete(tempFile);
        }
    }
}