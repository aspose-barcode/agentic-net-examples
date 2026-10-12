// Title: Parallel barcode generation and recognition with processor limits
// Description: Demonstrates generating multiple barcode images, then reading them in parallel while respecting Aspose.BarCode ProcessorSettings limits.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing how to use BarcodeGenerator and BarCodeReader together with TPL for high‑throughput scenarios. It illustrates configuring ProcessorSettings to control CPU core usage, a common requirement when integrating barcode operations into server‑side or batch processing pipelines. Developers often need to balance performance and resource consumption, and this snippet provides a reusable pattern for such tasks.
// Prompt: Write a script that processes a list of image paths in parallel using TPL while respecting ProcessorSettings limits.
// Tags: barcode, generation, recognition, parallel, tpl, processorsettings, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the parallel barcode generation and recognition demo.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample barcode images, configures processor limits, and reads the barcodes in parallel.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a dedicated temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type, text, file name)
        var samples = new List<(BaseEncodeType type, string text, string fileName)>
        {
            (EncodeTypes.Code128, "CODE128", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Aztec, "AZTEC", "aztec.png"),
            (EncodeTypes.Pdf417, "PDF417DATA", "pdf417.png")
        };

        var imagePaths = new List<string>();

        // Generate barcode images and collect their file paths
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.fileName);
            using (var generator = new BarcodeGenerator(sample.type, sample.text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Configure ProcessorSettings limits to control CPU usage
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 2;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 2;

        // Set up parallel options (use all logical processors as a baseline)
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };

        // Process each image in parallel, reading any barcodes it contains
        Parallel.ForEach(imagePaths, parallelOptions, imagePath =>
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            try
            {
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
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
                            Console.WriteLine($"{Path.GetFileName(imagePath)}: {result.CodeTypeName} - {result.CodeText}");
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error processing {imagePath}: {ex.Message}");
            }
        });

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
}