// Title: Reset ProcessorSettings after multithreaded barcode processing
// Description: Demonstrates generating barcode images, configuring multithreaded recognition, and resetting ProcessorSettings to defaults.
// Category-Description: This example belongs to the Aspose.BarCode multithreading and performance tuning category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and ProcessorSettings, illustrating typical use cases like batch barcode generation, parallel recognition, and proper cleanup of processor configurations. Developers often need to adjust these settings for optimal CPU utilization and then restore defaults to avoid side effects in subsequent operations.
// Prompt: Write a script that resets ProcessorSettings to default values after completing a multithreaded barcode job.
// Tags: code128, generation, recognition, multithreading, png, barcodgenerator, barcodereader, processorsettings

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcode images, configures multithreaded barcode recognition,
/// and resets ProcessorSettings to their default values after processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, multithreaded reading, and settings reset.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample barcode texts to encode
        string[] texts = { "ABC123", "XYZ789", "HELLO", "WORLD", "TEST5" };
        string[] imagePaths = new string[texts.Length];

        // Generate barcode images using default settings
        for (int i = 0; i < texts.Length; i++)
        {
            string filePath = Path.Combine(outputDir, $"barcode_{i}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, texts[i]))
            {
                // Save the barcode as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths[i] = filePath;
        }

        // Configure ProcessorSettings for a controlled multithreaded job
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;

        Console.WriteLine("ProcessorSettings configured for multithreaded job:");
        Console.WriteLine($"  UseAllCores = {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"  UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
        Console.WriteLine($"  MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");

        // Perform barcode reading using the configured ProcessorSettings
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                continue;
            }

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.Code128))
            {
                // Iterate through all detected barcodes in the image
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
        }

        // Reset ProcessorSettings to their default values
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        Console.WriteLine("ProcessorSettings have been reset to defaults:");
        Console.WriteLine($"  UseAllCores = {BarCodeReader.ProcessorSettings.UseAllCores}");
        Console.WriteLine($"  UseOnlyThisCoresCount = {BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
        Console.WriteLine($"  MaxAdditionalAllowedThreads = {BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads}");

        // Cleanup generated files (optional)
        foreach (string path in imagePaths)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        if (Directory.Exists(outputDir))
        {
            Directory.Delete(outputDir, true);
        }
    }
}