using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating barcodes, applying different quality settings,
/// and measuring recognition performance using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, reads them with various quality presets,
    /// logs recognition times, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare sample barcode texts
        // ------------------------------------------------------------
        string[] codeTexts = new string[]
        {
            "ABC1234567",
            "DEF9876543",
            "GHI1122334",
            "JKL5566778",
            "MNO9990001"
        };

        // ------------------------------------------------------------
        // Create a temporary directory for barcode images
        // ------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempDir))
        {
            Directory.CreateDirectory(tempDir);
        }

        // ------------------------------------------------------------
        // Generate barcode images and save them as PNG files
        // ------------------------------------------------------------
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[i]))
            {
                // Save the generated barcode as a PNG image
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // ------------------------------------------------------------
        // Define QualitySettings presets to test during recognition
        // ------------------------------------------------------------
        var presets = new (string Name, QualitySettings Settings)[]
        {
            ("HighPerformance", QualitySettings.HighPerformance),
            ("NormalQuality", QualitySettings.NormalQuality),
            ("HighQuality", QualitySettings.HighQuality),
            ("MaxQuality", QualitySettings.MaxQuality)
        };

        // ------------------------------------------------------------
        // Measure recognition duration for each image using each preset
        // ------------------------------------------------------------
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            foreach (var preset in presets)
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality preset to the reader
                    reader.QualitySettings = preset.Settings;

                    // Start timing the recognition process
                    var stopwatch = Stopwatch.StartNew();
                    var results = reader.ReadBarCodes();
                    stopwatch.Stop();

                    // Output the elapsed time and number of detected barcodes
                    Console.WriteLine($"Image {i + 1}, Preset {preset.Name}: {stopwatch.ElapsedMilliseconds} ms, Detected {results.Length} barcode(s)");
                }
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}