// Title: Export barcode configuration to XML, modify color, and generate updated image
// Description: Demonstrates exporting a barcode generator's settings to XML, changing the bar color to green, re‑importing the configuration, and creating a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist barcode settings using ExportToXml and ImportFromXml, adjust visual properties via XML, and regenerate barcodes. Key API classes include BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and System.Xml.Linq for XML manipulation. Developers often need to store, edit, or version barcode configurations outside code, especially for dynamic styling or batch processing scenarios.
// Prompt: Export barcode configuration to XML, edit ForeColor attribute to green, re‑import, and generate updated image.
// Tags: barcode, export, import, xml, color, png, aspnet, aspose.barcode, code128

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode configuration to XML, editing the bar color, re‑importing, and saving the updated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export‑modify‑import workflow and saves the resulting PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the XML configuration and the final barcode image
        string xmlPath = Path.Combine(tempFolder, "generator.xml");
        string outputPath = Path.Combine(tempFolder, "barcode_green.png");

        // --------------------------------------------------------------------
        // Step 1: Generate a barcode and export its configuration to XML
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Persist the current generator settings to an XML file
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Step 2: Load the XML, modify the BarColor element to Green
        // --------------------------------------------------------------------
        XDocument doc = XDocument.Load(xmlPath);
        var barColorElement = doc.Root
            ?.Element("Parameters")
            ?.Element("Barcode")
            ?.Element("BarColor");

        if (barColorElement != null)
        {
            // Existing BarColor element found – update its value
            barColorElement.Value = "Green";
        }
        else
        {
            // BarColor element missing – add a new element with the desired value
            var barcodeElem = doc.Root?.Element("Parameters")?.Element("Barcode");
            if (barcodeElem != null)
            {
                barcodeElem.Add(new XElement("BarColor", "Green"));
            }
        }

        // Save the modified XML back to disk
        doc.Save(xmlPath);

        // --------------------------------------------------------------------
        // Step 3: Import the modified configuration and generate the barcode image
        // --------------------------------------------------------------------
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Render the barcode to a PNG file using the updated settings
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the image was saved
        Console.WriteLine("Barcode image saved to: " + outputPath);
    }
}