// Title: Export Barcode XML State, Modify Dimensions, and Re‑Render
// Description: Demonstrates exporting a barcode's configuration to XML, editing dimension properties, and regenerating the barcode to observe size changes.
// Category-Description: Shows how to use Aspose.BarCode's BarcodeGenerator to export its state to XML, manipulate parameters such as XDimension (and attempt YDimension), and import the modified XML back into a generator. This example belongs to the "Barcode Generation and Configuration" category, illustrating typical workflows for persisting, editing, and re‑creating barcodes using the ExportToXml and ImportFromXml APIs.
// Prompt: Export barcode XML state, edit YDimension, re‑render to observe size change.
// Tags: barcode, qrcode, xdimension, ydimension, xml, export, import, aspose.barcode, image, png, generation

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that exports a QR code's configuration to XML, attempts to modify dimensions,
/// and re‑generates the barcode to illustrate the effect of changed parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates an original barcode, exports its state, edits dimension values in the XML,
    /// imports the modified XML, and saves both original and updated images.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output directory
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for original image, modified image, and exported XML
        string originalPath = Path.Combine(outputDir, "original.png");
        string modifiedPath = Path.Combine(outputDir, "modified.png");
        string xmlPath = Path.Combine(outputDir, "state.xml");

        // --------------------------------------------------------------------
        // Create a barcode generator, set initial XDimension, and save original image
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello"))
        {
            // Set the X dimension (module size) to 2 points
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated QR code as PNG
            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Export the generator's current state to an XML file
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Load the exported XML document
        // --------------------------------------------------------------------
        XDocument doc = XDocument.Load(xmlPath);

        // --------------------------------------------------------------------
        // Attempt to edit YDimension (not supported by this API)
        // --------------------------------------------------------------------
        var yDimElement = doc.Descendants("YDimension").FirstOrDefault();
        if (yDimElement != null)
        {
            yDimElement.Value = "5";
            Console.WriteLine("YDimension element found and modified.");
        }
        else
        {
            Console.WriteLine("YDimension element not found; property not supported in this API.");
        }

        // --------------------------------------------------------------------
        // Edit XDimension to demonstrate size change
        // --------------------------------------------------------------------
        var xDimElement = doc.Descendants("XDimension").FirstOrDefault();
        if (xDimElement != null)
        {
            xDimElement.Value = "4";
            Console.WriteLine("XDimension element modified from 2 to 4.");
        }
        else
        {
            Console.WriteLine("XDimension element not found in XML.");
        }

        // --------------------------------------------------------------------
        // Import the modified XML and generate the updated barcode
        // --------------------------------------------------------------------
        using (var ms = new MemoryStream())
        {
            // Save the edited XML into a memory stream
            doc.Save(ms);
            ms.Position = 0;

            // Create a new generator from the modified XML
            using (var generatorModified = BarcodeGenerator.ImportFromXml(ms))
            {
                // Save the updated barcode image
                generatorModified.Save(modifiedPath, BarCodeImageFormat.Png);
            }
        }

        // --------------------------------------------------------------------
        // Output result file locations
        // --------------------------------------------------------------------
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Modified barcode saved to: {modifiedPath}");
        Console.WriteLine("Processing completed.");
    }
}