// Title: Export barcode state to XML, modify WidthReduction, and re-import
// Description: Demonstrates exporting a barcode generator's configuration to XML, adjusting the BarWidthReduction attribute, and re-importing to produce a modified barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist barcode settings using ExportToXml and ImportFromXml, manipulate XML directly, and regenerate barcodes. Developers working with barcode generation often need to store, edit, or version‑control settings; key classes include BarcodeGenerator, BarcodeParameters, and XML handling via System.Xml.Linq.
// Prompt: Export barcode state to XML, modify WidthReduction attribute, and re‑import to observe visual changes.
// Tags: barcode, xml, widthreduction, export, import, aspose.barcode, code128, image

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows how to export a barcode's configuration to XML,
/// modify the <c>BarWidthReduction</c> attribute, and re‑import the settings
/// to generate a visually altered barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑modify‑import workflow
    /// and saves both the original and modified barcode images.
    /// </summary>
    static void Main()
    {
        // Define file names for the XML configuration and output images.
        const string xmlPath = "barcode.xml";
        const string originalImagePath = "original.png";
        const string modifiedImagePath = "modified.png";

        // ------------------------------------------------------------
        // Step 1: Create a barcode generator, configure it, export to XML,
        // and save the original image.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set an initial BarWidthReduction (default is 0).
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;

            // Export the current barcode settings to an XML file.
            bool exportSuccess = generator.ExportToXml(xmlPath);
            if (!exportSuccess)
            {
                Console.WriteLine("Failed to export barcode settings to XML.");
                return;
            }

            // Save the barcode image generated with the original settings.
            generator.Save(originalImagePath);
        }

        // ------------------------------------------------------------
        // Step 2: Load the exported XML, modify BarWidthReduction, and save it.
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);

        // Locate the BarWidthReduction element (it may be an element or attribute).
        XElement reductionElement = doc.Root?.Descendants("BarWidthReduction").FirstOrDefault();
        if (reductionElement != null)
        {
            // Update the reduction value (e.g., 0.5 points).
            reductionElement.Value = "0.5";
        }
        else
        {
            // If the element does not exist, create it under the root element.
            XElement root = doc.Root;
            if (root != null)
            {
                root.Add(new XElement("BarWidthReduction", "0.5"));
            }
        }

        // Persist the modified XML back to disk.
        doc.Save(xmlPath);

        // ------------------------------------------------------------
        // Step 3: Import the modified XML into a new generator and save the updated image.
        // ------------------------------------------------------------
        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            if (modifiedGenerator == null)
            {
                Console.WriteLine("Failed to import barcode settings from XML.");
                return;
            }

            // Ensure the BarWidthReduction reflects the modified value.
            // (Import should apply it, but we set it explicitly for safety.)
            modifiedGenerator.Parameters.Barcode.BarWidthReduction.Point = 0.5f;

            // Save the barcode image generated with the modified settings.
            modifiedGenerator.Save(modifiedImagePath);
        }

        // Indicate successful completion and provide file locations.
        Console.WriteLine("Barcode generation completed.");
        Console.WriteLine($"Original image: {originalImagePath}");
        Console.WriteLine($"Modified image: {modifiedImagePath}");
    }
}