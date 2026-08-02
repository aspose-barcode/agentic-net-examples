// Title: Log barcode recognition duration across QualitySettings presets
// Description: Generates sample Code128 barcodes, then measures and logs the time taken to recognize each image using different QualitySettings presets.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, demonstrating how to use the BarCodeReader with various QualitySettings (HighPerformance, NormalQuality, HighQuality, MaxQuality) to assess recognition speed. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and QualitySettings, which developers commonly use when optimizing barcode scanning in batch processing or real‑time applications.
// Prompt: Log recognition duration for each image when varying QualitySettings presets to evaluate performance impact.
// Tags: code128, barcode generation, barcode recognition, png, qualitysettings, performance, aspose.barcode, barcodereader, barcodegenerator

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate barcode images and log recognition duration
/// while varying <see cref="QualitySettings"/> presets using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, then measures
    /// recognition time for each image under different quality settings.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output folder for generated barcode images
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // --------------------------------------------------------------------
        // Generate sample barcode images (5 images) using Code128 symbology
        // --------------------------------------------------------------------
        List<string> imagePaths = new List<string>();
        for (int i = 1; i <= 5; i++)
        {
            string codeText = $"CODE{i:D3}";
            string filePath = Path.Combine(outputFolder, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Define QualitySettings presets to evaluate
        // --------------------------------------------------------------------
        var presets = new List<(string Name, QualitySettings Settings)>
        {
            ("HighPerformance", QualitySettings.HighPerformance),
            ("NormalQuality", QualitySettings.NormalQuality),
            ("HighQuality", QualitySettings.HighQuality),
            ("MaxQuality", QualitySettings.MaxQuality)
        };

        // --------------------------------------------------------------------
        // Evaluate each preset by measuring recognition duration per image
        // --------------------------------------------------------------------
        foreach (var preset in presets)
        {
            Console.WriteLine($"--- Evaluating preset: {preset.Name} ---");
            foreach (string imagePath in imagePaths)
            {
                // Verify that the image file exists before attempting recognition
                if (!File.Exists(imagePath))
                {
                    Console.WriteLine($"File not found: {imagePath}");
                    continue;
                }

                // Initialize BarCodeReader with all supported decode types
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    // Apply the current QualitySettings preset
                    reader.QualitySettings = preset.Settings;

                    // Start timing the recognition process
                    Stopwatch sw = Stopwatch.StartNew();
                    var results = reader.ReadBarCodes();
                    sw.Stop();

                    // Log elapsed time and number of detected barcodes
                    Console.WriteLine($"{Path.GetFileName(imagePath)}: {sw.ElapsedMilliseconds} ms, Detected {results.Length} barcode(s)");
                    foreach (var result in results)
                    {
                        Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                    }
                }
            }
            Console.WriteLine();
        }
    }
}