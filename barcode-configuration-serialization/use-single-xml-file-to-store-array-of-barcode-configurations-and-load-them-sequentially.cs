// Title: Generate Multiple Barcodes from XML Configuration
// Description: Loads barcode settings from a single XML file and generates corresponding barcode images sequentially.
// Prompt: Use a single XML file to store an array of barcode configurations and load them sequentially.
// Tags: barcode, symbology, xml, generation, aspose.barcode, image output

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates loading barcode configurations from an XML file and generating barcode images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads the XML, creates barcodes, and saves them as PNG files.
    /// </summary>
    static void Main()
    {
        // Path to the XML file that contains barcode configurations
        const string xmlPath = "barcodes.xml";

        // Verify that the XML file exists before proceeding
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {Path.GetFullPath(xmlPath)}");
            return;
        }

        // Load the XML document into memory
        XDocument doc = XDocument.Load(xmlPath);

        // Expect a root element <Barcodes> with multiple <Barcode> child elements
        var barcodeElements = doc.Root?.Elements("Barcode");
        if (barcodeElements == null)
        {
            Console.WriteLine("No <Barcode> elements found in the XML.");
            return;
        }

        // Ensure the output directory exists (creates it if missing)
        string outputDir = "GeneratedBarcodes";
        Directory.CreateDirectory(outputDir);

        int index = 1;
        // Iterate over each <Barcode> element and generate the corresponding image
        foreach (var elem in barcodeElements)
        {
            // Extract the symbology name (EncodeType) and the text to encode (CodeText)
            string symbologyName = (string)elem.Element("EncodeType");
            string codeText = (string)elem.Element("CodeText");

            // Validate required fields
            if (string.IsNullOrWhiteSpace(symbologyName) || string.IsNullOrWhiteSpace(codeText))
            {
                Console.WriteLine($"Skipping entry #{index}: missing EncodeType or CodeText.");
                index++;
                continue;
            }

            // Resolve the symbology name to a BaseEncodeType value using reflection
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology '{symbologyName}' in entry #{index}.");
                index++;
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create the barcode generator with the resolved type and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Set common visual parameters (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Resolution = 300; // DPI

                // Build the output file path and save the barcode as PNG
                string outputPath = Path.Combine(outputDir, $"barcode_{index}.png");
                generator.Save(outputPath);
                Console.WriteLine($"Generated barcode #{index}: {outputPath}");
            }

            index++;
        }
    }
}