// Title: Generate QR Code batch and monitor memory usage
// Description: Demonstrates creating multiple QR Code barcodes while tracking process memory to aid optimization.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with QR symbology, configure error correction, image size, and monitor memory consumption during large‑scale barcode creation. Developers often need to generate batches of barcodes efficiently and assess memory impact, making this pattern useful for performance tuning and resource‑aware applications.
// Prompt: Generate QR Code barcode and monitor memory usage during large batch generation for optimization.
// Tags: qr code, barcode generation, memory monitoring, batch processing, aspose.barcode, encode types, qrcode

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch generation of QR Code barcodes while monitoring memory usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR codes, saves them to disk, and logs memory statistics.
    /// </summary>
    static void Main()
    {
        // Define the output folder for generated QR code images.
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "QrBatch");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Sample data to encode into QR codes.
        List<string> qrTexts = new List<string>
        {
            "Sample QR 1",
            "Sample QR 2",
            "Sample QR 3",
            "Sample QR 4",
            "Sample QR 5"
        };

        // Obtain the current process to monitor private memory usage.
        Process currentProcess = Process.GetCurrentProcess();

        Console.WriteLine("Starting QR code batch generation...");

        // Iterate over each text entry and generate a corresponding QR code.
        for (int i = 0; i < qrTexts.Count; i++)
        {
            string text = qrTexts[i];
            string filePath = Path.Combine(outputFolder, $"qr_{i + 1}.png");

            // Generate QR code with high error correction and specific image dimensions.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set high error correction level (Level H).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Configure image size using interpolation mode.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the generated barcode image to the specified file.
                generator.Save(filePath);
            }

            // Force garbage collection to obtain a more accurate memory reading.
            GC.Collect();
            GC.WaitForPendingFinalizers();

            // Capture memory usage metrics.
            long privateBytes = currentProcess.PrivateMemorySize64;
            long gcBytes = GC.GetTotalMemory(forceFullCollection: false);

            // Output generation details and memory statistics.
            Console.WriteLine($"Generated QR {i + 1}: \"{text}\"");
            Console.WriteLine($"  File saved to: {filePath}");
            Console.WriteLine($"  Private memory (bytes): {privateBytes:N0}");
            Console.WriteLine($"  Managed heap memory (bytes): {gcBytes:N0}");
        }

        Console.WriteLine("QR code batch generation completed.");
    }
}