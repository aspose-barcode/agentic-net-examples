// Title: QR Code Batch Generation with Memory Usage Monitoring
// Description: Demonstrates how to generate multiple QR Code barcodes using Aspose.BarCode and track process memory consumption for each generated image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on QR Code creation and performance profiling. It showcases the BarcodeGenerator class, QR encoding settings, and memory diagnostics via System.Diagnostics.Process, which are common tasks for developers optimizing bulk barcode production.
// Prompt: Generate QR Code barcode and monitor memory usage during large batch generation for optimization.
// Tags: qr code, barcode generation, memory monitoring, batch processing, aspose.barcode, aspose.barcode.generation, performance profiling

using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch generation of QR Code barcodes while monitoring memory usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a set of QR codes, saves them as PNG files,
    /// and logs memory consumption before and after each generation.
    /// </summary>
    static void Main()
    {
        // Number of QR codes to generate in the batch
        const int batchSize = 5;

        // Create a unique temporary folder to store generated QR code images
        string outputFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Obtain a reference to the current process for memory measurement
        Process currentProcess = Process.GetCurrentProcess();

        Console.WriteLine($"Generating {batchSize} QR codes in folder: {outputFolder}");

        // Loop through each QR code generation iteration
        for (int i = 1; i <= batchSize; i++)
        {
            // Record memory usage before generating the QR code
            long memoryBefore = currentProcess.PrivateMemorySize64;

            // Define the text to encode in the QR code
            string codeText = $"Sample QR {i}";
            // Determine the output file path for the current QR code image
            string filePath = Path.Combine(outputFolder, $"qr_{i}.png");

            // Create and configure the barcode generator for QR encoding
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set the size of each QR module (pixel dimension)
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                // Set the error correction level to Medium (Level M)
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                // Save the generated QR code as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Record memory usage after the QR code has been generated and saved
            long memoryAfter = currentProcess.PrivateMemorySize64;
            long memoryDiff = memoryAfter - memoryBefore;

            // Output details about the generated QR code and memory consumption
            Console.WriteLine($"QR {i}: Saved to {filePath}");
            Console.WriteLine($"Memory before: {memoryBefore / 1024} KB, after: {memoryAfter / 1024} KB, diff: {memoryDiff / 1024} KB");
        }

        Console.WriteLine("Batch generation completed.");
    }
}