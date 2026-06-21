using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images, reading them in parallel,
/// measuring execution time and CPU utilization, and cleaning up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Generates sample barcodes, processes them concurrently, reports performance metrics,
    /// and removes temporary files.
    /// </summary>
    static void Main()
    {
        // Number of sample barcodes to generate and process (small safe number)
        const int sampleCount = 5;

        // Prepare a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample");
        if (!Directory.Exists(tempFolder))
        {
            Directory.CreateDirectory(tempFolder);
        }

        // Generate sample barcode images and collect their file paths
        var barcodeFiles = new List<string>();
        for (int i = 0; i < sampleCount; i++)
        {
            // Create a unique code text for each barcode
            string codeText = $"CODE{i:D4}";
            // Determine the output file path
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            // Use Code128 encoding for all samples
            BaseEncodeType encodeType = EncodeTypes.Code128;

            // Generate and save the barcode image
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Simple configuration (default settings)
                generator.Save(filePath);
            }

            // Store the generated file path for later processing
            barcodeFiles.Add(filePath);
        }

        // Set up performance measurement tools
        var stopwatch = new Stopwatch();                     // Measures elapsed wall‑clock time
        var process = Process.GetCurrentProcess();           // Provides CPU time information

        // Record CPU time before processing starts
        TimeSpan startCpuTime = process.TotalProcessorTime;
        stopwatch.Start();

        // Counter for total detected barcodes (thread‑safe)
        int totalDetected = 0;
        // Use all available logical processors for parallel execution
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        // Process each barcode image in parallel
        Parallel.ForEach(barcodeFiles, parallelOptions, file =>
        {
            // Open the image for barcode recognition
            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Iterate over all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Increment the shared counter safely
                    Interlocked.Increment(ref totalDetected);
                    // Optionally, output each result (commented out to keep output concise)
                    // Console.WriteLine($"File: {Path.GetFileName(file)} - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        });

        // Stop timing and capture CPU time after processing
        stopwatch.Stop();
        TimeSpan endCpuTime = process.TotalProcessorTime;

        // Calculate average CPU utilization as a percentage
        double elapsedMs = stopwatch.Elapsed.TotalMilliseconds;
        double cpuMs = (endCpuTime - startCpuTime).TotalMilliseconds;
        double cpuUtilization = (cpuMs / (Environment.ProcessorCount * elapsedMs)) * 100.0;

        // Output performance results to the console
        Console.WriteLine($"Processed {barcodeFiles.Count} barcode images.");
        Console.WriteLine($"Total barcodes detected: {totalDetected}");
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed.TotalSeconds:F2} seconds");
        Console.WriteLine($"Average CPU utilization across {Environment.ProcessorCount} cores: {cpuUtilization:F2}%");

        // Attempt to delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the OS will eventually remove the files.
        }
    }
}