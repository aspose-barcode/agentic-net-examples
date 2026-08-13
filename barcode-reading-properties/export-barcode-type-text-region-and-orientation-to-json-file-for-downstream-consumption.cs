// Title: Export barcode details to JSON
// Description: Demonstrates generating barcodes, reading them back, and exporting type, text, region, and orientation to a JSON file for downstream processing.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator for creating images and BarCodeReader for extracting metadata. Typical use cases include batch barcode creation, automated verification, and integration with downstream systems that consume JSON metadata. Developers often need to serialize barcode properties such as symbology, content, location, and rotation for reporting or further analysis.
// Prompt: Export barcode type, text, region, and orientation to a JSON file for downstream consumption.
// Tags: barcode symbology generation recognition json serialization aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

namespace BarcodeExportExample
{
    // Simple DTO for JSON serialization
    public class BarcodeInfo
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public RectangleInfo Region { get; set; }
        public double Orientation { get; set; }
    }

    public class RectangleInfo
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }

    /// <summary>
    /// Demonstrates generating barcodes, reading them, and exporting their metadata to a JSON file.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Generates sample barcodes, reads them, and writes metadata to JSON.
        /// </summary>
        static void Main()
        {
            // Define sample barcodes with symbology, text, and rotation angle
            var samples = new List<(BaseEncodeType encodeType, string codeText, float rotation)>
            {
                (EncodeTypes.Code128, "ABC123", 0f),
                (EncodeTypes.QR, "https://example.com", 45f),
                (EncodeTypes.DataMatrix, "DataMatrixSample", 90f)
            };

            var generatedFiles = new List<string>();
            int index = 0;

            // Generate barcode images based on the sample data
            foreach (var sample in samples)
            {
                string imagePath = $"barcode_{index}.png";

                using (var generator = new BarcodeGenerator(sample.encodeType, sample.codeText))
                {
                    // Apply rotation if needed
                    generator.Parameters.RotationAngle = sample.rotation;

                    // Save the generated barcode image to disk
                    generator.Save(imagePath);
                }

                generatedFiles.Add(imagePath);
                index++;
            }

            var results = new List<BarcodeInfo>();

            // Read each generated image and extract barcode information
            foreach (var filePath in generatedFiles)
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    continue;
                }

                using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        var regionRect = result.Region.Rectangle;

                        // Populate DTO with extracted data
                        var info = new BarcodeInfo
                        {
                            Type = result.CodeTypeName,
                            Text = result.CodeText,
                            Region = new RectangleInfo
                            {
                                X = regionRect.X,
                                Y = regionRect.Y,
                                Width = regionRect.Width,
                                Height = regionRect.Height
                            },
                            Orientation = result.Region.Angle
                        };

                        results.Add(info);
                    }
                }
            }

            // Serialize the list of barcode information to a formatted JSON string
            string jsonOutput = JsonSerializer.Serialize(
                results,
                new JsonSerializerOptions { WriteIndented = true });

            // Write the JSON output to a file
            string jsonPath = "barcode_info.json";
            File.WriteAllText(jsonPath, jsonOutput);

            Console.WriteLine($"Exported barcode information to {jsonPath}");
        }
    }
}