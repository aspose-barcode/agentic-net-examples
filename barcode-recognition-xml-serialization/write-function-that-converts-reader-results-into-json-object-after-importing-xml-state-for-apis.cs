// Title: Convert BarCodeReader results to JSON after importing XML state
// Description: Demonstrates generating a barcode, exporting the reader state to XML, importing it back, reading barcodes, and converting the results to a JSON document.
// Prompt: Write a function that converts reader results into a JSON object after importing the XML state for APIs.
// Tags: barcode symbology, generation, recognition, json, xml, aspose.barcode

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that shows how to generate a barcode, export/import the reader state via XML,
/// read the barcode, and serialize the results to JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define temporary file paths for the barcode image and the exported XML state
        string imagePath = "barcode.png";
        string xmlPath = "reader_state.xml";

        // ------------------------------------------------------------
        // Generate a sample Code128 barcode and save it to a PNG file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath);
        }

        // ------------------------------------------------------------
        // Create a BarCodeReader, export its internal state to XML, then dispose it
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            reader.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Import the previously saved reader state from the XML file
        // ------------------------------------------------------------
        BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath);
        if (importedReader == null)
        {
            Console.WriteLine("Failed to import BarCodeReader from XML.");
            return;
        }

        // Assign the image source to the imported reader (required after import)
        importedReader.SetBarCodeImage(imagePath);

        // ------------------------------------------------------------
        // Read barcodes from the image using the imported reader
        // ------------------------------------------------------------
        BarCodeResult[] results = importedReader.ReadBarCodes();

        // ------------------------------------------------------------
        // Convert each BarCodeResult into an anonymous object suitable for JSON serialization
        // ------------------------------------------------------------
        var jsonItems = new List<object>();
        foreach (var result in results)
        {
            var region = result.Region.Rectangle;
            jsonItems.Add(new
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

        // ------------------------------------------------------------
        // Serialize the list of result objects to a formatted JSON string
        // ------------------------------------------------------------
        string json = JsonSerializer.Serialize(jsonItems, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);

        // ------------------------------------------------------------
        // Clean up resources
        // ------------------------------------------------------------
        importedReader.Dispose();

        // Optionally delete temporary files (comment out if you want to keep them)
        try
        {
            if (File.Exists(imagePath)) File.Delete(imagePath);
            if (File.Exists(xmlPath)) File.Delete(xmlPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}