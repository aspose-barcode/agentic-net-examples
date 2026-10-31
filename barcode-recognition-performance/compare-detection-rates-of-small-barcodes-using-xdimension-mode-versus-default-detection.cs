// Title: Compare detection rates of small Code128 barcodes using XDimension modes
// Description: Generates short Code128 barcodes, then reads them using default settings and XDimension Small and UseMinimalXDimension modes to compare detection success rates.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create barcodes and BarCodeReader with QualitySettings (XDimensionMode) to improve detection of small barcodes. Typical use cases include validating scanner settings, optimizing barcode size, and benchmarking detection algorithms. Developers often work with BarcodeGenerator, BarCodeReader, DecodeType, and XDimensionMode when handling barcode creation and recognition tasks.
// Prompt: Compare detection rates of small barcodes using XDimension mode versus default detection.
// Tags: code128, detection, xdimension, generation, recognition, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates comparing detection rates of small Code128 barcodes using different XDimension settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates barcodes, runs detection with various settings, and reports results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "XDimDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define a set of short Code128 texts to test detection of small barcodes
        List<string> codeTexts = new List<string> { "A", "AB", "ABC", "12345", "XYZ" };
        List<string> imagePaths = new List<string>();

        // --------------------------------------------------------------------
        // Generate barcode images using BarcodeGenerator (default settings)
        // --------------------------------------------------------------------
        foreach (string text in codeTexts)
        {
            string filePath = Path.Combine(tempFolder, $"code_{text}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            imagePaths.Add(filePath);
        }

        // --------------------------------------------------------------
        // Detect barcodes with default reader settings (no XDimension tweak)
        // --------------------------------------------------------------
        int defaultDetected = 0;
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
                continue;

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.Code128))
            {
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                    defaultDetected++;
            }
        }

        // --------------------------------------------------------------
        // Detect barcodes with XDimension set to Small mode
        // --------------------------------------------------------------
        int xDimSmallDetected = 0;
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
                continue;

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.Code128))
            {
                reader.QualitySettings.XDimension = XDimensionMode.Small;
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                    xDimSmallDetected++;
            }
        }

        // --------------------------------------------------------------
        // Detect barcodes with XDimension set to UseMinimalXDimension mode
        // --------------------------------------------------------------
        int xDimMinimalDetected = 0;
        foreach (string path in imagePaths)
        {
            if (!File.Exists(path))
                continue;

            using (BarCodeReader reader = new BarCodeReader(path, DecodeType.Code128))
            {
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 1;
                BarCodeResult[] results = reader.ReadBarCodes();
                if (results != null && results.Length > 0)
                    xDimMinimalDetected++;
            }
        }

        // --------------------------------------------------------------
        // Output detection statistics
        // --------------------------------------------------------------
        int total = imagePaths.Count;
        Console.WriteLine($"Total barcodes generated: {total}");
        Console.WriteLine($"Detected with default settings: {defaultDetected} ({(defaultDetected * 100.0 / total):F1}%)");
        Console.WriteLine($"Detected with XDimension Small mode: {xDimSmallDetected} ({(xDimSmallDetected * 100.0 / total):F1}%)");
        Console.WriteLine($"Detected with XDimension UseMinimalXDimension mode: {xDimMinimalDetected} ({(xDimMinimalDetected * 100.0 / total):F1}%)");

        // --------------------------------------------------------------
        // Cleanup temporary files and folder
        // --------------------------------------------------------------
        try
        {
            foreach (string path in imagePaths)
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}