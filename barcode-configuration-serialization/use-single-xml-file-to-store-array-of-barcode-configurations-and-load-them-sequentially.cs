// Title: Barcode generation from XML configuration array
// Description: Demonstrates loading multiple barcode settings from a single XML file and generating corresponding images. Shows how to map configuration values to Aspose.BarCode API.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to read barcode parameters from external data sources such as XML, JSON, or databases. It uses the BarcodeGenerator class together with EncodeTypes to create barcodes of various symbologies. Developers often need to batch‑process barcode creation based on configuration files, making this pattern useful for automated reporting, inventory labeling, or document stamping.
// Prompt: Use a single XML file to store an array of barcode configurations and load them sequentially.
// Tags: barcode, generation, xml, batch, encode types, aspose.barcode, png, symbology

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading an array of barcode configurations from an XML file
/// and generating corresponding barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary XML configuration,
    /// loads each barcode definition, and saves generated images to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare temporary working directories
        // --------------------------------------------------------------------
        string tempRoot = Path.Combine(Path.GetTempPath(), "BarcodeConfigDemo_" + Guid.NewGuid().ToString("N"));
        string xmlPath = Path.Combine(tempRoot, "configs.xml");
        string outputDir = Path.Combine(tempRoot, "Output");
        Directory.CreateDirectory(outputDir);
        Directory.CreateDirectory(tempRoot);

        // --------------------------------------------------------------------
        // Create a sample XML file containing an array of barcode configurations
        // --------------------------------------------------------------------
        XDocument sampleDoc = new XDocument(
            new XElement("Barcodes",
                new XElement("BarcodeConfig",
                    new XElement("Symbology", "Code128"),
                    new XElement("CodeText", "Sample123"),
                    new XElement("XDimension", "2.0")
                ),
                new XElement("BarcodeConfig",
                    new XElement("Symbology", "QR"),
                    new XElement("CodeText", "Hello World"),
                    new XElement("XDimension", "3.0")
                )
            )
        );
        sampleDoc.Save(xmlPath);
        Console.WriteLine($"Configuration XML saved to: {xmlPath}");

        // --------------------------------------------------------------------
        // Load and validate the configuration XML
        // --------------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Configuration file not found.");
            return;
        }

        XDocument doc;
        try
        {
            doc = XDocument.Load(xmlPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load XML: {ex.Message}");
            return;
        }

        var configs = doc.Root?.Elements("BarcodeConfig");
        if (configs == null)
        {
            Console.WriteLine("No barcode configurations found.");
            return;
        }

        // --------------------------------------------------------------------
        // Iterate through each configuration and generate the barcode image
        // --------------------------------------------------------------------
        int index = 0;
        foreach (var cfg in configs)
        {
            index++;
            string symbologyName = cfg.Element("Symbology")?.Value?.Trim();
            string codeText = cfg.Element("CodeText")?.Value?.Trim();
            string xDimStr = cfg.Element("XDimension")?.Value?.Trim();

            // Validate required fields
            if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Config #{index} missing required fields. Skipping.");
                continue;
            }

            // Resolve symbology name to EncodeTypes enum via reflection
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology '{symbologyName}' in config #{index}. Skipping.");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create the barcode generator with the resolved symbology and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply optional XDimension if provided and parsable
                if (float.TryParse(xDimStr, out float xDimValue))
                {
                    generator.Parameters.Barcode.XDimension.Point = xDimValue;
                }

                // Explicitly set bar color to black (default, but shown for clarity)
                generator.Parameters.Barcode.BarColor = Color.Black;

                // Build output file path and save the image as PNG
                string outPath = Path.Combine(outputDir, $"barcode_{index}_{symbologyName}.png");
                generator.Save(outPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Generated barcode #{index}: {outPath}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}