// Title: Store QR barcode region polygon points to JSON for spatial analysis
// Description: Generates a QR barcode, reads its region polygon points, and saves them to a JSON file for later geometric processing.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition APIs. It creates a barcode image, extracts the region polygon using BarCodeReader, and serializes the coordinates. Developers working with spatial databases or geometric analysis often need to capture barcode region data for indexing, collision detection, or mapping purposes. The key classes used are BarcodeGenerator, BarCodeReader, BarCodeResult, and System.Text.Json for serialization.
// Prompt: Store barcode region polygon points in a spatial database for later geometric analysis.
// Tags: qr, barcode, region, polygon, json, spatial, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a QR barcode, extracting its region polygon points,
/// and persisting the data to a JSON file for downstream spatial analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, recognition, and data export.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for sample barcodes
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode content and output image path
        string codeText = "Sample123";
        string imagePath = Path.Combine(tempFolder, "sample_qr.png");

        // Generate a QR barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Collection to hold region records for each processed barcode image
        var records = new List<RegionRecord>();

        // Read the barcode image and extract the region polygon points
        using (var reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Convert Aspose.Point objects to simple DTOs for serialization
                var pointList = new List<PointDto>();
                foreach (var pt in result.Region.Points)
                {
                    pointList.Add(new PointDto { X = pt.X, Y = pt.Y });
                }

                // Store filename and associated polygon points
                records.Add(new RegionRecord
                {
                    FileName = Path.GetFileName(imagePath),
                    Points = pointList
                });
            }
        }

        // Serialize the region records to a formatted JSON file in the current directory
        string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_regions.json");
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(records, options);
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Region data saved to: {jsonPath}");
    }
}

/// <summary>
/// Represents a barcode image file and its associated polygon points.
/// </summary>
class RegionRecord
{
    public string FileName { get; set; }
    public List<PointDto> Points { get; set; }
}

/// <summary>
/// Simple data transfer object for a 2‑D point.
/// </summary>
class PointDto
{
    public double X { get; set; }
    public double Y { get; set; }
}