// Title: Batch generate barcodes from XML configuration files
// Description: Demonstrates reading XML configuration files that define barcode symbology and code text, generating the corresponding barcodes, and saving each as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to programmatically create barcodes using the BarcodeGenerator class. Typical scenarios include batch processing of configuration data, automated report creation, and bulk image generation for inventory or tracking systems. Developers often need to map external data (e.g., XML, JSON, databases) to barcode symbologies and export them in common image formats such as TIFF, PNG, or JPEG.
// Prompt: Batch process a folder of XML configuration files, generating a barcode for each and saving as TIFF images.
// Tags: barcode, symbology, generation, batch, tiff, xml, aspose.barcode, barcodegenerator, encode-types

using System;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch processing of XML configuration files to generate barcodes and save them as TIFF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary input/output folders, seeds sample XML files,
    /// processes each file to generate a barcode, and lists the generated TIFF images.
    /// </summary>
    static void Main()
    {
        // ----------------------------------------------------------------------
        // Create unique temporary input and output folders for the demo
        // ----------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "BatchInput_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(Path.GetTempPath(), "BatchOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // ----------------------------------------------------------------------
        // Seed the input folder with sample XML configuration files
        // Each file contains <Symbology> and <CodeText> elements
        // ----------------------------------------------------------------------
        for (int i = 1; i <= 3; i++)
        {
            string xmlPath = Path.Combine(inputFolder, $"Config{i}.xml");
            var doc = new XDocument(
                new XElement("Barcode",
                    new XElement("Symbology", i == 1 ? "Code128" : i == 2 ? "QR" : "DataMatrix"),
                    new XElement("CodeText", $"Sample{i}Text")
                )
            );
            doc.Save(xmlPath);
        }

        // ----------------------------------------------------------------------
        // Process each XML file: read configuration, resolve symbology, generate barcode
        // ----------------------------------------------------------------------
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                // Load XML document and locate the <Barcode> root element
                XDocument doc = XDocument.Load(xmlFile);
                XElement root = doc.Element("Barcode");
                if (root == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlFile)}': missing <Barcode> root.");
                    continue;
                }

                // Extract symbology name and code text values
                string symbologyName = root.Element("Symbology")?.Value?.Trim();
                string codeText = root.Element("CodeText")?.Value?.Trim();

                // Validate required elements
                if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlFile)}': Symbology or CodeText is empty.");
                    continue;
                }

                // Resolve the symbology name to a BaseEncodeType enum value using reflection
                FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symbologyName}' in '{Path.GetFileName(xmlFile)}'.");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Generate the barcode and save it as a TIFF image
                string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(xmlFile) + ".tiff");
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Save(outputFile, BarCodeImageFormat.Tiff);
                }

                Console.WriteLine($"Generated '{Path.GetFileName(outputFile)}' from '{Path.GetFileName(xmlFile)}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(xmlFile)}': {ex.Message}");
            }
        }

        // ----------------------------------------------------------------------
        // List all generated TIFF files
        // ----------------------------------------------------------------------
        Console.WriteLine("Batch processing completed. Generated files:");
        foreach (string file in Directory.GetFiles(outputFolder, "*.tiff"))
        {
            Console.WriteLine(file);
        }
    }
}