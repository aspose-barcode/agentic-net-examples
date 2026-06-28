using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and reading with fallback from multithreaded to single‑threaded processing.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a sample barcode image if missing and attempts to read it.
    /// </summary>
    static void Main()
    {
        const string imagePath = "sample_barcode.png";

        // Ensure a barcode image exists; generate one if it does not.
        if (!File.Exists(imagePath))
        {
            // Create a barcode generator for Code128 with sample data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode to the specified path.
                generator.Save(imagePath);
                Console.WriteLine($"Generated barcode image: {imagePath}");
            }
        }

        // Read the barcode using multithreaded processing, with fallback to single‑threaded if needed.
        ReadWithFallback(imagePath);
    }

    static void ReadWithFallback(string imagePath)
    {
        // Verify that the image file exists before attempting to read.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Attempt to decode using all available processor cores.
        try
        {
            // Configure the reader to use all cores.
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

            // Initialize the barcode reader for all supported decode types.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Iterate through all detected barcodes and output their type and text.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"[Multi] Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
        catch (OutOfMemoryException)
        {
            // If memory limits are hit, fall back to single‑threaded decoding.
            Console.WriteLine("Memory limit exceeded during multithreaded decoding. Switching to single‑thread mode.");

            // Restrict processing to a single core.
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;

            // Re‑initialize the reader in single‑thread mode.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"[Single] Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
        catch (Exception ex)
        {
            // Handle any other exceptions that may occur during decoding.
            Console.WriteLine($"Decoding failed: {ex.Message}");
        }
    }
}