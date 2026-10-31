// Title: Export Codabar barcode configuration to XML, modify stop symbol, re-import and generate PNG
// Description: Demonstrates exporting a Codabar barcode generator's settings to XML, editing the stop symbol, importing the modified XML, and creating a barcode image with the new stop character.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist barcode generator settings using XML, adjust parameters programmatically, and regenerate barcodes. It showcases key classes like BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use for saving, loading, and customizing barcode configurations across applications.
// Prompt: Export barcode XML, modify CodabarStopSymbol to B, re‑import, and generate barcode with new stop character.
// Tags: codabar, xml, export, import, png, barcodegenerator, parameters

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a Codabar barcode configuration to XML,
/// modifies the stop symbol, re-imports the configuration, and generates a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export‑modify‑import workflow and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where XML and PNG files will be stored.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define file paths for the intermediate XML and final PNG image.
        string xmlPath = Path.Combine(outputDir, "codabar.xml");
        string imagePath = Path.Combine(outputDir, "codabar_B.png");

        // Step 1: Create a Codabar generator with default start/stop symbols and export its configuration to XML.
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "12345"))
        {
            // Explicitly set start and stop symbols to 'A' (default) for clarity.
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.A;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.A;

            // Persist the generator's settings to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Load the exported XML, locate the <StopSymbol> element, and change its value to 'B'.
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);
            var stopSymbolElement = doc.Descendants("StopSymbol").FirstOrDefault();
            if (stopSymbolElement != null)
            {
                stopSymbolElement.Value = "B";
                doc.Save(xmlPath);
            }
            else
            {
                Console.WriteLine("StopSymbol element not found in XML.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // Step 3: Import the modified XML back into a generator and create the barcode image.
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Optional verification: output the stop symbol after import.
            var stopSymbol = generator.Parameters.Barcode.Codabar.StopSymbol;
            Console.WriteLine($"Stop symbol after import: {stopSymbol}");

            // Save the generated barcode as a PNG file.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {imagePath}");
    }
}