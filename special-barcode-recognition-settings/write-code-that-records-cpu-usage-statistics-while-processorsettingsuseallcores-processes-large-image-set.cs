// Title: Record CPU Usage While Processing Barcodes with Multi-Core Support
// Description: Demonstrates generating barcode images, enabling multi‑core processing, and measuring CPU time and wall‑clock duration during barcode recognition.
// Category-Description: This example belongs to the Aspose.BarCode processing category, illustrating how to use BarCodeGenerator to create barcodes, BarCodeReader with ProcessorSettings.UseAllCores for parallel decoding, and .NET diagnostics to capture CPU usage. Developers working with bulk barcode image sets often need to optimize performance by leveraging all CPU cores and monitoring resource consumption. The sample shows typical use cases such as batch generation, multi‑threaded recognition, and performance reporting.
// Prompt: Write code that records CPU usage statistics while ProcessorSettings.UseAllCores processes a large image set.
// Tags: barcode generation, barcode recognition, multithreading, cpu usage, performance monitoring, aspose.barcode, code128, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating sample Code128 barcodes, enabling multi‑core barcode
/// recognition, and measuring CPU and elapsed time for processing a set of images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates barcode images, processes them with
    /// BarCodeReader using all CPU cores, and reports performance metrics.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a temporary folder for barcode images
        string folderPath = Path.Combine(Path.GetTempPath(), "AsposeBarcodesSample");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Number of sample images (kept small for CI safety)
        int sampleCount = 5;

        // Generate sample barcode images
        for (int i = 0; i < sampleCount; i++)
        {
            string filePath = Path.Combine(folderPath, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = $"Sample{i}";
                // Optional: set colors using Aspose.Drawing
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Ensure the generated files exist
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode images found to process.");
            return;
        }

        // Enable multi-core processing for BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Record CPU usage and elapsed time
        Process currentProcess = Process.GetCurrentProcess();
        TimeSpan cpuStart = currentProcess.TotalProcessorTime;
        Stopwatch sw = Stopwatch.StartNew();

        // Process each image and read barcodes
        foreach (string imagePath in imageFiles)
        {
            using (var reader = new BarCodeReader())
            {
                // Set the image for recognition
                reader.SetBarCodeImage(imagePath);
                // Optionally set decode types (e.g., Code128)
                reader.BarCodeReadType = DecodeType.Code128;
                // Read barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        }

        // Stop timing and calculate CPU usage
        sw.Stop();
        TimeSpan cpuEnd = currentProcess.TotalProcessorTime;
        TimeSpan cpuUsed = cpuEnd - cpuStart;

        // Output performance summary
        Console.WriteLine();
        Console.WriteLine("Processing completed.");
        Console.WriteLine($"Elapsed wall-clock time: {sw.Elapsed.TotalSeconds:F2} seconds");
        Console.WriteLine($"CPU time used: {cpuUsed.TotalSeconds:F2} seconds");
        Console.WriteLine($"CPU usage ratio: {(cpuUsed.TotalSeconds / sw.Elapsed.TotalSeconds * 100):F2}%");
    }
}