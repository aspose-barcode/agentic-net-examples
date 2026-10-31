// Title: Process BMP barcode images with NormalQuality preset and measure execution time
// Description: Demonstrates generating sample BMP barcode files, reading them using Aspose.BarCode with the NormalQuality preset, and reporting the total processing duration.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and QualitySettings to control decoding performance. Typical scenarios include batch processing of image files, performance benchmarking, and automated barcode validation in .NET applications. Developers often need to generate test images, apply specific quality presets, and measure processing time for optimization.
// Prompt: Process a directory of BMP files using NormalQuality preset and record total processing time.
// Tags: barcode, symbology, generation, recognition, bmp, normalquality, performance, aspose.barcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates BMP barcode images, reads them with a NormalQuality preset,
/// and reports the total time taken for processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated BMP files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BmpProcess_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample BMP barcode images (Code128) for demonstration
        int sampleCount = 5;
        for (int i = 1; i <= sampleCount; i++)
        {
            string text = $"Sample{i}";
            string filePath = Path.Combine(tempFolder, $"barcode{i}.bmp");
            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text);
            generator.Save(filePath, BarCodeImageFormat.Bmp);
        }

        // Collect the file paths of the generated BMP images
        string[] bmpFiles = new string[sampleCount];
        for (int i = 1; i <= sampleCount; i++)
        {
            bmpFiles[i - 1] = Path.Combine(tempFolder, $"barcode{i}.bmp");
        }

        // Start measuring total processing time
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Process each BMP file using BarCodeReader with NormalQuality preset
        foreach (string file in bmpFiles)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            try
            {
                using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
                {
                    // Apply the NormalQuality preset to balance speed and accuracy
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Read all barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output each decoded result
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Failed to read '{Path.GetFileName(file)}': {ex.Message}");
            }
        }

        // Stop timing and display the total elapsed time
        sw.Stop();
        Console.WriteLine($"Total processing time: {sw.Elapsed.TotalSeconds:F3} seconds");
    }
}