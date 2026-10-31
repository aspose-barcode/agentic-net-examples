// Title: Generate Codabar barcode, edit start symbol via XML, and re‑import
// Description: Demonstrates creating a Codabar barcode, exporting its generation state to XML, modifying the CodabarStartSymbol attribute, and re‑importing to produce a barcode with a new start character.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category, showcasing how to use BarcodeGenerator, ExportToXml, and ImportFromXml APIs. Typical use cases include persisting barcode settings, batch editing via XML, and dynamic regeneration. Developers often need to modify barcode parameters programmatically without recreating the generator from scratch.
// Prompt: Generate a barcode, export its XML, edit CodabarStartSymbol attribute, and re‑import to change start character.
// Tags: codabar, barcode, xml, export, import, startsymbol, aspose.barcode, generation

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Codabar barcode, exporting its configuration to XML,
/// editing the start symbol, and re‑importing to create a modified barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, XML manipulation, and saves output images.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // -----------------------------------------------------------------
        // 1. Generate a Codabar barcode with default start/stop symbols (A)
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Save the original barcode image for reference
            string originalImagePath = Path.Combine(outputDir, "codabar_original.png");
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's state (including start/stop symbols) to XML
            string xmlPath = Path.Combine(outputDir, "codabar_state.xml");
            generator.ExportToXml(xmlPath);
        }

        // ---------------------------------------------------------------
        // 2. Load the exported XML and modify the start symbol to 'C'
        // ---------------------------------------------------------------
        string xmlFile = Path.Combine(outputDir, "codabar_state.xml");
        XDocument doc = XDocument.Load(xmlFile);
        var startSymbolElement = doc.Descendants("StartSymbol").FirstOrDefault();
        if (startSymbolElement != null)
        {
            startSymbolElement.Value = "C"; // Change start symbol from 'A' to 'C'
        }
        else
        {
            Console.WriteLine("StartSymbol element not found in XML.");
        }
        doc.Save(xmlFile); // Persist the modification

        // ---------------------------------------------------------------
        // 3. Import the modified XML and generate a new barcode image
        // ---------------------------------------------------------------
        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(xmlFile))
        {
            string modifiedImagePath = Path.Combine(outputDir, "codabar_modified.png");
            modifiedGenerator.Save(modifiedImagePath, BarCodeImageFormat.Png);
        }

        // Output the location of generated files
        Console.WriteLine("Barcode generation completed. Files are located at:");
        Console.WriteLine(outputDir);
    }
}