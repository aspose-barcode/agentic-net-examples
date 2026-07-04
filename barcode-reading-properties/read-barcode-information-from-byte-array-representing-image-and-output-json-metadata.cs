// Title: Read barcode from image byte array and output JSON
// Description: Generates a Code128 barcode, reads it from an in‑memory byte array, and prints the decoded information as formatted JSON.
// Prompt: Read barcode information from a byte array representing an image and output JSON metadata.
// Tags: barcode, code128, read, json, aspose.barcode, memorystream

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a barcode, read it from a byte array, and output the decoded data as JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it from memory, and prints JSON metadata.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image and store it in a byte array.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            using (var generationStream = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(generationStream, BarCodeImageFormat.Png);
                byte[] imageBytes = generationStream.ToArray();

                // Read barcode information from the byte array.
                using (var readStream = new MemoryStream(imageBytes))
                {
                    // Initialize the reader to decode all supported barcode types.
                    using (var reader = new BarCodeReader(readStream, DecodeType.AllSupportedTypes))
                    {
                        var results = new List<object>();

                        // Iterate over each detected barcode.
                        foreach (var result in reader.ReadBarCodes())
                        {
                            var rect = result.Region.Rectangle;
                            var info = new
                            {
                                CodeType = result.CodeTypeName,
                                CodeText = result.CodeText,
                                Confidence = result.Confidence,
                                ReadingQuality = result.ReadingQuality,
                                Angle = result.Region.Angle,
                                Region = new
                                {
                                    X = rect.X,
                                    Y = rect.Y,
                                    Width = rect.Width,
                                    Height = rect.Height
                                }
                            };
                            results.Add(info);
                        }

                        // Serialize the collected barcode data to formatted JSON.
                        string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
                        Console.WriteLine(json);
                    }
                }
            }
        }
    }
}