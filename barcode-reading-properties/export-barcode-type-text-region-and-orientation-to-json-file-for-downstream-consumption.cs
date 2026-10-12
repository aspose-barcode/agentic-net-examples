// Title: Export barcode details (type, text, region, orientation) to JSON
// Description: This example generates a Code128 barcode, reads it back, extracts key properties, and writes them to a JSON file for downstream consumption.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to decode them, and System.Text.Json to serialize extracted information. Developers working with barcode imaging often need to export metadata such as type, text, location, and orientation for analytics, inventory, or integration with other systems.
// Prompt: Export barcode type, text, region, and orientation to a JSON file for downstream consumption.
// Tags: barcode, code128, generation, recognition, json, export, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode, reading its metadata, and exporting that data to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode image,
    /// extracts its type, text, region, and orientation, and writes the information to JSON.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the example files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeExport_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define barcode content and output image path
        string barcodeText = "Sample12345";
        string barcodeFile = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // List to hold extracted barcode information objects
        var results = new List<object>();

        // Read the generated barcode and collect required properties
        using (var reader = new BarCodeReader(barcodeFile, DecodeType.AllSupportedTypes))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                var rect = result.Region.Rectangle;
                var info = new
                {
                    Type = result.CodeTypeName,
                    Text = result.CodeText,
                    Region = new
                    {
                        X = rect.X,
                        Y = rect.Y,
                        Width = rect.Width,
                        Height = rect.Height
                    },
                    Orientation = result.Region.Angle
                };
                results.Add(info);
            }
        }

        // Serialize the collected barcode data to a formatted JSON string
        string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        string jsonPath = Path.Combine(tempFolder, "barcode_info.json");
        File.WriteAllText(jsonPath, json);

        // Output the location of the generated JSON file
        Console.WriteLine("Barcode information exported to: " + jsonPath);
    }
}