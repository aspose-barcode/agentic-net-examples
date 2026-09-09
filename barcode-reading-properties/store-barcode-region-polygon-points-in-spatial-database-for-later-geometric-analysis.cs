// Title: Store barcode region polygon points for spatial analysis
// Description: Demonstrates generating a barcode, reading its region polygon points, and persisting them as JSON (simulating a spatial database) for later geometric analysis.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and the Region property to obtain polygon points. Developers working with barcode imaging often need to extract geometric data for spatial queries, GIS integration, or custom analytics, making this pattern useful for storing such data in spatial databases.
// Prompt: Store barcode region polygon points in a spatial database for later geometric analysis.
// Tags: barcode, code128, region, polygon, spatial database, json, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, region extraction, and storage of polygon points for later spatial analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads its region points, and saves them as JSON.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to hold generated files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode content and output image path.
        string barcodeText = "Sample123";
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a Code128 barcode image and save it as PNG.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Prepare a collection to store region data for each detected barcode.
        var records = new List<BarcodeRegionRecord>();

        // Read the barcode image and extract polygon points that define the barcode region.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Convert Aspose.Drawing.Point objects to simple serializable PointData structures.
                var points = new List<PointData>();
                foreach (Point pt in result.Region.Points)
                {
                    points.Add(new PointData { X = pt.X, Y = pt.Y });
                }

                // Build a record containing file path, decoded text, symbology, and polygon points.
                var record = new BarcodeRegionRecord
                {
                    FilePath = imagePath,
                    CodeText = result.CodeText,
                    CodeType = result.CodeTypeName,
                    Points = points
                };
                records.Add(record);
            }
        }

        // Serialize the collected records to JSON (acting as a stand‑in for a spatial database).
        string jsonPath = Path.Combine(tempFolder, "barcode_regions.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(records, jsonOptions);
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Barcode region data saved to: {jsonPath}");
        // In a real scenario, replace the JSON file with inserts into a spatial database.
    }
}

/// <summary>
/// Represents a barcode region record suitable for storage in a spatial database.
/// </summary>
public class BarcodeRegionRecord
{
    public string FilePath { get; set; }
    public string CodeText { get; set; }
    public string CodeType { get; set; }
    public List<PointData> Points { get; set; }
}

/// <summary>
/// Simple data transfer object for a point in 2‑D space.
/// </summary>
public class PointData
{
    public float X { get; set; }
    public float Y { get; set; }
}