// Title: Export barcode state to XML, modify BarWidthReduction, and re‑import
// Description: Demonstrates how to export a barcode generator's configuration to XML, edit the BarWidthReduction attribute, and reload the settings to produce a modified barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating the use of BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml. Developers often need to persist barcode settings, adjust parameters programmatically via XML, and regenerate barcodes without recreating the generator from scratch. Typical use cases include batch processing, dynamic styling, and integration with external configuration systems.
// Prompt: Export barcode state to XML, modify WidthReduction attribute, and re‑import to observe visual changes.
// Tags: barcode, widthreduction, xml, export, import, aspose.barcode, code128, png

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows how to export a barcode generator's state to XML,
/// modify the BarWidthReduction attribute, and re‑import the XML to generate a
/// barcode with updated visual properties.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑modify‑import workflow
    /// and saves both the original and modified barcode images.
    /// </summary>
    static void Main()
    {
        // Define file paths for the original image, modified image, and XML state file.
        string outputDir = Directory.GetCurrentDirectory();
        string originalImagePath = Path.Combine(outputDir, "barcode_original.png");
        string modifiedImagePath = Path.Combine(outputDir, "barcode_modified.png");
        string xmlPath = Path.Combine(outputDir, "barcode_state.xml");

        // Step 1: Create a barcode generator, configure it, and save the original image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set an initial BarWidthReduction (default is 0 points).
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;

            // Save the barcode as a PNG file.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's configuration to an XML file.
            bool exportSuccess = generator.ExportToXml(xmlPath);
            Console.WriteLine($"Export to XML {(exportSuccess ? "succeeded" : "failed")} at: {xmlPath}");
        }

        // Step 2: Load the exported XML, locate the BarWidthReduction element, and modify its value.
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);

            // Find the element representing BarWidthReduction (case‑insensitive search).
            var reductionElement = doc.Descendants()
                                      .FirstOrDefault(e => string.Equals(e.Name.LocalName, "BarWidthReduction", StringComparison.OrdinalIgnoreCase));

            if (reductionElement != null)
            {
                // Update the reduction value to 0.5 points.
                reductionElement.Value = "0.5";
                doc.Save(xmlPath);
                Console.WriteLine("Modified BarWidthReduction to 0.5 in XML.");
            }
            else
            {
                Console.WriteLine("BarWidthReduction element not found in XML; cannot modify.");
            }
        }
        else
        {
            Console.WriteLine("XML file not found; aborting modification step.");
            return;
        }

        // Step 3: Import the modified XML to create a new barcode generator instance.
        BarcodeGenerator modifiedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (modifiedGenerator == null)
        {
            Console.WriteLine("Failed to import generator from modified XML.");
            return;
        }

        // Save the barcode image generated with the modified settings.
        using (modifiedGenerator)
        {
            modifiedGenerator.Save(modifiedImagePath, BarCodeImageFormat.Png);
            Console.WriteLine($"Saved modified barcode image to: {modifiedImagePath}");
        }

        // Indicate that the process has completed.
        Console.WriteLine("Process completed. Compare the original and modified barcode images.");
    }
}