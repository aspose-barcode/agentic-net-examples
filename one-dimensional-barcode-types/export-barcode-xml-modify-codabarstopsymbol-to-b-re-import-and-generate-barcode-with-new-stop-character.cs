// Title: Export, modify, and re-import Codabar barcode settings via XML
// Description: Demonstrates exporting a Codabar barcode generator's settings to XML, editing the stop symbol, re-importing the modified settings, and generating a new barcode image.
// Category-Description: This example belongs to the Aspose.BarCode settings management category, showcasing how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml. Typical use cases include persisting barcode configurations, batch editing via XML, and regenerating barcodes with altered parameters. Developers often need to programmatically adjust symbology options such as stop symbols, and this snippet illustrates that workflow.
// Prompt: Export barcode XML, modify CodabarStopSymbol to B, re‑import, and generate barcode with new stop character.
// Tags: codabar, barcode, xml, export, import, modification, generation, aspose.barcode

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a Codabar barcode configuration to XML,
/// modifies the stop symbol, re-imports the configuration, and generates a new barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑modify‑import workflow and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Define file paths relative to the current working directory.
        string xmlPath = "codabar_original.xml";
        string modifiedXmlPath = "codabar_modified.xml";
        string outputImagePath = "codabar_modified.png";

        // 1. Create a Codabar barcode generator with an initial code text and export its settings to XML.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Export the generator's configuration (including symbology options) to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // 2. Load the exported XML, locate the CodabarStopSymbol element, change its value to 'B', and save the modified XML.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML file '{xmlPath}' not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);
        // Find the element that defines the stop symbol (case‑sensitive).
        XElement stopSymbolElement = doc.Root?.Descendants("CodabarStopSymbol").FirstOrDefault();
        if (stopSymbolElement == null)
        {
            Console.WriteLine("Error: CodabarStopSymbol element not found in XML.");
            return;
        }

        // Update the stop symbol value.
        stopSymbolElement.Value = "B";
        doc.Save(modifiedXmlPath);

        // 3. Import the modified XML back into a BarcodeGenerator, adjust the code text, and generate the barcode image.
        if (!File.Exists(modifiedXmlPath))
        {
            Console.WriteLine($"Error: Modified XML file '{modifiedXmlPath}' not found.");
            return;
        }

        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(modifiedXmlPath))
        {
            // Set the code text to use the new stop symbol.
            modifiedGenerator.CodeText = "B123456B";

            // Save the resulting barcode image to the specified file.
            modifiedGenerator.Save(outputImagePath);
        }

        Console.WriteLine($"Barcode image generated: {outputImagePath}");
    }
}