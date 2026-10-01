// Title: Barcode Recognition Performance with QualitySettings Presets
// Description: Demonstrates how to generate Code128 barcodes, then measure recognition time using different QualitySettings presets to assess performance impact.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category. It shows how to use BarcodeGenerator for creating barcodes, BarCodeReader with DecodeType.AllSupportedTypes for recognition, and the QualitySettings property to adjust processing speed versus accuracy. Developers often need to benchmark barcode scanning under various quality configurations to choose the optimal preset for their applications.
// Prompt: Log recognition duration for each image when varying QualitySettings presets to evaluate performance impact.
// Tags: barcode, code128, performance, qualitysettings, recognition, aspose.barcode, generation, reading, benchmarking

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation and recognition timing across different QualitySettings presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates sample barcodes, measures recognition duration for each QualitySettings preset, and logs the results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample barcode texts to encode
        string[] texts = { "1234567890", "ABCDEFGHIJ", "9876543210" };

        // Generate sample barcode images (Code128) and save them as PNG files
        foreach (string text in texts)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Save the generated barcode image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Define the QualitySettings presets to evaluate
        QualitySettings[] presets = new QualitySettings[]
        {
            QualitySettings.HighPerformance,
            QualitySettings.NormalQuality,
            QualitySettings.HighQuality,
            QualitySettings.MaxQuality
        };

        // Process each generated image and measure recognition time for each preset
        string[] imageFiles = Directory.GetFiles(tempFolder, "*.png");
        foreach (string imagePath in imageFiles)
        {
            Console.WriteLine($"Processing image: {Path.GetFileName(imagePath)}");
            foreach (QualitySettings preset in presets)
            {
                // Initialize a reader that supports all barcode types
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality preset
                    reader.QualitySettings = preset;

                    // Start timing the recognition operation
                    var stopwatch = Stopwatch.StartNew();
                    try
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();
                        stopwatch.Stop();

                        // Log the outcome and elapsed time
                        if (results.Length > 0)
                        {
                            Console.WriteLine($"  Preset: {preset.GetType().Name}.{preset} - Time: {stopwatch.ElapsedMilliseconds} ms - Detected: {results[0].CodeText}");
                        }
                        else
                        {
                            Console.WriteLine($"  Preset: {preset.GetType().Name}.{preset} - Time: {stopwatch.ElapsedMilliseconds} ms - No barcode detected");
                        }
                    }
                    catch (Exception ex)
                    {
                        stopwatch.Stop();
                        Console.WriteLine($"  Preset: {preset.GetType().Name}.{preset} - Time: {stopwatch.ElapsedMilliseconds} ms - Error: {ex.Message}");
                    }
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – the folder will be removed by the OS eventually
        }
    }
}