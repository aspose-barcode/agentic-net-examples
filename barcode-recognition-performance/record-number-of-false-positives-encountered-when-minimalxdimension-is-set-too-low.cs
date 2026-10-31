// Title: Detect false positives with low MinimalXDimension
// Description: This example generates several Code128 barcode images and a plain image, then reads them using a very low MinimalXDimension setting to count false positive recognitions.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs, focusing on BarcodeGenerator, BarCodeReader, and QualitySettings. Useful for developers testing barcode detection robustness, especially when XDimension parameters are misconfigured. Shows how to measure false positives in scanning scenarios.
// Prompt: Record the number of false positives encountered when MinimalXDimension is set too low.
// Tags: code128, false-positive, png, barcodegenerator, barcodereader, qualitysettings

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Program to demonstrate counting false positive barcode detections when MinimalXDimension is set too low.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes, a plain image, and reads them with low MinimalXDimension to count false positives.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for generated images
        string tempFolder = Path.Combine(Path.GetTempPath(), "FalsePositives_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // List to hold file paths of generated barcode images and the plain image
        List<string> barcodeFiles = new List<string>();

        // Generate sample Code128 barcode images
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(tempFolder, $"barcode{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{i}"))
            {
                // Set X-dimension to a typical value
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Create a plain (non‑barcode) image to test false‑positive detection
        string nonBarcodePath = Path.Combine(tempFolder, "plain.png");
        using (var bitmap = new Bitmap(200, 100))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
            }
            bitmap.Save(nonBarcodePath, ImageFormat.Png);
        }
        barcodeFiles.Add(nonBarcodePath);

        int falsePositives = 0;

        // Read each image with MinimalXDimension set intentionally low
        foreach (string file in barcodeFiles)
        {
            if (!File.Exists(file))
                continue;

            using (var reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Apply high‑performance preset and override X‑dimension settings
                reader.QualitySettings = QualitySettings.HighPerformance;
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 0.1f; // intentionally low value

                // Attempt to read barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Identify the plain image by file name
                bool isNonBarcode = string.Equals(Path.GetFileName(file), "plain.png", StringComparison.OrdinalIgnoreCase);

                // Increment false‑positive count if a barcode is reported in the plain image
                if (isNonBarcode && results.Length > 0)
                {
                    falsePositives++;
                }
            }
        }

        // Output the total number of false positives detected
        Console.WriteLine($"Total false positives detected: {falsePositives}");
    }
}