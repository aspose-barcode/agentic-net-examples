// Title: Batch generate barcodes from XML configurations and save as TIFF
// Description: The example reads XML files that specify barcode symbology and data, creates a barcode for each, and writes the result as a TIFF image.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and related parameter settings for bulk barcode creation. Typical use cases include processing configuration files, automating label production, or converting data definitions into visual barcodes. Developers often need to read external definitions, resolve symbology via reflection, and output high‑resolution images for printing or archival.
// Prompt: Batch process a folder of XML configuration files, generating a barcode for each and saving as TIFF images.
// Tags: barcode, symbology, batch processing, tiff, aspose.barcode, xml, generation

using System;
using System.IO;
using System.Xml;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch processing of XML configuration files to generate barcodes and save them as TIFF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Reads input and output folder paths, processes each XML file, and creates corresponding barcode images.
    /// </summary>
    /// <param name="args">Optional command‑line arguments: [0] input folder, [1] output folder.</param>
    static void Main(string[] args)
    {
        // Determine input folder containing XML configuration files
        string inputFolder = args.Length > 0 ? args[0] : "BarcodesConfig";

        // Determine output folder for generated TIFF images
        string outputFolder = args.Length > 1 ? args[1] : "BarcodesOutput";

        // Ensure the input folder exists; create it if missing
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            Console.WriteLine($"Created input folder: {Path.GetFullPath(inputFolder)}");
        }

        // Ensure the output folder exists; create it if missing
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            Console.WriteLine($"Created output folder: {Path.GetFullPath(outputFolder)}");
        }

        // Iterate over each XML file in the input folder
        foreach (string xmlPath in Directory.GetFiles(inputFolder, "*.xml"))
        {
            try
            {
                // Load the XML configuration document
                var doc = new XmlDocument();
                doc.Load(xmlPath);

                // Expected XML elements: <Symbology> and <CodeText>
                XmlNode symNode = doc.SelectSingleNode("//Symbology");
                XmlNode textNode = doc.SelectSingleNode("//CodeText");

                // Validate required elements are present
                if (symNode == null || textNode == null)
                {
                    Console.WriteLine($"Skipping '{xmlPath}': missing Symbology or CodeText element.");
                    continue;
                }

                // Extract symbology name and code text, trimming whitespace
                string symName = symNode.InnerText.Trim();
                string codeText = textNode.InnerText.Trim();

                // Resolve symbology name to an EncodeTypes field using reflection
                FieldInfo field = typeof(EncodeTypes).GetField(symName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symName}' in file '{xmlPath}'.");
                    continue;
                }

                // Cast the resolved field value to BaseEncodeType
                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Create a barcode generator with the resolved type and code text
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Set a higher resolution for better quality TIFF output
                    generator.Parameters.Resolution = 300;

                    // Build output file name (same as XML but with .tif extension)
                    string outputFileName = Path.GetFileNameWithoutExtension(xmlPath) + ".tif";
                    string outputPath = Path.Combine(outputFolder, outputFileName);

                    // Save the generated barcode as a TIFF image
                    generator.Save(outputPath, BarCodeImageFormat.Tiff);
                    Console.WriteLine($"Generated barcode: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered while processing the current XML file
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }
        }

        // Program completes without waiting for input
    }
}