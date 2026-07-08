// Title: Export barcode to XML, edit YDimension, re-import and compare images
// Description: Demonstrates exporting barcode generator settings to XML, modifying the YDimension attribute to change bar height, and re-importing to generate an updated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category, showcasing how to persist generator parameters via XML, edit them manually, and reload them. It highlights key classes such as BarcodeGenerator, EncodeTypes, and AutoSizeMode, useful for developers needing to programmatically adjust barcode dimensions or store settings for later reuse.
// Prompt: Export barcode XML, edit YDimension attribute, re‑import, and observe vertical size adjustment.
// Tags: barcode, xml, ydimension, generation, autosizemode, aspose.barcode, code128

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode's configuration to XML, editing the YDimension attribute,
/// re-importing the configuration, and observing the effect on the barcode's vertical size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it, modifies its YDimension via XML,
    /// and saves the updated barcode image.
    /// </summary>
    static void Main()
    {
        // Define file paths for temporary XML and PNG images
        string xmlPath = "barcode.xml";
        string beforeImage = "barcode_before.png";
        string afterImage = "barcode_after.png";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Disable automatic sizing so that YDimension influences bar height
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the initial barcode image (before modification)
            generator.Save(beforeImage);

            // Export the generator's settings to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Ensure the XML file was successfully created
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Failed to create XML file at '{xmlPath}'.");
            return;
        }

        // Load the XML, modify the YDimension attribute, and write the changes back
        XDocument doc = XDocument.Load(xmlPath);
        XElement root = doc.Root;
        if (root != null)
        {
            // Increase YDimension (e.g., to 10 points) to make bars taller
            root.SetAttributeValue("YDimension", "10");
            doc.Save(xmlPath);
        }
        else
        {
            Console.WriteLine("Invalid XML structure: missing root element.");
            return;
        }

        // Import the modified XML into a new generator instance
        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Preserve the same AutoSizeMode setting as the original generator
            modifiedGenerator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Save the barcode image after the YDimension change
            modifiedGenerator.Save(afterImage);
        }

        // Output the locations of the generated files
        Console.WriteLine($"Barcode before modification saved to: {beforeImage}");
        Console.WriteLine($"Barcode after modification saved to: {afterImage}");
        Console.WriteLine($"XML with edited YDimension saved to: {xmlPath}");
    }
}