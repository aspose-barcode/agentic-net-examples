// Title: Export Barcode State to XML, Modify Width Reduction, and Regenerate Barcode
// Description: Demonstrates exporting a barcode generator's configuration to XML, adjusting the BarWidthReduction to 10 percent, re‑importing the modified settings, and creating an updated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml to persist and modify barcode settings such as BarWidthReduction. Developers working with barcode generation often need to store configurations, edit them programmatically or manually, and regenerate barcodes without recreating the generator from scratch. The key API classes are BarcodeGenerator, BarCodeImageFormat, and System.Xml.Linq for XML manipulation.
// Prompt: Export barcode state to XML, change WidthReduction to 10 percent, re‑import, and generate updated barcode.
// Tags: barcode, export, import, xml, widthreduction, code128, png, aspose.barcode, configuration

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator's state to XML, modifying the bar width reduction,
/// re‑importing the configuration, and generating an updated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an initial barcode, saves its configuration, updates the BarWidthReduction,
    /// and produces both original and updated barcode images.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for XML state and PNG images
        string xmlPath = Path.Combine(outputDir, "generator.xml");
        string originalPath = Path.Combine(outputDir, "original.png");
        string updatedPath = Path.Combine(outputDir, "updated.png");

        // --------------------------------------------------------------------
        // Create the initial barcode, save the image, and export its state to XML
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set X-dimension (module width) to 2 points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the original barcode image as PNG
            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // ---------------------------------------------------------------
        // Load the exported XML and modify the BarWidthReduction to 10%
        // ---------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);

            // Try to find an existing <BarWidthReduction> element
            var reductionElement = doc.Descendants("BarWidthReduction").FirstOrDefault();
            if (reductionElement != null)
            {
                reductionElement.Value = "10";
            }
            else
            {
                // If the element is missing, add it under the <Barcode> node
                var barcodeElement = doc.Descendants("Barcode").FirstOrDefault();
                if (barcodeElement != null)
                {
                    barcodeElement.Add(new XElement("BarWidthReduction", "10"));
                }
            }

            // Save the modified XML back to disk
            doc.Save(xmlPath);
        }
        else
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // ---------------------------------------------------------------
        // Import the modified configuration and generate the updated barcode
        // ---------------------------------------------------------------
        using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the updated barcode image as PNG
            generator.Save(updatedPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Updated barcode saved to: {updatedPath}");
        Console.WriteLine($"Modified XML saved to: {xmlPath}");
    }
}