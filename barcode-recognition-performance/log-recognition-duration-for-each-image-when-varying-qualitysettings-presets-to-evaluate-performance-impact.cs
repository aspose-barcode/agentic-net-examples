// Title: Barcode recognition performance across QualitySettings presets
// Description: Demonstrates generating sample barcodes, then measuring recognition duration for each image while applying different QualitySettings presets to assess performance impact.
// Prompt: Log recognition duration for each image when varying QualitySettings presets to evaluate performance impact.
// Tags: barcode, recognition, performance, qualitysettings, aspnet, csharp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates sample barcodes and evaluates recognition speed using various QualitySettings presets.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, applies each QualitySettings preset, and logs recognition duration.
    /// </summary>
    static void Main()
    {
        // Define sample barcodes to generate and test
        var samples = new List<(BaseEncodeType encodeType, string text)>
        {
            (EncodeTypes.Code128, "1234567890"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // Define QualitySettings presets to evaluate
        var presets = new (QualitySettings settings, string name)[]
        {
            (QualitySettings.HighPerformance, "HighPerformance"),
            (QualitySettings.NormalQuality, "NormalQuality"),
            (QualitySettings.HighQuality, "HighQuality"),
            (QualitySettings.MaxQuality, "MaxQuality")
        };

        int imageIndex = 0;

        // Iterate over each sample barcode definition
        foreach (var (encodeType, text) in samples)
        {
            imageIndex++;

            // Generate barcode image for the current sample
            using (var generator = new BarcodeGenerator(encodeType, text))
            {
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Test each QualitySettings preset on the generated image
                    foreach (var (preset, presetName) in presets)
                    {
                        // Create a reader for the generated image
                        using (var reader = new BarCodeReader(bitmap))
                        {
                            // Apply the current QualitySettings preset
                            reader.QualitySettings = preset;

                            // Measure recognition time
                            var stopwatch = Stopwatch.StartNew();
                            var results = reader.ReadBarCodes();
                            stopwatch.Stop();

                            // Log the elapsed time and number of detected barcodes
                            Console.WriteLine($"Image {imageIndex}, Preset {presetName}: {stopwatch.ElapsedMilliseconds} ms, Detected {results.Length} barcode(s).");
                        }
                    }
                }
            }
        }
    }
}