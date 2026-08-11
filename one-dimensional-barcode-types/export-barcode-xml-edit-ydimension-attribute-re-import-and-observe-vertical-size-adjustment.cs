// Title: Export barcode to XML, modify YDimension, re‑import and compare size
// Description: Shows how to export a barcode's settings to XML, edit the YDimension (BarHeight) attribute, re‑import the modified XML, and observe the resulting change in the barcode's vertical size.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It demonstrates using BarcodeGenerator to create a barcode, exporting its configuration with ExportToXml, editing the XML manually, and re‑creating a generator via ImportFromXml. Typical use cases include persisting barcode settings, batch editing, or integrating with external configuration systems. Developers often work with BarcodeGenerator, BarCodeImageFormat, and XML manipulation classes for such scenarios.
// Prompt: Export barcode XML, edit YDimension attribute, re‑import, and observe vertical size adjustment.
// Tags: barcode, xml, export, import, ydimension, barheight, aspose.barcode, image, generation

using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting a barcode to XML, modifying the YDimension (BarHeight) attribute,
/// re‑importing the XML, and observing the effect on the barcode's vertical size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that performs the export‑modify‑import workflow and prints image heights.
    /// </summary>
    static void Main()
    {
        // Paths for temporary files
        string originalXml = "barcode_original.xml";
        string modifiedXml = "barcode_modified.xml";
        string originalImg = "barcode_original.png";
        string modifiedImg = "barcode_modified.png";

        // 1. Create a barcode generator and set a vertical size (BarHeight)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set bar height (vertical size) – this will be reflected in the exported XML
            generator.Parameters.Barcode.BarHeight.Point = 50f; // 50 points height

            // Export generator settings to XML
            generator.ExportToXml(originalXml);

            // Save the original barcode image for comparison
            generator.Save(originalImg, BarCodeImageFormat.Png);
        }

        // 2. Load the exported XML, modify the YDimension attribute (simulated by BarHeight)
        if (!File.Exists(originalXml))
        {
            Console.WriteLine("Exported XML not found.");
            return;
        }

        XDocument doc = XDocument.Load(originalXml);
        // The XML structure contains a BarHeight element; we treat it as YDimension for this demo
        XElement barHeightElement = doc.Root?.Descendants("BarHeight").FirstOrDefault();
        if (barHeightElement != null)
        {
            // Change the value to a larger height (e.g., 80 points)
            barHeightElement.Value = "80";
        }
        else
        {
            // If not present, add it under the Barcode element
            XElement barcodeElem = doc.Root?.Descendants("Barcode").FirstOrDefault();
            if (barcodeElem != null)
            {
                barcodeElem.Add(new XElement("BarHeight", "80"));
            }
        }

        // Save the modified XML
        doc.Save(modifiedXml);

        // 3. Re‑import the barcode from the modified XML
        if (!File.Exists(modifiedXml))
        {
            Console.WriteLine("Modified XML not found.");
            return;
        }

        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(modifiedXml))
        {
            // Save the barcode generated from the modified settings
            modifiedGenerator.Save(modifiedImg, BarCodeImageFormat.Png);

            // 4. Observe vertical size adjustment by checking image height
            using (Bitmap bmp = modifiedGenerator.GenerateBarCodeImage())
            {
                Console.WriteLine($"Modified barcode image height (pixels): {bmp.Height}");
            }
        }

        // 5. Also display original image height for comparison
        using (var originalGenerator = BarcodeGenerator.ImportFromXml(originalXml))
        {
            using (Bitmap bmp = originalGenerator.GenerateBarCodeImage())
            {
                Console.WriteLine($"Original barcode image height (pixels): {bmp.Height}");
            }
        }
    }
}