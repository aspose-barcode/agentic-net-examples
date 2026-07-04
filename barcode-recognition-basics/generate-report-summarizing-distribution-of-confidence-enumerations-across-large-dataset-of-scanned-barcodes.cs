// Title: Barcode Confidence Distribution Report
// Description: Generates sample barcodes, reads them, and reports the count of each confidence level across the scanned set.
// Prompt: Generate a report summarizing the distribution of Confidence enumerations across a large dataset of scanned barcodes.
// Tags: barcode symbology, confidence analysis, report, aspose.barcode, c#

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate barcodes, recognize them, and summarize the distribution
/// of <see cref="BarCodeConfidence"/> values across the scanned images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them,
    /// aggregates confidence levels, outputs a summary, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a small set of sample barcodes with expected confidence levels.
        // --------------------------------------------------------------------
        var samples = new (BaseEncodeType type, string text, string file)[]
        {
            (EncodeTypes.Code128, "12345", "code128.png"),   // expected Moderate confidence
            (EncodeTypes.QR, "12345", "qr.png"),           // expected Strong confidence
            (EncodeTypes.EAN13, "5901234123457", "ean13.png") // expected Strong confidence (EAN13 has checksum)
        };

        // --------------------------------------------------------------------
        // 2. Generate barcode images from the sample data.
        // --------------------------------------------------------------------
        foreach (var (type, text, file) in samples)
        {
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Save the generated barcode to a PNG file.
                generator.Save(file);
            }
        }

        // --------------------------------------------------------------------
        // 3. Initialize a dictionary to hold the count of each confidence level.
        // --------------------------------------------------------------------
        var confidenceCounts = new Dictionary<BarCodeConfidence, int>();

        // --------------------------------------------------------------------
        // 4. Recognize each generated image and collect confidence values.
        // --------------------------------------------------------------------
        foreach (var (_, _, file) in samples)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"Warning: file '{file}' not found, skipping.");
                continue;
            }

            using (var reader = new BarCodeReader(file, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    var confidence = result.Confidence;

                    // Increment the count for the observed confidence level.
                    if (confidenceCounts.ContainsKey(confidence))
                        confidenceCounts[confidence] += 1;
                    else
                        confidenceCounts[confidence] = 1;
                }
            }
        }

        // --------------------------------------------------------------------
        // 5. Output the confidence distribution summary to the console.
        // --------------------------------------------------------------------
        Console.WriteLine("Barcode Confidence Distribution:");
        foreach (BarCodeConfidence level in Enum.GetValues(typeof(BarCodeConfidence)))
        {
            confidenceCounts.TryGetValue(level, out int count);
            Console.WriteLine($"{level}: {count}");
        }

        // --------------------------------------------------------------------
        // 6. Clean up generated files (optional).
        // --------------------------------------------------------------------
        foreach (var (_, _, file) in samples)
        {
            try
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            catch
            {
                // Ignore any cleanup errors.
            }
        }
    }
}