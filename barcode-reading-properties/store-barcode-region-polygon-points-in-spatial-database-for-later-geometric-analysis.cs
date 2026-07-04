// Title: Store barcode region polygon points as JSON (demo for spatial DB)
// Description: Generates a Code128 barcode, reads its region polygon points, and saves them as JSON for later geometric analysis.
// Prompt: Store barcode region polygon points in a spatial database for later geometric analysis.
// Tags: barcode, code128, region, polygon, json, spatial database, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode, extracting its region polygon points,
/// and persisting those points as JSON (as a placeholder for a spatial database).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads its region,
    /// and writes the polygon points to a JSON file.
    /// </summary>
    static void Main()
    {
        // Define file paths for the barcode image and the JSON output.
        string imagePath = "barcode.png";
        string jsonPath = "barcode_regions.json";

        // -----------------------------------------------------------------
        // 1. Generate a simple Code128 barcode and save it as an image.
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Optional: configure image size for better visibility.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // -----------------------------------------------------------------
        // 2. Read the barcode image and obtain its region polygon points.
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Decode all barcodes present in the image.
            var results = reader.ReadBarCodes();

            // Prepare a list to hold region data for each detected barcode.
            var barcodeRegions = new System.Collections.Generic.List<object>();

            // Iterate over each decoded result.
            foreach (var result in results)
            {
                // result.Region.Points returns an array of PointF structures (X,Y coordinates).
                var points = result.Region.Points;
                var pointList = new System.Collections.Generic.List<object>();

                // Convert each PointF to an anonymous object with X and Y properties.
                foreach (var pt in points)
                {
                    pointList.Add(new { X = pt.X, Y = pt.Y });
                }

                // Add the barcode data and its polygon points to the collection.
                barcodeRegions.Add(new
                {
                    CodeText = result.CodeText,
                    Symbology = result.CodeTypeName,
                    Points = pointList
                });
            }

            // -----------------------------------------------------------------
            // 3. Store the polygon points locally as JSON (stand‑in for a spatial DB).
            // -----------------------------------------------------------------
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(barcodeRegions, jsonOptions);
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"Barcode region data written to '{jsonPath}'.");
        }

        // -----------------------------------------------------------------
        // NOTE: In a real scenario you would store the polygon points in a spatial
        // database (e.g., SQLite with SpatiaLite, PostgreSQL with PostGIS, etc.).
        // The code would involve creating a table with a geometry column and
        // inserting the points using appropriate spatial types (e.g., POLYGON).
        // -----------------------------------------------------------------
    }
}