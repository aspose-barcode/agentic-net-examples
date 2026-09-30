// Title: Generate multiple barcodes from an XML configuration file
// Description: Demonstrates how to store an array of barcode settings in a single XML document, load them sequentially, and generate PNG images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image export APIs. Typical use cases include batch barcode creation from external configuration sources such as XML, JSON, or databases. Developers often need to read configuration data, map symbology names to EncodeTypes, and produce image files in various formats.
// Prompt: Use a single XML file to store an array of barcode configurations and load them sequentially.
// Tags: barcode, symbology, generation, xml, aspose.barcode, png, batch-processing

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates batch barcode generation from an XML configuration file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary XML file with barcode definitions, reads it back,
    /// and generates PNG images for each configuration using Aspose.BarCode.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup temporary working directories for the XML config and output images
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string xmlPath = Path.Combine(tempDir, "barcodeConfigs.xml");
        string outputDir = Path.Combine(tempDir, "Output");
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------
        // Step 1: Create sample barcode configurations and store them in a single XML file
        // --------------------------------------------------------------
        var configs = new[]
        {
            new { Symbology = "QR", CodeText = "Hello QR" },
            new { Symbology = "Code128", CodeText = "1234567890" },
            new { Symbology = "DataMatrix", CodeText = "DataMatrix Sample" }
        };

        // Build the XML document using LINQ to XML
        var doc = new XDocument(
            new XElement("Barcodes",
                new XElement("Count", configs.Length),
                new XElement("Items",
                    // Create an <Items> element that contains a <Barcode> element for each configuration
                    new Func<XElement>(() =>
                    {
                        var root = new XElement("Items");
                        foreach (var cfg in configs)
                        {
                            root.Add(
                                new XElement("Barcode",
                                    new XElement("Symbology", cfg.Symbology),
                                    new XElement("CodeText", cfg.CodeText)
                                )
                            );
                        }
                        return root;
                    })()
                )
            )
        );

        // Persist the XML configuration to disk
        doc.Save(xmlPath);

        // --------------------------------------------------------------
        // Step 2: Load configurations sequentially from the XML file
        // --------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Configuration file not found.");
            return;
        }

        XDocument loadedDoc = XDocument.Load(xmlPath);
        var barcodeElements = loadedDoc.Root?.Element("Items")?.Elements("Barcode");
        if (barcodeElements == null)
        {
            Console.WriteLine("No barcode configurations found.");
            return;
        }

        int index = 0;
        foreach (var elem in barcodeElements)
        {
            // Extract symbology name and code text from the XML element
            string symbologyName = elem.Element("Symbology")?.Value;
            string codeText = elem.Element("CodeText")?.Value ?? string.Empty;

            if (string.IsNullOrWhiteSpace(symbologyName))
            {
                Console.WriteLine($"Skipping entry #{index}: missing symbology.");
                continue;
            }

            // Resolve symbology name to EncodeTypes enum value via reflection
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology '{symbologyName}' at entry #{index}.");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create the barcode generator and configure basic properties
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Example of setting a parameter: change bar color to blue
                generator.Parameters.Barcode.BarColor = Color.Blue;

                // Save the barcode image as PNG
                string fileName = $"{symbologyName}_{index}.png";
                string outputPath = Path.Combine(outputDir, fileName);
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated {outputPath}");
            }

            index++;
        }

        // --------------------------------------------------------------------
        // Completion message – the temporary directory holds the XML and images
        // --------------------------------------------------------------------
        Console.WriteLine($"All barcodes processed. Files are located in: {outputDir}");
    }
}