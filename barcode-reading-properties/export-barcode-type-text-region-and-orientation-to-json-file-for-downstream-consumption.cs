// Title: Export barcode details to JSON
// Description: Demonstrates generating a barcode, reading its properties, and exporting type, text, region, and orientation to a JSON file for downstream use.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode image, BarCodeReader to decode and retrieve metadata such as code type, text, region coordinates, and orientation, and then serialize the collected information with System.Text.Json. Developers working with barcode automation often need to produce machine‑readable metadata for further processing, reporting, or integration with other systems.
// Prompt: Export barcode type, text, region, and orientation to a JSON file for downstream consumption.
// Tags: barcode, generation, recognition, json, export, aspose.barcode, code128, metadata

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, recognition, and exporting metadata to JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads its metadata, and writes it to a JSON file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the generated barcode image and the output JSON
        string barcodePath = Path.Combine(tempFolder, "barcode.png");
        string jsonPath = Path.Combine(tempFolder, "barcodeInfo.json");

        // Generate a sample Code128 barcode and save it as a PNG image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Optional: adjust the X-dimension (module width) for better readability
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Prepare a collection to hold barcode information extracted from the image
        var barcodeInfos = new List<BarcodeInfo>();

        // Verify that the barcode image was created before attempting to read it
        if (File.Exists(barcodePath))
        {
            // Initialize a reader configured for Code128 decoding
            using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                // Iterate through all detected barcodes (single in this example)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Extract the bounding rectangle of the barcode region
                    var rect = result.Region.Rectangle;

                    // Populate a BarcodeInfo object with the extracted metadata
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

                    // Add the populated info to the collection
                    barcodeInfos.Add(info);
                }
            }
        }
        else
        {
            Console.WriteLine($"Barcode image not found at {barcodePath}");
        }

        // Serialize the collection of barcode information to a formatted JSON string
        string json = JsonSerializer.Serialize(barcodeInfos, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON output to the designated file
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Barcode information exported to: {jsonPath}");
    }

    // Simple DTO representing barcode metadata
    class BarcodeInfo
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public RegionInfo Region { get; set; }
        public double Orientation { get; set; }
    }

    // Simple DTO representing the rectangular region of a barcode
    class RegionInfo
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }
}