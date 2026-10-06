// Title: Fallback barcode decoder with single‑thread fallback
// Description: Demonstrates generating a QR barcode, attempting multithreaded decoding, and falling back to single‑thread mode when memory limits are hit.
// Category-Description: This example belongs to the Aspose.BarCode decoding category, showcasing how to configure BarCodeReader processor settings for multithreaded and single‑thread operation. It covers key API classes such as BarcodeGenerator, BarCodeReader, and ProcessorSettings, typical for scenarios where large images or limited memory require adaptive threading strategies. Developers often need to switch threading modes to balance performance and resource usage.
// Prompt: Implement a fallback decoder that switches to single‑thread mode if multithreaded processing exceeds memory limits.
// Tags: qr, barcode, decoding, multithreading, fallback, aspose.barcode, processorsettings

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and a fallback decoding strategy that switches to single‑thread mode when multithreaded processing fails due to memory constraints.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR barcode, tries multithreaded decoding, and falls back to single‑thread decoding if needed.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "FallbackDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the path for the sample barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 5;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Generated barcode at: {barcodePath}");

        // First attempt: use multithreaded decoding
        bool success = TryReadBarcodes(barcodePath, useSingleThread: false);

        if (!success)
        {
            // Multithreaded read failed (likely due to memory). Switch to single‑thread mode.
            Console.WriteLine("Multithreaded read failed (likely due to memory). Switching to single‑thread mode.");

            // Configure processor settings for single‑thread operation
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
            BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

            // Retry decoding with single‑thread settings
            success = TryReadBarcodes(barcodePath, useSingleThread: true);
        }

        Console.WriteLine(success ? "Barcode read successfully." : "Failed to read barcode.");
    }

    /// <summary>
    /// Attempts to read barcodes from the specified image using either multithreaded or single‑thread settings.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image.</param>
    /// <param name="useSingleThread">If true, forces single‑thread decoding; otherwise uses multithreaded defaults.</param>
    /// <returns>True if decoding succeeds; otherwise false.</returns>
    static bool TryReadBarcodes(string imagePath, bool useSingleThread)
    {
        try
        {
            // Adjust processor settings based on the requested threading mode
            if (!useSingleThread)
            {
                // Enable default multithreaded settings (use all cores, allow extra threads)
                BarCodeReader.ProcessorSettings.UseAllCores = true;
                BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;
                BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 4;
            }

            // Create a reader for the QR code type
            using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
            {
                Stopwatch watch = Stopwatch.StartNew();

                // Perform the decoding operation
                reader.ReadBarCodes();

                watch.Stop();

                Console.WriteLine($"Found {reader.FoundCount} barcode(s) in {watch.ElapsedMilliseconds} ms.");

                // Output each detected barcode's type and text
                foreach (BarCodeResult result in reader.FoundBarCodes)
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }

            return true;
        }
        catch (OutOfMemoryException oom)
        {
            // Memory limit reached during decoding
            Console.WriteLine($"OutOfMemoryException: {oom.Message}");
            return false;
        }
        catch (Exception ex)
        {
            // Any other decoding error
            Console.WriteLine($"Exception during barcode read: {ex.Message}");
            return false;
        }
    }
}