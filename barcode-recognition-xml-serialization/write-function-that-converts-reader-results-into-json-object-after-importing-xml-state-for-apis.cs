// Title: Convert BarCodeReader results to JSON after XML state import
// Description: Demonstrates exporting a BarCodeReader configuration to XML, importing it back, and converting the read results into a formatted JSON string.
// Category-Description: This example belongs to the Aspose.BarCode reading and serialization category. It shows how to use BarCodeReader.ExportToXml, BarCodeReader.ImportFromXml, and related classes to persist reader settings, then deserialize barcode detection results into JSON using System.Text.Json. Developers often need to store reader configurations and share scan results across services, making this pattern useful for API integrations and data pipelines.
// Prompt: Write a function that converts reader results into a JSON object after importing the XML state for APIs.
// Tags: barcode symbology, reading, json serialization, export xml, import xml, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode (if needed), exports a <see cref="BarCodeReader"/> configuration to XML,
/// imports it back, reads barcodes from an image, and outputs the results as JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the barcode generation, state export/import, reading, and JSON conversion.
    /// </summary>
    static void Main()
    {
        // Define file paths for the barcode image and the exported reader state.
        string imagePath = "barcode.png";
        string readerXmlPath = "reader_state.xml";

        // Ensure a barcode image exists; generate one if it does not.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(imagePath);
            }
        }

        // Create a reader for the image and export its configuration (excluding the image) to XML.
        using (var initialReader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Export the reader's settings to an XML file.
            initialReader.ExportToXml(readerXmlPath);
        }

        // Import a new reader instance from the previously saved XML configuration.
        BarCodeReader importedReader = BarCodeReader.ImportFromXml(readerXmlPath);
        if (importedReader == null)
        {
            Console.WriteLine("Failed to import BarCodeReader from XML.");
            return;
        }

        // Reassign the image source to the imported reader (required after import).
        importedReader.SetBarCodeImage(imagePath);

        // Perform barcode detection on the image.
        BarCodeResult[] results = importedReader.ReadBarCodes();

        // Prepare a list of anonymous objects representing the results for JSON serialization.
        var jsonObjects = new System.Collections.Generic.List<object>();
        foreach (var result in results)
        {
            var region = result.Region.Rectangle;
            jsonObjects.Add(new
            {
                CodeText = result.CodeText,
                CodeTypeName = result.CodeTypeName,
                Confidence = result.Confidence,
                ReadingQuality = result.ReadingQuality,
                Angle = result.Region.Angle,
                Region = new
                {
                    X = region.X,
                    Y = region.Y,
                    Width = region.Width,
                    Height = region.Height
                }
            });
        }

        // Serialize the result list to a formatted JSON string.
        string json = JsonSerializer.Serialize(jsonObjects, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);

        // Release resources held by the imported reader.
        importedReader.Dispose();
    }
}