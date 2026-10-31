// Title: Multithreaded barcode scanning of image files using Aspose.BarCode
// Description: Demonstrates generating sample barcode images and scanning them concurrently with default ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode processing category, showcasing parallel barcode recognition using BarCodeReader and BarcodeGenerator. It illustrates typical use cases such as batch processing of scanned documents, where developers need to read multiple images efficiently. The example highlights key API classes like BarcodeGenerator, BarCodeReader, DecodeType, and ProcessorSettings for high‑throughput scenarios.
// Prompt: Create a multithreaded barcode scanner that processes image files in parallel using default ProcessorSettings.
// Tags: barcode, multithreading, parallel, processing, generation, recognition, code128, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates parallel generation and scanning of barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, scans them concurrently, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and collect their file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 5; i++)
        {
            string codeText = $"Sample{i + 1}";
            string filePath = Path.Combine(tempFolder, $"barcode{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Process the images in parallel using default ProcessorSettings
        Parallel.ForEach(barcodeFiles, filePath =>
        {
            // Verify the file exists before attempting to read
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                // Initialize the reader for all supported barcode types
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output detection results
                    if (results.Length > 0)
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"{Path.GetFileName(filePath)}: {result.CodeTypeName} - {result.CodeText}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)}: No barcode detected.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed as a barcode image
                Console.WriteLine($"Error reading {Path.GetFileName(filePath)}: {ex.Message}");
            }
        });

        // Clean up temporary files and directory
        try
        {
            foreach (var file in barcodeFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors (e.g., file in use)
        }
    }
}