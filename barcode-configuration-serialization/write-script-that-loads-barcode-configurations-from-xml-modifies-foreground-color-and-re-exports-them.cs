// Title: Load and modify barcode configuration XML
// Description: Demonstrates loading a barcode configuration from an XML file, changing the bar color, and exporting the updated configuration.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showing how to use BarcodeGenerator to export and import settings via XML. It covers key classes like BarcodeGenerator, EncodeTypes, and the Parameters property for customizing barcode appearance. Developers often need to persist barcode settings, adjust them programmatically, and re‑use them across applications, making this pattern useful for batch processing or dynamic styling.
// Prompt: Write a script that loads barcode configurations from XML, modifies the foreground color, and re‑exports them.
// Tags: barcode, xml, configuration, color, export, import, aspose.barcode, code128, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, exports its configuration to XML,
/// imports the configuration, changes the bar color, and re‑exports the modified XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the XML export/import workflow.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the XML files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and modified configuration XML files
        string originalXmlPath = Path.Combine(tempFolder, "original.xml");
        string modifiedXmlPath = Path.Combine(tempFolder, "modified.xml");

        // Step 1: Generate a sample barcode and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Optional: set an initial bar (foreground) color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Export the current configuration to the original XML file
            generator.ExportToXml(originalXmlPath);
        }

        // Step 2: Import the configuration, modify the foreground (bar) color, and re‑export
        using (var generator = BarcodeGenerator.ImportFromXml(originalXmlPath))
        {
            // Change the bar color to blue
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Export the modified configuration to a new XML file
            generator.ExportToXml(modifiedXmlPath);
        }

        // Output the locations of the generated XML files for verification
        Console.WriteLine("Original configuration XML: " + originalXmlPath);
        Console.WriteLine("Modified configuration XML: " + modifiedXmlPath);
    }
}