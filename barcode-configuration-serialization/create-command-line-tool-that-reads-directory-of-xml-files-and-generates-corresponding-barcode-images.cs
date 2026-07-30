// Title: Command‑line XML‑to‑Barcode image generator
// Description: Reads barcode configuration XML files from a directory and creates PNG barcode images using Aspose.BarCode.
// Category-Description: Demonstrates Aspose.BarCode generation workflow where barcode settings are stored in XML. Shows how to import settings with BarcodeGenerator.ImportFromXml, generate images, and handle batch processing. Useful for developers automating barcode creation from configuration files or integrating barcode generation into CI pipelines.
// Prompt: Create a command‑line tool that reads a directory of XML files and generates corresponding barcode images.
// Tags: barcode symbology, generation, png, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Command‑line utility that converts barcode definition XML files into PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional input and output directory arguments, processes each XML file,
    /// and generates a corresponding barcode image.
    /// </summary>
    /// <param name="args">
    /// args[0] – input directory (default: "InputXml").
    /// args[1] – output directory (default: "OutputImages").
    /// </param>
    static void Main(string[] args)
    {
        // Determine input and output directories (fallback to defaults)
        string inputDir = args.Length > 0 ? args[0] : "InputXml";
        string outputDir = args.Length > 1 ? args[1] : "OutputImages";

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // If input directory does not exist, create it and generate a sample XML file
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
            string sampleXmlPath = Path.Combine(inputDir, "sample.xml");
            using (var sampleGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Export generator settings to XML
                sampleGenerator.ExportToXml(sampleXmlPath);
                Console.WriteLine($"Created sample XML: {sampleXmlPath}");
            }
        }

        // Get all XML files in the input directory
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        // Process each XML file and generate a PNG barcode image
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load barcode generator settings from XML
                using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Determine output image path (same name, .png extension)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string outputPath = Path.Combine(outputDir, fileNameWithoutExt + ".png");

                    // Save barcode image as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode image: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }
    }
}