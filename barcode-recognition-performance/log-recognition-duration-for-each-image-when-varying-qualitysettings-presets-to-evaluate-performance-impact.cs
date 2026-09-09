// Title: Barcode Recognition Performance Benchmark Across Quality Settings
// Description: Demonstrates generating sample barcodes, then measuring recognition time for each image using different QualitySettings presets to assess performance impact.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, illustrating how to use BarcodeGenerator for image creation and BarCodeReader with QualitySettings to evaluate recognition speed. Developers often need to benchmark barcode scanning under various quality configurations to choose optimal settings for their applications.
// Prompt: Log recognition duration for each image when varying QualitySettings presets to evaluate performance impact.
// Tags: barcode, performance, qualitysettings, recognition, generation, csharp, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides an example that generates barcode images and measures the time required to recognize them
/// using different <see cref="QualitySettings"/> presets. This helps evaluate the performance impact
/// of each preset on barcode recognition.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, runs recognition with various quality
    /// presets, logs the duration for each operation, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate
        var samples = new[]
        {
            new { Encode = EncodeTypes.Code128, Text = "1234567890", File = Path.Combine(tempFolder, "code128.png") },
            new { Encode = EncodeTypes.QR, Text = "https://example.com", File = Path.Combine(tempFolder, "qr.png") },
            new { Encode = EncodeTypes.DataMatrix, Text = "DataMatrixTest", File = Path.Combine(tempFolder, "datamatrix.png") }
        };

        // Generate barcode images and save them as PNG files
        foreach (var sample in samples)
        {
            using (var generator = new BarcodeGenerator(sample.Encode, sample.Text))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Save(sample.File, BarCodeImageFormat.Png);
            }
        }

        // Define the quality presets to test during recognition
        var presets = new[]
        {
            QualitySettings.NormalQuality,
            QualitySettings.HighPerformance,
            QualitySettings.HighQuality,
            QualitySettings.MaxQuality
        };

        // Perform recognition for each preset and each image, logging the elapsed time
        foreach (var preset in presets)
        {
            foreach (var sample in samples)
            {
                if (!File.Exists(sample.File))
                {
                    Console.WriteLine($"File not found: {sample.File}");
                    continue;
                }

                using (var reader = new BarCodeReader(sample.File, DecodeType.AllSupportedTypes))
                {
                    // Apply the current quality setting to the reader
                    reader.QualitySettings = preset;

                    // Measure recognition duration
                    var stopwatch = Stopwatch.StartNew();
                    var results = reader.ReadBarCodes();
                    stopwatch.Stop();

                    // Output benchmark information
                    Console.WriteLine($"Preset: {preset} | Image: {Path.GetFileName(sample.File)} | Time: {stopwatch.Elapsed.TotalMilliseconds:F2} ms | Detected: {results.Length}");
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
            // Ignore any cleanup errors
        }
    }
}