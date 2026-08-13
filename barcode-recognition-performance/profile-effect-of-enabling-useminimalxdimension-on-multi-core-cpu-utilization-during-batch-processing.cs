// Title: Profiling UseMinimalXDimension Impact on Multi‑Core CPU Utilization
// Description: Demonstrates how enabling the UseMinimalXDimension setting affects processing time when reading a batch of Code128 barcodes.
// Category-Description: This example belongs to the Aspose.BarCode performance profiling category, illustrating the use of BarCodeReader.QualitySettings to control X‑dimension detection. Developers often need to benchmark different quality settings to optimize CPU usage during bulk barcode recognition, especially on multi‑core systems. The snippet shows typical usage of BarcodeGenerator for creating test images and BarCodeReader for decoding, helping users compare default and minimal X‑dimension modes.
// Prompt: Profile the effect of enabling UseMinimalXDimension on multi‑core CPU utilization during batch processing.
// Tags: barcode symbology, performance, code128, png, useminimalxdimension, qualitysettings, multithreading, aspose.barcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a set of Code128 barcode images,
/// then measures the processing time of reading them with and without
/// the <c>UseMinimalXDimension</c> quality setting. Useful for profiling
/// CPU utilization on multi‑core machines during batch barcode recognition.
/// </summary>
class Program
{
    // Number of sample barcode images to generate.
    const int SampleCount = 10;

    // Folder where barcode images are saved.
    const string OutputFolder = "Barcodes";

    /// <summary>
    /// Entry point of the application. Creates sample barcodes,
    /// runs two profiling scenarios, and outputs the elapsed times
    /// along with logical processor count.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists.
        if (!Directory.Exists(OutputFolder))
        {
            Directory.CreateDirectory(OutputFolder);
        }

        // Generate a batch of barcode images for the test.
        GenerateBarcodes();

        // Profile processing time using the default XDimension detection.
        double timeDefault = ProcessBarcodes(useMinimal: false);
        Console.WriteLine($"Processing time (default XDimension): {timeDefault:F2} ms");

        // Profile processing time with UseMinimalXDimension enabled.
        double timeMinimal = ProcessBarcodes(useMinimal: true);
        Console.WriteLine($"Processing time (UseMinimalXDimension): {timeMinimal:F2} ms");

        // Display the number of logical processors available.
        Console.WriteLine($"Logical processors: {Environment.ProcessorCount}");
    }

    /// <summary>
    /// Generates <c>SampleCount</c> PNG images containing Code128 barcodes.
    /// Each image uses a consistent XDimension to keep the test conditions uniform.
    /// </summary>
    static void GenerateBarcodes()
    {
        for (int i = 0; i < SampleCount; i++)
        {
            // Create a unique text value for each barcode.
            string codeText = $"Sample{i:D2}";
            string filePath = Path.Combine(OutputFolder, $"barcode_{i}.png");

            // Initialize the generator with Code128 symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set a modest XDimension (2 points) for consistency across samples.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode as a PNG file.
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }

    /// <summary>
    /// Reads all PNG barcode files in <c>OutputFolder</c> and measures the elapsed time.
    /// The <paramref name="useMinimal"/> flag determines whether <c>UseMinimalXDimension</c>
    /// is applied to the reader's quality settings.
    /// </summary>
    /// <param name="useMinimal">If true, enables minimal XDimension mode; otherwise uses auto detection.</param>
    /// <returns>Total processing time in milliseconds.</returns>
    static double ProcessBarcodes(bool useMinimal)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // Retrieve all PNG files generated earlier.
        string[] files = Directory.GetFiles(OutputFolder, "*.png");

        foreach (string file in files)
        {
            // Initialize a reader for each file, targeting Code128 decoding.
            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Configure quality settings based on the profiling scenario.
                if (useMinimal)
                {
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    reader.QualitySettings.MinimalXDimension = 5f; // pixels
                }
                else
                {
                    reader.QualitySettings.XDimension = XDimensionMode.Auto;
                }

                // Iterate through all detected barcodes (no further processing needed for profiling).
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // Intentionally left blank.
                }
            }
        }

        stopwatch.Stop();
        return stopwatch.Elapsed.TotalMilliseconds;
    }
}