// Title: Generate Barcodes from XML Configuration
// Description: Demonstrates reading an XML file that defines an array of barcode configurations and generating corresponding barcode images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and related parameter classes to create barcodes programmatically. Typical use cases include batch barcode creation from external data sources such as XML, JSON, or databases. Developers often need to load configuration data, resolve symbology types, and output images in formats like PNG or JPEG.
// Prompt: Use a single XML file to store an array of barcode configurations and load them sequentially.
// Tags: barcode generation xml aspose.barcode symbology png batch processing

using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that reads barcode configurations from an XML file and generates barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a sample XML if missing, loads each configuration, and saves generated barcodes.
    /// </summary>
    static void Main()
    {
        // Define paths for the XML configuration file and the output directory.
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcodeConfigs.xml");
        string outputDir = Path.Combine(Path.GetTempPath(), "GeneratedBarcodes");
        Directory.CreateDirectory(outputDir);

        // Create a sample XML configuration file if it does not already exist.
        if (!File.Exists(xmlPath))
        {
            var sampleDoc = new XDocument(
                new XElement("BarcodeConfigs",
                    new XElement("BarcodeConfig",
                        new XElement("Symbology", "Code128"),
                        new XElement("CodeText", "Sample123"),
                        new XElement("XDimension", "2")
                    ),
                    new XElement("BarcodeConfig",
                        new XElement("Symbology", "QR"),
                        new XElement("CodeText", "https://example.com"),
                        new XElement("XDimension", "4")
                    ),
                    new XElement("BarcodeConfig",
                        new XElement("Symbology", "DataMatrix"),
                        new XElement("CodeText", "DM12345"),
                        new XElement("XDimension", "3")
                    )
                )
            );
            sampleDoc.Save(xmlPath);
            Console.WriteLine($"Created sample configuration file at: {xmlPath}");
        }

        // Load the XML document containing barcode configurations.
        XDocument doc = XDocument.Load(xmlPath);
        IEnumerable<XElement> configElements = doc.Root.Elements("BarcodeConfig");
        int index = 0;

        // Process each configuration element sequentially.
        foreach (XElement elem in configElements)
        {
            index++;
            string symbologyName = elem.Element("Symbology")?.Value;
            string codeText = elem.Element("CodeText")?.Value;
            string xDimStr = elem.Element("XDimension")?.Value;

            // Validate required fields.
            if (string.IsNullOrWhiteSpace(symbologyName) || string.IsNullOrWhiteSpace(codeText))
            {
                Console.WriteLine($"Skipping config #{index}: missing Symbology or CodeText.");
                continue;
            }

            // Resolve the symbology name to a BaseEncodeType using reflection.
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Skipping config #{index}: unknown symbology '{symbologyName}'.");
                continue;
            }
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a BarcodeGenerator instance and apply optional parameters.
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                if (!string.IsNullOrWhiteSpace(xDimStr) && float.TryParse(xDimStr, out float xDimValue))
                {
                    generator.Parameters.Barcode.XDimension.Point = xDimValue;
                }

                // Define the output file path.
                string outFile = Path.Combine(outputDir, $"{symbologyName}_{index}.png");
                try
                {
                    // Save the generated barcode as a PNG image.
                    generator.Save(outFile, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode #{index}: {outFile}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error generating barcode #{index}: {ex.Message}");
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}