// Title: Fallback barcode decoder with single‑thread fallback
// Description: Demonstrates reading a barcode using multithreaded processing and automatically falling back to single‑thread mode when memory limits are exceeded.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to configure BarCodeReader processor settings for multi‑core and single‑core execution. It illustrates typical use cases such as handling large images or memory‑constrained environments where developers need to switch processing modes dynamically.
// Prompt: Implement a fallback decoder that switches to single‑thread mode if multithreaded processing exceeds memory limits.
// Tags: qr, fallback, multithread, singlethread, memory, barcodereader, aspose.barcode, decode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode, attempts to read it using multithreaded processing,
/// and falls back to single‑threaded processing if an exception (e.g., memory pressure) occurs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with multithreading,
    /// and retries with a single thread on failure.
    /// </summary>
    static void Main()
    {
        // Generate a sample QR barcode and keep it in memory
        using (var ms = new MemoryStream())
        {
            var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample fallback barcode");
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading

            // Load the image into a bitmap for recognition
            using (var bitmap = new Bitmap(ms))
            {
                // Enable multi‑threaded processing (use all available CPU cores)
                BarCodeReader.ProcessorSettings.UseAllCores = true;

                try
                {
                    // Attempt to read using multi‑threaded mode
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        Console.WriteLine("Reading with multi‑threaded mode:");
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // If any exception occurs (e.g., memory pressure), fall back to single‑thread mode
                    Console.WriteLine($"Multi‑threaded read failed: {ex.Message}");
                    Console.WriteLine("Switching to single‑thread mode...");

                    // Configure processor to use only one core
                    BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;

                    // Re‑attempt reading with single‑threaded settings
                    using (var singleReader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        Console.WriteLine("Reading with single‑threaded mode:");
                        foreach (var result in singleReader.ReadBarCodes())
                        {
                            Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}