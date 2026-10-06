// Title: Generate Barcodes from XML Files Using Aspose.BarCode
// Description: Demonstrates reading XML files that specify barcode symbology and data, then creating PNG barcode images with Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to parse input data, resolve symbology via EncodeTypes, and produce barcode images. It showcases the BarcodeGenerator class, EncodeTypes, and image saving options—common tasks for developers automating barcode creation in batch or service scenarios.
// Prompt: Build a Windows service that watches a folder for new XML files and automatically generates barcodes.
// Tags: barcode, symbology, generation, png, xml, aspose.barcode, aspose.barcode.generation, encode-types

using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates processing XML files to generate barcode images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary folders, writes a sample XML, processes each XML file,
    /// generates a barcode image, and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: create temporary input and output directories for the demo.
        // --------------------------------------------------------------------
        string inputDir = Path.Combine(Path.GetTempPath(), "BarcodeInput_" + Guid.NewGuid().ToString("N"));
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeOutput_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------------
        // Create a sample XML file that defines the barcode to generate.
        // --------------------------------------------------------------------
        string sampleXmlPath = Path.Combine(inputDir, "sample1.xml");
        string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Barcode>
    <Symbology>Code128</Symbology>
    <CodeText>ABC12345</CodeText>
</Barcode>";
        File.WriteAllText(sampleXmlPath, xmlContent, Encoding.UTF8);

        // --------------------------------------------------------------------
        // Process each XML file found in the input directory.
        // --------------------------------------------------------------------
        string[] xmlFiles = Directory.GetFiles(inputDir, "*.xml");
        foreach (string xmlFile in xmlFiles)
        {
            try
            {
                // Load the XML document and extract required elements.
                var doc = new XmlDocument();
                doc.Load(xmlFile);
                var symNode = doc.SelectSingleNode("//Symbology");
                var textNode = doc.SelectSingleNode("//CodeText");
                if (symNode == null || textNode == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlFile)}': missing required elements.");
                    continue;
                }

                string symbologyName = symNode.InnerText.Trim();
                string codeText = textNode.InnerText.Trim();

                // Resolve the symbology name to a BaseEncodeType instance using reflection.
                BaseEncodeType encodeType = ResolveEncodeType(symbologyName);
                if (encodeType == null)
                {
                    Console.WriteLine($"Skipping '{Path.GetFileName(xmlFile)}': unknown symbology '{symbologyName}'.");
                    continue;
                }

                // ----------------------------------------------------------------
                // Generate the barcode image and save it as PNG.
                // ----------------------------------------------------------------
                string outputFileName = Path.GetFileNameWithoutExtension(xmlFile) + ".png";
                string outputPath = Path.Combine(outputDir, outputFileName);
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Configure appearance parameters (optional).
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.BarHeight.Point = 30f;
                    generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;
                    generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;
                    generator.Parameters.Barcode.BarColor = Color.Black;
                    generator.Parameters.BackColor = Color.White;

                    // Save the generated barcode to the output path.
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode for '{Path.GetFileName(xmlFile)}' -> '{outputFileName}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{Path.GetFileName(xmlFile)}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Cleanup: optionally delete temporary folders (commented out to allow inspection).
        // --------------------------------------------------------------------
        // Directory.Delete(inputDir, true);
        // Directory.Delete(outputDir, true);
    }

    /// <summary>
    /// Resolves a symbology name to the corresponding <see cref="BaseEncodeType"/> using reflection.
    /// Returns null if the symbology is not found.
    /// </summary>
    /// <param name="symbologyName">The name of the barcode symbology (e.g., "Code128").</param>
    /// <returns>The matching <see cref="BaseEncodeType"/> or null.</returns>
    static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        // Look for a public static field in EncodeTypes that matches the provided name.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
        if (field == null)
            return null;

        return field.GetValue(null) as BaseEncodeType;
    }
}