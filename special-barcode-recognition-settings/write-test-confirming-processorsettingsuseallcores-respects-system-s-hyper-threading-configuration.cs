// Title: Verify ProcessorSettings.UseAllCores performance impact on barcode reading
// Description: Demonstrates how to measure barcode reading time with Aspose.BarCode when using all CPU cores versus a single core.
// Category-Description: This example belongs to the Aspose.BarCode performance tuning category, illustrating the use of BarCodeReader.ProcessorSettings to control multithreading. It shows typical scenarios where developers need to benchmark or validate the effect of hyper‑threading on barcode recognition using classes like BarCodeReader, BarcodeGenerator, and related settings.
// Prompt: Write a test confirming ProcessorSettings.UseAllCores respects the system's hyper‑threading configuration.
// Tags: barcode symbology, generation, recognition, performance, multithreading, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that measures the impact of ProcessorSettings.UseAllCores on barcode reading performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, then measures read time with different processor settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for test images
        string tempFolder = Path.Combine(Path.GetTempPath(), "ProcessorTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate a sample barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        GenerateSampleBarcode(barcodePath);

        // Ensure the image exists before proceeding
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // -------------------------------------------------
        // Test with UseAllCores = true (leveraging hyper‑threading)
        // -------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = true;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = Environment.ProcessorCount * 2;
        long timeAllCores = MeasureReadTime(barcodePath);
        Console.WriteLine($"UseAllCores = true, Logical processors: {Environment.ProcessorCount}, Time = {timeAllCores} ms");

        // -------------------------------------------------
        // Test with UseAllCores = false (single‑core execution)
        // -------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        BarCodeReader.ProcessorSettings.MaxAdditionalAllowedThreads = 0;
        long timeSingleCore = MeasureReadTime(barcodePath);
        Console.WriteLine($"UseAllCores = false, Logical processors: {Environment.ProcessorCount}, Time = {timeSingleCore} ms");

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect test result
        }
    }

    /// <summary>
    /// Generates a simple Code128 barcode and saves it as a PNG file.
    /// </summary>
    /// <param name="path">Full file path where the barcode image will be saved.</param>
    static void GenerateSampleBarcode(string path)
    {
        // Use BarcodeGenerator to create a Code128 barcode with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }

    /// <summary>
    /// Measures the time required to read barcodes from the specified image.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <returns>Elapsed time in milliseconds.</returns>
    static long MeasureReadTime(string imagePath)
    {
        Stopwatch watch = Stopwatch.StartNew();

        // Initialize BarCodeReader for Code128 decoding and read all barcodes
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.ReadBarCodes();
        }

        watch.Stop();
        return watch.ElapsedMilliseconds;
    }
}