// Title: Barcode generation and recognition with confidence and quality metrics output as JSON
// Description: Generates a QR code, reads it using Aspose.BarCode, and returns the decoded text together with confidence and reading quality metrics serialized to formatted JSON.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, demonstrating how to create a barcode image, configure reader quality settings, and extract detailed metrics such as confidence and reading quality. Developers working with barcode scanning APIs often need to assess detection reliability, making use of classes like BarcodeGenerator, BarCodeReader, QualitySettings, and BarCodeResult to fine‑tune performance and obtain diagnostic information.
// Prompt: Integrate BarCodeReader into an ASP.NET Core API endpoint that returns confidence and quality metrics in JSON.
// Tags: barcode, qr, confidence, quality, json, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, recognition, and JSON serialization of confidence and quality metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates a QR code, reads it, and prints JSON with metrics.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Create a temporary folder for the demo files
        // ----------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // ----------------------------------------------------------------------
        // Define the full path for the generated barcode image
        // ----------------------------------------------------------------------
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // ----------------------------------------------------------------------
        // Generate a QR barcode image containing the text "Hello World"
        // ----------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ----------------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // ----------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at {barcodePath}");
            return;
        }

        // ----------------------------------------------------------------------
        // Prepare a collection to hold the recognition results
        // ----------------------------------------------------------------------
        var resultsList = new List<object>();

        // ----------------------------------------------------------------------
        // Initialize the barcode reader for all supported symbologies
        // ----------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Optional: apply a high‑performance quality preset to speed up processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform the recognition and retrieve all detected barcodes
            BarCodeResult[] results = reader.ReadBarCodes();

            // Iterate over each result and extract relevant metrics
            foreach (BarCodeResult result in results)
            {
                var dto = new
                {
                    CodeText = result.CodeText,
                    CodeTypeName = result.CodeTypeName,
                    Confidence = result.Confidence.ToString(),
                    ReadingQuality = result.ReadingQuality
                };
                resultsList.Add(dto);
            }
        }

        // ----------------------------------------------------------------------
        // Serialize the results collection to indented JSON and output to console
        // ----------------------------------------------------------------------
        string json = JsonSerializer.Serialize(resultsList, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }
}