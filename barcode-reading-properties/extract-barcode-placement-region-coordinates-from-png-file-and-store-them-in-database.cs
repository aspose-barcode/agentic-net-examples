// Title: Extract barcode region coordinates from a PNG and save to JSON (as DB placeholder)
// Description: Demonstrates how to read barcodes from a PNG image using Aspose.BarCode, retrieve each barcode's placement region (position, size, angle), and store the data in a JSON file that can be used as a simple database.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader, DecodeType, and BarCodeResult to locate barcodes within images. Typical scenarios include inventory scanning, document processing, and quality control where developers need to capture barcode geometry for further analysis or storage. The code shows how to extract region information and serialize it for persistence.
// Prompt: Extract barcode placement region coordinates from a PNG file and store them in a database.
// Tags: barcode, recognition, png, region, coordinates, json, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode detection and region extraction from a PNG image, storing results in JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO for persisting barcode metadata and its placement region.
    /// </summary>
    class BarcodeRecord
    {
        public string CodeType { get; set; }
        public string CodeText { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public double Angle { get; set; }
    }

    /// <summary>
    /// Entry point. Reads barcodes from the specified PNG, logs details, and writes region data to a JSON file.
    /// </summary>
    static void Main()
    {
        // Path to the sample PNG file that contains one or more barcodes.
        string imagePath = "sample_barcode.png";

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Collection to hold extracted barcode region information.
        var records = new List<BarcodeRecord>();

        // Initialize the barcode reader to detect all supported barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through each detected barcode in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the rectangular region that encloses the barcode.
                var rect = result.Region.Rectangle; // RectangleF returned by the API

                // Populate a record with barcode data and its geometric properties.
                var record = new BarcodeRecord
                {
                    CodeType = result.CodeTypeName,
                    CodeText = result.CodeText,
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width,
                    Height = rect.Height,
                    Angle = result.Region.Angle
                };
                records.Add(record);

                // Output details to the console for immediate feedback.
                Console.WriteLine($"Found {record.CodeType}: {record.CodeText}");
                Console.WriteLine($"Region - X:{record.X}, Y:{record.Y}, W:{record.Width}, H:{record.Height}, Angle:{record.Angle}");
            }
        }

        // Serialize the collected records to a formatted JSON file (acting as a simple database).
        string jsonPath = "barcode_regions.json";
        string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Barcode region data saved to {jsonPath}");

        // In a real application, replace the JSON file with database storage logic,
        // e.g., using SQLite or SQL Server to insert records into a table.
    }
}