// Title: Store barcode region polygon points for spatial analysis
// Description: Demonstrates generating a QR barcode, reading its region polygon points, and saving them as JSON to simulate storage in a spatial database.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and related result classes to extract geometric region data. Developers often need to persist barcode location information for GIS or spatial queries, typically using databases with spatial extensions.
// Prompt: Store barcode region polygon points in a spatial database for later geometric analysis.
// Tags: qr, barcode generation, barcode recognition, json, spatial database, aspose.barcode, aspose.drawing

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR barcode, extracting its region polygon points,
/// and persisting the data for later geometric analysis.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads its region points,
    /// and stores the information in a JSON file (simulating a spatial database).
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary working directory for generated files.
        // --------------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // --------------------------------------------------------------------
        // Generate a sample QR barcode image and save it as PNG.
        // --------------------------------------------------------------------
        string barcodeFile = Path.Combine(workDir, "sample_qr.png");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the barcode image, collect region polygon points for each result.
        // --------------------------------------------------------------------
        var records = new List<BarcodeRecord>();
        if (File.Exists(barcodeFile))
        {
            using (var reader = new BarCodeReader(barcodeFile, DecodeType.QR, DecodeType.Code128))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    var points = new List<PointDto>();
                    foreach (Point pt in result.Region.Points)
                    {
                        points.Add(new PointDto { X = pt.X, Y = pt.Y });
                    }

                    records.Add(new BarcodeRecord
                    {
                        FileName = barcodeFile,
                        CodeText = result.CodeText,
                        CodeType = result.CodeTypeName,
                        Points = points
                    });
                }
            }
        }
        else
        {
            Console.WriteLine($"Barcode image not found: {barcodeFile}");
        }

        // --------------------------------------------------------------------
        // Serialize the collected region data to JSON (simulating a spatial DB).
        // --------------------------------------------------------------------
        string jsonPath = Path.Combine(workDir, "barcode_regions.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(records, jsonOptions);
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"Region data saved to: {jsonPath}");
        // In a real scenario, replace the JSON storage with a spatial database (e.g., SQLite with spatial extensions).
    }
}

/// <summary>
/// Represents a barcode record containing file information, decoded text,
/// symbology type, and the polygon points of its detected region.
/// </summary>
class BarcodeRecord
{
    public string FileName { get; set; }
    public string CodeText { get; set; }
    public string CodeType { get; set; }
    public List<PointDto> Points { get; set; }
}

/// <summary>
/// Simple DTO for a point in the barcode region polygon.
/// </summary>
class PointDto
{
    public int X { get; set; }
    public int Y { get; set; }
}