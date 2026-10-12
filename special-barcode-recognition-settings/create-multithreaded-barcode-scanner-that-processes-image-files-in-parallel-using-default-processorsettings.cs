// Title: Multithreaded Barcode Scanner Using Aspose.BarCode
// Description: Demonstrates generating several barcode images and scanning them concurrently with Parallel.ForEach and the default ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, highlighting typical scenarios such as batch processing, high‑throughput scanning, and parallel execution. Developers often need to process large sets of images efficiently, and this pattern illustrates how to leverage .NET parallelism with Aspose.BarCode APIs.
// Prompt: Create a multithreaded barcode scanner that processes image files in parallel using default ProcessorSettings.
// Tags: barcode, scanning, multithreading, parallel, aspose.barcode, processorsettings, image

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcode images and scanning them concurrently using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, scans them in parallel, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold the full paths of generated image files
        List<string> imageFiles = new List<string>();

        // Define sample barcode data (symbology and corresponding text)
        var samples = new (BaseEncodeType encodeType, string text)[]
        {
            (EncodeTypes.Code128, "CODE128-123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Aztec, "AztecDemo")
        };

        // Generate barcode images and store their file paths
        foreach (var (encodeType, text) in samples)
        {
            string filePath = Path.Combine(tempFolder, $"{encodeType}_{Guid.NewGuid().ToString("N")}.png");
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imageFiles.Add(filePath);
        }

        // Process the generated images in parallel using the default ProcessorSettings
        Parallel.ForEach(imageFiles, filePath =>
        {
            // Verify that the file exists before attempting to read it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            try
            {
                // Initialize the barcode reader for all supported symbologies
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();
                    foreach (var result in results)
                    {
                        Console.WriteLine($"{Path.GetFileName(filePath)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
            // Handle cases where the image cannot be loaded (e.g., corrupted file)
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                Console.WriteLine($"Skipping unreadable file: {filePath}");
            }
            // Catch any other unexpected exceptions during processing
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        });

        // Cleanup: delete all generated image files
        foreach (var file in imageFiles)
        {
            try { File.Delete(file); } catch { }
        }

        // Remove the temporary folder
        try { Directory.Delete(tempFolder, true); } catch { }
    }
}