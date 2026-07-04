// Title: Generate barcodes from XML files in a folder
// Description: Demonstrates a console app that scans a directory for XML definitions and creates PNG barcodes using Aspose.BarCode.
// Prompt: Build a Windows service that watches a folder for new XML files and automatically generates barcodes.
// Tags: barcode symbology, generation, png, aspose.barcode, xml, file-io

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Scans the InputBarcodes folder for XML files, reads barcode specifications,
    /// generates corresponding PNG images, and saves them to the OutputBarcodes folder.
    /// </summary>
    static void Main()
    {
        // Define input and output directories (relative to the executable location)
        string inputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "InputBarcodes");
        string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OutputBarcodes");

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Verify the input directory exists
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Get all XML files in the input folder
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found to process.");
            return;
        }

        // Process each XML file individually
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load the XML document
                XDocument doc = XDocument.Load(xmlPath);
                XElement barcodeElement = doc.Root?.Element("Barcode");
                if (barcodeElement == null)
                {
                    Console.WriteLine($"Invalid format in file: {Path.GetFileName(xmlPath)}");
                    continue;
                }

                // Extract symbology name and code text
                string symbologyName = barcodeElement.Element("Symbology")?.Value?.Trim();
                string codeText = barcodeElement.Element("CodeText")?.Value?.Trim();

                // Validate required elements
                if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Missing Symbology or CodeText in file: {Path.GetFileName(xmlPath)}");
                    continue;
                }

                // Resolve symbology name to BaseEncodeType using reflection
                var field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symbologyName}' in file: {Path.GetFileName(xmlPath)}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Prepare output file path (same name with .png extension)
                string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                string outputPath = Path.Combine(outputFolder, outputFileName);

                // Generate and save the barcode
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated barcode for '{Path.GetFileName(xmlPath)}' -> {outputFileName}");
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors gracefully
                Console.WriteLine($"Error processing file '{Path.GetFileName(xmlPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}