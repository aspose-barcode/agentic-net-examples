// Title: Compare CPU usage between HighPerformance and MaxQuality barcode recognition presets
// Description: This example measures and compares the time taken to recognize a Code128 barcode using Aspose.BarCode's HighPerformance and MaxQuality quality settings.
// Category-Description: Demonstrates performance testing of Aspose.BarCode recognition by toggling QualitySettings presets. It showcases the use of BarcodeGenerator, BarCodeReader, and QualitySettings classes to generate a sample barcode, read it, and time the operation with Stopwatch. Developers often need to balance speed versus accuracy when processing large volumes of barcodes, making this pattern useful for benchmarking and optimization.
// Prompt: Compare CPU usage between HighPerformance and MaxQuality presets using Stopwatch timing measurements.
// Tags: code128, barcode recognition, performance, qualitysettings, stopwatch, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to compare CPU usage (recognition time) between
/// HighPerformance and MaxQuality QualitySettings presets using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, measures recognition times,
    /// and outputs the comparison.
    /// </summary>
    static void Main()
    {
        // Define the path for the temporary barcode image
        string imagePath = "sample.png";

        // Generate a Code128 barcode and save it to the specified path
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            generator.Save(imagePath);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Local function that measures the time required to read the barcode
        // using a specific QualitySettings preset.
        long MeasureRecognitionTime(QualitySettings preset)
        {
            // Initialize the reader for the generated image and specify the expected symbology
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Apply the desired quality preset (HighPerformance or MaxQuality)
                reader.QualitySettings = preset;

                // Start timing the recognition process
                Stopwatch sw = Stopwatch.StartNew();

                // Perform the read operation; results are enumerated to ensure full processing
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    // No additional processing needed; iteration forces full decode
                }

                // Stop the timer and return elapsed milliseconds
                sw.Stop();
                return sw.ElapsedMilliseconds;
            }
        }

        // Measure recognition time with the HighPerformance preset
        long highPerfTime = MeasureRecognitionTime(QualitySettings.HighPerformance);

        // Measure recognition time with the MaxQuality preset
        long maxQualityTime = MeasureRecognitionTime(QualitySettings.MaxQuality);

        // Output the timing comparison to the console
        Console.WriteLine($"HighPerformance recognition time: {highPerfTime} ms");
        Console.WriteLine($"MaxQuality recognition time: {maxQualityTime} ms");
    }
}