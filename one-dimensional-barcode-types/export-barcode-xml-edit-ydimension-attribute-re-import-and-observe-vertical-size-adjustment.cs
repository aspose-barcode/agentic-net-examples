// Title: Export Barcode to XML, Modify YDimension, Re‑Import and Compare Height
// Description: Demonstrates exporting a barcode's configuration to XML, editing the YDimension attribute, re‑importing the modified XML, and observing the effect on the barcode's vertical size.
// Category-Description: This example belongs to the Aspose.BarCode configuration manipulation category, illustrating how to use BarcodeGenerator to export settings to XML, edit parameters such as YDimension, and re‑import the configuration. Developers working with barcode generation often need to persist, edit, or transfer barcode settings across environments; the key API classes include BarcodeGenerator, BarCodeImageFormat, and System.Xml.Linq for XML handling. Typical use cases involve batch processing, dynamic barcode styling, and integration with external configuration systems.
// Prompt: Export barcode XML, edit YDimension attribute, re‑import, and observe vertical size adjustment.
// Tags: pdf417, ydimension, xml, export, import, barcode, aspose.barcode, image-comparison

using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that shows how to export a barcode configuration to XML,
/// modify the YDimension attribute, re‑import the configuration, and compare
/// the resulting barcode image heights.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a PDF417 barcode, exports its state to XML,
    /// changes the YDimension, re‑imports the modified XML, and prints the heights of
    /// the original and modified barcode images.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary working directory for all generated files.
        // --------------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for original and modified images and XML files.
        string originalImagePath = Path.Combine(workDir, "original.png");
        string modifiedImagePath = Path.Combine(workDir, "modified.png");
        string xmlPath = Path.Combine(workDir, "state.xml");
        string modifiedXmlPath = Path.Combine(workDir, "state_modified.xml");

        // --------------------------------------------------------------------
        // 2. Generate the initial barcode and export its configuration to XML.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Aspose.Barcode PDF417 Example"))
        {
            // Optionally set YDimension if the property is available in the used version.
            // generator.Parameters.Barcode.YDimension.Point = 2f; // Uncomment if supported

            // Save the barcode image as PNG.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the generator's state (including parameters) to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // 3. Load the exported XML, modify (or add) the YDimension element.
        // --------------------------------------------------------------------
        XDocument doc = XDocument.Load(xmlPath);
        XElement barcodeElem = doc.Descendants("Barcode").FirstOrDefault();
        if (barcodeElem != null)
        {
            XElement yDimElem = barcodeElem.Element("YDimension");
            if (yDimElem != null)
            {
                // Update existing YDimension value.
                yDimElem.Value = "5";
            }
            else
            {
                // Add YDimension element with the new value if it does not exist.
                barcodeElem.Add(new XElement("YDimension", "5"));
            }
        }
        // Save the modified XML to a new file.
        doc.Save(modifiedXmlPath);

        // --------------------------------------------------------------------
        // 4. Import the modified XML back into a BarcodeGenerator and create a new image.
        // --------------------------------------------------------------------
        BarcodeGenerator modifiedGenerator = BarcodeGenerator.ImportFromXml(modifiedXmlPath);
        modifiedGenerator.Save(modifiedImagePath, BarCodeImageFormat.Png);

        // --------------------------------------------------------------------
        // 5. Load both images to compare their heights (vertical size).
        // --------------------------------------------------------------------
        int originalHeight;
        int modifiedHeight;
        using (Bitmap bmpOriginal = (Bitmap)Image.FromFile(originalImagePath))
        {
            originalHeight = bmpOriginal.Height;
        }
        using (Bitmap bmpModified = (Bitmap)Image.FromFile(modifiedImagePath))
        {
            modifiedHeight = bmpModified.Height;
        }

        // --------------------------------------------------------------------
        // 6. Output the comparison results and the location of the temporary files.
        // --------------------------------------------------------------------
        Console.WriteLine($"Original barcode height: {originalHeight}px");
        Console.WriteLine($"Modified barcode height (after YDimension change): {modifiedHeight}px");
        Console.WriteLine($"Working directory: {workDir}");
    }
}