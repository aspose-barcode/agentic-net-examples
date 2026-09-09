// Title: Codabar barcode generation with XML export and start symbol modification
// Description: Demonstrates creating a Codabar barcode, exporting its configuration to XML, editing the start symbol attribute, and re‑importing to generate a barcode with a new start character.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and manipulation category. It showcases the use of BarcodeGenerator, its Parameters for Codabar settings, and the ExportToXml/ImportFromXml APIs. Typical use cases include persisting barcode settings, batch editing via XML, and dynamically changing symbology options. Developers often need to modify barcode properties programmatically or via configuration files, and this snippet illustrates that workflow.
// Prompt: Generate a barcode, export its XML, edit CodabarStartSymbol attribute, and re‑import to change start character.
// Tags: codabar, barcode generation, xml export, xml import, start symbol, aspose.barcode, barcode manipulation

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Codabar barcode, exports its configuration to XML,
/// modifies the start symbol in the XML, and re‑imports the configuration to generate a new barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the three‑step process of barcode generation,
    /// XML manipulation, and re‑generation with the updated start symbol.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Step 0: Prepare a unique temporary folder for all output files
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "CodabarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original barcode, the XML state, and the modified barcode
        string barcodePathA = Path.Combine(tempFolder, "barcode_startA.png");
        string xmlPath = Path.Combine(tempFolder, "barcode_state.xml");
        string barcodePathC = Path.Combine(tempFolder, "barcode_startC.png");

        // ------------------------------------------------------------
        // Step 1: Generate a Codabar barcode with the default start/stop symbol (A)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            // Set both start and stop symbols to 'A'
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

            // Save the barcode image and export its configuration to XML
            generator.Save(barcodePathA, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Step 2: Load the exported XML and change the start symbol to 'C'
        // ------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);

            // The start symbol element may be named either "StartSymbol" or "CodabarStartSymbol"
            var startElem = doc.Descendants("StartSymbol").FirstOrDefault()
                           ?? doc.Descendants("CodabarStartSymbol").FirstOrDefault();

            if (startElem != null)
            {
                startElem.Value = "C"; // Update the start symbol value
                doc.Save(xmlPath);     // Persist the change back to the XML file
            }
            else
            {
                Console.WriteLine("StartSymbol element not found in XML.");
                return;
            }
        }
        else
        {
            Console.WriteLine("XML file not found.");
            return;
        }

        // ------------------------------------------------------------
        // Step 3: Import the modified XML and generate a new barcode with the updated start symbol
        // ------------------------------------------------------------
        using (var generatorModified = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            generatorModified.Save(barcodePathC, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Output the locations of the generated files for verification
        // ------------------------------------------------------------
        Console.WriteLine("Original barcode saved to: " + barcodePathA);
        Console.WriteLine("Modified barcode saved to: " + barcodePathC);
        Console.WriteLine("XML file path: " + xmlPath);
    }
}