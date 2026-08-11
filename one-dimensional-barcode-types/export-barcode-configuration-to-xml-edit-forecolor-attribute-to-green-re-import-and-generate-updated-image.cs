// Title: Export, edit, and re-import barcode configuration via XML to change bar color
// Description: Demonstrates exporting a barcode generator's settings to an XML file, modifying the bar color to green, re‑importing the configuration, and generating a PNG image with the updated appearance.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml together with Aspose.Drawing to persist, edit, and reuse barcode settings. Typical scenarios include batch processing, dynamic style changes, and integration with external configuration systems where developers need to programmatically adjust barcode properties such as colors, fonts, or symbology.
// Prompt: Export barcode configuration to XML, edit ForeColor attribute to green, re‑import, and generate updated image.
// Tags: barcode, export, import, xml, color, code128, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting a barcode configuration to XML, editing the bar color,
/// re‑importing the configuration, and generating an updated barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the export‑modify‑import workflow and saves the resulting PNG image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the temporary XML configuration and the final PNG image.
        string xmlPath = Path.Combine(Environment.CurrentDirectory, "barcode.xml");
        string imagePath = Path.Combine(Environment.CurrentDirectory, "barcode.png");

        // --------------------------------------------------------------------
        // 1. Create a barcode generator with sample data and export its settings.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Export the current generator configuration to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // 2. Load the exported XML, modify the BarColor element to "Green", and save.
        // --------------------------------------------------------------------
        XDocument doc = XDocument.Load(xmlPath);
        XElement barColorElement = doc.Root
            ?.Element("Parameters")
            ?.Element("Barcode")
            ?.Element("BarColor");

        if (barColorElement != null)
        {
            // Change the bar color value to green.
            barColorElement.Value = "Green";
        }

        // Persist the modified XML back to disk.
        doc.Save(xmlPath);

        // --------------------------------------------------------------------
        // 3. Import the modified configuration and generate the barcode image.
        // --------------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Generate a bitmap image using the updated settings.
            using (Bitmap bitmap = importedGenerator.GenerateBarCodeImage())
            {
                // Save the bitmap as a PNG file.
                bitmap.Save(imagePath, ImageFormat.Png);
            }
        }

        // Output the location of the generated image for verification.
        Console.WriteLine("Barcode image generated at: " + imagePath);
    }
}