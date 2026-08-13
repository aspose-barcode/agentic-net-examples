// Title: Fallback Barcode Decoder with Single‑Thread Fallback
// Description: Demonstrates decoding a barcode using multithreaded processing and automatically falling back to single‑thread mode when memory limits are exceeded.
// Category-Description: This example belongs to the Aspose.BarCode decoding category, showcasing how to configure BarCodeReader.ProcessorSettings for high‑performance, multithreaded barcode recognition. It illustrates typical use cases such as processing large images or batch decoding where memory consumption may vary, and provides a graceful fallback strategy for developers who need reliable decoding without crashes.
// Prompt: Implement a fallback decoder that switches to single‑thread mode if multithreaded processing exceeds memory limits.
// Tags: barcode, decoding, multithread, fallback, memory, aspose.barcode, code128, processorsettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that decodes a barcode image, using multithreaded processing with a fallback to single‑thread mode on <see cref="OutOfMemoryException"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode if needed, then attempts to decode it using multithreaded processing,
    /// falling back to single‑thread mode if memory is insufficient.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a sample barcode image path
        string imagePath = "sample.png";

        // Ensure a barcode image exists (generate if missing)
        if (!File.Exists(imagePath))
        {
            GenerateSampleBarcode(imagePath);
        }

        // Attempt to decode using multithreaded mode
        bool decoded = false;
        try
        {
            // Enable all CPU cores for processing
            BarCodeReader.ProcessorSettings.UseAllCores = true;
            decoded = DecodeBarcodes(imagePath);
        }
        catch (OutOfMemoryException)
        {
            Console.WriteLine("OutOfMemoryException caught: switching to single‑thread mode.");
            // Fallback to single‑thread mode
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            decoded = DecodeBarcodes(imagePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        if (!decoded)
        {
            Console.WriteLine("No barcodes were detected.");
        }
    }

    // Generates a simple Code128 barcode and saves it to the specified path
    private static void GenerateSampleBarcode(string path)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(path);
            Console.WriteLine($"Generated sample barcode at '{path}'.");
        }
    }

    // Decodes barcodes from the given image file and prints results
    private static bool DecodeBarcodes(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return false;
        }

        // Use DecodeType.Code128 as an example; you can add more types if needed
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Optionally set quality preset (default is NormalQuality)
            // reader.QualitySettings = QualitySettings.HighPerformance;

            var results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes found in the image.");
                return false;
            }

            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            return true;
        }
    }
}