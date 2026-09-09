// Title: Barcode Generation and Recognition with Timing and CPU Usage Logging
// Description: Demonstrates generating barcodes, recognizing them, and logging the elapsed recognition time and CPU usage for each image.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include batch processing of images, performance measurement, and logging of CPU consumption, which developers often need when optimizing barcode workflows.
// Prompt: Write a wrapper that logs both recognition time and CPU usage for each processed image.
// Tags: barcode, generation, recognition, performance, timing, cpu, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating barcode images, recognizing them, and logging performance metrics such as
/// recognition time and CPU usage for each processed image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, processes each image to read barcodes,
    /// and logs timing and CPU usage information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data for barcode generation: text and corresponding symbology
        var samples = new (string Text, BaseEncodeType Type)[]
        {
            ("HelloWorld", EncodeTypes.Code128),
            ("1234567890", EncodeTypes.QR),
            ("ABC-123", EncodeTypes.DataMatrix)
        };

        // List to hold file paths of generated barcode images
        var generatedFiles = new System.Collections.Generic.List<string>();

        // -----------------------------------------------------------------
        // Generate barcode images and store their file paths
        // -----------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{sample.Text}_{sample.Type}.png");
            using (var generator = new BarcodeGenerator(sample.Type, sample.Text))
            {
                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        // -----------------------------------------------------------------
        // Process each image: log recognition time and CPU usage
        // -----------------------------------------------------------------
        foreach (string imagePath in generatedFiles)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Capture CPU time before recognition
            var process = Process.GetCurrentProcess();
            TimeSpan cpuBefore = process.TotalProcessorTime;

            // Start stopwatch to measure elapsed wall-clock time
            var watch = Stopwatch.StartNew();

            try
            {
                // Initialize barcode reader for all supported symbologies
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Perform barcode detection
                    reader.ReadBarCodes();

                    // Stop timing after recognition completes
                    watch.Stop();

                    // Calculate CPU time used during recognition
                    TimeSpan cpuAfter = process.TotalProcessorTime;
                    TimeSpan cpuUsed = cpuAfter - cpuBefore;

                    // Output performance metrics and detection results
                    Console.WriteLine($"Image: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"Recognition time: {watch.ElapsedMilliseconds} ms");
                    Console.WriteLine($"CPU time used: {cpuUsed.TotalMilliseconds} ms");
                    Console.WriteLine($"Barcodes found: {reader.FoundCount}");
                    foreach (BarCodeResult result in reader.FoundBarCodes)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
            catch (RecognitionAbortedException ex)
            {
                watch.Stop();
                Console.WriteLine($"Recognition aborted for {Path.GetFileName(imagePath)}: {ex.Message}");
            }
            catch (Exception ex)
            {
                watch.Stop();
                Console.WriteLine($"Error processing {Path.GetFileName(imagePath)}: {ex.Message}");
            }

            // Separator for readability between image logs
            Console.WriteLine(new string('-', 40));
        }

        // -----------------------------------------------------------------
        // Cleanup temporary folder and its contents
        // -----------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors (e.g., files still in use)
        }
    }
}