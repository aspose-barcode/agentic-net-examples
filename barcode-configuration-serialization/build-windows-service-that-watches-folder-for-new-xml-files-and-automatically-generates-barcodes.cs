// Title: Generate Barcodes from XML Files in a Folder
// Description: Demonstrates creating temporary input/output directories, writing a sample XML that defines a barcode, reading each XML file, resolving the symbology via reflection, and generating a PNG barcode image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to programmatically create barcodes from data sources such as XML files. It highlights key API classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, and illustrates typical batch‑processing scenarios where developers need to convert structured data into visual barcode assets for printing or digital distribution.
// Prompt: Build a Windows service that watches a folder for new XML files and automatically generates barcodes.
// Tags: barcode, symbology, generation, xml, file-io, aspose.barcode, png, reflection

using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample console application that reads XML files describing barcodes,
/// generates corresponding barcode images, and saves them to an output folder.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates temporary folders, writes a sample XML,
    /// processes each XML file to generate a barcode image, and lists the results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: create dedicated temporary input and output folders
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "BarcodeInput_" + Guid.NewGuid().ToString("N"));
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // --------------------------------------------------------------------
        // Create a sample XML file that defines a barcode (symbology + code text)
        // --------------------------------------------------------------------
        string sampleXmlPath = Path.Combine(inputFolder, "sample1.xml");
        string sampleXmlContent =
            @"<Barcode>" +
            @"  <Symbology>Code128</Symbology>" +
            @"  <CodeText>ABC12345</CodeText>" +
            @"</Barcode>";
        File.WriteAllText(sampleXmlPath, sampleXmlContent, Encoding.UTF8);

        // --------------------------------------------------------------------
        // Process each XML file found in the input folder
        // --------------------------------------------------------------------
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        for (int i = 0; i < xmlFiles.Length; i++)
        {
            string xmlFile = xmlFiles[i];
            try
            {
                // Load XML document
                XDocument doc = XDocument.Load(xmlFile);
                XElement root = doc.Element("Barcode");
                if (root == null)
                {
                    Console.WriteLine($"Skipping file (invalid root): {Path.GetFileName(xmlFile)}");
                    continue;
                }

                // Extract required elements
                string symbologyName = root.Element("Symbology")?.Value?.Trim();
                string codeText = root.Element("CodeText")?.Value?.Trim();

                // Validate extracted data
                if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Skipping file (missing data): {Path.GetFileName(xmlFile)}");
                    continue;
                }

                // Resolve symbology name to BaseEncodeType using reflection
                FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symbologyName}' in file {Path.GetFileName(xmlFile)}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // ----------------------------------------------------------------
                // Generate barcode image using Aspose.BarCode
                // ----------------------------------------------------------------
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Basic appearance settings
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;
                    generator.Parameters.Resolution = 300f;

                    // Determine output file path and save as PNG
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlFile) + ".png";
                    string outputPath = Path.Combine(outputFolder, outputFileName);
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {Path.GetFileName(xmlFile)}: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // List all generated barcode image files
        // --------------------------------------------------------------------
        Console.WriteLine("Barcode generation completed. Output files:");
        string[] generatedFiles = Directory.GetFiles(outputFolder, "*.png");
        for (int i = 0; i < generatedFiles.Length; i++)
        {
            Console.WriteLine(generatedFiles[i]);
        }
    }
}