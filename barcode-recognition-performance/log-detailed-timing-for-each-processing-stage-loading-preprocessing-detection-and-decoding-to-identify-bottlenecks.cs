using System;
using System.Diagnostics;
using System.IO;
using Aspose.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, loading, preprocessing, detection, and decoding
/// while measuring the time taken for each stage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, reads it back, and outputs detection results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary file path for the sample barcode image
        // --------------------------------------------------------------------
        string tempDir = Path.GetTempPath();
        string imagePath = Path.Combine(tempDir, "sample_barcode.png");

        // --------------------------------------------------------------------
        // Stage: Barcode generation (setup)
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Save the generated barcode to the temporary file
            generator.Save(imagePath);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at {imagePath}");
            return;
        }

        // Stopwatch for timing each processing stage
        var stopwatch = new Stopwatch();

        // --------------------------------------------------------------------
        // Stage: Loading the image
        // --------------------------------------------------------------------
        stopwatch.Start();
        using (var bitmap = new Bitmap(imagePath))
        {
            stopwatch.Stop();
            Console.WriteLine($"Loading time: {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Reset();

            // ----------------------------------------------------------------
            // Stage: Preprocessing (configure reader and quality settings)
            // ----------------------------------------------------------------
            stopwatch.Start();
            using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
            {
                // Example preprocessing: set high performance quality preset
                reader.QualitySettings = QualitySettings.HighPerformance;
                stopwatch.Stop();
                Console.WriteLine($"Preprocessing time: {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Reset();

                // ----------------------------------------------------------------
                // Stage: Detection (read barcodes)
                // ----------------------------------------------------------------
                stopwatch.Start();
                var results = reader.ReadBarCodes();
                stopwatch.Stop();
                Console.WriteLine($"Detection time: {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Reset();

                // ----------------------------------------------------------------
                // Stage: Decoding (extract code text and other info)
                // ----------------------------------------------------------------
                stopwatch.Start();
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Confidence: {result.Confidence}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
                stopwatch.Stop();
                Console.WriteLine($"Decoding time: {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Reset();
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary image file
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}