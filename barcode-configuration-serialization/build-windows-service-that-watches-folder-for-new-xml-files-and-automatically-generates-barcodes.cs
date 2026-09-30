// Title: Generate Barcodes from XML Files in a Folder
// Description: Demonstrates creating temporary input/output directories, seeding sample XML files that specify barcode type and value, reading each XML, resolving the symbology via reflection, and generating PNG barcode images using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode batch‑processing category, showcasing how to work with EncodeTypes, BarcodeGenerator, and image export APIs. Typical scenarios include automated barcode creation from data files, bulk image generation, and integration into file‑based workflows. Developers often need to parse input data, map it to supported symbologies, and produce visual barcode assets for downstream systems.
// Prompt: Build a Windows service that watches a folder for new XML files and automatically generates barcodes.
// Tags: barcode, symbology, generation, png, xml, aspose.barcode, batch-processing

using System;
using System.IO;
using System.Xml.Linq;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Sample console application that reads XML definitions of barcodes from a folder
/// and generates corresponding PNG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates temporary folders, seeds sample XML files,
    /// processes each file to generate a barcode image, and lists the generated files.
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
        // Seed the input folder with sample XML files describing barcode type/value
        // --------------------------------------------------------------------
        for (int i = 1; i <= 3; i++)
        {
            string xmlContent = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<Barcode>
    <Type>{(i == 1 ? "Code128" : i == 2 ? "QR" : "DataMatrix")}</Type>
    <Value>Sample{i}123</Value>
</Barcode>";
            File.WriteAllText(Path.Combine(inputFolder, $"Sample{i}.xml"), xmlContent);
        }

        // --------------------------------------------------------------------
        // Process each XML file in the input folder
        // --------------------------------------------------------------------
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load XML and extract barcode type and value
                XDocument doc = XDocument.Load(xmlPath);
                string typeName = doc.Root.Element("Type")?.Value?.Trim();
                string codeText = doc.Root.Element("Value")?.Value?.Trim();

                // Validate required elements
                if (string.IsNullOrEmpty(typeName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlPath)}': missing Type or Value.");
                    continue;
                }

                // Resolve the EncodeTypes member that matches the type name via reflection
                FieldInfo field = typeof(EncodeTypes).GetField(typeName, BindingFlags.Public | BindingFlags.Static);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{typeName}' in '{Path.GetFileName(xmlPath)}'.");
                    continue;
                }
                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Generate the barcode image
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Set basic appearance: black bars on white background
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Determine output file path and save as PNG
                    string outputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(xmlPath) + ".png");
                    generator.Save(outputFile, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for '{Path.GetFileName(xmlPath)}' -> '{Path.GetFileName(outputFile)}'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(xmlPath)}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // List all generated barcode image files
        // --------------------------------------------------------------------
        Console.WriteLine("\nGenerated barcode files:");
        foreach (string file in Directory.GetFiles(outputFolder, "*.png"))
        {
            Console.WriteLine(file);
        }
    }
}