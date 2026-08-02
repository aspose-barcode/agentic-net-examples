// Title: Compare detection rates of small Code128 barcodes using XDimension mode
// Description: Generates small Code128 barcodes with reduced XDimension and compares detection success using default settings versus XDimensionMode.Small.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to create barcodes with the BarcodeGenerator class, adjust visual parameters such as XDimension, and read them back using BarCodeReader with custom QualitySettings. Developers often need to fine‑tune detection for low‑resolution or compact barcodes, making XDimensionMode a key setting for reliable scanning.
// Prompt: Compare detection rates of small barcodes using XDimension mode versus default detection.
// Tags: code128, detection, png, xdimensionmode, barcodereader, barcodegenerator

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate small Code128 barcodes and compare detection
/// success using default settings versus XDimensionMode.Small.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a set of small barcodes,
    /// reads them back with two different detection configurations,
    /// and outputs the detection counts.
    /// </summary>
    static void Main()
    {
        // Prepare a collection to hold barcode images in memory.
        List<byte[]> barcodeImages = new List<byte[]>();
        const int sampleCount = 5;

        // Generate small barcodes with reduced XDimension.
        for (int i = 0; i < sampleCount; i++)
        {
            // Each barcode encodes a simple numeric string.
            string codeText = $"12345{i}";
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Reduce XDimension to make the barcode visually small.
                generator.Parameters.Barcode.XDimension.Point = 0.5f;

                // Save the barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    barcodeImages.Add(ms.ToArray());
                }
            }
        }

        int defaultDetected = 0;   // Counter for detections using default settings.
        int smallModeDetected = 0; // Counter for detections using XDimensionMode.Small.

        // Iterate over each generated barcode image.
        foreach (var imgData in barcodeImages)
        {
            // ----- Default detection (no XDimension mode change) -----
            using (var stream = new MemoryStream(imgData))
            {
                using (var reader = new BarCodeReader(stream, DecodeType.Code128))
                {
                    // Use default QualitySettings.
                    var results = reader.ReadBarCodes();
                    if (reader.FoundCount > 0)
                        defaultDetected++;
                }
            }

            // ----- Detection with XDimensionMode.Small -----
            using (var stream = new MemoryStream(imgData))
            {
                using (var reader = new BarCodeReader(stream, DecodeType.Code128))
                {
                    // Configure QualitySettings to target small XDimension barcodes.
                    reader.QualitySettings.XDimension = XDimensionMode.Small;
                    var results = reader.ReadBarCodes();
                    if (reader.FoundCount > 0)
                        smallModeDetected++;
                }
            }
        }

        // Output the comparison results.
        Console.WriteLine($"Total barcodes generated: {sampleCount}");
        Console.WriteLine($"Detected with default settings: {defaultDetected}");
        Console.WriteLine($"Detected with XDimensionMode.Small: {smallModeDetected}");
    }
}