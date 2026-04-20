using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define file names
        string xmlPath = "barcode.xml";
        string beforeImage = "barcode_before.png";
        string afterImage = "barcode_after.png";

        // Step 1: Create a barcode, save image and export its settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optional: set a known XDimension to see effect clearly
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the original barcode image
            generator.Save(beforeImage);

            // Export settings to XML file
            bool exported = generator.ExportToXml(xmlPath);
            if (!exported)
            {
                Console.WriteLine("Failed to export barcode settings to XML.");
                return;
            }
        }

        // Step 2: Load the XML, modify the YDimension attribute
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("XML file not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);

        // Find any element that has a YDimension attribute; if not found, add it to the root
        XElement elementWithY = null;
        foreach (var elem in doc.Descendants())
        {
            XAttribute attr = elem.Attribute("YDimension");
            if (attr != null)
            {
                elementWithY = elem;
                break;
            }
        }

        if (elementWithY != null)
        {
            // Change existing YDimension value
            elementWithY.SetAttributeValue("YDimension", "3");
        }
        else
        {
            // Add YDimension attribute to the root element with a new value
            doc.Root.SetAttributeValue("YDimension", "3");
        }

        // Save the modified XML back to the same file
        doc.Save(xmlPath);

        // Step 3: Re‑import the barcode from the modified XML and save the new image
        using (var generatorFromXml = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode after re‑import
            generatorFromXml.Save(afterImage);

            // Output image height to demonstrate vertical size change (if any)
            float heightBefore = generatorFromXml.Parameters.ImageHeight.Point;
            Console.WriteLine($"Image height after re‑import: {heightBefore} points");
        }

        Console.WriteLine("Process completed. Check the generated PNG files.");
    }
}