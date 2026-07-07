// Title: Generate Codabar barcode, modify start symbol via XML, and re‑import
// Description: Demonstrates creating a Codabar barcode, exporting its settings to XML, editing the start symbol attribute, and re‑importing to produce a barcode with a new start character.
// Category-Description: This example belongs to the Aspose.BarCode generation and XML manipulation category. It showcases the use of BarcodeGenerator for creating barcodes, ExportToXml for persisting settings, and ImportFromXml for re‑creating a generator with modified parameters. Typical use cases include batch barcode customization, dynamic symbol changes, and configuration persistence. Developers often need to adjust barcode attributes programmatically without rebuilding the entire generator.
// Prompt: Generate a barcode, export its XML, edit CodabarStartSymbol attribute, and re‑import to change start character.
// Tags: codabar, barcode, xml, start-symbol, generation, aspose.barcode

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Codabar barcode, modifies its start symbol via XML,
/// and generates the final barcode image with the updated settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode creation, XML export/modification,
    /// and final image generation steps.
    /// </summary>
    static void Main()
    {
        // Define file paths for the intermediate XML files and the final image.
        const string xmlPath = "codabar.xml";
        const string modifiedXmlPath = "codabar_modified.xml";
        const string outputImagePath = "codabar_final.png";

        // 1. Create a Codabar barcode generator with the default start/stop symbol 'A'.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Export the generator's current configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // 2. Load the exported XML and change the CodabarStartSymbol to 'B'.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML file '{xmlPath}' not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);
        // Locate the CodabarStartSymbol element within the XML structure.
        XElement startSymbolElement = doc.Root?.Descendants("CodabarStartSymbol").FirstOrDefault();
        if (startSymbolElement == null)
        {
            Console.WriteLine("Error: CodabarStartSymbol element not found in XML.");
            return;
        }

        // Update the element's value to the new start symbol.
        startSymbolElement.Value = "B";

        // Save the modified XML to a new file.
        doc.Save(modifiedXmlPath);

        // 3. Import the modified XML to create a new generator with the updated start symbol.
        if (!File.Exists(modifiedXmlPath))
        {
            Console.WriteLine($"Error: Modified XML file '{modifiedXmlPath}' not found.");
            return;
        }

        using (var generatorModified = BarcodeGenerator.ImportFromXml(modifiedXmlPath))
        {
            // The CodeText still contains the old start/stop symbols; update it to match the new symbol.
            generatorModified.CodeText = "B123456B";

            // Save the final barcode image to the specified path.
            generatorModified.Save(outputImagePath);
        }

        Console.WriteLine($"Barcode generated with new start symbol and saved to '{outputImagePath}'.");
    }
}