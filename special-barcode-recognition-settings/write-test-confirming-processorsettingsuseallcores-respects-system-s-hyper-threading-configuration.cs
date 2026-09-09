// Title: Barcode Recognition Core Utilization Test
// Description: Demonstrates generating a PDF417 barcode image and reading it while toggling ProcessorSettings.UseAllCores to verify core usage.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader with ProcessorSettings to control multithreading. Developers often need to benchmark or validate how barcode processing utilizes CPU cores, especially when optimizing performance on hyper‑threaded systems.
// Prompt: Write a test confirming ProcessorSettings.UseAllCores respects the system's hyper‑threading configuration.
// Tags: pdf417, barcode, generation, recognition, multithreading, processorsettings, useallcores, aspose.barcode

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Contains the entry point for the barcode core‑utilization demonstration.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a PDF417 barcode, runs recognition tests with different core settings,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for test artifacts
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a PDF417 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "AsposeTest"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Run recognition test using a single core (UseAllCores = false)
        RunRecognitionTest(imagePath, useAllCores: false, coreCount: 1);

        // Run recognition test using all available cores (UseAllCores = true)
        RunRecognitionTest(imagePath, useAllCores: true, coreCount: null);

        // Attempt to delete the generated files and folder; ignore any errors
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Cleanup failures are non‑critical for the test outcome
        }
    }

    /// <summary>
    /// Executes a barcode recognition test with specified processor settings.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="useAllCores">Whether to enable UseAllCores.</param>
    /// <param name="coreCount">
    /// Desired core count when UseAllCores is false; if null, defaults to the system's processor count.
    /// </param>
    static void RunRecognitionTest(string imagePath, bool useAllCores, int? coreCount)
    {
        // Configure the static ProcessorSettings for the BarCodeReader
        BarCodeReader.ProcessorSettings.UseAllCores = useAllCores;
        if (coreCount.HasValue)
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = coreCount.Value;
        else
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = Environment.ProcessorCount;

        // Perform barcode reading and measure execution time
        using (var reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            Stopwatch watch = Stopwatch.StartNew();
            BarCodeResult[] results = reader.ReadBarCodes();
            watch.Stop();

            // Output test results
            Console.WriteLine($"UseAllCores={useAllCores}, CoresUsed={BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount}");
            Console.WriteLine($"Found {results.Length} barcode(s) in {watch.ElapsedMilliseconds} ms");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}