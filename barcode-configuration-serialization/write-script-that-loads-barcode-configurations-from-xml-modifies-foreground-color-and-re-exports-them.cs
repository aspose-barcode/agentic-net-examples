// Title: Load and modify barcode configuration XML
// Description: Demonstrates loading a barcode configuration from an XML file, changing its foreground color, and exporting the modified configuration.
// Prompt: Write a script that loads barcode configurations from XML, modifies the foreground color, and re‑exports them.
// Tags: barcode symbology, configuration, xml, color modification, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that imports a barcode configuration from XML,
/// changes the barcode's foreground color, and exports the updated configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Loads the XML configuration, modifies the bar color, and saves the result.
    /// </summary>
    static void Main()
    {
        // Input and output XML file paths
        const string inputXmlPath = "barcode_config.xml";
        const string outputXmlPath = "barcode_config_modified.xml";

        // Verify that the input XML file exists before proceeding
        if (!File.Exists(inputXmlPath))
        {
            Console.WriteLine($"Input XML file not found: {inputXmlPath}");
            return;
        }

        // Load barcode configuration from XML, modify the foreground color, and save back to XML
        using (var generator = BarcodeGenerator.ImportFromXml(inputXmlPath))
        {
            // Change the barcode foreground (bar) color to Red
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;

            // Export the modified configuration to a new XML file
            generator.ExportToXml(outputXmlPath);
        }

        // Inform the user that the modified configuration has been saved
        Console.WriteLine($"Modified barcode configuration saved to: {outputXmlPath}");
    }
}