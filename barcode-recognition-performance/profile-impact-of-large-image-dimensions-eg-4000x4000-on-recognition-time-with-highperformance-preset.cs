// Title: Profile HighPerformance Barcode Recognition on Large Images
// Description: Demonstrates generating a 4000x4000 QR code image and measuring the time required to recognize it using the HighPerformance quality preset.
// Category-Description: This example belongs to the Aspose.BarCode performance profiling category. It shows how to use BarcodeGenerator to create large barcode images and BarCodeReader with QualitySettings.HighPerformance to evaluate recognition speed. Developers working on high‑throughput scanning or large‑format barcode processing can use these APIs to benchmark and optimize their solutions.
// Prompt: Profile the impact of large image dimensions (e.g., 4000x4000) on recognition time with HighPerformance preset.
// Tags: qr,barcode,recognition,performance,highperformance,large-image,aspose.barcode,generation,reading

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a large QR code image and profiling its recognition time using the HighPerformance preset.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a 4000x4000 QR barcode, measures recognition time, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodeFile = Path.Combine(tempFolder, "large.png");

        // Generate a QR barcode with large image dimensions (4000x4000)
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set image size to 4000x4000 pixels
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;
            generator.Parameters.ImageWidth.Pixels = 4000f;
            generator.Parameters.ImageHeight.Pixels = 4000f;

            // Save the barcode image as PNG
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodeFile))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Measure recognition time using the HighPerformance quality preset
        Stopwatch sw = Stopwatch.StartNew();
        using (var reader = new BarCodeReader(barcodeFile, DecodeType.QR))
        {
            // Apply HighPerformance settings to speed up recognition
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform barcode detection
            var results = reader.ReadBarCodes();
            sw.Stop();

            // Output profiling results
            Console.WriteLine($"Recognition time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder
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