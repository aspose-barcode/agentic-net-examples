// Title: Process BMP barcode images with NormalQuality preset and measure execution time
// Description: Generates sample BMP barcode files, reads them using Aspose.BarCode with NormalQuality settings, and reports total processing duration.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to create barcodes with BarcodeGenerator, read them with BarCodeReader, and adjust QualitySettings for performance tuning. Typical use cases include batch processing of image files, performance benchmarking, and automated barcode validation in enterprise applications. Developers often need to generate test images, apply specific quality presets, and measure processing times for optimization.
// Prompt: Process a directory of BMP files using NormalQuality preset and record total processing time.
// Tags: barcode, bmp, normalquality, generation, recognition, performance, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates processing a directory of BMP barcode images using the NormalQuality preset
/// and records the total processing time.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample BMP barcode images using BarcodeGenerator
        string[] sampleTexts = { "123456", "ABCDEF", "987654", "ZXCVBN", "HELLO" };
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"sample{i + 1}.bmp");
            using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, sampleTexts[i]))
            {
                gen.Save(filePath, BarCodeImageFormat.Bmp);
            }
        }

        // Retrieve all BMP files from the temporary folder
        string[] bmpFiles = Directory.GetFiles(tempFolder, "*.bmp");

        // Start measuring total processing time
        Stopwatch sw = new Stopwatch();
        sw.Start();

        // Process each BMP file with NormalQuality preset
        foreach (string file in bmpFiles)
        {
            try
            {
                using (BarCodeReader reader = new BarCodeReader(
                    file,
                    DecodeType.Code128,
                    DecodeType.QR,
                    DecodeType.DataMatrix,
                    DecodeType.Aztec,
                    DecodeType.Pdf417,
                    DecodeType.Codabar,
                    DecodeType.Code39,
                    DecodeType.Code93))
                {
                    // Apply NormalQuality settings for balanced speed and accuracy
                    reader.QualitySettings = QualitySettings.NormalQuality;

                    // Read all barcodes present in the image
                    BarCodeResult[] results = reader.ReadBarCodes();

                    // Output each detected barcode's type and text
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"{Path.GetFileName(file)}: {result.CodeTypeName} - {result.CodeText}");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                // Handle cases where the file cannot be processed
                Console.WriteLine($"Failed to read {Path.GetFileName(file)}: {ex.Message}");
            }
        }

        // Stop timing and display total elapsed time
        sw.Stop();
        Console.WriteLine($"Total processing time: {sw.ElapsedMilliseconds} ms");

        // Cleanup: delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}