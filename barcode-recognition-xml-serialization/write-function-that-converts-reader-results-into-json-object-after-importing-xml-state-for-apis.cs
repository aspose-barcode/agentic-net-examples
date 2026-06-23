using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, reading it, exporting/importing reader state,
/// and serializing the results to JSON.
/// </summary>
class Program
{
    // Simple DTO for JSON serialization of barcode results
    class BarcodeResultInfo
    {
        public string CodeTypeName { get; set; }
        public string CodeText { get; set; }
        public int Confidence { get; set; }
        public double ReadingQuality { get; set; }
        public RegionInfo Region { get; set; }
    }

    // DTO representing the region of a detected barcode
    class RegionInfo
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public double Angle { get; set; }
    }

    /// <summary>
    /// Entry point of the application.
    /// Generates a sample barcode if needed, reads it, exports/imports reader state,
    /// and prints the detection results as formatted JSON.
    /// </summary>
    static void Main()
    {
        const string imagePath = "barcode.png";
        const string xmlPath = "reader.xml";

        // Ensure a sample barcode image exists; generate one if missing
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
            {
                generator.Save(imagePath);
            }
        }

        // Create a reader, assign the image, and export its state to XML
        using (var bitmap = new Bitmap(imagePath))
        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
        {
            // Export the current reader configuration and state to an XML file
            reader.ExportToXml(xmlPath);
        }

        // Import the reader state from the previously saved XML
        using (var importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Reassign the image after import (required for further reading)
            importedReader.SetBarCodeImage(imagePath);

            // Perform barcode detection on the image
            var results = importedReader.ReadBarCodes();

            // Convert detection results into a list of DTOs for serialization
            var resultList = new List<BarcodeResultInfo>();
            foreach (var result in results)
            {
                var regionRect = result.Region.Rectangle;
                var info = new BarcodeResultInfo
                {
                    CodeTypeName = result.CodeTypeName,
                    CodeText = result.CodeText,
                    Confidence = (int)result.Confidence,
                    ReadingQuality = result.ReadingQuality,
                    Region = new RegionInfo
                    {
                        X = regionRect.X,
                        Y = regionRect.Y,
                        Width = regionRect.Width,
                        Height = regionRect.Height,
                        Angle = result.Region.Angle
                    }
                };
                resultList.Add(info);
            }

            // Serialize the DTO list to formatted JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(resultList, jsonOptions);

            // Output the JSON to the console
            Console.WriteLine(json);
        }
    }
}