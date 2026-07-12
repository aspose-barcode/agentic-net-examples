// Title: Parallel barcode recognition from multiple images
// Description: Demonstrates how to read barcodes from a collection of image files concurrently using TPL while respecting the processor limit defined in Aspose.BarCode's ProcessorSettings.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader to detect and extract barcode data from images. It illustrates typical scenarios such as batch processing of scanned documents or photos, where developers need to maximize throughput while honoring the library's MaxProcessorCount setting. The code leverages Parallel.ForEach and reflection to adapt to runtime configuration, a common pattern for high‑performance barcode processing pipelines.
// Prompt: Write a script that processes a list of image paths in parallel using TPL while respecting ProcessorSettings limits.
// Tags: barcode, recognition, parallel, tpl, aspose.barcode, barcodereader

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that processes a list of image files in parallel,
/// reading any barcodes they contain using Aspose.BarCode's <see cref="BarCodeReader"/>.
/// The degree of parallelism respects the library's <c>ProcessorSettings.MaxProcessorCount</c> if available.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Sample list of image paths (replace with actual paths as needed)
        string[] imagePaths = new string[]
        {
            "sample1.png",
            "sample2.png",
            "sample3.png",
            "sample4.png",
            "sample5.png"
        };

        // Determine the maximum degree of parallelism.
        // Default to the number of logical processors; override if ProcessorSettings provides a limit.
        int maxDegree = Environment.ProcessorCount;

        // Use reflection to safely access BarCodeReader.ProcessorSettings.MaxProcessorCount.
        PropertyInfo procSettingsProp = typeof(BarCodeReader).GetProperty(
            "ProcessorSettings",
            BindingFlags.Static | BindingFlags.Public);

        if (procSettingsProp != null)
        {
            object procSettings = procSettingsProp.GetValue(null);
            if (procSettings != null)
            {
                PropertyInfo maxProp = procSettings.GetType().GetProperty(
                    "MaxProcessorCount",
                    BindingFlags.Instance | BindingFlags.Public);

                if (maxProp != null && maxProp.PropertyType == typeof(int))
                {
                    maxDegree = (int)maxProp.GetValue(procSettings);
                }
            }
        }

        // Configure ParallelOptions with the resolved degree of parallelism.
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = maxDegree };

        // Process each image path concurrently.
        Parallel.ForEach(imagePaths, parallelOptions, path =>
        {
            // Verify that the file exists before attempting to read.
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }

            try
            {
                // Initialize BarCodeReader for the current image file.
                using (var reader = new BarCodeReader(path))
                {
                    // Read all barcodes present in the image.
                    var results = reader.ReadBarCodes();

                    // Output results or indicate that no barcodes were found.
                    if (results == null || results.Length == 0)
                    {
                        Console.WriteLine($"No barcodes detected in: {path}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {path}");
                            Console.WriteLine($"  Type: {result.CodeTypeName}");
                            Console.WriteLine($"  CodeText: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during processing of the current file.
                Console.WriteLine($"Error processing {path}: {ex.Message}");
            }
        });
    }
}