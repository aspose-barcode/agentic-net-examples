// Title: Demonstrate ProcessorSettings.UseAllCores impact on barcode reading performance
// Description: Generates a PDF417 barcode, reads it twice—once with single‑core processing and once using all CPU cores—to show how the UseAllCores flag influences execution time.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with its ProcessorSettings to control multithreading. Developers often need to tune performance for large‑scale scanning scenarios, adjusting UseAllCores, UseOnlyThisCoresCount, and MaxAdditionalAllowedThreads to match hardware capabilities.
// Prompt: Write a test confirming ProcessorSettings.UseAllCores respects the system's hyper‑threading configuration.
// Tags: pdf417, barcode, performance, multithreading, processorsettings, useallcores, aspose.barcode, generation, recognition

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that measures the effect of <c>ProcessorSettings.UseAllCores</c> on barcode
/// recognition time. It creates a temporary PDF417 barcode image, reads it with single‑core
/// and all‑core settings, and outputs the elapsed times for comparison.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, performs two reads with different
    /// core usage settings, displays the timing results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "sample.png");

        // Generate a PDF417 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Aspose Barcode Test"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Measure reading time with single‑core processing (UseAllCores = false)
        long timeSingleCore = ReadBarcode(imagePath, useAllCores: false);

        // Measure reading time with all cores enabled (UseAllCores = true)
        long timeAllCores = ReadBarcode(imagePath, useAllCores: true);

        // Output the timing comparison
        Console.WriteLine($"Single-core time: {timeSingleCore} ms");
        Console.WriteLine($"All-cores time:   {timeAllCores} ms");

        // Attempt to delete temporary files; ignore any errors to avoid affecting test outcome
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Cleanup failure is non‑critical for the demonstration
        }
    }

    /// <summary>
    /// Reads a barcode from the specified image using the given core usage setting.
    /// Configures <c>BarCodeReader.ProcessorSettings</c> before performing the read operation.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image file.</param>
    /// <param name="useAllCores">If true, the reader will utilize all available CPU cores.</param>
    /// <returns>The elapsed time in milliseconds for the read operation.</returns>
    static long ReadBarcode(string imagePath, bool useAllCores)
    {
        // Apply processor settings: enable or disable multi‑core usage
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        // Reset optional settings to their defaults for a fair comparison
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;

        // Initialize the barcode reader for PDF417 decoding
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            // Start timing the read operation
            Stopwatch watch = Stopwatch.StartNew();
            var results = reader.ReadBarCodes();
            watch.Stop();

            // Output detailed read information
            Console.WriteLine($"UseAllCores={useAllCores}, FoundCount={reader.FoundCount}, Time={watch.ElapsedMilliseconds} ms");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Return the elapsed time for external comparison
            return watch.ElapsedMilliseconds;
        }
    }
}