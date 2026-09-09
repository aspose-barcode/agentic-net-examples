// Title: Parallel barcode reading with processor core limits
// Description: Demonstrates generating sample Code128 barcode images, then reading them in parallel using TPL while honoring Aspose.BarCode ProcessorSettings core limits.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing how to configure BarCodeReader.ProcessorSettings for multithreaded operations, use Parallel.ForEach with a degree of parallelism, and handle barcode generation and recognition. Developers often need to balance performance and resource usage when scanning many images, making this pattern useful for batch processing scenarios.
// Prompt: Write a script that processes a list of image paths in parallel using TPL while respecting ProcessorSettings limits.
// Tags: barcode, parallel, tpl, aspose.barcode, code128, png, processorsettings

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point for the barcode parallel processing example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample barcode images, configures processor settings, reads barcodes in parallel, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        var filePaths = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            GenerateBarcodeImage($"Sample{i}", filePath);
            filePaths.Add(filePath);
        }

        // Configure ProcessorSettings to limit the number of cores used by the reader
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 2;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 4;

        // Set up ParallelOptions to respect the core limit (max 2 concurrent tasks)
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 2 };

        // Process each image in parallel, reading any barcodes it contains
        Parallel.ForEach(filePaths, parallelOptions, filePath =>
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error reading {filePath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error for {filePath}: {ex.Message}");
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

    /// <summary>
    /// Generates a Code128 barcode image with the specified text and saves it to the given path.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the PNG image will be saved.</param>
    static void GenerateBarcodeImage(string codeText, string outputPath)
    {
        var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText);
        generator.Parameters.Barcode.BarColor = Color.Black;
        generator.Parameters.BackColor = Color.White;

        using (var ms = new MemoryStream())
        {
            // Save the barcode to a memory stream in PNG format
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0;

            // Write the memory stream contents to the output file
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(fileStream);
            }
        }
    }
}