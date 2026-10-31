// Title: Generate barcodes from XML files in a watched folder
// Description: Demonstrates reading XML definitions of barcodes, generating images using Aspose.BarCode, and saving them to an output directory.
// Category-Description: This example belongs to the Aspose.BarCode file‑processing category, illustrating how to parse XML input, map symbology names to EncodeTypes via reflection, and create barcode images with the BarcodeGenerator class. Typical use cases include batch barcode creation, automated document processing, and integration with services that monitor file systems for new data.
// Prompt: Build a Windows service that watches a folder for new XML files and automatically generates barcodes.
// Tags: barcode symbology, xml processing, image output, aspose.barcode, barcodegenerator

using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample console application that reads XML files describing barcodes,
/// generates corresponding barcode images using Aspose.BarCode, and saves them
/// to an output folder. This logic can be adapted for a Windows service that
/// watches a directory for new XML files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates temporary input/output folders,
    /// writes a sample XML definition, processes each XML file, and generates
    /// barcode images.
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
        // Create a sample XML file that describes a barcode (symbology + data)
        // --------------------------------------------------------------------
        string sampleXmlPath = Path.Combine(inputFolder, "sample1.xml");
        XDocument sampleDoc = new XDocument(
            new XElement("Barcode",
                new XElement("Symbology", "Code128"),
                new XElement("CodeText", "ABC123456")
            )
        );
        sampleDoc.Save(sampleXmlPath);

        // --------------------------------------------------------------------
        // Retrieve all XML files from the input folder
        // --------------------------------------------------------------------
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        Console.WriteLine($"Found {xmlFiles.Length} XML file(s) in {inputFolder}");

        // --------------------------------------------------------------------
        // Process each XML file: parse, map symbology, generate and save barcode
        // --------------------------------------------------------------------
        for (int i = 0; i < xmlFiles.Length; i++)
        {
            string xmlPath = xmlFiles[i];
            try
            {
                // Load XML document
                XDocument doc = XDocument.Load(xmlPath);
                XElement root = doc.Root;
                if (root == null)
                {
                    Console.WriteLine($"Skipping file (no root element): {Path.GetFileName(xmlPath)}");
                    continue;
                }

                // Extract required elements
                string symbologyName = root.Element("Symbology")?.Value?.Trim();
                string codeText = root.Element("CodeText")?.Value?.Trim();

                // Validate extracted data
                if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Skipping file (missing data): {Path.GetFileName(xmlPath)}");
                    continue;
                }

                // Resolve symbology name to BaseEncodeType using reflection
                FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symbologyName}' in file {Path.GetFileName(xmlPath)}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
                if (encodeType == null)
                {
                    Console.WriteLine($"Failed to obtain encode type for '{symbologyName}'");
                    continue;
                }

                // ----------------------------------------------------------------
                // Generate barcode image with basic appearance settings
                // ----------------------------------------------------------------
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    generator.Parameters.Barcode.XDimension.Point = 2f;   // narrow bar width
                    generator.Parameters.Barcode.BarHeight.Point = 30f; // bar height
                    generator.Parameters.Resolution = 300f;              // image DPI

                    // Determine output file path and save as PNG
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                    string outputPath = Path.Combine(outputFolder, outputFileName);
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode saved to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {Path.GetFileName(xmlPath)}: {ex.Message}");
            }
        }

        Console.WriteLine("Processing complete.");
    }
}