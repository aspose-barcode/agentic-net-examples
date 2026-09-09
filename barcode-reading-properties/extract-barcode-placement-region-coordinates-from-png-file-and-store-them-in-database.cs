// Title: Extract barcode region coordinates from a PNG image and store them in a JSON file
// Description: Demonstrates how to read a PNG file, detect all supported barcodes, retrieve each barcode's placement region (position, size, angle), and persist the data for later use.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, showcasing the BarCodeReader and BarCodeResult classes. Typical scenarios include inventory scanning, document processing, and quality control where developers need to locate barcodes within images and record their geometric information for downstream systems such as databases or analytics pipelines.
// Prompt: Extract barcode placement region coordinates from a PNG file and store them in a database.
// Tags: barcode, region, extraction, png, json, aspose.barcode, recognition, coordinates, database

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Represents the geometric and textual information of a detected barcode within an image.
/// </summary>
class BarcodeRegionInfo
{
    public string ImagePath { get; set; }
    public string CodeType { get; set; }
    public string CodeText { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public double Angle { get; set; }
}

/// <summary>
/// Entry point of the console application that reads a PNG image, detects barcodes,
/// extracts their placement regions, and saves the results to a JSON file (as a stand‑in for a database).
/// </summary>
class Program
{
    /// <summary>
    /// Main method that orchestrates barcode detection and data persistence.
    /// </summary>
    /// <param name="args">Optional command‑line arguments; the first argument can specify the image file path.</param>
    static void Main(string[] args)
    {
        // Determine the PNG file to process: use the first argument if supplied, otherwise fall back to a default name.
        string imagePath = args.Length > 0 ? args[0] : "sample.png";

        // Verify that the specified file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Prepare a collection to hold information about each detected barcode.
        var records = new List<BarcodeRegionInfo>();

        // Initialize the barcode reader to scan the image for all supported barcode types.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate over each detection result returned by the reader.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Extract the rectangular region and rotation angle of the barcode.
                RectangleF rect = result.Region.Rectangle;
                double angle = result.Region.Angle;

                // Populate a data object with both textual and geometric details.
                var info = new BarcodeRegionInfo
                {
                    ImagePath = imagePath,
                    CodeType = result.CodeTypeName,
                    CodeText = result.CodeText,
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width,
                    Height = rect.Height,
                    Angle = angle
                };

                // Add the populated object to the results list.
                records.Add(info);

                // Output detection details to the console for immediate feedback.
                Console.WriteLine($"Detected {info.CodeType}: {info.CodeText}");
                Console.WriteLine($"Region - X:{info.X}, Y:{info.Y}, W:{info.Width}, H:{info.Height}, Angle:{info.Angle}");
            }
        }

        // Serialize the collected barcode region data to a formatted JSON file.
        // In a production scenario this could be replaced with direct database insertion.
        string jsonPath = "barcode_regions.json";
        string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Region data saved to {jsonPath}");

        // Note: Replace the JSON persistence with actual database logic (e.g., using Entity Framework or ADO.NET) as needed.
    }
}