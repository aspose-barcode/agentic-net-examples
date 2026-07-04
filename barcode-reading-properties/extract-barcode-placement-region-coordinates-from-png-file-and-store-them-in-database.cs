// Title: Extract barcode region coordinates from PNG and save as JSON
// Description: Demonstrates how to read a PNG image, detect barcodes, obtain their placement rectangles, and store the data for later use.
// Prompt: Extract barcode placement region coordinates from a PNG file and store them in a database.
// Tags: barcode, region extraction, png, json, aspose, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeRegionExtractor
{
    /// <summary>
    /// Represents a barcode detection result with its placement region.
    /// </summary>
    public class BarcodeRegionRecord
    {
        public string CodeType { get; set; }
        public string CodeText { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
    }

    /// <summary>
    /// Entry point for the barcode region extraction example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Reads a PNG image, detects barcodes, extracts their region coordinates,
        /// and writes the information to a JSON file (placeholder for database storage).
        /// </summary>
        static void Main()
        {
            // Path to the PNG image containing barcodes.
            const string imagePath = "sample.png";

            // Verify that the image file exists before proceeding.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Image file not found: {imagePath}");
                return;
            }

            // Collection to hold detection results.
            var records = new List<BarcodeRegionRecord>();

            // Initialize BarCodeReader to detect all supported barcode types.
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Iterate through each detected barcode.
                foreach (var result in reader.ReadBarCodes())
                {
                    // Retrieve the bounding rectangle of the detected barcode.
                    var rect = result.Region.Rectangle;

                    // Populate a record with barcode details and region coordinates.
                    var record = new BarcodeRegionRecord
                    {
                        CodeType = result.CodeTypeName,
                        CodeText = result.CodeText,
                        X = rect.X,
                        Y = rect.Y,
                        Width = rect.Width,
                        Height = rect.Height
                    };

                    // Add the record to the collection.
                    records.Add(record);

                    // Output detection details to the console for verification.
                    Console.WriteLine($"Detected {record.CodeType}: \"{record.CodeText}\" at [{record.X}, {record.Y}, {record.Width}, {record.Height}]");
                }
            }

            // Serialize the results to JSON (acting as a stand‑in for database storage).
            const string outputPath = "barcode_regions.json";
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(records, jsonOptions);
            File.WriteAllText(outputPath, json);
            Console.WriteLine($"Barcode region data written to {outputPath}");

            // In a production scenario, replace the JSON file write with actual database insertion logic,
            // such as using Entity Framework or ADO.NET to persist records.
        }
    }
}