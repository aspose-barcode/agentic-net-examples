// Title: Read barcode from byte array and output JSON metadata
// Description: Demonstrates generating a barcode image, converting it to a byte array, reading the barcode from that array, and serializing the detection results to JSON.
// Category-Description: This example belongs to the Aspose.BarCode reading and serialization category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them from streams, and System.Text.Json to produce structured JSON output. Developers often need to process barcode images received as byte arrays (e.g., from databases or web services) and extract metadata for logging, analytics, or further processing.
// Prompt: Read barcode information from a byte array representing an image and output JSON metadata.
// Tags: code128, barcode, read, json, aspose.barcode, aspose.drawing, serialization

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, reads it from a byte array,
/// and outputs the detection metadata as formatted JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample barcode, reads it from memory,
    /// collects detection details, and prints them as JSON.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Generate a sample Code128 barcode and store it in a memory stream.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image as PNG into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                // Convert the stream contents to a byte array.
                byte[] imageBytes = memoryStream.ToArray();

                // Initialize a barcode reader to decode all supported types from the byte array.
                using (var reader = new BarCodeReader(new MemoryStream(imageBytes), DecodeType.AllSupportedTypes))
                {
                    var barcodeInfos = new List<object>();

                    // Iterate over each detected barcode and collect its metadata.
                    foreach (var result in reader.ReadBarCodes())
                    {
                        var rect = result.Region.Rectangle;
                        var info = new
                        {
                            CodeType = result.CodeTypeName,
                            CodeText = result.CodeText,
                            Confidence = result.Confidence,
                            ReadingQuality = result.ReadingQuality,
                            Region = new
                            {
                                X = rect.X,
                                Y = rect.Y,
                                Width = rect.Width,
                                Height = rect.Height
                            }
                        };
                        barcodeInfos.Add(info);
                    }

                    // Serialize the collected metadata to indented JSON and output it.
                    string json = JsonSerializer.Serialize(
                        barcodeInfos,
                        new JsonSerializerOptions { WriteIndented = true });
                    Console.WriteLine(json);
                }
            }
        }
    }
}