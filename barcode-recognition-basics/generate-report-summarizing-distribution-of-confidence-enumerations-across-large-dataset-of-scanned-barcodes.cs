using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating various barcode types, recognizing them,
/// and reporting the confidence levels of the recognition results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, reads them back, and prints a confidence distribution.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare sample barcode data (symbology and code text)
        // --------------------------------------------------------------------
        var samples = new List<(BaseEncodeType encode, string text)>
        {
            (EncodeTypes.Code128, "ABC123456"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.Code39FullASCII, "CODE39*"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text")
        };

        // --------------------------------------------------------------------
        // 2. Generate barcode images and store them in memory streams
        // --------------------------------------------------------------------
        var barcodeStreams = new List<MemoryStream>();

        foreach (var sample in samples)
        {
            // Create a generator for the current barcode type and text
            using (var generator = new BarcodeGenerator(sample.encode, sample.text))
            {
                // Save the generated barcode as PNG into a memory stream
                var ms = new MemoryStream();
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for later reading
                barcodeStreams.Add(ms);
            }
        }

        // --------------------------------------------------------------------
        // 3. Initialize counters for each confidence level
        // --------------------------------------------------------------------
        var confidenceCounts = new Dictionary<BarCodeConfidence, int>
        {
            { BarCodeConfidence.None, 0 },
            { BarCodeConfidence.Moderate, 0 },
            { BarCodeConfidence.Strong, 0 }
        };

        // --------------------------------------------------------------------
        // 4. Recognize each barcode image and tally confidence levels
        // --------------------------------------------------------------------
        foreach (var stream in barcodeStreams)
        {
            // Ensure the stream is positioned at the beginning before reading
            stream.Position = 0;

            // Use BarCodeReader to decode all supported barcode types
            using (var reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    var confidence = result.Confidence;

                    // Increment the appropriate counter if the confidence level exists in the dictionary
                    if (confidenceCounts.ContainsKey(confidence))
                    {
                        confidenceCounts[confidence]++;
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // 5. Output a summary of the confidence distribution
        // --------------------------------------------------------------------
        Console.WriteLine("Barcode Confidence Distribution:");
        foreach (var kvp in confidenceCounts)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // --------------------------------------------------------------------
        // 6. Clean up memory streams
        // --------------------------------------------------------------------
        foreach (var ms in barcodeStreams)
        {
            ms.Dispose();
        }
    }
}