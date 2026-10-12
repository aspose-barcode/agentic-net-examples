// Title: Export barcode configuration to XML, modify color, re-import and generate image
// Description: Demonstrates exporting a BarcodeGenerator's settings to XML, editing the BarColor to green, re‑importing the configuration, and creating an updated PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showing how to persist generator settings with ExportToXml, edit the XML manually, and restore them using ImportFromXml. It highlights key classes such as BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and System.Xml.Linq for XML manipulation. Developers often need to store, version‑control, or program‑matically adjust barcode parameters across environments.
// Prompt: Export barcode configuration to XML, edit ForeColor attribute to green, re‑import, and generate updated image.
// Tags: barcode symbology, export, import, xml, color, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that exports a barcode generator configuration to XML,
/// modifies the bar color, re‑imports the configuration, and saves the updated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export‑modify‑import workflow and writes the resulting image to a temporary folder.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary working directory for all generated files
        // ------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the exported XML and the final PNG image
        string xmlPath = Path.Combine(workDir, "generator.xml");
        string outputPath = Path.Combine(workDir, "updated_barcode.png");

        // ------------------------------------------------------------
        // Step 1: Create a barcode generator and export its configuration to XML
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Optional: set an initial bar color (default is Black)
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Persist the generator's current settings to an XML file
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Step 2: Load the exported XML, modify the BarColor element to Green
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);
        XElement barColorElement = doc.Root
            ?.Element("Parameters")
            ?.Element("Barcode")
            ?.Element("BarColor");

        if (barColorElement != null)
        {
            // Change the color value to "Green"
            barColorElement.Value = "Green";
        }
        else
        {
            Console.WriteLine("BarColor element not found in XML.");
            return;
        }

        // Save the modified XML back to the same file location
        doc.Save(xmlPath);

        // ------------------------------------------------------------
        // Step 3: Import the modified configuration and generate the updated barcode image
        // ------------------------------------------------------------
        using (var updatedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode image as a PNG file
            updatedGenerator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Updated barcode image saved to: {outputPath}");
    }
}