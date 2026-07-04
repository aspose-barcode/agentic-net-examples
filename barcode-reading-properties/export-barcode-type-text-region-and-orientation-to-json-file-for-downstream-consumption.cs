// Title: Export barcode details to JSON
// Description: Demonstrates generating a barcode, reading its type, text, region, and orientation, and exporting this information to a JSON file for downstream consumption.
// Prompt: Export barcode type, text, region, and orientation to a JSON file for downstream consumption.
// Tags: barcode symbology, export, json, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeExportExample
{
    // Simple DTO for JSON serialization
    public class BarcodeInfo
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public RegionInfo Region { get; set; }
        public double Angle { get; set; }
    }

    // DTO representing the bounding rectangle of a detected barcode
    public class RegionInfo
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }

    /// <summary>
    /// Entry point for the barcode export example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a barcode image, reads its properties, and writes them to a JSON file.
        /// </summary>
        static void Main()
        {
            const string imagePath = "sample.png";
            const string jsonPath = "barcode_info.json";

            // Generate a sample barcode image using Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                generator.Save(imagePath);
            }

            // Verify the image was created successfully
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
                return;
            }

            // Collection to hold extracted barcode information
            var barcodeData = new List<BarcodeInfo>();

            // Read barcodes from the generated image using all supported decode types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                foreach (var result in reader.ReadBarCodes())
                {
                    // Extract the bounding rectangle of the detected barcode
                    var rect = result.Region.Rectangle;

                    // Populate the DTO with type, text, region, and orientation
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
                        Angle = result.Region.Angle
                    };

                    // Add the DTO to the collection
                    barcodeData.Add(info);
                }
            }

            // Serialize the collected data to a formatted JSON string
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(barcodeData, jsonOptions);

            // Write the JSON output to the specified file
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"Exported barcode information to '{jsonPath}'.");
        }
    }
}