using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // File paths
        string xmlPath = "codabar.xml";
        string originalImagePath = "codabar_original.png";
        string modifiedImagePath = "codabar_modified.png";

        // 1. Generate a Codabar barcode and export its settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            // Save the original barcode image
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export generator settings to XML file
            generator.ExportToXml(xmlPath);
        }

        // 2. Edit the exported XML to change the start symbol
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);
        var startSymbolElement = doc.Descendants("CodabarStartSymbol").FirstOrDefault();
        if (startSymbolElement != null)
        {
            // Change start symbol from 'A' to 'B'
            startSymbolElement.Value = "B";
        }
        else
        {
            Console.WriteLine("CodabarStartSymbol element not found in XML.");
            return;
        }
        doc.Save(xmlPath);

        // 3. Re‑import the modified XML and generate a new barcode with the new start symbol
        using (var generatorModified = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Update the code text to match the new start/stop symbols
            generatorModified.CodeText = "B123456B";

            // Save the modified barcode image
            generatorModified.Save(modifiedImagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generation and XML modification completed.");
    }
}