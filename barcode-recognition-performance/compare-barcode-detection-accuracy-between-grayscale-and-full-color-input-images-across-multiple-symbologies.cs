// Title: Compare barcode detection accuracy between color and grayscale images
// Description: Generates barcodes in full‑color and grayscale, then uses Aspose.BarCode recognition to compare detection success rates across multiple symbologies.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to create barcode images with different visual properties and evaluate their readability. It showcases the BarcodeGenerator, BarCodeReader, and related parameter classes, which developers commonly use for testing scan reliability, preparing assets for varied printing conditions, and validating barcode quality across diverse symbologies.
// Prompt: Compare barcode detection accuracy between grayscale and full‑color input images across multiple symbologies.
// Tags: barcode symbology, detection, comparison, color, grayscale, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate color and grayscale barcode images,
/// detect them using Aspose.BarCode, and compare detection accuracy.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, runs detection,
    /// outputs accuracy statistics, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the set of symbologies to test
        var symbologies = new List<(BaseEncodeType Encode, string Name)>
        {
            (EncodeTypes.QR, "QR"),
            (EncodeTypes.Code128, "Code128"),
            (EncodeTypes.DataMatrix, "DataMatrix"),
            (EncodeTypes.Aztec, "Aztec")
        };

        // Sample text to encode in all barcodes
        const string sampleText = "Test123";

        // Lists to hold file paths for later detection
        var colorFiles = new List<string>();
        var grayFiles = new List<string>();

        // Generate barcode images for each symbology
        foreach (var (encode, name) in symbologies)
        {
            // ----- Color barcode (blue bars on yellow background) -----
            using (var generator = new BarcodeGenerator(encode, sampleText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;
                string colorPath = Path.Combine(tempFolder, $"{name}_color.png");
                generator.Save(colorPath, BarCodeImageFormat.Png);
                colorFiles.Add(colorPath);
            }

            // ----- Grayscale barcode (default black on white) -----
            using (var generator = new BarcodeGenerator(encode, sampleText))
            {
                string grayPath = Path.Combine(tempFolder, $"{name}_gray.png");
                generator.Save(grayPath, BarCodeImageFormat.Png);
                grayFiles.Add(grayPath);
            }
        }

        // Counters for detection results
        int total = symbologies.Count;
        int successColor = 0;
        int successGray = 0;

        // Local function: attempts to read a barcode image and returns true if detection succeeds
        bool TryDetect(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    var results = reader.ReadBarCodes();
                    return results != null && results.Length > 0;
                }
            }
            catch (ArgumentException)
            {
                // Image loading failed; treat as detection failure
                return false;
            }
        }

        // Evaluate detection on color images
        foreach (var file in colorFiles)
        {
            if (TryDetect(file))
                successColor++;
        }

        // Evaluate detection on grayscale images
        foreach (var file in grayFiles)
        {
            if (TryDetect(file))
                successGray++;
        }

        // Output comparison results
        Console.WriteLine("Barcode Detection Accuracy Comparison");
        Console.WriteLine($"Total symbologies tested: {total}");
        Console.WriteLine($"Color images detected: {successColor}/{total} ({(successColor * 100.0 / total):F1}%)");
        Console.WriteLine($"Grayscale images detected: {successGray}/{total} ({(successGray * 100.0 / total):F1}%)");

        // Cleanup temporary files and folder
        try
        {
            foreach (var file in colorFiles) File.Delete(file);
            foreach (var file in grayFiles) File.Delete(file);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}