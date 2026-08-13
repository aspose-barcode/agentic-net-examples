// Title: Parallel Barcode Image Processing with TPL and ProcessorSettings
// Description: Demonstrates generating sample Code128 barcode images and reading them in parallel while limiting CPU usage via BarCodeReader.ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode operations collection focusing on barcode generation, recognition, and performance tuning. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and ProcessorSettings, illustrating typical scenarios where developers need to process large batches of barcode images efficiently using TPL while controlling resource consumption.
// Prompt: Write a script that processes a list of image paths in parallel using TPL while respecting ProcessorSettings limits.
// Tags: code128, barcode-generation, barcode-recognition, parallel-processing, tpls, processorsettings, aspose-barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that generates barcode images, then reads them in parallel
/// while respecting the ProcessorSettings limits to control CPU usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, configures parallel processing limits,
    /// reads barcodes from the images in parallel, and cleans up temporary files.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for sample barcode images
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample");
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Generate a few sample barcode images (Code128)
        // --------------------------------------------------------------------
        var sampleTexts = new[] { "ABC123", "XYZ789", "HELLO", "WORLD", "TEST01" };
        var imagePaths = new List<string>();

        foreach (var text in sampleTexts)
        {
            string filePath = Path.Combine(tempFolder, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Configure ProcessorSettings to limit parallelism
        // Use only half of the available cores (at least 1)
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Math.Max(1, Environment.ProcessorCount / 2);
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount;

        // --------------------------------------------------------------------
        // Prepare ParallelOptions respecting the configured core count
        // --------------------------------------------------------------------
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount
        };

        Console.WriteLine($"Processing {imagePaths.Count} images using up to {parallelOptions.MaxDegreeOfParallelism} parallel tasks.");

        // --------------------------------------------------------------------
        // Process the list of image paths in parallel
        // --------------------------------------------------------------------
        Parallel.ForEach(imagePaths, parallelOptions, imagePath =>
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            try
            {
                using (var reader = new BarCodeReader(imagePath))
                {
                    var results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in {Path.GetFileName(imagePath)}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(imagePath)}: {ex.Message}");
            }
        });

        // --------------------------------------------------------------------
        // Cleanup: delete temporary files and folder
        // --------------------------------------------------------------------
        foreach (var path in imagePaths)
        {
            try { File.Delete(path); } catch { /* ignore */ }
        }
        try { Directory.Delete(tempFolder, true); } catch { /* ignore */ }

        Console.WriteLine("Processing completed.");
    }
}