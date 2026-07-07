// Title: Export barcode configuration to XML, modify foreground color, and regenerate image
// Description: Demonstrates exporting a barcode generator's settings to XML, changing the bar color to green, re-importing the configuration, and saving the updated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to use BarcodeGenerator's ExportToXml and ImportFromXml methods. Developers often need to persist barcode settings, edit them (e.g., colors, sizes) via XML, and recreate barcodes without rebuilding code. The key API classes include BarcodeGenerator, EncodeTypes, and XML handling via System.Xml.Linq.
// Prompt: Export barcode configuration to XML, edit ForeColor attribute to green, re‑import, and generate updated image.
// Tags: barcode, export, import, xml, color, code128, aspose.barcode, image generation

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that exports a barcode configuration to XML, modifies the foreground color,
/// re-imports the configuration, and generates an updated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export‑modify‑import workflow and saves the resulting images.
    /// </summary>
    static void Main()
    {
        // Define file paths in the current working directory
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.xml");
        string updatedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_updated.png");

        // Step 1: Create a barcode generator, generate the initial image, and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath);          // Save the original barcode image
            generator.ExportToXml(xmlPath);     // Export the generator's settings to an XML file
        }

        // Verify that the XML file was created before attempting to modify it
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // Step 2: Load the exported XML and change the bar (foreground) color to green
        XDocument doc = XDocument.Load(xmlPath);

        // Try to locate a <BarColor> element and set its value to green (ARGB hex format)
        var barColorElement = doc.Descendants("BarColor").FirstOrDefault();
        if (barColorElement != null)
        {
            // Green color in ARGB format (#FF00FF00)
            barColorElement.Value = "#FF00FF00";
        }
        else
        {
            // If <BarColor> is not present, look for a ForeColor attribute on any element
            var elementWithForeColor = doc.Descendants()
                                          .FirstOrDefault(e => e.Attribute("ForeColor") != null);
            if (elementWithForeColor != null)
            {
                elementWithForeColor.SetAttributeValue("ForeColor", "#FF00FF00");
            }
            else
            {
                Console.WriteLine("No BarColor or ForeColor node found in XML.");
                return;
            }
        }

        // Save the modified XML back to disk
        doc.Save(xmlPath);

        // Step 3: Import the modified XML to create a new generator and save the updated barcode image
        using (var generatorModified = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // The generator automatically applies the color defined in the XML (green)
            generatorModified.Save(updatedImagePath);
        }

        // Output the locations of the generated files
        Console.WriteLine("Original barcode saved to: " + imagePath);
        Console.WriteLine("Modified barcode saved to: " + updatedImagePath);
    }
}