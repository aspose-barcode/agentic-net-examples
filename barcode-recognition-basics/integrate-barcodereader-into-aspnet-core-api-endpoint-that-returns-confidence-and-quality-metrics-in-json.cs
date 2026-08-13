// Title: ASP.NET Core API style barcode reading with confidence and quality metrics
// Description: Demonstrates generating a barcode, reading it with BarCodeReader, and outputting confidence and quality data as JSON.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and related classes to extract detailed metrics such as confidence and reading quality. Typical use cases include building web APIs that return barcode analysis results in JSON for client applications. Developers often need to integrate these APIs into ASP.NET Core services for real-time scanning and validation.
// Prompt: Integrate BarCodeReader into an ASP.NET Core API endpoint that returns confidence and quality metrics in JSON.
// Tags: barcode, code128, confidence, readingquality, json, aspnetcore, apireader, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, reading, and JSON serialization of confidence and quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, reads it, and prints the results as formatted JSON.
    /// </summary>
    static void Main()
    {
        // Generate a Code128 barcode image in memory.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode to the memory stream as PNG.
                generator.Save(imageStream, BarCodeImageFormat.Png);
                imageStream.Position = 0; // Reset stream position for reading.

                // Initialize the reader to decode all supported barcode types.
                using (var reader = new BarCodeReader(imageStream, DecodeType.AllSupportedTypes))
                {
                    var results = new List<BarcodeInfo>();

                    // Iterate through all detected barcodes.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Capture relevant information for each barcode.
                        var info = new BarcodeInfo
                        {
                            CodeText = result.CodeText,
                            Confidence = result.Confidence.ToString(),
                            ReadingQuality = result.ReadingQuality
                        };
                        results.Add(info);
                    }

                    // Serialize the list of barcode info objects to indented JSON.
                    var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(json);
                }
            }
        }
    }

    /// <summary>
    /// Simple DTO for serializing barcode details.
    /// </summary>
    private class BarcodeInfo
    {
        public string CodeText { get; set; }
        public string Confidence { get; set; }
        public double ReadingQuality { get; set; }
    }
}