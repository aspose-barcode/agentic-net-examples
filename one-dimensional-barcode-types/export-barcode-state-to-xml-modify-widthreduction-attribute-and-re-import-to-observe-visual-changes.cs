// Title: Export and Modify Barcode State via XML
// Description: Demonstrates exporting a barcode generator's state to XML, editing the BarWidthReduction attribute, and re‑importing to produce a modified barcode image.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showcasing how to persist a BarcodeGenerator's configuration using ExportToXml, adjust settings directly in the XML (e.g., BarWidthReduction), and restore the configuration with ImportFromXml. Developers often use these APIs for batch processing, configuration versioning, or dynamic adjustments without recreating the generator from scratch.
// Prompt: Export barcode state to XML, modify WidthReduction attribute, and re‑import to observe visual changes.
// Tags: barcode, symbology, export, import, xml, widthreduction, aspose.barcode, code128, png

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode's configuration to XML, modifying the BarWidthReduction,
/// and re‑importing the state to generate a new barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, saves its image, exports its state,
    /// edits the XML, re‑imports the state, and saves the modified image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for all generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the original image, XML state, and modified image
        string originalImagePath = Path.Combine(tempDir, "original.png");
        string xmlPath = Path.Combine(tempDir, "state.xml");
        string modifiedImagePath = Path.Combine(tempDir, "modified.png");

        // --------------------------------------------------------------------
        // 1. Generate the initial barcode, save it as PNG, and export its state to XML
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set the X-dimension (module width) and initial BarWidthReduction
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 0;

            // Save the barcode image
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // 2. Load the exported XML and modify the BarWidthReduction value
        // --------------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);
            var reductionElement = doc.Descendants("BarWidthReduction").FirstOrDefault();

            if (reductionElement != null)
            {
                // Change the reduction from 0 to 4 pixels
                reductionElement.Value = "4";
                doc.Save(xmlPath);
            }
            else
            {
                Console.WriteLine("BarWidthReduction element not found in XML.");
            }
        }
        else
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // --------------------------------------------------------------------
        // 3. Import the modified XML state and generate the updated barcode image
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the modified barcode image
            importedGenerator.Save(modifiedImagePath, BarCodeImageFormat.Png);

            // Output the new BarWidthReduction value for verification
            Console.WriteLine($"Modified BarWidthReduction: {importedGenerator.Parameters.Barcode.BarWidthReduction.Pixels}");
        }

        // --------------------------------------------------------------------
        // 4. Report file locations
        // --------------------------------------------------------------------
        Console.WriteLine($"Original image saved to: {originalImagePath}");
        Console.WriteLine($"Modified image saved to: {modifiedImagePath}");
        Console.WriteLine($"XML state file: {xmlPath}");
    }
}