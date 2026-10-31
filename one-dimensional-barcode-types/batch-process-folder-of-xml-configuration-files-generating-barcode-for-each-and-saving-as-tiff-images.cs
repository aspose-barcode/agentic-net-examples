// Title: Batch generate barcodes from XML configuration files
// Description: Demonstrates how to read multiple XML files that define barcode symbology and data, generate the corresponding barcodes, and save each as a TIFF image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical scenarios include bulk barcode creation from configuration files, automated document processing, and integration with legacy systems that store barcode definitions in XML. Developers often need to parse configuration data, resolve symbology via reflection, and output images in various formats.
// Prompt: Batch process a folder of XML configuration files, generating a barcode for each and saving as TIFF images.
// Tags: barcode, symbology, batch, tiff, xml, generation, aspose.barcode, encode-types

using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a console application that reads XML configuration files,
/// generates barcodes according to the specified symbology and text,
/// and saves each barcode as a TIFF image in an output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates sample XML files, processes each file,
    /// and generates corresponding TIFF barcode images.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: create temporary input and output directories for the demo.
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlInput_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeTiffOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Generate sample XML configuration files.
        // Each file contains <EncodeType> and <CodeText> elements.
        // --------------------------------------------------------------------
        List<string> xmlFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"config{i}.xml");
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("<Barcode>");
                writer.WriteLine($"  <EncodeType>{(i == 1 ? "Code128" : i == 2 ? "QR" : "DataMatrix")}</EncodeType>");
                writer.WriteLine($"  <CodeText>Sample{i}123</CodeText>");
                writer.WriteLine("</Barcode>");
            }
            xmlFiles.Add(filePath);
        }

        // --------------------------------------------------------------------
        // Process each XML file: read configuration, resolve symbology,
        // generate barcode, and save as TIFF.
        // --------------------------------------------------------------------
        foreach (string xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"File not found: {xmlPath}");
                continue;
            }

            string encodeTypeName = null;
            string codeText = null;

            // Load XML and extract EncodeType and CodeText values.
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);
                XmlNode encodeNode = doc.SelectSingleNode("//EncodeType");
                XmlNode textNode = doc.SelectSingleNode("//CodeText");
                if (encodeNode != null) encodeTypeName = encodeNode.InnerText.Trim();
                if (textNode != null) codeText = textNode.InnerText.Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading XML '{xmlPath}': {ex.Message}");
                continue;
            }

            // Validate extracted values.
            if (string.IsNullOrEmpty(encodeTypeName) || string.IsNullOrEmpty(codeText))
            {
                Console.WriteLine($"Invalid configuration in '{xmlPath}'.");
                continue;
            }

            // Resolve the EncodeTypes enum member using reflection.
            var field = typeof(EncodeTypes).GetField(encodeTypeName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology '{encodeTypeName}' in '{xmlPath}'.");
                continue;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(xmlPath) + ".tiff");

            // Generate the barcode and save it as a TIFF image.
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Save(outputFile, BarCodeImageFormat.Tiff);
                }
                Console.WriteLine($"Generated TIFF: {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate barcode for '{xmlPath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}