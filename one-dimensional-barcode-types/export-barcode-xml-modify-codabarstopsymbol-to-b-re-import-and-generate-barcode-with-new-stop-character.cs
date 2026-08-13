// Title: Export Codabar barcode settings to XML, modify stop symbol, re-import and generate image
// Description: Demonstrates exporting a Codabar barcode generator's configuration to XML, editing the CodabarStopSymbol to 'B', importing the modified XML, and creating a barcode image with the new stop character.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml to persist and modify barcode settings. Typical use cases include batch updating barcode parameters, customizing symbology options, and integrating external configuration files. Developers often need to adjust properties like stop symbols, checksum settings, or visual styles without recompiling code.
// Prompt: Export barcode XML, modify CodabarStopSymbol to B, re‑import, and generate barcode with new stop character.
// Tags: codabar, stop-symbol, xml, export, import, barcode-generation, aspose.barcode, configuration

using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that exports a Codabar barcode configuration to XML,
/// modifies the stop symbol, re-imports the configuration, and generates a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs export, modification, import, and image generation steps.
    /// </summary>
    static void Main()
    {
        // Define file paths for the intermediate XML and final PNG image
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.xml");
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_modified.png");

        // Step 1: Create a Codabar barcode generator with default settings and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Load the exported XML, locate the CodabarStopSymbol element, change its value to "B", and save the XML
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);

            // Perform a case‑insensitive search for the element named "CodabarStopSymbol"
            var stopSymbolElement = doc.Descendants()
                .FirstOrDefault(e => string.Equals(e.Name.LocalName, "CodabarStopSymbol", StringComparison.OrdinalIgnoreCase));

            if (stopSymbolElement != null)
            {
                // Update the element's value to the enum name representing stop symbol B
                stopSymbolElement.Value = "B";
                doc.Save(xmlPath);
            }
            else
            {
                Console.WriteLine("CodabarStopSymbol element not found in XML.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // Step 3: Import the modified XML back into a new generator instance and generate the barcode image
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Optional verification of the imported stop symbol
            var stopSymbol = importedGenerator.Parameters.Barcode.Codabar.StopSymbol;
            Console.WriteLine($"Imported Stop Symbol: {stopSymbol}");

            // Save the generated barcode image with the updated stop symbol
            importedGenerator.Save(outputPath);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}