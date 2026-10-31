// Title: Load, modify, and export barcode configuration XML
// Description: Demonstrates loading a barcode generator configuration from an XML file, changing the bar color, and saving the updated configuration back to XML.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on configuration import/export via XML. It showcases the BarcodeGenerator class, its Parameters property, and the ImportFromXml/ExportToXml methods—common tasks for developers who need to persist, edit, or share barcode settings across applications or environments.
// Prompt: Write a script that loads barcode configurations from XML, modifies the foreground color, and re‑exports them.
// Tags: barcode symbology, configuration, xml, import, export, color, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to load a barcode configuration from XML,
/// change its foreground color, and re‑export it.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary barcode configuration, modifies it,
    /// and writes the updated XML file to disk.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeConfigDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and modified XML configurations
        string originalXml = Path.Combine(tempFolder, "config.xml");
        string modifiedXml = Path.Combine(tempFolder, "config_modified.xml");

        // Step 1: Create a barcode generator, set initial properties, and export to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the initial bar (foreground) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;
            // Export the current configuration to an XML file
            generator.ExportToXml(originalXml);
        }

        // Verify that the original XML file was created successfully
        if (!File.Exists(originalXml))
        {
            Console.WriteLine("Failed to create the original XML configuration.");
            return;
        }

        // Step 2: Import the configuration from XML, modify the foreground (bar) color, and re‑export
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(originalXml))
        {
            // Change the bar color to blue
            importedGenerator.Parameters.Barcode.BarColor = Color.Blue;
            // Export the modified configuration to a new XML file
            importedGenerator.ExportToXml(modifiedXml);
        }

        // Report the result of the modified XML export
        if (File.Exists(modifiedXml))
        {
            Console.WriteLine("Modified XML configuration saved successfully:");
            Console.WriteLine(modifiedXml);
        }
        else
        {
            Console.WriteLine("Failed to save the modified XML configuration.");
        }

        // Clean up: optional deletion of temporary files (comment out if inspection is needed)
        // try { File.Delete(originalXml); File.Delete(modifiedXml); Directory.Delete(tempFolder, true); } catch { }
    }
}