// Title: Load and modify barcode settings via XML
// Description: Demonstrates how to export barcode generator settings to XML, load them back, change the XDimension, and re‑export the updated configuration.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating the use of BarcodeGenerator for persisting settings to XML and re‑importing them. Typical use cases include batch processing, configuration versioning, and dynamic adjustment of barcode parameters. Developers often work with BarcodeGenerator, EncodeTypes, and the Parameters.Barcode.XDimension property to fine‑tune barcode appearance.
// Prompt: Load barcode settings from an XML file, modify XDimension, and re‑save the updated XML.
// Tags: barcode, code128, xml, xdimension, configuration, aspnet.barcode, bargenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows how to export barcode settings to XML,
/// import them back, modify the XDimension, and save the updated XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a barcode,
    /// exports its settings to XML, re‑imports, modifies XDimension, and saves the updated XML.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and updated XML configurations
        string originalXmlPath = Path.Combine(tempFolder, "original.xml");
        string updatedXmlPath = Path.Combine(tempFolder, "updated.xml");

        // Step 1: Create a barcode generator, set initial XDimension, and export to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Set the initial XDimension (module width) in points
            generator.Parameters.Barcode.XDimension.Point = 2f;
            // Export the current generator settings to an XML file
            generator.ExportToXml(originalXmlPath);
        }

        // Step 2: Import the generator from the XML, modify XDimension, and export again
        using (var generator = BarcodeGenerator.ImportFromXml(originalXmlPath))
        {
            // Change XDimension to a new value (e.g., double the width)
            generator.Parameters.Barcode.XDimension.Point = 4f;
            // Export the updated settings to a new XML file
            generator.ExportToXml(updatedXmlPath);
        }

        // Output the locations of the generated XML files
        Console.WriteLine("Original XML saved to: " + originalXmlPath);
        Console.WriteLine("Updated XML saved to: " + updatedXmlPath);
    }
}