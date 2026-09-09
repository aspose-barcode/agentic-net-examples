// Title: Export Codabar barcode configuration to XML, modify stop symbol, and regenerate barcode
// Description: Shows how to generate a Codabar barcode, export its settings to XML, edit the stop symbol, re-import the configuration, and produce a new barcode image.
// Category-Description: This example demonstrates the Aspose.BarCode workflow for persisting barcode settings to XML, editing the configuration, and recreating the barcode with modified parameters. It uses the BarcodeGenerator class along with ExportToXml and ImportFromXml methods, focusing on Codabar symbology adjustments. Developers often need to store, modify, or batch‑process barcode configurations, and this pattern provides a clear template for such tasks.
// Prompt: Export barcode XML, modify CodabarStopSymbol to B, re‑import, and generate barcode with new stop character.
// Tags: codabar, export, import, xml, png, barcode, generation

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a Codabar barcode configuration to XML, modifying the stop symbol,
/// re‑importing the configuration, and generating a new barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑modify‑import workflow and saves the resulting barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "CodabarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the exported XML and the final PNG image
        string xmlPath = Path.Combine(tempFolder, "codabar.xml");
        string outputPath = Path.Combine(tempFolder, "codabar_modified.png");

        // Step 1: Generate an initial Codabar barcode and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // Set visual density (pixel size of the smallest bar)
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Explicitly set start/stop symbols to 'A' for clarity (default is also 'A')
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

            // Persist the current generator settings to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Load the exported XML and change the stop symbol from 'A' to 'B'
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);
            var stopElement = doc.Descendants("CodabarStopSymbol").FirstOrDefault();
            if (stopElement != null)
            {
                stopElement.Value = "B"; // Update the stop symbol
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

        // Step 3: Import the modified XML configuration and generate the barcode with the new stop symbol
        using (var generatorModified = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the resulting barcode image as PNG
            generatorModified.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Modified barcode saved to: " + outputPath);
    }
}