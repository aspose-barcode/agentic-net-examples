// Title: Store barcode region polygon points in a spatial database (JSON example)
// Description: Generates a QR barcode, reads its region polygon points, and saves them to a JSON file representing a spatial database for later geometric analysis.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It demonstrates how to use BarcodeGenerator to create a barcode, BarCodeReader to extract the barcode region polygon, and standard .NET serialization to store geometric data. Developers working with spatial databases, GIS, or geometric analysis often need to capture precise barcode locations for mapping, collision detection, or area calculations.
// Prompt: Store barcode region polygon points in a spatial database for later geometric analysis.
// Tags: qr, barcode, region, polygon, spatial, json, aspose.barcode, generation, recognition, geometry

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR barcode, extracting its region polygon points,
/// and persisting them for spatial analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads region points,
    /// and writes them to a JSON file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for this demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Optional: set module size (X dimension) for better readability
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // List to hold region records extracted from the barcode image
        var records = new List<BarcodeRegionRecord>();

        // Read the barcode image and extract region polygon points
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // According to the API rules, call SetBarCodeImage after construction
            reader.SetBarCodeImage(barcodePath);

            // Iterate through all detected barcodes (only one in this demo)
            foreach (var result in reader.ReadBarCodes())
            {
                // Collect points of the barcode region polygon
                var points = new List<PointDto>();
                foreach (var pt in result.Region.Points)
                {
                    points.Add(new PointDto { X = pt.X, Y = pt.Y });
                }

                // Create a record that captures the barcode text, symbology, and geometry
                var record = new BarcodeRegionRecord
                {
                    CodeText = result.CodeText,
                    Symbology = result.CodeTypeName,
                    Points = points
                };
                records.Add(record);
            }
        }

        // Serialize records to a JSON file (acts as a stand‑in for a spatial DB)
        string jsonPath = Path.Combine(tempFolder, "barcode_regions.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(records, jsonOptions);
        File.WriteAllText(jsonPath, json);

        // Output paths for verification
        Console.WriteLine($"Barcode image saved to: {barcodePath}");
        Console.WriteLine($"Region data saved to: {jsonPath}");
    }
}

/// <summary>
/// Simple DTO representing a point (X, Y) in the barcode region polygon.
/// </summary>
public class PointDto
{
    public float X { get; set; }
    public float Y { get; set; }
}

/// <summary>
/// Record representing a barcode region polygon, including its text, symbology, and vertex points.
/// </summary>
public class BarcodeRegionRecord
{
    public string CodeText { get; set; }
    public string Symbology { get; set; }
    public List<PointDto> Points { get; set; }
}