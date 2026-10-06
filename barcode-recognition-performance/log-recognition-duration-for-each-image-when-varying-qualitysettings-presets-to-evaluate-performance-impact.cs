// Title: Barcode Recognition Performance with QualitySettings Presets
// Description: Demonstrates how to generate various barcode images, recognize them using Aspose.BarCode, and log the time taken for each image under different QualitySettings presets to assess performance impact.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader, QualitySettings, and DecodeType classes. It shows typical use cases such as batch processing of barcode images, measuring recognition speed, and comparing quality presets. Developers working on high‑throughput scanning or performance tuning can reference this pattern.
// Prompt: Log recognition duration for each image when varying QualitySettings presets to evaluate performance impact.
// Tags: barcode, recognition, performance, qualitysettings, aspose.barcode, barcodegenerator, barcodereader, decode, timing

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates sample barcodes, reads them with different QualitySettings presets,
/// and logs the recognition duration for each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, recognition, timing, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcode data (type, text, output file name)
        var samples = new List<(BaseEncodeType encodeType, string codeText, string fileName)>
        {
            (EncodeTypes.Code128, "ABC123456", "code128.png"),
            (EncodeTypes.QR, "https://example.com", "qr.png"),
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png"),
            (EncodeTypes.Pdf417, "PDF417_SAMPLE", "pdf417.png"),
            (EncodeTypes.Aztec, "AZTEC123", "aztec.png")
        };

        // Generate barcode images and save them as PNG files
        foreach (var (encodeType, codeText, fileName) in samples)
        {
            string filePath = Path.Combine(tempFolder, fileName);
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Build a list of full paths to the generated image files
        var imageFiles = new List<string>();
        foreach (var (_, _, fileName) in samples)
        {
            imageFiles.Add(Path.Combine(tempFolder, fileName));
        }

        // Define the QualitySettings presets to be evaluated
        var presets = new Dictionary<string, QualitySettings>
        {
            { "NormalQuality", QualitySettings.NormalQuality },
            { "HighPerformance", QualitySettings.HighPerformance },
            { "HighQuality", QualitySettings.HighQuality },
            { "MaxQuality", QualitySettings.MaxQuality }
        };

        // Use a decode type that supports all barcode symbologies
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Iterate over each preset, recognize all images, and log the elapsed time
        foreach (var preset in presets)
        {
            Console.WriteLine($"Preset: {preset.Key}");
            foreach (string imagePath in imageFiles)
            {
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                var stopwatch = Stopwatch.StartNew();

                // Initialize the reader with the current image and decode type
                using (var reader = new BarCodeReader(imagePath, decodeType))
                {
                    // Apply the current quality preset
                    reader.QualitySettings = preset.Value;
                    try
                    {
                        // Perform barcode recognition
                        var results = reader.ReadBarCodes();

                        // Access each result to ensure full processing (no-op)
                        foreach (var result in results)
                        {
                            string _ = result.CodeText;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading {Path.GetFileName(imagePath)}: {ex.Message}");
                    }
                }

                stopwatch.Stop();
                Console.WriteLine($"{Path.GetFileName(imagePath)} - {stopwatch.ElapsedMilliseconds} ms");
            }
            Console.WriteLine();
        }

        // Attempt to delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors (e.g., file locks)
        }
    }
}