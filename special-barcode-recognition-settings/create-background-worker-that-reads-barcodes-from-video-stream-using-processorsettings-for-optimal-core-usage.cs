// Title: Background barcode reading from video frames using ProcessorSettings
// Description: Demonstrates generating barcode images, configuring ProcessorSettings for multi‑core processing, and reading them in a background task that simulates a video stream.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, ProcessorSettings, and QualitySettings. Typical use cases include real‑time barcode scanning from video feeds, batch processing of image sequences, and performance‑optimized recognition on multi‑core systems. Developers often need to balance accuracy and speed, making ProcessorSettings essential for scaling barcode processing workloads.
// Prompt: Create a background worker that reads barcodes from a video stream using ProcessorSettings for optimal core usage.
// Tags: barcode, symbology, generation, recognition, multithreading, processorsettings, backgroundworker, videostream

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates barcode images, configures multi‑core processing,
/// and reads the barcodes in a background task to simulate video‑stream scanning.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample barcodes, sets up ProcessorSettings,
    /// and runs a background task that reads the barcodes as if they were video frames.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Create a temporary folder for generated barcode images
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // ----------------------------------------------------------------------
        // Define sample barcode texts and prepare a list to hold file paths
        // ----------------------------------------------------------------------
        List<string> texts = new List<string> { "ABC123", "XYZ789", "HELLO", "WORLD", "12345" };
        List<string> imageFiles = new List<string>();

        // ----------------------------------------------------------------------
        // Generate barcode images using BarcodeGenerator (Code128 symbology)
        // ----------------------------------------------------------------------
        foreach (string text in texts)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // ----------------------------------------------------------------------
        // Configure ProcessorSettings for optimal core usage
        // ----------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        // ----------------------------------------------------------------------
        // Start a background task that reads barcodes from the generated images
        // (simulating frames from a video stream)
        // ----------------------------------------------------------------------
        Task readTask = Task.Run(() =>
        {
            Console.WriteLine("Background barcode reading started.");
            Stopwatch sw = Stopwatch.StartNew();

            foreach (string file in imageFiles)
            {
                // Verify that the image file exists before attempting to read
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                // Open the image with BarCodeReader and set performance‑oriented quality settings
                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    reader.QualitySettings = QualitySettings.HighPerformance;

                    // Read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(file)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                    }
                }
            }

            sw.Stop();
            Console.WriteLine($"Background reading completed in {sw.ElapsedMilliseconds} ms.");
        });

        // ----------------------------------------------------------------------
        // Wait for the background task to finish before proceeding to cleanup
        // ----------------------------------------------------------------------
        readTask.Wait();

        // ----------------------------------------------------------------------
        // Clean up temporary files and directory
        // ----------------------------------------------------------------------
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cleanup failed: {ex.Message}");
        }
    }
}