// Title: Generate Barcodes from XML Files via Command‑Line
// Description: Demonstrates a console utility that scans a folder of XML definitions and creates corresponding barcode images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to read external data (XML) and produce barcode graphics. It showcases the BarcodeGenerator, EncodeTypes, and image‑format APIs, typical for batch processing, automated labeling, and integration pipelines. Developers often need to convert structured data into visual codes for inventory, shipping, or authentication scenarios.
// Prompt: Create a command‑line tool that reads a directory of XML files and generates corresponding barcode images.
// Tags: barcode, symbology, generation, png, xml, command-line, aspose.barcode, barcodegenerator, encode-types

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Command‑line utility that reads XML files describing barcodes and generates PNG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional input and output folder arguments, processes each XML file, and writes PNG barcode images.
    /// </summary>
    /// <param name="args">[0] optional input directory, [1] optional output directory.</param>
    static void Main(string[] args)
    {
        // Determine input and output directories (fallback to defaults)
        string inputDir = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
            ? args[0]
            : Path.Combine(Path.GetTempPath(), "BarcodeXmlInput_" + Guid.NewGuid().ToString("N"));
        string outputDir = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
            ? args[1]
            : Path.Combine(Directory.GetCurrentDirectory(), "BarcodeImages");

        // Ensure input directory exists; if not, create it and add sample XML files
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
            CreateSampleXml(Path.Combine(inputDir, "SampleQR.xml"), "QR", "HelloWorld");
            CreateSampleXml(Path.Combine(inputDir, "SampleCode128.xml"), "Code128", "1234567890");
        }

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Get all XML files in the input directory
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found in the input directory.");
            return;
        }

        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load XML and extract required elements
                XDocument doc = XDocument.Load(xmlPath);
                XElement root = doc.Root;
                if (root == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlPath)}': missing root element.");
                    continue;
                }

                string symbologyName = (string)root.Element("Symbology");
                string codeText = (string)root.Element("CodeText");

                if (string.IsNullOrWhiteSpace(symbologyName) || string.IsNullOrWhiteSpace(codeText))
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlPath)}': Symbology or CodeText is empty.");
                    continue;
                }

                // Resolve symbology name to BaseEncodeType via reflection
                var fieldInfo = typeof(EncodeTypes).GetField(symbologyName);
                if (fieldInfo == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlPath)}': unknown symbology '{symbologyName}'.");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)fieldInfo.GetValue(null);

                // Create barcode generator
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Optional visual settings
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                    generator.Parameters.Barcode.XDimension.Point = 2f; // module size

                    // Determine output image path
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                    string outputPath = Path.Combine(outputDir, outputFileName);

                    // Save barcode image as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode '{outputFileName}' from '{Path.GetFileName(xmlPath)}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(xmlPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }

    // Helper to create a simple sample XML file
    private static void CreateSampleXml(string filePath, string symbology, string codeText)
    {
        var doc = new XDocument(
            new XElement("Barcode",
                new XElement("Symbology", symbology),
                new XElement("CodeText", codeText)
            )
        );

        using (var writer = new StreamWriter(filePath, false))
        {
            doc.Save(writer);
        }
    }
}