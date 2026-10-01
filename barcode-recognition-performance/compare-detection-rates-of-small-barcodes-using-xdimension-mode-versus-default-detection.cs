// Title: Compare detection rates of small barcodes using XDimension mode vs default detection
// Description: Generates very small Code128 barcodes, saves them as PNG, and compares recognition success using default settings versus minimal XDimension mode.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and recognition for small-sized symbols. It uses BarcodeGenerator, BarCodeReader, and QualitySettings.XDimension to adjust detection sensitivity. Typical use cases include testing scanner performance on low-resolution barcodes, optimizing settings for mobile capture, and evaluating recognition reliability. Developers working with barcode quality tuning often need such examples to understand how XDimension mode impacts detection rates.
// Prompt: Compare detection rates of small barcodes using XDimension mode versus default detection.
// Tags: barcode, code128, detection, xdimension, qualitysettings, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates tiny Code128 barcodes and measures detection success
/// with and without the minimal XDimension mode enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, runs two detection passes, reports results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXDimTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Sample data representing small barcodes to be generated
        var codes = new List<string> { "A1", "B2", "C3", "D4", "E5" };
        var barcodeFiles = new List<string>();

        // --------------------------------------------------------------------
        // Generate small barcodes with a tiny XDimension (module size)
        // --------------------------------------------------------------------
        foreach (var text in codes)
        {
            string filePath = Path.Combine(tempFolder, $"{text}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                // Set a very small module size (0.5 points)
                generator.Parameters.Barcode.XDimension.Point = 0.5f;
                // Save the barcode image in PNG format
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Local function that counts how many barcodes are successfully detected
        // --------------------------------------------------------------------
        int CountDetections(bool useXDimMode)
        {
            int success = 0;
            foreach (var file in barcodeFiles)
            {
                if (!File.Exists(file))
                {
                    Console.WriteLine($"File not found: {file}");
                    continue;
                }

                using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
                {
                    if (useXDimMode)
                    {
                        // Enable minimal XDimension mode for better detection of tiny symbols
                        reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    }

                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results != null && results.Length > 0 && !string.IsNullOrEmpty(results[0].CodeText))
                    {
                        success++;
                    }
                }
            }
            return success;
        }

        // Perform detection using default settings
        int defaultSuccess = CountDetections(false);
        // Perform detection with XDimension mode enabled
        int xDimSuccess = CountDetections(true);

        // Output summary of detection results
        Console.WriteLine($"Total barcodes generated: {codes.Count}");
        Console.WriteLine($"Detected with default settings: {defaultSuccess}");
        Console.WriteLine($"Detected with XDimension mode: {xDimSuccess}");

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            foreach (var file in barcodeFiles)
            {
                File.Delete(file);
            }
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failures should not affect program outcome
        }
    }
}