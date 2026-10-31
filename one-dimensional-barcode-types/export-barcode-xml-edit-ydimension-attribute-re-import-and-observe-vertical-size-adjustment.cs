// Title: Export Barcode to XML, Modify BarHeight, and Re‑Import to Adjust Size
// Description: Demonstrates exporting a barcode definition to XML, editing the BarHeight attribute, re‑importing the XML, and generating a new barcode image with the updated vertical size.
// Category-Description: This example belongs to the Aspose.BarCode XML manipulation category. It shows how to use BarcodeGenerator to create a barcode, export its definition to XML, modify properties such as BarHeight, and import the modified XML back into a generator. Developers working with dynamic barcode layouts, custom sizing, or persisting barcode configurations will find these APIs—ExportToXml, ImportFromXml, and related XML handling—particularly useful.
/// Prompt: Export barcode XML, edit YDimension attribute, re‑import, and observe vertical size adjustment.
/// Tags: barcode, xml, export, import, barheight, code128, aspose.barcode, image, png

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting a barcode to XML, modifying its vertical size (BarHeight),
/// re‑importing the XML, and comparing the resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode generation, XML manipulation,
    /// and image size comparison workflow.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 0. Prepare a temporary working directory for all files.
        // ------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for original, modified images and the XML definition.
        string originalImagePath = Path.Combine(workDir, "original.png");
        string modifiedImagePath = Path.Combine(workDir, "modified.png");
        string xmlPath = Path.Combine(workDir, "barcode.xml");

        // ------------------------------------------------------------
        // 1. Generate the original barcode and export its definition to XML.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the barcode as a PNG image.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
            // Export the barcode settings to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // 2. Load the exported XML and change the BarHeight (vertical size).
        // ------------------------------------------------------------
        XDocument doc = XDocument.Load(xmlPath);
        XElement barcodeElem = doc.Descendants("Barcode").FirstOrDefault();
        if (barcodeElem == null)
        {
            Console.WriteLine("Barcode element not found in XML.");
            return;
        }

        // Locate or create the BarHeight element and set its value to 50.
        XElement barHeightElem = barcodeElem.Element("BarHeight");
        if (barHeightElem == null)
        {
            barHeightElem = new XElement("BarHeight", "50");
            barcodeElem.Add(barHeightElem);
        }
        else
        {
            barHeightElem.Value = "50";
        }

        // Save the modified XML back to disk.
        doc.Save(xmlPath);

        // ------------------------------------------------------------
        // 3. Import the modified XML and generate a new barcode image.
        // ------------------------------------------------------------
        using (var generatorModified = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            generatorModified.Save(modifiedImagePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 4. Load both images to compare their heights (vertical dimensions).
        // ------------------------------------------------------------
        int originalHeight;
        int modifiedHeight;

        using (var bmpOriginal = new Bitmap(originalImagePath))
        {
            originalHeight = bmpOriginal.Height;
        }

        using (var bmpModified = new Bitmap(modifiedImagePath))
        {
            modifiedHeight = bmpModified.Height;
        }

        // Output the results.
        Console.WriteLine($"Original barcode height: {originalHeight}px");
        Console.WriteLine($"Modified barcode height (BarHeight=50): {modifiedHeight}px");
        Console.WriteLine($"Work directory: {workDir}");
    }
}