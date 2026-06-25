using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a barcode, exporting its configuration to XML,
/// modifying the XML, and re‑importing it to generate an updated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define file paths for the original image, XML export, modified XML, and updated image.
        const string originalImagePath = "original.png";
        const string xmlPath = "barcode.xml";
        const string modifiedXmlPath = "barcode_modified.xml";
        const string updatedImagePath = "updated.png";

        // ------------------------------------------------------------
        // 1. Create a barcode generator with Code128 symbology and save the initial image.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode image to disk.
            generator.Save(originalImagePath);

            // Export the generator's current configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // 2. Load the exported XML and modify the BarWidthReduction element.
        // ------------------------------------------------------------
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Exported XML not found: {xmlPath}");
            return;
        }

        // Load the XML document from the file system.
        XDocument doc = XDocument.Load(xmlPath);

        // Locate the BarWidthReduction element (case‑insensitive search).
        XElement reductionElement = null;
        foreach (var elem in doc.Descendants())
        {
            if (string.Equals(elem.Name.LocalName, "BarWidthReduction", StringComparison.OrdinalIgnoreCase))
            {
                reductionElement = elem;
                break;
            }
        }

        // If the element is missing, report and exit.
        if (reductionElement == null)
        {
            Console.WriteLine("BarWidthReduction element not found in XML.");
            return;
        }

        // Set the BarWidthReduction value to 10 (percent).
        reductionElement.Value = "10";

        // Save the modified XML to a new file.
        doc.Save(modifiedXmlPath);

        // ------------------------------------------------------------
        // 3. Import the modified XML to create a new barcode generator.
        // ------------------------------------------------------------
        if (!File.Exists(modifiedXmlPath))
        {
            Console.WriteLine($"Modified XML not found: {modifiedXmlPath}");
            return;
        }

        using (var generatorModified = BarcodeGenerator.ImportFromXml(modifiedXmlPath))
        {
            // Generate and save the updated barcode image using the modified settings.
            generatorModified.Save(updatedImagePath);
        }

        // ------------------------------------------------------------
        // 4. Output the locations of the generated files.
        // ------------------------------------------------------------
        Console.WriteLine($"Original barcode saved to: {originalImagePath}");
        Console.WriteLine($"Updated barcode saved to: {updatedImagePath}");
    }
}