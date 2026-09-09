// Title: Command‑line XML to Barcode Image Converter
// Description: Demonstrates a console application that reads barcode definition XML files from a directory and generates PNG barcode images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to import barcode settings from XML (via BarcodeGenerator.ImportFromXml) and save the resulting images. Typical use cases include batch processing of barcode definitions, automated image creation for inventory systems, and integration into CI pipelines. Developers often need to work with EncodeTypes, BarCodeImageFormat, and file‑system utilities to streamline barcode production.
// Prompt: Create a command‑line tool that reads a directory of XML files and generates corresponding barcode images.
// Tags: barcode symbology, generation, xml import, png output, aspose.barcode, console, file-io

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the command‑line tool that converts barcode definition XML files into PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Main method processes command‑line arguments, prepares input/output folders,
    /// creates a sample XML if none exist, and generates barcode images for each XML file.
    /// </summary>
    /// <param name="args">
    /// args[0] – optional input directory path containing XML files.
    /// args[1] – optional output directory path for generated PNG images.
    /// </param>
    static void Main(string[] args)
    {
        // Determine input and output directories, using temporary folders when not supplied.
        string inputDir;
        string outputDir;

        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            inputDir = args[0];
        }
        else
        {
            inputDir = Path.Combine(Path.GetTempPath(), "BarcodesInput_" + Guid.NewGuid().ToString("N"));
        }

        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputDir = args[1];
        }
        else
        {
            outputDir = Path.Combine(Path.GetTempPath(), "BarcodesOutput_" + Guid.NewGuid().ToString("N"));
        }

        // Ensure the input and output directories exist.
        if (!Directory.Exists(inputDir))
        {
            Directory.CreateDirectory(inputDir);
        }

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // If the input directory is empty, create a sample XML file to demonstrate functionality.
        string[] existingXmlFiles = Directory.GetFiles(inputDir, "*.xml");
        if (existingXmlFiles.Length == 0)
        {
            string sampleXmlPath = Path.Combine(inputDir, "SampleBarcode.xml");
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.ExportToXml(sampleXmlPath);
            }
            existingXmlFiles = new string[] { sampleXmlPath };
            Console.WriteLine($"Created sample XML at: {sampleXmlPath}");
        }

        // Retrieve all XML files from the input directory for processing.
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        List<string> processedFiles = new List<string>();

        // Iterate through each XML file, import its settings, and save the barcode image.
        for (int i = 0; i < xmlFiles.Length; i++)
        {
            string xmlPath = xmlFiles[i];
            try
            {
                using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string outputPath = Path.Combine(outputDir, fileNameWithoutExt + ".png");
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    processedFiles.Add(outputPath);
                    Console.WriteLine($"Generated barcode image: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to process '{xmlPath}': {ex.Message}");
            }
        }

        // Summarize the processing results.
        Console.WriteLine($"Processing complete. {processedFiles.Count} image(s) generated in '{outputDir}'.");
    }
}