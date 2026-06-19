using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.BarCodeRecognition; // for ChecksumValidation enum

/// <summary>
/// Demonstrates generating Code128 barcodes, recognizing them, and benchmarking
/// the impact of checksum validation settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, runs recognition with default and forced
    /// checksum validation, reports confidence and timing, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Define sample texts to encode as Code128 barcodes
        string[] samples = new string[]
        {
            "1234567890",
            "ABCDEFGHIJ",
            "CODE128TEST",
            "9876543210",
            "A1B2C3D4E5"
        };

        // Create a temporary directory for storing generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(tempDir))
            Directory.CreateDirectory(tempDir);

        Console.WriteLine("Generating barcode images...");

        // Generate a PNG image for each sample text
        string[] imagePaths = new string[samples.Length];
        for (int i = 0; i < samples.Length; i++)
        {
            string filePath = Path.Combine(tempDir, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, samples[i]))
            {
                // Save the barcode image to the temporary file
                generator.Save(filePath);
            }
            imagePaths[i] = filePath;
        }

        Console.WriteLine("Starting recognition benchmark...");

        // Iterate over each generated image and benchmark recognition
        for (int i = 0; i < imagePaths.Length; i++)
        {
            string path = imagePaths[i];
            Console.WriteLine($"\nSample {i + 1}: {samples[i]}");

            // ---------- Default checksum validation (no explicit setting) ----------
            double defaultConfidence = 0;
            long defaultTicks = 0;
            using (var readerDefault = new BarCodeReader(path, DecodeType.Code128))
            {
                var sw = Stopwatch.StartNew(); // Start timing
                foreach (var result in readerDefault.ReadBarCodes())
                {
                    // Capture the confidence of the last read result
                    defaultConfidence = (double)result.Confidence;
                }
                sw.Stop(); // Stop timing
                defaultTicks = sw.ElapsedTicks;
            }

            // ---------- Forced checksum validation (ChecksumValidation.On) ----------
            double forcedConfidence = 0;
            long forcedTicks = 0;
            using (var readerForced = new BarCodeReader(path, DecodeType.Code128))
            {
                // Enable checksum validation explicitly
                readerForced.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                var sw = Stopwatch.StartNew(); // Start timing
                foreach (var result in readerForced.ReadBarCodes())
                {
                    // Capture the confidence of the last read result
                    forcedConfidence = (double)result.Confidence;
                }
                sw.Stop(); // Stop timing
                forcedTicks = sw.ElapsedTicks;
            }

            // Output benchmark results for the current sample
            Console.WriteLine($"Default Validation - Confidence: {defaultConfidence}, Time (ticks): {defaultTicks}");
            Console.WriteLine($"Forced Validation  - Confidence: {forcedConfidence}, Time (ticks): {forcedTicks}");
        }

        // ---------- Cleanup temporary files ----------
        try
        {
            foreach (var file in imagePaths)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }

        Console.WriteLine("\nBenchmark completed.");
    }
}