// Title: Generate Barcodes from XML Configuration File
// Description: Demonstrates loading multiple barcode definitions from a single XML file and generating corresponding images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to read barcode settings (symbology and data) from an XML source, resolve the symbology via EncodeTypes, and create image files with BarcodeGenerator. Typical use cases include batch barcode creation, dynamic report generation, and automated asset labeling. Developers often need to parse configuration files, map symbology names to API enums, and customize output parameters such as image format and dimensions.
// Prompt: Use a single XML file to store an array of barcode configurations and load them sequentially.
// Tags: barcode, symbology, generation, xml, aspose.barcode, png, encode types

using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Loads barcode definitions from an XML file and generates PNG images for each entry using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary working folder, ensures a sample XML file exists,
    /// reads each barcode configuration, and generates the corresponding image files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string xmlPath = Path.Combine(tempDir, "barcodes.xml");

        // If the XML file does not exist, create a sample file with two barcode definitions
        if (!File.Exists(xmlPath))
        {
            var sampleXml = new XDocument(
                new XElement("Barcodes",
                    new XElement("Barcode",
                        new XElement("Symbology", "Code128"),
                        new XElement("CodeText", "ABC123")
                    ),
                    new XElement("Barcode",
                        new XElement("Symbology", "QR"),
                        new XElement("CodeText", "Hello World")
                    )
                )
            );
            sampleXml.Save(xmlPath);
        }

        // Load the XML document containing barcode configurations
        XDocument doc = XDocument.Load(xmlPath);
        var barcodeElements = doc.Root?.Elements("Barcode");
        if (barcodeElements == null)
        {
            Console.WriteLine("No barcode configurations found.");
            return;
        }

        int index = 1;
        // Process each <Barcode> element sequentially
        foreach (var elem in barcodeElements)
        {
            // Extract symbology name and code text from the XML
            string symbologyName = elem.Element("Symbology")?.Value;
            string codeText = elem.Element("CodeText")?.Value;

            // Validate that both required values are present
            if (string.IsNullOrWhiteSpace(symbologyName) || string.IsNullOrWhiteSpace(codeText))
            {
                Console.WriteLine($"Skipping invalid configuration at index {index}.");
                index++;
                continue;
            }

            // Resolve the symbology string to the corresponding EncodeTypes enum value using reflection
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology '{symbologyName}' at index {index}.");
                index++;
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Generate the barcode image with the specified symbology and data
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example parameter: set module size (X dimension) to 2 points
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Save the generated barcode as a PNG file
                string outputPath = Path.Combine(tempDir, $"barcode_{index}.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode {index}: {outputPath}");
            }

            index++;
        }

        Console.WriteLine($"All barcodes processed. Files are located in: {tempDir}");
    }
}