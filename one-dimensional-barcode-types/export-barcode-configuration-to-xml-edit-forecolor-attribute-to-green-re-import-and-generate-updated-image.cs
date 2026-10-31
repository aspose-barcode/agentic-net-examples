// Title: Export barcode configuration to XML, modify bar color, and generate updated image
// Description: This example shows how to export a barcode generator's settings to an XML file, change the bar color to green, re‑import the configuration, and create a new barcode image.
// Category-Description: Aspose.BarCode configuration management examples demonstrate how to persist barcode generator settings using XML, adjust visual properties such as bar color, and reload the configuration to produce updated barcodes. These samples use BarcodeGenerator, ExportToXml, ImportFromXml, and image saving APIs, helping developers automate barcode styling and maintain consistent settings across applications.
// Prompt: Export barcode configuration to XML, edit ForeColor attribute to green, re‑import, and generate updated image.
// Tags: barcode, export, import, xml, color, code128, png, aspose.barcode, configuration

using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode configuration to XML, editing the bar color, re‑importing, and generating an updated image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the export‑modify‑import workflow and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working directory for intermediate files
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define paths for the exported XML configuration and the final barcode image
        string xmlPath = Path.Combine(workDir, "barcodeConfig.xml");
        string outputPath = Path.Combine(workDir, "updatedBarcode.png");

        // Step 1: Create a barcode generator and export its configuration to an XML file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Export the current generator settings to the specified XML file
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Load the exported XML, modify the BarColor element to Green
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);
        var barColorElement = doc.Root?.Descendants("BarColor").FirstOrDefault();

        if (barColorElement != null)
        {
            // Update existing BarColor element value
            barColorElement.Value = "Green";
        }
        else
        {
            // If BarColor element is missing, add it under the <Barcode> element
            var barcodeElement = doc.Root?.Descendants("Barcode").FirstOrDefault();
            if (barcodeElement != null)
            {
                barcodeElement.Add(new XElement("BarColor", "Green"));
            }
        }

        // Save the modified XML back to the file system
        doc.Save(xmlPath);

        // Step 3: Import the modified configuration and generate the updated barcode image
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode as a PNG image using the updated settings
            importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Updated barcode image saved to: {outputPath}");
    }
}