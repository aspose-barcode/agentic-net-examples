// Title: Export barcode XML, modify YDimension, and compare image sizes
// Description: Demonstrates exporting a barcode generator's state to XML, changing the YDimension parameter, re‑importing the XML, and generating a new barcode image to see the effect on size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and configuration category. It shows how to use BarcodeGenerator, ExportToXml, ImportFromXml, and adjust dimensional parameters such as XDimension and YDimension. Typical use cases include persisting barcode settings, batch editing via XML, and verifying visual impact of parameter changes. Developers often need to serialize generator settings, edit them programmatically or manually, and regenerate barcodes without recreating the generator from scratch.
// Prompt: Export barcode XML state, edit YDimension, re‑render to observe size change.
// Tags: barcode, xml, ydimension, export, import, generation, png, aspose.barcode, aspnet, image-size

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode generator's configuration to XML,
/// modifying the YDimension parameter, and regenerating the barcode to
/// observe the size change.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a PDF417 barcode, exports its state,
    /// edits YDimension in the XML, re‑imports the state, and compares the original
    /// and modified image dimensions.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all generated files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the original and modified images and the XML state file
        string originalImagePath = Path.Combine(tempFolder, "original.png");
        string modifiedImagePath = Path.Combine(tempFolder, "modified.png");
        string xmlPath = Path.Combine(tempFolder, "generator.xml");

        // Generate the initial barcode (PDF417) with a specific XDimension and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "1234567890"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
            // Export the generator's current configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Load the exported XML, locate the YDimension element, and set it to 5 (or add it if missing)
        XDocument doc = XDocument.Load(xmlPath);
        XElement yDimElement = doc.Descendants("YDimension").FirstOrDefault();
        if (yDimElement == null)
        {
            // If YDimension is not present, create it under the Barcode element
            XElement barcodeElem = doc.Descendants("Barcode").FirstOrDefault();
            if (barcodeElem != null)
            {
                yDimElement = new XElement("YDimension", "5");
                barcodeElem.Add(yDimElement);
            }
        }
        else
        {
            // Update the existing YDimension value
            yDimElement.Value = "5";
        }
        // Save the modified XML back to disk
        doc.Save(xmlPath);

        // Import the modified XML state into a new generator and save the updated barcode image
        using (var generatorModified = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            generatorModified.Save(modifiedImagePath, BarCodeImageFormat.Png);
        }

        // Load and display the dimensions of the original barcode image
        using (var bmpOriginal = new Bitmap(originalImagePath))
        {
            Console.WriteLine($"Original image size: {bmpOriginal.Width}x{bmpOriginal.Height}");
        }

        // Load and display the dimensions of the modified barcode image
        using (var bmpModified = new Bitmap(modifiedImagePath))
        {
            Console.WriteLine($"Modified image size: {bmpModified.Width}x{bmpModified.Height}");
        }

        // Optional cleanup: delete the temporary folder and its contents
        // Directory.Delete(tempFolder, true);
    }
}