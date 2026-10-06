// Title: Export Codabar barcode to XML, modify stop symbol, and regenerate image
// Description: Demonstrates exporting a Codabar barcode's configuration to XML, editing the stop symbol, re‑importing the XML, and generating a new barcode image with the updated stop character.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration management category. It showcases the use of BarcodeGenerator for creating barcodes, ExportToXml for persisting settings, ImportFromXml for reloading modified configurations, and Save for rendering images. Developers often need to programmatically adjust barcode parameters (e.g., start/stop symbols) without recreating the generator from scratch, making XML export/import a convenient workflow.
// Prompt: Export barcode XML, modify CodabarStopSymbol to B, re‑import, and generate barcode with new stop character.
// Tags: codabar, barcode, export, import, xml, stop-symbol, aspose.barcode, generation, png

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a Codabar barcode configuration to XML,
/// changes the stop symbol, re‑imports the modified XML, and saves the resulting barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑modify‑import workflow and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "CodabarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the intermediate XML and final PNG image
        string xmlPath = Path.Combine(tempFolder, "codabar.xml");
        string imagePath = Path.Combine(tempFolder, "codabar_modified.png");

        // Step 1: Generate an initial Codabar barcode and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // The default start/stop symbols are 'A'; no explicit setting required
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Load the exported XML and modify the stop symbol to 'B'
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);
        // Find the element whose name ends with "StopSymbol" (e.g., CodabarStopSymbol)
        var stopElement = doc.Descendants()
                             .FirstOrDefault(e => e.Name.LocalName.EndsWith("StopSymbol", StringComparison.Ordinal));
        if (stopElement != null)
        {
            stopElement.Value = "B";
        }
        else
        {
            Console.WriteLine("StopSymbol element not found in XML.");
            return;
        }
        doc.Save(xmlPath);

        // Step 3: Import the modified XML and generate a new barcode image with the updated stop symbol
        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            modifiedGenerator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated image
        Console.WriteLine("Modified barcode image saved to:");
        Console.WriteLine(imagePath);
    }
}