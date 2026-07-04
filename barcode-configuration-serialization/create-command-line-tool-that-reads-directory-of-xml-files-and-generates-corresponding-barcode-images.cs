// Title: Generate Barcode Images from XML Definitions
// Description: This example reads XML files that define barcode settings and creates PNG images for each, storing them in a subfolder.
// Prompt: Create a command‑line tool that reads a directory of XML files and generates corresponding barcode images.
// Tags: barcode, xml, generation, png, command-line, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Command‑line utility that imports barcode configurations from XML files
/// and generates corresponding PNG images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts an optional directory path argument; processes up to five XML files
    /// and creates barcode images in a subfolder named "GeneratedImages".
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may specify the input directory.</param>
    static void Main(string[] args)
    {
        // Determine the input directory: use first argument or fallback to a sample folder.
        string inputDir = args.Length > 0 ? args[0] : "BarcodesXml";

        // Verify that the input directory exists.
        if (!Directory.Exists(inputDir))
        {
            Console.WriteLine($"Input directory does not exist: {inputDir}");
            return;
        }

        // Prepare output directory inside the input folder.
        string outputDir = Path.Combine(inputDir, "GeneratedImages");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Retrieve all XML files in the input directory (limit to 5 for safe batch processing).
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        int maxFiles = Math.Min(5, xmlFiles.Length);

        // Process each selected XML file.
        for (int i = 0; i < maxFiles; i++)
        {
            string xmlPath = xmlFiles[i];
            try
            {
                // Import barcode settings from the XML file.
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Determine output image path (same name with .png extension).
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string outputPath = Path.Combine(outputDir, fileNameWithoutExt + ".png");

                    // Save the generated barcode image.
                    generator.Save(outputPath);
                    Console.WriteLine($"Generated barcode image: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log the error and continue with the next file.
                Console.WriteLine($"Failed to process '{xmlPath}': {ex.Message}");
            }
        }
    }
}