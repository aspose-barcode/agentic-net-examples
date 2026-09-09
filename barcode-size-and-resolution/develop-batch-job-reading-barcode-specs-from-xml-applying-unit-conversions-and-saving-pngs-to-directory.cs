// Title: Batch barcode generation from XML specification
// Description: Demonstrates reading barcode definitions from an XML file, converting XDimension units, and generating PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and XDimension properties for batch processing. Typical use cases include automated creation of multiple barcodes from data sources, unit conversion handling, and saving images to a file system. Developers often need to parse input specifications, map symbology names to EncodeTypes via reflection, and output barcodes in common formats.
// Prompt: Develop batch job reading barcode specs from XML, applying unit conversions, and saving PNGs to directory.
// Tags: barcode, batch, xml, png, aspose.barcode, generation, unit-conversion, symbology

using System;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Program that reads barcode specifications from an XML file, applies unit conversions,
/// and generates PNG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary working folder, generates a sample XML,
    /// parses barcode entries, and produces PNG files for each valid specification.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Paths for sample XML and output images
        string xmlPath = Path.Combine(workFolder, "BarcodesSpec.xml");
        string outputFolder = Path.Combine(workFolder, "Output");
        Directory.CreateDirectory(outputFolder);

        // Create a sample XML specification file
        CreateSampleXml(xmlPath);

        // Parse XML and generate barcodes
        List<string> generatedFiles = new List<string>();
        XDocument doc = XDocument.Load(xmlPath);
        IEnumerable<XElement> barcodeElements = doc.Root?.Elements("Barcode") ?? new List<XElement>();

        int index = 1;
        foreach (XElement elem in barcodeElements)
        {
            // Extract required fields from XML
            string symbologyName = elem.Element("Symbology")?.Value?.Trim() ?? "";
            string codeText = elem.Element("CodeText")?.Value?.Trim() ?? "";
            string xDimValueStr = elem.Element("XDimension")?.Value?.Trim() ?? "";
            string xDimUnit = elem.Element("XDimensionUnit")?.Value?.Trim() ?? "Point";

            // Validate required fields
            if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText) || string.IsNullOrEmpty(xDimValueStr))
            {
                Console.WriteLine($"Skipping entry {index}: missing required fields.");
                index++;
                continue;
            }

            // Parse XDimension value
            if (!float.TryParse(xDimValueStr, out float xDimValue))
            {
                Console.WriteLine($"Skipping entry {index}: invalid XDimension value.");
                index++;
                continue;
            }

            // Resolve symbology name to EncodeTypes enum via reflection
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Skipping entry {index}: unknown symbology '{symbologyName}'.");
                index++;
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create generator and apply settings
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply XDimension based on unit
                switch (xDimUnit.ToLowerInvariant())
                {
                    case "point":
                    case "points":
                        generator.Parameters.Barcode.XDimension.Point = xDimValue;
                        break;
                    case "pixel":
                    case "pixels":
                        generator.Parameters.Barcode.XDimension.Pixels = xDimValue;
                        break;
                    case "millimeter":
                    case "millimeters":
                        generator.Parameters.Barcode.XDimension.Millimeters = xDimValue;
                        break;
                    case "inch":
                    case "inches":
                        generator.Parameters.Barcode.XDimension.Inches = xDimValue;
                        break;
                    default:
                        Console.WriteLine($"Entry {index}: unknown XDimension unit '{xDimUnit}', defaulting to Point.");
                        generator.Parameters.Barcode.XDimension.Point = xDimValue;
                        break;
                }

                // Save barcode as PNG
                string fileName = $"{symbologyName}_{index}.png";
                string filePath = Path.Combine(outputFolder, fileName);
                try
                {
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    generatedFiles.Add(filePath);
                    Console.WriteLine($"Generated: {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to generate barcode for entry {index}: {ex.Message}");
                }
            }

            index++;
        }

        Console.WriteLine("Batch processing completed.");
        Console.WriteLine($"Generated {generatedFiles.Count} barcode image(s) in folder: {outputFolder}");
    }

    /// <summary>
    /// Creates a sample XML file containing barcode specifications used for the demonstration.
    /// </summary>
    /// <param name="path">The file path where the XML document will be saved.</param>
    static void CreateSampleXml(string path)
    {
        XDocument doc = new XDocument(
            new XElement("Barcodes",
                new XElement("Barcode",
                    new XElement("Symbology", "Code128"),
                    new XElement("CodeText", "ABC123456"),
                    new XElement("XDimension", "2.0"),
                    new XElement("XDimensionUnit", "Millimeters")
                ),
                new XElement("Barcode",
                    new XElement("Symbology", "QR"),
                    new XElement("CodeText", "https://example.com"),
                    new XElement("XDimension", "3.0"),
                    new XElement("XDimensionUnit", "Point")
                ),
                new XElement("Barcode",
                    new XElement("Symbology", "DataMatrix"),
                    new XElement("CodeText", "DataMatrixSample"),
                    new XElement("XDimension", "1.5"),
                    new XElement("XDimensionUnit", "Pixel")
                )
            )
        );
        doc.Save(path);
    }
}