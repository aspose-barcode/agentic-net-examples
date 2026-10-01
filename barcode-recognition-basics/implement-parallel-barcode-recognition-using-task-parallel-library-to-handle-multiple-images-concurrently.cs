// Title: Parallel barcode recognition using Aspose.BarCode and TPL
// Description: Demonstrates how to generate sample barcode images and recognize them concurrently using the Task Parallel Library.
// Category-Description: This example belongs to the Aspose.BarCode image processing category, showcasing barcode generation, multi‑core recognition, and cleanup. It uses BarCodeGenerator, BarCodeReader, and related settings, illustrating typical scenarios where developers need to process many barcode images efficiently in parallel.
// Prompt: Implement parallel barcode recognition using Task Parallel Library to handle multiple images concurrently.
// Tags: barcode, parallel, tpl, generation, recognition, code128, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates parallel barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs parallel recognition tasks, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Generate a set of sample barcode images in a temporary folder.
        List<string> barcodeFiles = GenerateSampleBarcodes();

        // Configure the reader to use all processor cores for maximum performance.
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // Create a task for each image to read it concurrently.
        List<Task> readTasks = new List<Task>();
        foreach (string filePath in barcodeFiles)
        {
            readTasks.Add(Task.Run(() => ReadBarcodeAsync(filePath)));
        }

        // Wait for all reading tasks to complete.
        Task.WaitAll(readTasks.ToArray());

        // Clean up temporary files.
        foreach (string filePath in barcodeFiles)
        {
            try
            {
                File.Delete(filePath);
            }
            catch
            {
                // Ignore any cleanup errors.
            }
        }
    }

    // Generates a few barcode images and returns their file paths.
    private static List<string> GenerateSampleBarcodes()
    {
        string tempDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        var files = new List<string>();
        string[] sampleTexts = { "ABC123", "9876543210", "HelloWorld", "Aspose2024", "Parallel" };
        BaseEncodeType encodeType = EncodeTypes.Code128; // Use Code128 for all samples.

        foreach (string text in sampleTexts)
        {
            string filePath = Path.Combine(tempDir, $"{text}.png");
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                // Optional visual settings.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Resolution = 300f;

                // Save the barcode image.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            files.Add(filePath);
        }

        return files;
    }

    // Reads a single barcode image and writes the result to the console.
    private static void ReadBarcodeAsync(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Use DecodeType.AllSupportedTypes to detect any symbology.
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
        try
        {
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                // Optional quality settings.
                reader.QualitySettings = QualitySettings.HighPerformance;
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                BarCodeResult[] results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine($"No barcode detected in file: {Path.GetFileName(imagePath)}");
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
        catch (ArgumentException ex) when (ex.Message.Contains("Image loading failed"))
        {
            Console.WriteLine($"Failed to load image '{Path.GetFileName(imagePath)}': {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing '{Path.GetFileName(imagePath)}': {ex.Message}");
        }
    }
}