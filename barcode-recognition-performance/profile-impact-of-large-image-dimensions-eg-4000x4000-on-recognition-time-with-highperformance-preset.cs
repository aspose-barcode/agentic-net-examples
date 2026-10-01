// Title: Profile barcode recognition time for large 4000x4000 images using HighPerformance preset
// Description: Demonstrates generating a 4000x4000 PNG barcode image and measuring the time required to recognize it with the HighPerformance quality preset.
// Category-Description: This example belongs to the Aspose.BarCode image processing and recognition category. It showcases the use of BarcodeGenerator for creating large barcode images and BarCodeReader with QualitySettings to evaluate performance. Developers often need to benchmark recognition speed for high‑resolution images, adjust quality presets, and handle large files efficiently.
// Prompt: Profile the impact of large image dimensions (e.g., 4000x4000) on recognition time with HighPerformance preset.
// Tags: barcode, code128, performance, highperformance, recognition, large-image, aspose.barcode, c#

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a large barcode image and profiling its recognition time using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a large barcode, verifies creation, profiles recognition, and cleans up.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary file path for the large barcode image
        string tempFile = Path.Combine(Path.GetTempPath(), "LargeBarcode_" + Guid.NewGuid().ToString("N") + ".png");

        try
        {
            // Generate a barcode image with 4000x4000 dimensions
            GenerateLargeBarcode(tempFile);

            // Verify that the file was created
            if (!File.Exists(tempFile))
            {
                Console.WriteLine("Failed to create the barcode image.");
                return;
            }

            // Recognize the barcode and profile the time using HighPerformance preset
            RecognizeAndProfile(tempFile);
        }
        finally
        {
            // Clean up the temporary file
            if (File.Exists(tempFile))
            {
                try
                {
                    File.Delete(tempFile);
                }
                catch
                {
                    // Ignored – cleanup failure should not crash the program
                }
            }
        }
    }

    /// <summary>
    /// Generates a 4000x4000 PNG barcode image using Code128 symbology.
    /// </summary>
    /// <param name="outputPath">Full path where the barcode image will be saved.</param>
    static void GenerateLargeBarcode(string outputPath)
    {
        // Use Code128 as an example symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set manual image size to 4000x4000 pixels
            generator.Parameters.ImageWidth.Pixels = 4000f;
            generator.Parameters.ImageHeight.Pixels = 4000f;

            // Ensure the manually set size is respected
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Optional: set resolution for better quality
            generator.Parameters.Resolution = 300f;

            // Save the barcode as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Recognizes barcodes in the specified image and measures the recognition time using the HighPerformance preset.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    static void RecognizeAndProfile(string imagePath)
    {
        // Ensure the image file exists before attempting recognition
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Image file not found: " + imagePath);
            return;
        }

        // Create a barcode reader that attempts to detect all supported types
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the HighPerformance quality preset
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Measure recognition time
            Stopwatch sw = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            sw.Stop();

            Console.WriteLine($"Recognition time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Barcodes detected: {results.Length}");

            // Output details of each detected barcode
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeText: {result.CodeText}, Type: {result.CodeTypeName}");
            }
        }
    }
}