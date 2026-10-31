// Title: Export barcode state to XML, modify width reduction, and re‑import
// Description: Demonstrates generating a Code128 barcode, exporting its generator state to XML, editing the BarWidthReduction attribute, and regenerating the barcode to observe visual changes.
// Category-Description: This example belongs to the Aspose.BarCode generation and state management category. It illustrates using BarcodeGenerator, its Parameters, ExportToXml, and ImportFromXml methods to persist and modify barcode settings. Typical scenarios include saving barcode configurations, batch editing, or applying custom adjustments without recreating the generator. Developers often need to serialize generator state, edit XML, and reload it to produce consistent barcodes.
// Prompt: Export barcode state to XML, modify WidthReduction attribute, and re‑import to observe visual changes.
// Tags: barcode, code128, widthreduction, xml, export, import, aspose.barcode, generation, state

using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode generator's state to XML, modifying the
/// BarWidthReduction attribute, and re‑importing the state to generate a
/// visually altered barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates an original barcode, exports its
    /// state, edits the XML, and regenerates the barcode with the modified
    /// width reduction setting.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a temporary working directory for all output files.
        string basePath = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(basePath);

        // Paths for the original barcode image, modified image, and XML state file.
        string originalImagePath = Path.Combine(basePath, "Code128_Original.png");
        string modifiedImagePath = Path.Combine(basePath, "Code128_Modified.png");
        string xmlPath = Path.Combine(basePath, "generatorState.xml");

        // ------------------------------------------------------------
        // Generate the original barcode with no width reduction.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set the X‑dimension (module width) to 10 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            // Ensure no bar width reduction is applied initially.
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 0;

            // Save the barcode image and export the generator state to XML.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Load the exported XML and modify the BarWidthReduction value.
        // ------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);
            // Locate the BarWidthReduction element within the XML.
            XElement reductionElement = doc.Root?.Descendants("BarWidthReduction").FirstOrDefault();
            if (reductionElement != null)
            {
                // Change the reduction from 0 to 4 pixels.
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
        }

        // ------------------------------------------------------------
        // Import the modified state and generate the barcode again.
        // ------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            using (var generator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Save the barcode image that reflects the new width reduction.
                generator.Save(modifiedImagePath, BarCodeImageFormat.Png);
            }
        }

        // Output the locations of the generated files.
        Console.WriteLine($"Original barcode saved to: {originalImagePath}");
        Console.WriteLine($"Modified barcode saved to: {modifiedImagePath}");
        Console.WriteLine($"XML state file: {xmlPath}");
    }
}