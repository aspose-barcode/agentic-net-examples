// Title: Generate QR Code Batch and Monitor Memory Usage
// Description: Demonstrates generating multiple QR Code barcodes using Aspose.BarCode and tracking process memory to help optimize large‑scale generation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create QR Code barcodes with specific error correction levels, save them as PNG files, and monitor memory consumption during batch processing. It highlights key API classes such as BarcodeGenerator, EncodeTypes, QRErrorLevel, and BarCodeImageFormat, which developers commonly use for high‑volume barcode creation and performance tuning.
// Prompt: Generate QR Code barcode and monitor memory usage during large batch generation for optimization.
// Tags: qr code, barcode generation, memory monitoring, batch processing, aspose.barcode, png

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a batch of QR Code barcodes and reports memory usage after each creation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary folder, generates QR codes, and logs memory consumption.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch output
        string outputFolder = Path.Combine(Path.GetTempPath(), "QRBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Output folder: {outputFolder}");

        // Define batch size (safe sample size)
        const int batchSize = 10;

        // Record initial memory usage
        long initialMemory = Process.GetCurrentProcess().PrivateMemorySize64;
        Console.WriteLine($"Initial memory: {initialMemory / 1024 / 1024} MB");

        // Loop to generate each QR code in the batch
        for (int i = 1; i <= batchSize; i++)
        {
            // Prepare code text for this QR code
            string codeText = $"Sample QR {i}";

            // Generate QR code and save to file
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set high error correction level (Level H) for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Determine file path and save as PNG
                string filePath = Path.Combine(outputFolder, $"qr_{i}.png");
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated: {filePath}");
            }

            // Measure memory after each generation
            long currentMemory = Process.GetCurrentProcess().PrivateMemorySize64;
            long delta = currentMemory - initialMemory;
            Console.WriteLine($"After {i} items: {currentMemory / 1024 / 1024} MB (Δ {delta / 1024 / 1024} MB)");
        }

        // Final memory usage after completing the batch
        long finalMemory = Process.GetCurrentProcess().PrivateMemorySize64;
        Console.WriteLine($"Final memory: {finalMemory / 1024 / 1024} MB");
        Console.WriteLine("Batch generation completed.");
    }
}