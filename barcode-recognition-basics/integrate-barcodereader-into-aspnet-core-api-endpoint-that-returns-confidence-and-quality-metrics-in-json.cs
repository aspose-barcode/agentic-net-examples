// Title: ASP.NET Core API Barcode Reader Example
// Description: Demonstrates using Aspose.BarCode's BarCodeReader to extract barcode text, type, confidence, and quality, then serializes the results to JSON.
// Category-Description: This example belongs to the Aspose.BarCode reading operations category, showcasing how to employ BarCodeReader with DecodeType.AllSupportedTypes to recognize multiple symbologies. It highlights key API classes such as BarCodeReader, BarCodeResult, and QualitySettings, useful for developers building web services that need to return barcode metadata in JSON format.
// Prompt: Integrate BarCodeReader into an ASP.NET Core API endpoint that returns confidence and quality metrics in JSON.
// Tags: barcode, barcode-reader, confidence, quality, json, aspnet-core, apibarcode, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Simple DTO that holds barcode recognition details for JSON serialization.
/// </summary>
class BarcodeInfo
{
    public string CodeText { get; set; }
    public string CodeTypeName { get; set; }
    public string Confidence { get; set; }
    public double ReadingQuality { get; set; }
}

/// <summary>
/// Demonstrates core barcode reading logic that would normally reside in an ASP.NET Core controller action.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample barcode (if missing), reads it, and outputs JSON with confidence and quality metrics.
    /// </summary>
    static void Main()
    {
        // In a real ASP.NET Core API this logic would be inside a controller action.
        // The snippet runner cannot host a web server, so we demonstrate the core logic
        // and output the JSON result to the console.

        // Determine the path to the sample barcode image.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "sample_barcode.png");

        // Ensure a sample barcode image exists; create one if it does not.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Verify the image was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // Collection to hold recognition results.
        var barcodeInfos = new List<BarcodeInfo>();

        // Use all supported decode types to recognize any barcode present.
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Initialize the reader with the image path and decode type.
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Example: keep default quality settings.
            // reader.QualitySettings.AllowIncorrectBarcodes = false; // default

            BarCodeResult[] results;
            try
            {
                // Perform the recognition.
                results = reader.ReadBarCodes();
            }
            catch (RecognitionAbortedException ex)
            {
                Console.WriteLine($"Recognition aborted: {ex.Message}");
                return;
            }

            // Transform each result into a DTO for JSON serialization.
            foreach (BarCodeResult result in results)
            {
                var info = new BarcodeInfo
                {
                    CodeText = result.CodeText,
                    CodeTypeName = result.CodeTypeName,
                    Confidence = result.Confidence.ToString(),
                    ReadingQuality = result.ReadingQuality
                };
                barcodeInfos.Add(info);
            }
        }

        // Serialize the list of DTOs to formatted JSON.
        string json = JsonSerializer.Serialize(barcodeInfos, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }
}