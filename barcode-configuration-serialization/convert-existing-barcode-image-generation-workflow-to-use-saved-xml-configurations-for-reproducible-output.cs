// Title: Generate Barcode Image and Reproduce via XML Configuration
// Description: Demonstrates creating a barcode image, exporting its generation settings to an XML file, and then recreating the same barcode from that XML configuration.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration management category. It showcases the use of BarcodeGenerator for creating barcodes, exporting its parameters to XML with ExportToXml, and importing them back with ImportFromXml. Developers often need reproducible barcode output across environments or sessions, and this pattern provides a reliable way to store and reuse generation settings.
// Prompt: Convert an existing barcode image generation workflow to use saved XML configurations for reproducible output.
// Tags: barcode symbology, image generation, xml configuration, reproducible output, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode creation, exporting its settings to XML, and regenerating the barcode from that XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves its configuration, and recreates the barcode from the saved XML.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: define output directory and file paths
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo");
        Directory.CreateDirectory(outputDir);

        string imagePath1 = Path.Combine(outputDir, "barcode1.png");
        string xmlConfigPath = Path.Combine(outputDir, "barcodeConfig.xml");
        string imagePath2 = Path.Combine(outputDir, "barcode_from_xml.png");

        // --------------------------------------------------------------------
        // First generation: create barcode and export its configuration to XML
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Apply custom visual settings
            generator.Parameters.Barcode.BarColor = Color.Green;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the generated barcode image
            generator.Save(imagePath1, BarCodeImageFormat.Png);

            // Export the current generator settings to an XML file for later reuse
            generator.ExportToXml(xmlConfigPath);
        }

        // --------------------------------------------------------------------
        // Verify that the XML configuration file was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(xmlConfigPath))
        {
            Console.WriteLine("Failed to create XML configuration.");
            return;
        }

        // --------------------------------------------------------------------
        // Second generation: import configuration from XML and generate barcode
        // --------------------------------------------------------------------
        using (var generatorFromXml = BarcodeGenerator.ImportFromXml(xmlConfigPath))
        {
            // Optional: modify imported settings here if needed
            // generatorFromXml.Parameters.Barcode.BarHeight.Point = 30f;

            // Save the barcode image generated from the imported configuration
            generatorFromXml.Save(imagePath2, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Output the locations of the generated files
        // --------------------------------------------------------------------
        Console.WriteLine($"Original barcode saved to: {imagePath1}");
        Console.WriteLine($"XML configuration saved to: {xmlConfigPath}");
        Console.WriteLine($"Barcode generated from XML saved to: {imagePath2}");
    }
}