// Title: Parallel barcode reading with processor settings limits
// Description: Demonstrates generating sample barcode images, then reading them in parallel while controlling CPU usage via BarCodeReader.ProcessorSettings and ParallelOptions.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing how to use BarCodeReader, BarcodeGenerator, and ProcessorSettings for high‑performance batch operations. Typical use cases include bulk barcode scanning, automated document processing, and performance‑tuned applications where developers need to limit thread usage and core affinity.
// Prompt: Write a script that processes a list of image paths in parallel using TPL while respecting ProcessorSettings limits.
// Tags: barcode, parallel, tpl, processorsettings, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates parallel barcode reading with controlled processor settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, configures processor settings, and reads barcodes in parallel.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> imagePaths = new List<string>();
        GenerateSampleBarcodes(tempFolder, imagePaths);

        // Configure processor settings to limit core usage and thread count
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 2;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 2;

        // Define parallel options with a maximum degree of parallelism
        ParallelOptions options = new ParallelOptions
        {
            MaxDegreeOfParallelism = 2
        };

        Console.WriteLine("Starting parallel barcode reading...");

        // Measure total processing time
        Stopwatch totalWatch = Stopwatch.StartNew();

        // Process each image path in parallel
        Parallel.ForEach(imagePaths, options, path =>
        {
            // Verify that the file exists before attempting to read
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }

            try
            {
                // Initialize the barcode reader for all supported types
                using (BarCodeReader reader = new BarCodeReader(path, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"{Path.GetFileName(path)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the image cannot be processed
                Console.WriteLine($"Failed to read {path}: {ex.Message}");
            }
        });

        totalWatch.Stop();
        Console.WriteLine($"Total processing time: {totalWatch.ElapsedMilliseconds} ms");

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates sample barcode images for a predefined set of symbologies and stores their file paths.
    /// </summary>
    /// <param name="folder">The folder where barcode images will be saved.</param>
    /// <param name="paths">The list that will receive the generated image file paths.</param>
    static void GenerateSampleBarcodes(string folder, List<string> paths)
    {
        // Sample data: tuple of (symbology name, code text)
        var samples = new (string symbology, string text)[]
        {
            ("Code128", "ABC123"),
            ("QR", "https://example.com"),
            ("DataMatrix", "DM12345"),
            ("Aztec", "AZTEC"),
            ("Pdf417", "PDF417DATA")
        };

        foreach (var (symbology, text) in samples)
        {
            // Resolve symbology name to BaseEncodeType via reflection
            var field = typeof(EncodeTypes).GetField(symbology);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {symbology}");
                continue;
            }
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a barcode generator for the specified symbology and text
            BarcodeGenerator generator = new BarcodeGenerator(encodeType, text);
            // Set common barcode parameters
            generator.Parameters.Barcode.XDimension.Point = 2.5f;
            generator.Parameters.Barcode.FilledBars = true;
            generator.Parameters.Barcode.ThrowExceptionWhenCodeTextIncorrect = false;

            // Save the generated barcode image as PNG
            string filePath = Path.Combine(folder, $"{symbology}_{Guid.NewGuid().ToString("N")}.png");
            generator.Save(filePath, BarCodeImageFormat.Png);
            paths.Add(filePath);
        }
    }
}