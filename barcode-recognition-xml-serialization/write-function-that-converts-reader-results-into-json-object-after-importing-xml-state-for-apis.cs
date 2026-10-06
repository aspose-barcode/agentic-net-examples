// Title: Convert BarCodeReader XML State to JSON Output
// Description: Demonstrates generating a QR barcode, exporting the BarCodeReader state to XML, importing it back, reading the barcode, and serializing the results to JSON.
// Category-Description: This example belongs to the Aspose.BarCode operations collection that covers barcode generation, recognition, and state management. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and related settings for exporting/importing reader state. Typical use cases include persisting recognition configurations, batch processing, and integrating barcode data into JSON-based services. Developers often need to generate barcodes, configure readers, export settings to XML for reuse, and transform read results into common data formats like JSON.
// Prompt: Write a function that converts reader results into a JSON object after importing the XML state for APIs.
// Tags: barcode, qr, generation, recognition, export, import, json, aspose.barcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, reader state export/import, and JSON serialization of read results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, exports and imports reader state, reads the barcode, and outputs JSON.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary working directory for generated files
        // --------------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the barcode image and the exported XML state
        string imagePath = Path.Combine(workDir, "sample.png");
        string xmlPath = Path.Combine(workDir, "readerState.xml");

        // --------------------------------------------------------------------
        // Generate a sample QR barcode image
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the module size (X dimension) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Create a BarCodeReader, configure it, and export its state to XML
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.QR))
        {
            // Example setting: strip Function Code (FNC) characters from the result
            reader.BarcodeSettings.StripFNC = true;
            // Use high‑performance quality settings for faster processing
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Export the current reader configuration to an XML file
            reader.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the reader state from XML, assign the image, and read barcodes
        // --------------------------------------------------------------------
        List<object> results = new List<object>();
        using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlPath))
        {
            // Associate the previously generated image with the imported reader
            importedReader.SetBarCodeImage(imagePath);
            // Ensure the reader is set to decode QR codes
            importedReader.SetBarCodeReadType(DecodeType.QR);

            // Perform barcode recognition
            BarCodeResult[] readResults = importedReader.ReadBarCodes();
            foreach (BarCodeResult result in readResults)
            {
                // Extract region information for the detected barcode
                var regionRect = result.Region.Rectangle;
                // Build an anonymous object representing the result
                var resultObj = new
                {
                    CodeText = result.CodeText,
                    CodeTypeName = result.CodeTypeName,
                    ReadingQuality = result.ReadingQuality,
                    Region = new
                    {
                        X = regionRect.X,
                        Y = regionRect.Y,
                        Width = regionRect.Width,
                        Height = regionRect.Height
                    }
                };
                // Add the result object to the collection
                results.Add(resultObj);
            }
        }

        // --------------------------------------------------------------------
        // Serialize the collection of results to formatted JSON and output it
        // --------------------------------------------------------------------
        string json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine(json);
    }
}