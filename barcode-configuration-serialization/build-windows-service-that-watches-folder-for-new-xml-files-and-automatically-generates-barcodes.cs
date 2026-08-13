// Title: Windows Service Example – Generate Barcodes from XML Files
// Description: Demonstrates watching a folder for XML files and creating barcode images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode file‑processing and barcode generation category. It shows how to read XML input, map symbology names to EncodeTypes, and generate PNG images with BarcodeGenerator. Developers often need to automate barcode creation from data files, integrate with services, or batch‑process documents, and this snippet illustrates the core API usage for such scenarios.
// Prompt: Build a Windows service that watches a folder for new XML files and automatically generates barcodes.
// Tags: barcode, symbology, generation, png, aspose.barcode, barcodegenerator, encode types

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates a simple console‑style implementation that could be adapted into a Windows Service to monitor a folder,
/// read XML definitions, and generate barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Scans the Input folder for XML files, creates barcodes per definition, and saves PNG files to Output.
    /// </summary>
    static void Main()
    {
        // Define input and output directories relative to the current working directory.
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Input");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        // Ensure the directories exist.
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // Retrieve all XML files in the input folder.
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files found in the input folder.");
            return;
        }

        // Process each XML file individually.
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load the XML document.
                XDocument doc = XDocument.Load(xmlPath);
                XElement root = doc.Root;
                if (root == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlPath)}': Empty XML.");
                    continue;
                }

                // Expected XML format:
                // <Barcode>
                //   <Symbology>Code128</Symbology>
                //   <Value>123456</Value>
                // </Barcode>
                string symbologyName = root.Element("Symbology")?.Value?.Trim();
                string codeText = root.Element("Value")?.Value?.Trim();

                // Validate required elements.
                if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlPath)}': Missing Symbology or Value.");
                    continue;
                }

                // Resolve symbology name to BaseEncodeType using reflection.
                var field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlPath)}': Unknown symbology '{symbologyName}'.");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
                if (encodeType == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlPath)}': Failed to obtain encode type.");
                    continue;
                }

                // Create the barcode generator and configure optional parameters.
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Example of setting a simple parameter (optional).
                    generator.Parameters.Barcode.XDimension.Point = 2f; // module size

                    // Build the output file path.
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".png";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the generated barcode image.
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode for '{Path.GetFileName(xmlPath)}' -> '{outputFileName}'.");
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected errors for the current file.
                Console.WriteLine($"Error processing '{Path.GetFileName(xmlPath)}': {ex.Message}");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}