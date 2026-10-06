// Title: Codabar barcode generation, XML export, start symbol edit, and re‑import
// Description: This example creates a Codabar barcode, saves its configuration as XML, changes the start character from C to D, and regenerates the barcode using the modified XML.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and XML manipulation. It showcases the BarcodeGenerator class, its Parameters for Codabar settings, ExportToXml and ImportFromXml methods, and typical workflows where barcode specifications need to be stored, edited, or transferred. Ideal for developers integrating barcode creation with external configuration pipelines.
// Prompt: Generate a barcode, export its XML, edit CodabarStartSymbol attribute, and re‑import to change start character.
// Tags: codabar, xml, export, import, barcode generation, aspose.barcode, generation

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Codabar barcode, exporting its configuration to XML,
/// modifying the start symbol, and re‑importing the XML to produce an updated barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates temporary files, generates the original barcode,
    /// edits its XML, and saves the modified barcode image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for original and modified barcode images and XML files
        string originalImagePath = Path.Combine(tempFolder, "codabar_original.png");
        string originalXmlPath = Path.Combine(tempFolder, "codabar_original.xml");
        string modifiedXmlPath = Path.Combine(tempFolder, "codabar_modified.xml");
        string modifiedImagePath = Path.Combine(tempFolder, "codabar_modified.png");

        // Generate initial Codabar barcode with start/stop symbol C and save image + XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "123456"))
        {
            generator.Parameters.Barcode.Codabar.StartSymbol = CodabarSymbol.C;
            generator.Parameters.Barcode.Codabar.StopSymbol = CodabarSymbol.C;
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
            generator.ExportToXml(originalXmlPath);
        }

        // Load the exported XML, locate the StartSymbol element, and change its value to D
        XDocument doc = XDocument.Load(originalXmlPath);
        var startElement = doc.Descendants()
                              .FirstOrDefault(e => e.Name.LocalName.Equals("StartSymbol", StringComparison.OrdinalIgnoreCase));
        if (startElement != null)
        {
            startElement.Value = CodabarSymbol.D.ToString();
        }
        doc.Save(modifiedXmlPath);

        // Import the modified XML and generate a new barcode image with the updated start symbol
        var generatorModified = BarcodeGenerator.ImportFromXml(modifiedXmlPath);
        generatorModified.Save(modifiedImagePath, BarCodeImageFormat.Png);
        generatorModified.Dispose();

        // Output the locations of the generated files
        Console.WriteLine("Original barcode saved to: " + originalImagePath);
        Console.WriteLine("Modified barcode saved to: " + modifiedImagePath);
    }
}