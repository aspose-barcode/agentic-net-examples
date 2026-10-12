// Title: Load barcode configuration from XML, modify bar color, and export
// Description: Demonstrates how to export a barcode's settings to XML, import them back, change the foreground color, and save the updated configuration.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating the use of BarcodeGenerator.ExportToXml, BarcodeGenerator.ImportFromXml, and barcode parameter manipulation. Developers often need to persist barcode settings, adjust visual properties like bar color, and re‑use configurations across applications. The snippet shows a typical workflow for XML‑based barcode configuration handling.
// Prompt: Write a script that loads barcode configurations from XML, modifies the foreground color, and re‑exports them.
// Tags: barcode, xml, configuration, color, export, import, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that demonstrates exporting a barcode configuration to XML,
/// importing it, changing the bar (foreground) color, and re‑exporting the modified configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the XML export/import workflow and reports results.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string demoFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(demoFolder);

        // Define file paths for the original and modified XML configurations
        string originalXmlPath = Path.Combine(demoFolder, "original.xml");
        string modifiedXmlPath = Path.Combine(demoFolder, "modified.xml");

        // Step 1: Generate a barcode and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Export the initial configuration to the original XML file
            generator.ExportToXml(originalXmlPath);
        }

        // Verify the original XML file exists
        if (!File.Exists(originalXmlPath))
        {
            Console.WriteLine("Failed to create the original XML configuration.");
            return;
        }

        // Step 2: Import the configuration from XML, modify the foreground color, and re‑export
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(originalXmlPath))
        {
            // Change the foreground (bar) color to Red
            importedGenerator.Parameters.Barcode.BarColor = Color.Red;

            // Export the modified configuration to the new XML file
            importedGenerator.ExportToXml(modifiedXmlPath);
        }

        // Verify the modified XML file exists and report the outcome
        if (File.Exists(modifiedXmlPath))
        {
            Console.WriteLine("Modified XML configuration saved to:");
            Console.WriteLine(modifiedXmlPath);
        }
        else
        {
            Console.WriteLine("Failed to create the modified XML configuration.");
        }
    }
}