using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and recognition of Code39 barcodes using Aspose.BarCode.
/// Measures performance of both operations and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a set of Code39 barcodes, recognizes them, and reports timing.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare sample data: an array of strings to encode as barcodes
        // ------------------------------------------------------------
        string[] sampleTexts = new string[]
        {
            "CODE39-1",
            "CODE39-2",
            "CODE39-3",
            "CODE39-4",
            "CODE39-5"
        };

        // Create a temporary directory to store generated barcode images
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // ------------------------------------------------------------
        // Measure generation time for Code39 barcodes
        // ------------------------------------------------------------
        Stopwatch genWatch = Stopwatch.StartNew();

        for (int i = 0; i < sampleTexts.Length; i++)
        {
            // Build file path for the current barcode image
            string filePath = Path.Combine(tempDir, $"code39_{i}.png");

            // Generate barcode with full ASCII Code39 encoding
            using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, sampleTexts[i]))
            {
                // Optional: set resolution for consistency across images
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode image to the temporary directory
                generator.Save(filePath);
            }
        }

        genWatch.Stop();
        Console.WriteLine($"Generation of {sampleTexts.Length} Code39 barcodes took {genWatch.ElapsedMilliseconds} ms.");

        // ------------------------------------------------------------
        // Measure recognition time for the generated barcodes
        // ------------------------------------------------------------
        Stopwatch recWatch = Stopwatch.StartNew();

        for (int i = 0; i < sampleTexts.Length; i++)
        {
            // Build file path for the current barcode image
            string filePath = Path.Combine(tempDir, $"code39_{i}.png");

            // Initialize a barcode reader for full ASCII Code39 decoding
            using (var reader = new BarCodeReader(filePath, DecodeType.Code39FullASCII))
            {
                // Iterate through all recognized barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output recognized text (optional)
                    Console.WriteLine($"Recognized: {result.CodeText}");
                }
            }
        }

        recWatch.Stop();
        Console.WriteLine($"Recognition of {sampleTexts.Length} Code39 barcodes took {recWatch.ElapsedMilliseconds} ms.");

        // ------------------------------------------------------------
        // Cleanup temporary files and directory
        // ------------------------------------------------------------
        try
        {
            // Delete each file in the temporary directory
            foreach (var file in Directory.GetFiles(tempDir))
                File.Delete(file);

            // Remove the temporary directory itself
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}