// Title: QR Code batch generation with memory usage monitoring
// Description: Demonstrates generating multiple QR Code barcodes using Aspose.BarCode and logs process memory consumption after each generation.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and QRErrorLevel to create QR Code images. Typical use cases include bulk barcode creation for inventory, ticketing, or marketing, where developers need to monitor resource usage for performance optimization. The snippet illustrates best practices for batch processing and memory tracking in .NET applications.
// Prompt: Generate QR Code barcode and monitor memory usage during large batch generation for optimization.
// Tags: qr code, barcode generation, memory monitoring, batch processing, aspose.barcode, png output

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of QR Code barcodes while tracking memory usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, generates a set of QR Code images,
    /// and writes memory consumption after each file is saved.
    /// </summary>
    static void Main()
    {
        // Number of QR codes to generate in this batch
        const int batchSize = 5;

        // Create a unique temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        Console.WriteLine($"Generating {batchSize} QR codes in: {outputFolder}");

        // Get reference to the current process for memory monitoring
        Process currentProcess = Process.GetCurrentProcess();

        // Loop through each barcode to generate
        for (int i = 1; i <= batchSize; i++)
        {
            // Text to encode in the QR code
            string codeText = $"Sample QR {i}";

            // Destination file path for the generated PNG image
            string filePath = Path.Combine(outputFolder, $"qr_{i}.png");

            // Create and configure the barcode generator
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set module size (pixel dimension) for the QR code
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Use high error correction level for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Save the generated QR code as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Retrieve current private memory usage (in bytes)
            long memoryBytes = currentProcess.PrivateMemorySize64;

            // Output file path and memory usage in megabytes
            Console.WriteLine($"Generated {filePath} | Memory usage: {memoryBytes / 1024 / 1024} MB");
        }

        Console.WriteLine("Batch generation completed.");
    }
}