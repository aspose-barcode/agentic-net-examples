// Title: Export Barcode Details to JSON
// Description: Generates a Code128 barcode, reads its properties, and writes type, text, region, and orientation to a JSON file for downstream processing.
// Category-Description: This example demonstrates core Aspose.BarCode operations—barcode generation with BarcodeGenerator and barcode recognition with BarCodeReader. It shows how to extract metadata such as symbology, decoded text, bounding region, and orientation, then serialize the data to JSON. Developers working with barcode automation, inventory systems, or data pipelines often need to export barcode information for analytics or integration with other services.
// Prompt: Export barcode type, text, region, and orientation to a JSON file for downstream consumption.
// Tags: barcode symbology, generation, recognition, json, export, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a barcode, reading its metadata, and exporting the details to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, extracts its properties, and writes them to JSON.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the barcode content and symbology.
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Create a barcode generator and save the image to a memory stream.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Initialize a reader to decode all supported barcode types from the stream.
                using (var reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes))
                {
                    var barcodeInfos = new List<object>();

                    // Iterate over each detected barcode and collect its details.
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        var rect = result.Region.Rectangle;
                        var info = new
                        {
                            Type = result.CodeTypeName,
                            Text = result.CodeText,
                            Region = new
                            {
                                X = rect.X,
                                Y = rect.Y,
                                Width = rect.Width,
                                Height = rect.Height
                            },
                            Orientation = result.Region.Angle
                        };
                        barcodeInfos.Add(info);
                    }

                    // Serialize the collected information to a formatted JSON string.
                    string json = JsonSerializer.Serialize(
                        barcodeInfos,
                        new JsonSerializerOptions { WriteIndented = true });

                    // Write the JSON output to a file in the current directory.
                    string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcodeInfo.json");
                    File.WriteAllText(outputPath, json);

                    Console.WriteLine($"Barcode information saved to {outputPath}");
                }
            }
        }
    }
}