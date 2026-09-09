// Title: Convert BarCodeReader XML State to JSON Output
// Description: Demonstrates generating a QR barcode, exporting the reader state to XML, importing it back, reading barcodes, and serializing the results to JSON.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases key API classes such as BarcodeGenerator, BarCodeReader, and related methods for exporting/importing reader state, setting read types, and extracting barcode regions. Developers often need to persist reader configurations, reuse them across sessions, and transform recognition results into common data formats like JSON for downstream processing.
// Prompt: Write a function that converts reader results into a JSON object after importing the XML state for APIs.
// Tags: barcode, qr, generation, recognition, xml, json, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation, state export/import, and JSON serialization using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, exports reader state to XML,
    /// imports the state, reads the barcode, and outputs the results as formatted JSON.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the generated barcode image and the exported XML state
        string barcodeImagePath = Path.Combine(tempFolder, "qr.png");
        string readerXmlPath = Path.Combine(tempFolder, "readerState.xml");

        // ------------------------------------------------------------
        // 1. Generate a QR barcode image and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Set the module size (pixel dimension) for the QR code
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodeImagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 2. Create a BarCodeReader, read the QR code, and export its state to XML
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodeImagePath, DecodeType.QR))
        {
            // Persist the reader configuration and internal state to an XML file
            reader.ExportToXml(readerXmlPath);
        }

        // ------------------------------------------------------------
        // 3. Import the previously saved reader state, set the image, and read barcodes
        // ------------------------------------------------------------
        using (var importedReader = BarCodeReader.ImportFromXml(readerXmlPath))
        {
            // Associate the same image file with the imported reader instance
            importedReader.SetBarCodeImage(barcodeImagePath);
            // Ensure the reader is configured to decode QR codes
            importedReader.SetBarCodeReadType(DecodeType.QR);

            // Perform barcode detection and obtain results
            BarCodeResult[] results = importedReader.ReadBarCodes();

            // ------------------------------------------------------------
            // 4. Transform each BarCodeResult into a serializable anonymous object
            // ------------------------------------------------------------
            var resultList = new List<object>();
            foreach (var result in results)
            {
                var rect = result.Region.Rectangle;
                var obj = new
                {
                    CodeText = result.CodeText,
                    CodeTypeName = result.CodeTypeName,
                    ReadingQuality = result.ReadingQuality,
                    Region = new
                    {
                        X = rect.X,
                        Y = rect.Y,
                        Width = rect.Width,
                        Height = rect.Height,
                        Angle = result.Region.Angle
                    }
                };
                resultList.Add(obj);
            }

            // ------------------------------------------------------------
            // 5. Serialize the collection to indented JSON and write to console
            // ------------------------------------------------------------
            string json = JsonSerializer.Serialize(resultList, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }

        // ------------------------------------------------------------
        // 6. Clean up temporary files and folder (optional)
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodeImagePath)) File.Delete(barcodeImagePath);
            if (File.Exists(readerXmlPath)) File.Delete(readerXmlPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the demo flow
        }
    }
}