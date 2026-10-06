// Title: Batch generate barcodes from XML configuration files
// Description: Demonstrates how to read a set of XML files that define barcode parameters, generate each barcode, and save the results as TIFF images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating bulk processing of configuration data. It uses the BarcodeGenerator class together with EncodeTypes to create barcodes of various symbologies. Typical use cases include automated creation of product labels, inventory tags, or any scenario where barcode data is defined externally in XML files.
// Prompt: Batch process a folder of XML configuration files, generating a barcode for each and saving as TIFF images.
// Tags: barcode, symbology, batch processing, tiff, aspose.barcode, xml, generation

using System;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that reads XML configuration files, generates corresponding barcodes,
/// and saves them as TIFF images in an output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates sample XML files, processes each file,
    /// generates a barcode based on the defined symbology and code text, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create dedicated temporary input and output folders for the demo.
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "BarcodesInput_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodesOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Seed the input folder with sample XML configuration files.
        // Each file contains a <Barcode> root with <Symbology> and <CodeText> elements.
        // --------------------------------------------------------------------
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"Config{i}.xml");
            var doc = new XDocument(
                new XElement("Barcode",
                    new XElement("Symbology", "Code128"),
                    new XElement("CodeText", $"Sample{i}123")
                )
            );
            doc.Save(filePath);
        }

        // --------------------------------------------------------------------
        // Process each XML file in the input folder.
        // --------------------------------------------------------------------
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                // Load the XML document and locate the <Barcode> root element.
                XDocument doc = XDocument.Load(xmlFile);
                XElement root = doc.Element("Barcode");
                if (root == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlFile)}': missing <Barcode> root.");
                    continue;
                }

                // Extract symbology name and code text values.
                string symbologyName = root.Element("Symbology")?.Value?.Trim();
                string codeText = root.Element("CodeText")?.Value?.Trim();

                // Validate required elements.
                if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlFile)}': missing Symbology or CodeText.");
                    continue;
                }

                // Resolve the symbology name to a BaseEncodeType enum value using reflection.
                FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symbologyName}' in '{Path.GetFileName(xmlFile)}'.");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Generate the barcode and save it as a TIFF image.
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(xmlFile) + ".tiff");
                    generator.Save(outputPath, BarCodeImageFormat.Tiff);
                    Console.WriteLine($"Generated '{Path.GetFileName(outputPath)}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(xmlFile)}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Report completion and display the locations of the input and output folders.
        // --------------------------------------------------------------------
        Console.WriteLine("Batch processing completed.");
        Console.WriteLine($"Input folder: {inputFolder}");
        Console.WriteLine($"Output folder: {outputFolder}");
    }
}