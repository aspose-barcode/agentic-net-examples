// Title: Barcode detection from image stream in a simulated web API
// Description: Demonstrates how to detect barcodes in an image stream using Aspose.BarCode, mimicking a web API endpoint that receives uploaded images.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the BarCodeReader class together with System.Drawing.Bitmap to read any supported symbology from an image stream. Typical use cases include server‑side processing of uploaded photos, document scanning pipelines, and real‑time mobile uploads where developers need to extract barcode data quickly.
// Prompt: Integrate barcode detection into a web API endpoint that accepts uploaded image streams for instant processing.
// Tags: barcode detection, image stream, aspnet, aspose.barcode, qr, barcode recognition, web api, c#

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a simple demonstration of barcode detection from an image stream,
/// representing a typical web API endpoint implementation.
/// </summary>
class Program
{
    // Simulated web API endpoint: accepts an image stream and returns detected barcodes.
    static List<(string CodeType, string CodeText)> DetectBarcodes(Stream imageStream)
    {
        var results = new List<(string, string)>();

        // Load the image from the stream into a Bitmap.
        using (var bitmap = new Bitmap(imageStream))
        {
            // Use the default constructor that detects all supported barcode types.
            using (var reader = new BarCodeReader(bitmap))
            {
                // Iterate through all detected barcodes.
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    // result.CodeTypeName provides a readable symbology name.
                    results.Add((result.CodeTypeName, result.CodeText));
                }
            }
        }

        return results;
    }

    /// <summary>
    /// Generates a sample QR code, feeds it to the detection routine,
    /// and writes the detection results to the console.
    /// </summary>
    static void Main()
    {
        // Simulate a client uploading an image by generating a barcode in memory.
        const string sampleText = "Hello World";
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, sampleText))
        {
            // Optional: customize appearance.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.QR.Version = QRVersion.Auto;
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            using (var ms = new MemoryStream())
            {
                // Save generated barcode as PNG into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream for reading.

                // Call the simulated API method.
                List<(string CodeType, string CodeText)> detected = DetectBarcodes(ms);

                // Output detection results.
                if (detected.Count == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (var item in detected)
                    {
                        Console.WriteLine($"Detected {item.CodeType}: {item.CodeText}");
                    }
                }
            }
        }
    }
}