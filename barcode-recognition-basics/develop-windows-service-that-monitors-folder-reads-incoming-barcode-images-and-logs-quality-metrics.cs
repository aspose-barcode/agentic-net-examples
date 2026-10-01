// Title: Barcode generation, recognition, and quality metrics logging example
// Description: Demonstrates creating sample barcode images, reading them with Aspose.BarCode, and logging each barcode's reading quality and region data.
// Category-Description: This example belongs to the Aspose.BarCode for .NET suite, illustrating how to generate barcodes, recognize multiple symbologies, and retrieve quality metrics using BarCodeGenerator, BarCodeReader, and BarCodeResult classes. Typical use cases include batch processing of scanned images, quality assurance of barcode scans, and automated logging in services or applications. Developers often need to generate test images, decode them, and evaluate reading quality for monitoring and diagnostics.
// Prompt: Develop a Windows service that monitors a folder, reads incoming barcode images, and logs quality metrics.
// Tags: barcode, generation, recognition, quality-metrics, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating sample barcodes, reading them, and logging quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads each image, and outputs barcode data and reading quality.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary folder for the batch operation
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // List to hold generated barcode image file paths
        List<string> barcodeFiles = new List<string>();

        // Generate sample barcode images and collect their file paths
        GenerateSampleBarcodes(batchFolder, barcodeFiles);

        // Process each barcode image and log quality metrics
        foreach (string filePath in barcodeFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Initialize reader for all supported barcode types
                using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Read all barcodes from the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in file: {Path.GetFileName(filePath)}");
                        continue;
                    }

                    // Log information for each detected barcode
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(filePath)}");
                        Console.WriteLine($"  Code Text      : {result.CodeText}");
                        Console.WriteLine($"  Symbology      : {result.CodeTypeName}");
                        Console.WriteLine($"  Reading Quality: {result.ReadingQuality}");

                        // Log region bounds and angle
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"  Region X       : {bounds.X}");
                        Console.WriteLine($"  Region Y       : {bounds.Y}");
                        Console.WriteLine($"  Region Width   : {bounds.Width}");
                        Console.WriteLine($"  Region Height  : {bounds.Height}");
                        Console.WriteLine($"  Angle          : {result.Region.Angle}");
                    }
                }
            }
            catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
            {
                // Handle unsupported image formats gracefully
                Console.WriteLine($"Skipping unsupported file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors during processing
                Console.WriteLine($"Error processing file '{Path.GetFileName(filePath)}': {ex.Message}");
            }
        }

        // Cleanup: delete the temporary folder and its contents
        try
        {
            Directory.Delete(batchFolder, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to delete temporary folder: {ex.Message}");
        }
    }

    // Generates a few sample barcode images and records their file paths
    private static void GenerateSampleBarcodes(string folderPath, List<string> fileList)
    {
        // Sample data: tuple of (symbology, code text, file name)
        var samples = new (BaseEncodeType encodeType, string codeText, string fileName)[]
        {
            (EncodeTypes.Code128, "ABC123456", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.EAN13, "5901234123457", "ean13.png")
        };

        foreach (var sample in samples)
        {
            string filePath = Path.Combine(folderPath, sample.fileName);
            using (BarcodeGenerator generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
            {
                // Optional: set some visual parameters
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.Padding.Left.Point = 5f;
                generator.Parameters.Barcode.Padding.Top.Point = 5f;

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }

            // Add the generated file path to the list for later processing
            fileList.Add(filePath);
        }
    }
}