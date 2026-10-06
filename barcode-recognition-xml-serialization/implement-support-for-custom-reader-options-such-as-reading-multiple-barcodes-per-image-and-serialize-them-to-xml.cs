// Title: Demonstrate custom BarCodeReader options and XML serialization with Aspose.BarCode
// Description: This example generates sample Code128 barcodes, configures a BarCodeReader with custom settings, reads multiple barcodes from an image, and serializes the reader configuration to XML.
// Category-Description: Shows how to work with Aspose.BarCode generation and recognition APIs, focusing on customizing BarCodeReader settings (e.g., StripFNC, XDimension, AllowIncorrectBarcodes) and persisting them via ExportToXml/ImportFromXml. Typical use cases include batch barcode scanning, fine‑tuning recognition quality, and reusing reader configurations across sessions. Developers often need to adjust quality settings and serialize them for repeatable processing pipelines.
// Prompt: Implement support for custom reader options, such as reading multiple barcodes per image, and serialize them to XML.
// Tags: barcode generation, barcode recognition, custom reader options, xml serialization, aspose.barcode, csharp

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Entry point for the barcode generation and recognition demo.
/// </summary>
class Program
{
    /// <summary>
    /// Generates sample barcodes, reads them with custom options, and demonstrates XML export/import of reader settings.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for sample barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Generate sample barcode images and store their file paths
        List<string> barcodeFiles = new List<string>();
        for (int i = 0; i < 3; i++)
        {
            string codeText = $"CODE{i}";
            string filePath = Path.Combine(tempFolder, $"barcode_{i}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            barcodeFiles.Add(filePath);
        }

        // Use the first generated image for demonstration
        string sampleImage = barcodeFiles[0];
        if (!File.Exists(sampleImage))
        {
            Console.WriteLine("Sample image not found.");
            return;
        }

        // Create a BarCodeReader with custom options to read all supported types
        BaseDecodeType decodeAll = DecodeType.AllSupportedTypes;
        using (var reader = new BarCodeReader(sampleImage, decodeAll))
        {
            // Apply custom reader settings
            reader.BarcodeSettings.StripFNC = true;                     // Remove FNC characters from the result
            reader.QualitySettings.XDimension = XDimensionMode.Small; // Use a smaller X-dimension for higher density
            reader.QualitySettings.AllowIncorrectBarcodes = true;     // Permit reading of slightly malformed barcodes

            // Read all barcodes present in the image (multiple per image are supported)
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Initial read - barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Serialize the current reader configuration to an XML file
            string xmlPath = Path.Combine(tempFolder, "readerState.xml");
            reader.ExportToXml(xmlPath);
            Console.WriteLine($"Reader state exported to: {xmlPath}");
        }

        // Import the previously saved reader configuration from XML and read again
        string importedXml = Path.Combine(tempFolder, "readerState.xml");
        if (File.Exists(importedXml))
        {
            using (var importedReader = BarCodeReader.ImportFromXml(importedXml))
            {
                // Associate the image with the imported reader (image data is not stored in XML)
                importedReader.SetBarCodeImage(sampleImage);

                // Perform barcode reading using the imported settings
                BarCodeResult[] importedResults = importedReader.ReadBarCodes();
                Console.WriteLine($"After import - barcodes found: {importedResults.Length}");
                foreach (var result in importedResults)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Cleanup temporary files and folder (optional)
        try
        {
            foreach (var file in barcodeFiles)
            {
                if (File.Exists(file)) File.Delete(file);
            }
            if (File.Exists(importedXml)) File.Delete(importedXml);
            if (Directory.Exists(tempFolder)) Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}