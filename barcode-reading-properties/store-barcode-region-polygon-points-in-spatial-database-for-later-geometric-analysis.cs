// Title: Store barcode region polygon points in a spatial database
// Description: Demonstrates generating a barcode, reading its region polygon points, and persisting them for later geometric analysis.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, and related region data classes. Developers often need to extract barcode location geometry for spatial indexing, GIS integration, or custom analytics. The snippet illustrates creating a barcode, retrieving its region points, and serializing them for storage, a common workflow when building spatial databases of barcode locations.
// Prompt: Store barcode region polygon points in a spatial database for later geometric analysis.
// Tags: barcode, code128, region, polygon, json, spatial database, generation, recognition, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeRegionStorage
{
    // Simple DTO for JSON serialization of a point
    public class PointInfo
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    // DTO that groups a barcode's text with its region polygon points
    public class RegionInfo
    {
        public string CodeText { get; set; }
        public List<PointInfo> Points { get; set; }
    }

    /// <summary>
    /// Demonstrates generating a barcode, extracting its region polygon points, and storing them for later geometric analysis.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates a barcode image, reads its region points, and writes them to a JSON file.
        /// </summary>
        static void Main()
        {
            // Define file paths for the temporary barcode image and the output JSON file
            string imagePath = "sample_barcode.png";
            string jsonPath = "barcode_regions.json";

            // 1. Generate a barcode image using Code128 symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode to the specified image file
                generator.Save(imagePath);
            }

            // 2. Read the barcode from the image and extract its region polygon points
            var regions = new List<RegionInfo>();
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Iterate over all detected barcodes (there should be only one in this example)
                foreach (var result in reader.ReadBarCodes())
                {
                    var regionInfo = new RegionInfo
                    {
                        CodeText = result.CodeText,
                        Points = new List<PointInfo>()
                    };

                    // result.Region.Points provides the polygon vertices of the barcode region
                    foreach (var pt in result.Region.Points)
                    {
                        // Convert each Aspose.BarCode.Point to the serializable PointInfo DTO
                        regionInfo.Points.Add(new PointInfo
                        {
                            X = pt.X,
                            Y = pt.Y
                        });
                    }

                    // Add the populated region information to the collection
                    regions.Add(regionInfo);
                }
            }

            // 3. Serialize the extracted region data to JSON (acting as a stand‑in for a spatial database)
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(regions, jsonOptions);
            File.WriteAllText(jsonPath, json);

            // Inform the user that the operation completed successfully
            Console.WriteLine($"Barcode region data saved to '{jsonPath}'.");
        }
    }
}