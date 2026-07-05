// Title: Parallel barcode reading across multiple CPU cores
// Description: Demonstrates generating barcode images, then reading them concurrently using separate BarCodeReader instances per image to utilize all processor cores.
// Prompt: Parallelize barcode reading across multiple CPU cores by creating separate BarCodeReader instances for each image.
// Tags: barcode, parallel, multithreading, code128, aspose, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates parallel barcode reading using Aspose.BarCode.
/// Generates sample barcode images, reads them concurrently on multiple CPU cores,
/// and then cleans up the temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates sample barcodes, reads them in parallel, and cleans up.
    /// </summary>
    static void Main()
    {
        // Configure Aspose.BarCode to use all available processor cores for each reader instance.
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Create a temporary directory to store generated barcode images.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        Directory.CreateDirectory(tempDir);

        // Sample data to encode into barcodes.
        var sampleTexts = new List<string>
        {
            "Sample001",
            "Sample002",
            "Sample003",
            "Sample004",
            "Sample005"
        };

        // Generate barcode images and collect their file paths.
        var imagePaths = new List<string>();
        foreach (var text in sampleTexts)
        {
            string filePath = Path.Combine(tempDir, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Save each barcode as a PNG image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // Read barcodes in parallel, one BarCodeReader per image.
        Parallel.ForEach(imagePaths, imagePath =>
        {
            // Verify that the image file exists before attempting to read.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Each thread creates its own BarCodeReader instance.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Read all barcodes present in the image.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the results to the console.
                foreach (var result in results)
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Type: {result.CodeTypeName} | Text: {result.CodeText}");
                }
            }
        });

        // Cleanup temporary barcode image files (optional).
        foreach (var path in imagePaths)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
                // Ignore any errors during file deletion.
            }
        }

        // Remove the temporary directory.
        try
        {
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors during directory deletion.
        }
    }
}