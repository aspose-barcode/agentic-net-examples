// Title: Multithreaded Barcode Reading with ProcessorSettings Reset
// Description: Demonstrates generating sample barcodes, reading them using Aspose.BarCode in a multithreaded manner, and resetting ProcessorSettings to their default values after the job.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and the ProcessorSettings class to control multithreading behavior. Developers often need to fine‑tune thread usage for high‑throughput scanning scenarios and then restore defaults to avoid side effects in subsequent operations. The snippet serves as a reference for configuring, executing, and cleaning up multithreaded barcode processing tasks.
// Prompt: Write a script that resets ProcessorSettings to default values after completing a multithreaded barcode job.
// Tags: code128, multithreading, processorsettings, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, multithreaded recognition, and resetting processor settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// Generates sample barcodes, reads them concurrently, and restores default processor settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i + 1}"))
            {
                // Set barcode color to black
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                // Save the barcode as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Configure multithreaded recognition settings
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        Console.WriteLine("Starting multithreaded barcode reading...");

        // Measure the time taken for the recognition process
        Stopwatch sw = Stopwatch.StartNew();

        // Read each generated barcode file
        foreach (string file in barcodeFiles)
        {
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)} - Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        sw.Stop();
        Console.WriteLine($"Reading completed in {sw.ElapsedMilliseconds} ms.");

        // Reset ProcessorSettings to default values
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        Console.WriteLine("ProcessorSettings have been reset to default values.");

        // Clean up temporary files
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }
}