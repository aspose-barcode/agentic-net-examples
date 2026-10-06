// Title: Generate QR Code batch and monitor memory usage
// Description: This example creates a series of QR Code barcodes using Aspose.BarCode, saves them as PNG files, and logs the process's private memory usage after each generation to help developers optimize large‑scale barcode production.
// Category-Description: Demonstrates batch barcode generation with Aspose.BarCode's BarcodeGenerator, focusing on QR Code symbology. It shows how to configure QR parameters, save images, and monitor memory consumption, a common need for developers building high‑throughput barcode services or performing performance tuning.
// Prompt: Generate QR Code barcode and monitor memory usage during large batch generation for optimization.
// Tags: qr code, barcode generation, memory monitoring, batch processing, aspose.barcode, png, csharp

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Contains the entry point for the QR Code batch generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a batch of QR Code barcodes, saves them, and reports memory usage after each creation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch output
        string batchFolder = Path.Combine(Path.GetTempPath(), "QrBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Define how many QR codes to generate (sample size)
        int batchSize = 5;
        List<string> generatedFiles = new List<string>();

        Console.WriteLine("Starting QR code batch generation...");

        // Loop to generate each QR code
        for (int i = 1; i <= batchSize; i++)
        {
            // Prepare the text to encode and the target file path
            string codeText = $"Sample QR {i}";
            string filePath = Path.Combine(batchFolder, $"qr_{i}.png");

            // Initialize the barcode generator for QR Code symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set the size of a single QR module (pixel dimension)
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Use high error correction level for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Optionally, a specific QR version can be set:
                // generator.Parameters.Barcode.QR.Version = QRVersion.Version05;

                // Save the generated QR code as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Keep track of the generated file
            generatedFiles.Add(filePath);

            // Capture and display memory usage after this generation
            Process currentProcess = Process.GetCurrentProcess();
            long memoryBytes = currentProcess.PrivateMemorySize64;
            Console.WriteLine($"Generated {Path.GetFileName(filePath)} - Memory Usage: {memoryBytes / 1024 / 1024} MB");
        }

        Console.WriteLine("Batch generation completed.");
        Console.WriteLine($"Barcodes saved in: {batchFolder}");
    }
}