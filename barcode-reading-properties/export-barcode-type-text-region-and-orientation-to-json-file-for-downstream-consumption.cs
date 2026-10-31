// Title: Export Barcode Details to JSON
// Description: Generates sample barcodes, reads them, and exports type, text, region, and orientation to a JSON file for downstream consumption.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition APIs. It shows how to create barcodes with BarcodeGenerator, read them using BarCodeReader, and extract key properties such as CodeTypeName, CodeText, Region, and Angle. Developers working with barcode imaging often need to serialize this metadata for analytics, reporting, or integration with other systems.
// Prompt: Export barcode type, text, region, and orientation to a JSON file for downstream consumption.
// Tags: barcode, generation, recognition, json, export, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents the extracted information of a barcode.
/// </summary>
class BarcodeInfo
{
    public string Type { get; set; }
    public string Text { get; set; }
    public RegionInfo Region { get; set; }
    public double Orientation { get; set; }
}

/// <summary>
/// Represents the rectangular region of a barcode within an image.
/// </summary>
class RegionInfo
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}

/// <summary>
/// Demonstrates generating barcodes, reading them, and exporting their metadata to JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates sample barcodes, reads them, and writes metadata to a JSON file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a dedicated temporary folder for generated barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define sample barcodes to generate (type, text, output file name)
        var samples = new List<(BaseEncodeType Encode, string Text, string FileName)>
        {
            (EncodeTypes.QR, "Hello QR", "qr.png"),
            (EncodeTypes.Code128, "1234567890", "code128.png")
        };

        // Generate barcode images and save them to the temporary folder
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            using (var generator = new BarcodeGenerator(sample.Encode, sample.Text))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }

        // Read each generated barcode and collect its metadata
        var barcodeInfos = new List<BarcodeInfo>();
        foreach (var sample in samples)
        {
            string filePath = Path.Combine(tempFolder, sample.FileName);
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Initialize a reader capable of decoding the expected symbologies
            using (var reader = new BarCodeReader(filePath, DecodeType.QR, DecodeType.Code128))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    var rect = result.Region.Rectangle;
                    var info = new BarcodeInfo
                    {
                        Type = result.CodeTypeName,
                        Text = result.CodeText,
                        Region = new RegionInfo
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
            }
        }

        // Serialize the collected barcode information to a formatted JSON file
        string jsonPath = Path.Combine(tempFolder, "barcodeInfo.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(barcodeInfos, jsonOptions);
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Exported barcode information to: {jsonPath}");
    }
}