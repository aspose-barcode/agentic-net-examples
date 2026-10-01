// Title: Read barcode from image byte array and output JSON metadata
// Description: Generates a QR barcode, reads it from a memory stream, extracts detailed barcode information, and serializes the data to indented JSON.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category. It demonstrates how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them from in‑memory images, and System.Text.Json to produce structured output. Developers commonly need these APIs for automated scanning, data extraction, and integration with downstream systems that consume JSON metadata.
// Prompt: Read barcode information from a byte array representing an image and output JSON metadata.
// Tags: barcode, qr, read, json, aspose.barcode, csharp, metadata, decode, encode

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading barcode data from a byte array and serializing the results to JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, reads it, and prints JSON metadata.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample QR barcode and capture its image as a byte array.
        // ------------------------------------------------------------
        byte[] barcodeImageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode image in PNG format to the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the stream contents to a byte array for later processing.
                barcodeImageBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Decode the barcode from the byte array and collect metadata.
        // ------------------------------------------------------------
        var metadataList = new List<BarcodeMetadata>();
        using (var reader = new BarCodeReader(new MemoryStream(barcodeImageBytes), DecodeType.AllSupportedTypes))
        {
            // Read all barcodes present in the image.
            BarCodeResult[] results = reader.ReadBarCodes();
            foreach (BarCodeResult result in results)
            {
                // Extract the bounding rectangle of the detected barcode region.
                var bounds = result.Region.Rectangle;

                // Populate a metadata object with relevant properties.
                var metadata = new BarcodeMetadata
                {
                    CodeText = result.CodeText,
                    CodeTypeName = result.CodeTypeName,
                    ReadingQuality = result.ReadingQuality,
                    Region = new RegionInfo
                    {
                        X = bounds.X,
                        Y = bounds.Y,
                        Width = bounds.Width,
                        Height = bounds.Height,
                        Angle = result.Region.Angle
                    }
                };
                metadataList.Add(metadata);
            }
        }

        // ------------------------------------------------------------
        // 3. Serialize the collected metadata to formatted JSON and output it.
        // ------------------------------------------------------------
        string json = JsonSerializer.Serialize(
            metadataList,
            new JsonSerializerOptions { WriteIndented = true });

        Console.WriteLine(json);
    }
}

// ------------------------------------------------------------
// Helper classes defining the JSON structure for barcode metadata.
// ------------------------------------------------------------
class BarcodeMetadata
{
    public string CodeText { get; set; }
    public string CodeTypeName { get; set; }
    public double ReadingQuality { get; set; }
    public RegionInfo Region { get; set; }
}

class RegionInfo
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public double Angle { get; set; }
}