// Title: Barcode Reader Demo with Confidence and Quality Metrics
// Description: Demonstrates generating a QR code, reading it with Aspose.BarCode, and returning confidence and quality metrics as JSON.
// Prompt: Integrate BarCodeReader into an ASP.NET Core API endpoint that returns confidence and quality metrics in JSON.
// Tags: barcode symbology, reading, json output, aspnet core, confidence, quality, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode; // Required for BarCodeConfidence enum

/// <summary>
/// Demonstrates barcode generation, reading, and JSON output of confidence and quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a QR code, reads it, and prints metrics as JSON.
    /// </summary>
    static void Main()
    {
        // NOTE: The original request was for an ASP.NET Core API endpoint.
        // The snippet runner environment only supports a console application,
        // so we demonstrate the core barcode reading logic here and output JSON to the console.

        // Path for the temporary barcode image.
        string imagePath = "sample.png";

        // Generate a sample QR barcode image with the text "12345".
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "12345"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image file '{imagePath}' was not found.");
            return;
        }

        // Prepare a list to hold barcode information including confidence and quality.
        var resultsInfo = new List<BarcodeInfo>();

        // Initialize the barcode reader for all supported types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply normal quality settings for reading.
            reader.QualitySettings = QualitySettings.NormalQuality;

            // Iterate over all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Map the raw result to a simple DTO for JSON serialization.
                var info = new BarcodeInfo
                {
                    CodeTypeName = result.CodeTypeName,
                    CodeText = result.CodeText,
                    Confidence = result.Confidence.ToString(),
                    ReadingQuality = result.ReadingQuality
                };
                resultsInfo.Add(info);
            }
        }

        // Serialize the collected barcode information to formatted JSON.
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(resultsInfo, jsonOptions);

        // Output the JSON to the console.
        Console.WriteLine(json);
    }

    // Simple DTO for JSON serialization of barcode metrics.
    private class BarcodeInfo
    {
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
        public string Confidence { get; set; }
        public double ReadingQuality { get; set; }
    }
}