// Title: Export/Import BarCodeReader Settings and Convert Results to JSON
// Description: Demonstrates generating a QR barcode, exporting the BarCodeReader configuration to XML, importing it into a new reader, and converting the read results to a formatted JSON string.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing how to persist and reuse BarCodeReader settings via XML. It uses BarcodeGenerator, BarCodeReader, and related classes to illustrate typical workflows such as barcode generation, recognition, configuration export/import, and result serialization—common tasks for developers integrating barcode processing into applications.
// Prompt: Write a function that converts reader results into a JSON object after importing the XML state for APIs.
// Tags: barcode, qr, generation, recognition, xml export, xml import, json serialization, aspose.barcode, aspose.barcode.generation, aspose.barcode.recognition

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, reader configuration export/import, and JSON conversion of results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a QR code, reads it, exports/imports reader settings, and outputs JSON.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for the demo files
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define the path for the sample barcode image
        string barcodePath = Path.Combine(workFolder, "sample.png");

        // Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode and export the reader's configuration to XML
        BarCodeResult[] originalResults;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            originalResults = reader.ReadBarCodes();

            using (var xmlStream = new MemoryStream())
            {
                // Export current reader configuration to an in‑memory XML stream
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0; // Reset stream position for reading

                // Import the configuration into a new reader instance
                using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                {
                    // Assign the same image source to the imported reader
                    importedReader.SetBarCodeImage(barcodePath);
                    BarCodeResult[] importedResults = importedReader.ReadBarCodes();

                    // Convert the imported results to a formatted JSON string
                    string json = ConvertResultsToJson(importedResults);
                    Console.WriteLine("JSON representation of imported reader results:");
                    Console.WriteLine(json);
                }
            }
        }

        // Clean up temporary files and directories
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(workFolder, true);
        }
        catch
        {
            // Ignored – cleanup failures should not interrupt the demo
        }
    }

    // Converts an array of BarCodeResult into a JSON string
    static string ConvertResultsToJson(BarCodeResult[] results)
    {
        var list = new List<object>();
        foreach (var result in results)
        {
            var regionRect = result.Region.Rectangle;
            var item = new
            {
                CodeText = result.CodeText,
                CodeTypeName = result.CodeTypeName,
                ReadingQuality = result.ReadingQuality,
                Region = new
                {
                    X = regionRect.X,
                    Y = regionRect.Y,
                    Width = regionRect.Width,
                    Height = regionRect.Height,
                    Angle = result.Region.Angle
                }
            };
            list.Add(item);
        }

        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(list, options);
    }
}