// Title: Extract barcode region coordinates from a PNG and store them as JSON
// Description: Demonstrates reading a PNG image, detecting barcodes, extracting their placement region coordinates, and persisting the data for later use.
// Category-Description: This example belongs to the Aspose.BarCode reading and generation category. It showcases the BarCodeReader, BarcodeGenerator, and related classes to locate barcodes within images, retrieve geometric information (position, size, angle), and serialize results. Developers working with inventory, document processing, or quality control often need to capture barcode locations for analytics or database storage.
// Prompt: Extract barcode placement region coordinates from a PNG file and store them in a database.
// Tags: barcode, region, extraction, png, json, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample console application that generates a QR code (if needed), reads barcodes from a PNG file,
/// extracts their placement region data, and writes the information to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the temporary PNG file path.
        string imagePath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // If the PNG does not exist, generate a sample QR code image.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }

        // Collection to hold extracted barcode region records.
        var records = new List<BarcodeRegionRecord>();

        // Open the image with BarCodeReader to detect all supported barcode types.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through each detected barcode result.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the rectangle that defines the barcode's placement region.
                var rect = result.Region.Rectangle; // Aspose.Drawing.RectangleF

                // Populate a record with file and barcode details.
                var record = new BarcodeRegionRecord
                {
                    FileName = Path.GetFileName(imagePath),
                    CodeType = result.CodeTypeName,
                    CodeText = result.CodeText,
                    X = rect.X,
                    Y = rect.Y,
                    Width = rect.Width,
                    Height = rect.Height,
                    Angle = result.Region.Angle
                };

                // Add the record to the collection.
                records.Add(record);
            }
        }

        // Serialize the collection to a formatted JSON file.
        string jsonPath = Path.Combine(Path.GetTempPath(), "barcode_regions.json");
        string json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);

        // Inform the user where the results were saved.
        Console.WriteLine($"Extraction complete. Results saved to: {jsonPath}");

        // In a real application, the data could be stored in a database (e.g., SQLite, SQL Server).
        // Database code is omitted here because the required packages are not available in the snippet runner.
    }
}

/// <summary>
/// Data model representing the extracted region information for a single barcode.
/// </summary>
class BarcodeRegionRecord
{
    public string FileName { get; set; }
    public string CodeType { get; set; }
    public string CodeText { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public double Angle { get; set; }
}