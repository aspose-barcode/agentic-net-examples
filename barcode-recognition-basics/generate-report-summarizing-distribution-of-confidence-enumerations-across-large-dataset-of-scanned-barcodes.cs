// Title: Barcode Confidence Distribution Report
// Description: Generates sample barcodes, scans them, and reports the distribution of confidence levels across the scanned results.
// Category-Description: This example demonstrates Aspose.BarCode's generation and recognition APIs, focusing on creating barcodes, reading them, and analyzing the BarCodeConfidence enumeration. It showcases typical use cases such as batch processing of barcode images, confidence assessment for quality control, and reporting. Developers working with barcode scanning pipelines often need to aggregate confidence metrics to evaluate scanner performance and data reliability.
// Prompt: Generate a report summarizing the distribution of Confidence enumerations across a large dataset of scanned barcodes.
// Tags: barcode symbology, generation, recognition, confidence, report, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a set of barcodes, read them back,
/// and produce a summary of the confidence levels reported by the Aspose.BarCode recognizer.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, scans them,
    /// and prints the distribution of <see cref="BarCodeConfidence"/> values.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output folder for generated barcode images
        // --------------------------------------------------------------------
        string outputFolder = "Barcodes";
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Define a small set of sample barcodes (type, text, file name)
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType Type, string Text, string FileName)>
        {
            (EncodeTypes.Code128, "Sample123", "code128.png"),   // typically Moderate confidence
            (EncodeTypes.QR, "SampleQR", "qr.png"),             // typically Strong confidence
            (EncodeTypes.DataMatrix, "DM12345", "datamatrix.png") // confidence may vary
        };

        // --------------------------------------------------------------------
        // Generate barcode images using default settings
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(outputFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.Type, sample.Text))
            {
                // Save the generated barcode as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Initialize a dictionary to count each confidence level
        // --------------------------------------------------------------------
        var confidenceCounts = new Dictionary<BarCodeConfidence, int>
        {
            { BarCodeConfidence.None, 0 },
            { BarCodeConfidence.Moderate, 0 },
            { BarCodeConfidence.Strong, 0 }
        };

        // --------------------------------------------------------------------
        // Read each generated image and collect confidence values
        // --------------------------------------------------------------------
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(outputFolder, sample.FileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Warning: File not found - {filePath}");
                continue;
            }

            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    BarCodeConfidence conf = result.Confidence;
                    if (confidenceCounts.ContainsKey(conf))
                        confidenceCounts[conf]++;
                    else
                        confidenceCounts[conf] = 1;
                }
            }
        }

        // --------------------------------------------------------------------
        // Output summary of confidence distribution
        // --------------------------------------------------------------------
        Console.WriteLine("Barcode Confidence Distribution:");
        foreach (var kvp in confidenceCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}