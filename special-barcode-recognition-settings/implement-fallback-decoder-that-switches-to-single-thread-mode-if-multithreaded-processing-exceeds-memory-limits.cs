// Title: QR Barcode Generation and Memory‑Aware Decoding with Fallback
// Description: Generates a QR code image, then decodes it using Aspose.BarCode, switching to single‑thread mode when multithreaded processing exceeds a specified memory limit.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create a QR barcode, BarCodeReader to decode it, and ProcessorSettings to control threading behavior. Developers often need to balance performance with memory consumption, especially in high‑throughput or resource‑constrained environments; this snippet shows a practical fallback strategy.
// Prompt: Implement a fallback decoder that switches to single‑thread mode if multithreaded processing exceeds memory limits.
// Tags: qr, barcode generation, barcode recognition, memory limit, fallback, multithread, single‑thread, aspose.barcode, c#

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR barcode creation and a memory‑aware decoding routine that falls back to single‑thread processing when needed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, attempts multithreaded decoding, and falls back to single‑thread decoding if memory usage exceeds the defined limit.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 5;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Decode the image with fallback logic (memory limit set to 100 MB)
        DecodeWithFallback(barcodePath, 100L * 1024 * 1024);

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not crash the program
        }
    }

    /// <summary>
    /// Attempts to decode a barcode image using multithreaded processing. If the process memory exceeds <paramref name="memoryLimitBytes"/>,
    /// the method falls back to single‑thread decoding.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="memoryLimitBytes">Maximum allowed memory usage in bytes before falling back.</param>
    static void DecodeWithFallback(string imagePath, long memoryLimitBytes)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Configure processor for multithreaded execution
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 4;

        Console.WriteLine("Attempting multithreaded decoding...");

        // Perform multithreaded decoding
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            reader.ReadBarCodes();
            PrintResults(reader);
        }

        // Check memory consumption after multithreaded read
        long usedMemory = Process.GetCurrentProcess().PrivateMemorySize64;
        Console.WriteLine($"Memory after multithreaded read: {usedMemory / (1024 * 1024)} MB");

        if (usedMemory > memoryLimitBytes)
        {
            Console.WriteLine("Memory limit exceeded. Falling back to single‑thread decoding.");

            // Reconfigure processor for single‑thread execution
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

            // Perform single‑thread decoding
            using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                reader.ReadBarCodes();
                PrintResults(reader);
            }

            long finalMemory = Process.GetCurrentProcess().PrivateMemorySize64;
            Console.WriteLine($"Memory after single‑thread read: {finalMemory / (1024 * 1024)} MB");
        }
        else
        {
            Console.WriteLine("Memory usage within limits; no fallback needed.");
        }
    }

    /// <summary>
    /// Prints the decoding results to the console.
    /// </summary>
    /// <param name="reader">The <see cref="BarCodeReader"/> instance containing the results.</param>
    static void PrintResults(BarCodeReader reader)
    {
        Console.WriteLine($"Barcodes found: {reader.FoundCount}");
        foreach (BarCodeResult result in reader.FoundBarCodes)
        {
            Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
        }
    }
}