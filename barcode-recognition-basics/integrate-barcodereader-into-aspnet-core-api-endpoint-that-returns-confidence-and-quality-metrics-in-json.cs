// Title: ASP.NET Core API barcode reader returning confidence and quality metrics
// Description: Demonstrates generating a Code128 barcode, reading it with BarCodeReader, and outputting confidence and reading quality as JSON.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to use BarCodeGenerator, BarCodeReader, and related quality settings. Typical use cases include validating barcode readability in web services, extracting confidence scores, and providing structured JSON responses for client applications. Developers often need to generate barcodes, read them, and expose metrics via APIs.
// Prompt: Integrate BarCodeReader into an ASP.NET Core API endpoint that returns confidence and quality metrics in JSON.
// Tags: barcode, code128, confidence, readingquality, json, aspnetcore, apirest, aspose.barcode, barcodereader, barcodelibrary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample console application that generates a barcode, reads it, and outputs confidence and quality metrics in JSON format.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Generates a temporary barcode image, reads it using BarCodeReader,
    /// and writes JSON strings containing the barcode text, confidence, and reading quality to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the sample barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "sample.png");

        // Generate a Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the image file exists before attempting to read
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{barcodePath}'.");
            return;
        }

        // Read the barcode and output confidence and reading quality as JSON
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Ensure incorrect barcodes are not allowed (default behavior)
            reader.QualitySettings.AllowIncorrectBarcodes = false;

            // Perform the barcode recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, output an error JSON
            if (results.Length == 0)
            {
                Console.WriteLine("{\"error\":\"No barcodes detected.\"}");
                return;
            }

            // Iterate over each detected barcode and output its details in JSON format
            foreach (var result in results)
            {
                string json = $"{{\"codeText\":\"{EscapeJson(result.CodeText)}\",\"confidence\":\"{result.Confidence}\",\"readingQuality\":{result.ReadingQuality}}}";
                Console.WriteLine(json);
            }
        }

        // Clean up temporary files
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }

    // Simple JSON string escaper for barcode text
    static string EscapeJson(string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;

        return s.Replace("\\", "\\\\").Replace("\"", "\\\"");
    }
}