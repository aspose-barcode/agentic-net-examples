// Title: Background worker barcode reading with processor settings
// Description: Demonstrates generating sample barcode images, configuring Aspose.BarCode processor settings for multi‑core usage, and reading the barcodes asynchronously using a BackgroundWorker.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and ProcessorSettings, illustrating typical scenarios where developers need to generate barcodes, optimize recognition performance across CPU cores, and process images in a background thread for responsive applications.
// Prompt: Create a background worker that reads barcodes from a video stream using ProcessorSettings for optimal core usage.
// Tags: code128, qr, generation, reading, png, barcodegenerator, barcodereader, backgroundworker

using System;
using System.IO;
using System.ComponentModel;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating sample barcodes, configuring multi‑core processor settings,
/// and reading the barcodes asynchronously using a BackgroundWorker.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Sets processor settings, creates sample barcodes, runs a background
    /// worker to read them, and cleans up temporary files.
    /// </summary>
    static void Main(string[] args)
    {
        // Enable use of all CPU cores for barcode processing
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        // Allow additional threads proportional to processor count for better throughput
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // Create a temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample");
        Directory.CreateDirectory(tempFolder);
        GenerateSampleBarcodes(tempFolder);

        // Set up a BackgroundWorker to process the images without blocking the main thread
        using (var worker = new BackgroundWorker())
        {
            var completedEvent = new ManualResetEventSlim(false);

            // Define the work to be performed in the background thread
            worker.DoWork += (sender, e) => ProcessImages(tempFolder);
            // Signal completion when the background work finishes
            worker.RunWorkerCompleted += (sender, e) => completedEvent.Set();

            // Start the background operation
            worker.RunWorkerAsync();

            // Wait for the background worker to finish, but limit wait time to avoid hanging
            if (!completedEvent.Wait(TimeSpan.FromSeconds(30)))
            {
                Console.WriteLine("Processing timed out.");
            }
        }

        // Attempt to delete the temporary folder and its contents; ignore any errors in CI environments
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress cleanup exceptions
        }
    }

    // Generates a few barcode images for demonstration purposes
    private static void GenerateSampleBarcodes(string folder)
    {
        const int sampleCount = 5;
        for (int i = 0; i < sampleCount; i++)
        {
            string text = $"Sample{i}";
            string filePath = Path.Combine(folder, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Simple generation; default settings are sufficient
                generator.Save(filePath);
            }
        }
    }

    // Reads barcodes from all PNG files in the specified folder
    private static void ProcessImages(string folder)
    {
        string[] files = Directory.GetFiles(folder, "*.png");
        // Iterate over each image file
        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            try
            {
                // Initialize the reader for Code128 and QR symbologies
                using (var reader = new BarCodeReader(file, DecodeType.Code128, DecodeType.QR))
                {
                    // Apply a high‑performance quality preset
                    reader.QualitySettings = QualitySettings.HighPerformance;
                    // Read and output each detected barcode
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(file)}': {ex.Message}");
            }
        }
    }
}