// Title: ASP.NET Core API Barcode Reader Example
// Description: Demonstrates reading a barcode image with Aspose.BarCode and returning confidence metrics as JSON.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class to decode various symbologies, extract reading quality, and serialize results. Typical use cases include API endpoints that need to validate scanned barcodes and provide confidence scores. Developers often need to generate barcodes, read them, and return structured data such as JSON for client applications.
// Prompt: Integrate BarCodeReader into an ASP.NET Core API endpoint that returns confidence and quality metrics in JSON.
// Tags: barcode symbology, barcode reading, json output, aspnet core, aspose barcode, barcodereader, quality metrics

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program illustrating barcode generation, reading, and JSON serialization of confidence metrics.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, reads it using BarCodeReader, and outputs JSON with reading quality.
    /// </summary>
    static void Main()
    {
        // In a real ASP.NET Core API, the following logic would be placed in a controller action.
        // Here we demonstrate the core barcode reading and JSON response generation in a console app.

        // Create a temporary folder for the sample barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR code barcode.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Save the barcode image to the file system.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{barcodePath}'.");
            return;
        }

        // Prepare the decode type to detect all supported symbologies.
        BaseDecodeType decodeType = DecodeType.AllSupportedTypes;

        // Read the barcode and collect metrics.
        var results = new List<object>();
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Optional: set high-performance quality settings.
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Perform the read operation.
            BarCodeResult[] barCodeResults = reader.ReadBarCodes();
            foreach (var result in barCodeResults)
            {
                // Capture relevant data, including the reading quality (0-100 confidence metric).
                var entry = new
                {
                    CodeText = result.CodeText,
                    CodeTypeName = result.CodeTypeName,
                    ReadingQuality = result.ReadingQuality
                };
                results.Add(entry);
            }
        }

        // Serialize the results to formatted JSON.
        string jsonOutput = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(jsonOutput);

        // Clean up temporary files.
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored: cleanup failures should not affect program exit.
        }
    }
}