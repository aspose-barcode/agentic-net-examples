// Title: Performance Graph of Barcode Recognition Time vs XDimension Modes
// Description: Demonstrates measuring recognition time for Code128 barcodes while varying XDimension settings, useful for performance tuning.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to create barcodes with BarcodeGenerator, read them with BarCodeReader, and adjust QualitySettings such as XDimensionMode. Developers often need to benchmark recognition speed for different scanner configurations, making this pattern valuable for performance analysis and optimization.
// Prompt: Generate performance graphs comparing recognition time versus XDimension values for a sample dataset.
// Tags: barcode symbology, performance, recognition, xdimension, code128, aspose.barcode, image generation

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates Code128 barcodes, measures recognition time across different XDimension modes,
/// and outputs timing results. Useful for performance analysis of Aspose.BarCode recognition settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, runs recognition with various XDimension modes, prints timing results,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodePerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data to encode in barcodes
        string[] codes = { "Sample1", "Sample2", "Sample3", "Sample4", "Sample5" };
        string[] imagePaths = new string[codes.Length];

        // Generate barcode images and store their file paths
        for (int i = 0; i < codes.Length; i++)
        {
            string filePath = Path.Combine(tempFolder, $"code_{i + 1}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codes[i]))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths[i] = filePath;
        }

        // Define the XDimension modes that will be tested
        XDimensionMode[] modes = new XDimensionMode[]
        {
            XDimensionMode.Normal,
            XDimensionMode.Small,
            XDimensionMode.UseMinimalXDimension
        };

        // Header for console output
        Console.WriteLine("Recognition Time (ms) vs XDimension Mode");
        Console.WriteLine("Mode\tTimeMs\tBarcodesRead");

        // Iterate over each XDimension mode and measure recognition performance
        foreach (var mode in modes)
        {
            int totalRead = 0;
            Stopwatch sw = Stopwatch.StartNew();

            // Recognize each generated barcode image using the current mode
            foreach (var path in imagePaths)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine($"File not found: {path}");
                    continue;
                }

                using (var reader = new BarCodeReader(path, DecodeType.Code128))
                {
                    // Apply the XDimension mode to the reader's quality settings
                    reader.QualitySettings.XDimension = mode;

                    // If using minimal XDimension, set a specific value
                    if (mode == XDimensionMode.UseMinimalXDimension)
                    {
                        reader.QualitySettings.MinimalXDimension = 1;
                    }

                    // Perform barcode reading and count results
                    var results = reader.ReadBarCodes();
                    totalRead += results.Length;
                }
            }

            sw.Stop();
            // Output the elapsed time and total barcodes read for the current mode
            Console.WriteLine($"{mode}\t{sw.ElapsedMilliseconds}\t{totalRead}");
        }

        // Optional cleanup of temporary files and folder
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors during cleanup
        }
    }
}