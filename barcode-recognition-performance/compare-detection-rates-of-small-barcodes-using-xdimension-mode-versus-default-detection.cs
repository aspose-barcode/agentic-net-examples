// Title: Compare detection rates of small barcodes using XDimension mode vs default
// Description: Generates small Code128 barcodes and compares detection success using default settings and XDimensionMode.Small.
// Prompt: Compare detection rates of small barcodes using XDimension mode versus default detection.
// Tags: code128, detection, xdimension, barcode, aspose, console

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how XDimensionMode.Small affects barcode detection compared to default settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates small barcodes, reads them with default and small XDimension settings,
    /// and prints a comparison of detection counts.
    /// </summary>
    static void Main()
    {
        // Define folder for generated barcode images
        string outputFolder = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Generate a small set of Code128 barcodes with a reduced XDimension
        int barcodeCount = 5;
        for (int i = 0; i < barcodeCount; i++)
        {
            string codeText = $"Test{i}";
            string filePath = Path.Combine(outputFolder, $"barcode{i}.png");

            // Create barcode generator with Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set a small XDimension (1 point) to make barcode elements tiny
                generator.Parameters.Barcode.XDimension.Point = 1f;
                // Save the generated barcode image
                generator.Save(filePath);
            }
        }

        int defaultDetected = 0;   // Counter for detections using default settings
        int smallModeDetected = 0; // Counter for detections using XDimensionMode.Small

        // Iterate over each generated barcode image and test detection
        foreach (string file in Directory.GetFiles(outputFolder, "*.png"))
        {
            // ----- Default detection (auto XDimension) -----
            using (var readerDefault = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                var result = readerDefault.ReadBarCodes().FirstOrDefault();
                if (result != null && !string.IsNullOrEmpty(result.CodeText))
                {
                    defaultDetected++;
                }
            }

            // ----- Detection with XDimensionMode.Small -----
            using (var readerSmall = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                // Force the reader to use the Small XDimension mode
                readerSmall.QualitySettings.XDimension = XDimensionMode.Small;
                var result = readerSmall.ReadBarCodes().FirstOrDefault();
                if (result != null && !string.IsNullOrEmpty(result.CodeText))
                {
                    smallModeDetected++;
                }
            }
        }

        // Output the comparison results to the console
        Console.WriteLine($"Total barcodes generated: {barcodeCount}");
        Console.WriteLine($"Detected with default settings: {defaultDetected}");
        Console.WriteLine($"Detected with XDimensionMode.Small: {smallModeDetected}");
    }
}