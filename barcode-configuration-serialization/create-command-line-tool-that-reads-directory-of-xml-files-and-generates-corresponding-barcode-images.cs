// Title: Generate barcode images from XML files using a command‑line tool
// Description: Demonstrates how to read a directory of XML files that define barcodes and produce PNG images for each using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and import category, showcasing the BarcodeGenerator class for importing barcode settings from XML (ImportFromXml) and exporting images (Save). Typical use cases include batch processing of pre‑configured barcode definitions stored as XML, useful for automated reporting, inventory labeling, or integration pipelines. Developers often need to read configuration files, generate barcodes, and output them in common image formats.
// Prompt: Create a command‑line tool that reads a directory of XML files and generates corresponding barcode images.
// Tags: barcode, code128, xml, png, aspose.barcode, generation, import, command-line

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
    /// Entry point. Accepts an optional directory path argument; if omitted, creates a sample XML file for demonstration.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the input directory.</param>
    static void Main(string[] args)
    {
        string inputDirectory;

        // Determine input directory: use argument if valid, otherwise create a temporary sample directory
        if (args.Length > 0 && Directory.Exists(args[0]))
        {
            inputDirectory = args[0];
        }
        else
        {
            // Create a temporary directory with a sample XML file for demonstration purposes
            inputDirectory = Path.Combine(Path.GetTempPath(), "BarcodeXml_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(inputDirectory);

            string sampleXmlPath = Path.Combine(inputDirectory, "sample.xml");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Set barcode visual property
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Export the barcode configuration to XML
                generator.ExportToXml(sampleXmlPath);
            }

            Console.WriteLine($"No valid input directory provided. Created sample directory: {inputDirectory}");
        }

        // Retrieve all XML files in the specified directory
        string[] xmlFiles = Directory.GetFiles(inputDirectory, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found in the specified directory.");
            return;
        }

        // Process each XML file: import settings and generate a PNG image
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                using (var generator = BarcodeGenerator.ImportFromXml(xmlFile))
                {
                    string outputImagePath = Path.ChangeExtension(xmlFile, ".png");
                    // Save the generated barcode as a PNG image
                    generator.Save(outputImagePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode image: {outputImagePath}");
                }
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing a file
                Console.WriteLine($"Error processing '{xmlFile}': {ex.Message}");
            }
        }
    }
}