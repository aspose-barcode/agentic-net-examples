using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // File paths
        string xmlPath = "barcodeConfig.xml";
        string originalImagePath = "barcode_original.png";
        string updatedImagePath = "barcode_updated.png";

        // Create a barcode, save image and export configuration to XML
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: set initial bar color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save original barcode image
            generator.Save(originalImagePath);

            // Export configuration to XML file
            generator.ExportToXml(xmlPath);
        }

        // Load the exported XML, modify the ForeColor attribute to green
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);
            XElement root = doc.Root;
            if (root != null)
            {
                XAttribute foreColorAttr = root.Attribute("ForeColor");
                if (foreColorAttr != null)
                {
                    foreColorAttr.Value = "Green";
                }
                else
                {
                    root.SetAttributeValue("ForeColor", "Green");
                }
                doc.Save(xmlPath);
            }
        }
        else
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // Import the modified configuration and generate updated barcode image
        using (BarcodeGenerator updatedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the updated barcode image
            updatedGenerator.Save(updatedImagePath);
        }

        Console.WriteLine("Barcode generation completed.");
    }
}