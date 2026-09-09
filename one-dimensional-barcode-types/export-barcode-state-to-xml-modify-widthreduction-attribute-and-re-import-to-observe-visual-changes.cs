// Title: Export and Modify Barcode Width Reduction via XML
// Description: Demonstrates exporting a barcode generator's state to XML, changing the BarWidthReduction attribute, and re‑importing to produce a modified barcode image.
// Category-Description: This example belongs to the Aspose.BarCode state‑management category, illustrating how to persist a BarcodeGenerator configuration to XML, edit specific parameters (such as BarWidthReduction), and reload the configuration. It showcases key API classes like BarcodeGenerator, ExportToXml, and ImportFromXml, which are commonly used for saving, sharing, or programmatically adjusting barcode settings in batch processing or CI pipelines.
// Prompt: Export barcode state to XML, modify WidthReduction attribute, and re‑import to observe visual changes.
// Tags: barcode, widthreduction, xml, export, import, aspose.barcode, code128, png

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a barcode generator's configuration to XML,
/// modifies the BarWidthReduction value, and re‑imports the configuration to
/// generate a barcode with the updated visual appearance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑modify‑import workflow
    /// and writes the paths of the generated images to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Prepare a temporary working folder for all generated files.
        // --------------------------------------------------------------------
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for original and modified images and XML files.
        string originalImagePath = Path.Combine(workFolder, "Original.png");
        string modifiedImagePath = Path.Combine(workFolder, "Modified.png");
        string originalXmlPath   = Path.Combine(workFolder, "Generator.xml");
        string modifiedXmlPath   = Path.Combine(workFolder, "GeneratorModified.xml");

        // --------------------------------------------------------------------
        // Create the initial barcode with no width reduction and save it.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 10;
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 0; // No reduction
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(originalXmlPath);
        }

        // --------------------------------------------------------------------
        // Load the exported XML, change the BarWidthReduction value, and save.
        // --------------------------------------------------------------------
        if (File.Exists(originalXmlPath))
        {
            XDocument doc = XDocument.Load(originalXmlPath);
            XElement reductionElement = doc.Root
                ?.Descendants("BarWidthReduction")
                .FirstOrDefault();

            if (reductionElement != null)
            {
                // Update existing element.
                reductionElement.Value = "4";
            }
            else
            {
                // Element missing – create it under the <Barcode> node.
                XElement barcodeNode = doc.Root?.Descendants("Barcode").FirstOrDefault();
                if (barcodeNode != null)
                {
                    barcodeNode.Add(new XElement("BarWidthReduction", "4"));
                }
            }

            doc.Save(modifiedXmlPath);
        }
        else
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Import the modified XML and generate a barcode with the new width reduction.
        // --------------------------------------------------------------------
        if (File.Exists(modifiedXmlPath))
        {
            BarcodeGenerator modifiedGenerator = BarcodeGenerator.ImportFromXml(modifiedXmlPath);
            modifiedGenerator.Save(modifiedImagePath, BarCodeImageFormat.Png);
        }
        else
        {
            Console.WriteLine("Modified XML file not found.");
            return;
        }

        // Output the locations of the generated images.
        Console.WriteLine("Original barcode image: " + originalImagePath);
        Console.WriteLine("Modified barcode image: " + modifiedImagePath);
    }
}