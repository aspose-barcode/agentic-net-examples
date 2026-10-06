// Title: Generate barcode images from XML files using a command‑line tool
// Description: Demonstrates reading barcode definition XML files from a directory and creating PNG images with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to import barcode settings from XML (BarcodeGenerator.ImportFromXml), generate barcodes, and save them in common image formats. Developers often need to batch‑process barcode definitions stored as XML for automated workflows, reporting, or integration with other systems. The key API classes used are BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and the ExportToXml/ImportFromXml methods.
// Prompt: Create a command‑line tool that reads a directory of XML files and generates corresponding barcode images.
// Tags: barcode, code128, xml, generation, png, aspose.barcode, barcodegenerator, command-line

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Command‑line utility that converts barcode definition XML files into PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts optional input and output directory arguments, creates sample XML if none exist, and generates barcode images.
    /// </summary>
    /// <param name="args">[0] Input directory (optional), [1] Output directory (optional).</param>
    static void Main(string[] args)
    {
        // Determine input and output directories (use provided arguments or temporary folders)
        string inputDir = args.Length > 0
            ? args[0]
            : Path.Combine(Path.GetTempPath(), "BarcodesXml_" + Guid.NewGuid().ToString("N"));
        string outputDir = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetTempPath(), "BarcodesImg_" + Guid.NewGuid().ToString("N"));

        // Ensure the input directory exists
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If no XML files are present, create a sample barcode definition and export it to XML
        string[] existingXml = Directory.GetFiles(inputDir, "*.xml");
        if (existingXml.Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputDir, "sample1.xml");
            using (var gen = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                gen.ExportToXml(sampleXmlPath);
            }
            existingXml = new[] { sampleXmlPath };
        }

        // Process each XML file: import settings, generate barcode, and save as PNG
        foreach (string xmlFile in existingXml)
        {
            try
            {
                using (var gen = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlFile) + ".png";
                    string outputPath = Path.Combine(outputDir, outputFileName);
                    gen.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode image: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{xmlFile}': {ex.Message}");
            }
        }
    }
}