// Title: Export barcode configuration to XML, modify width reduction, and regenerate barcode
// Description: Demonstrates exporting a barcode generator's state to an XML file, editing the BarWidthReduction parameter, re‑importing the configuration, and creating an updated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to persist barcode settings using ExportToXml and ImportFromXml. It highlights key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Typical use cases include saving barcode configurations for later reuse, batch processing, or applying dynamic adjustments without recreating settings programmatically. Developers often need to modify parameters like BarWidthReduction to fine‑tune barcode appearance across different outputs.
// Prompt: Export barcode state to XML, change WidthReduction to 10 percent, re‑import, and generate updated barcode.
// Tags: barcode, code128, xml, export, import, widthreduction, png, aspose.barcode, generation

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that shows how to export a barcode's state to XML,
/// modify the BarWidthReduction setting, re‑import the configuration,
/// and generate an updated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates an original barcode, exports its
    /// configuration to XML, changes the width reduction to 10 %, re‑imports the
    /// configuration and saves the updated barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the original barcode, the XML configuration, and the updated barcode
        string originalPath = Path.Combine(tempDir, "original.png");
        string xmlPath = Path.Combine(tempDir, "barcode.xml");
        string updatedPath = Path.Combine(tempDir, "updated.png");

        // ------------------------------------------------------------
        // Generate the original barcode and export its state to XML
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a specific X‑dimension for better visual quality
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode image in PNG format
            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Export the complete generator configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Modify the BarWidthReduction value in the exported XML
        // ------------------------------------------------------------
        XDocument doc = XDocument.Load(xmlPath);
        var reductionElement = doc.Descendants("BarWidthReduction").FirstOrDefault();
        if (reductionElement != null)
        {
            // Set the reduction to 10 percent (value is expressed as an integer)
            reductionElement.Value = "10";
        }
        doc.Save(xmlPath);

        // ------------------------------------------------------------
        // Import the modified configuration and generate the updated barcode
        // ------------------------------------------------------------
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // The BarWidthReduction value (10 %) is now applied automatically
            generator.Save(updatedPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for verification
        Console.WriteLine("Original barcode saved to: " + originalPath);
        Console.WriteLine("Modified XML saved to: " + xmlPath);
        Console.WriteLine("Updated barcode saved to: " + updatedPath);
    }
}