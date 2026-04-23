using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Paths for temporary XML and final image
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "codabar.xml");
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "codabar_modified.png");

        // 1. Create a Codabar generator with default stop symbol (A) and export its settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "A123456A"))
        {
            generator.ExportToXml(xmlPath);
        }

        // 2. Load the exported XML, modify the CodabarStopSymbol to B, and save the XML back
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlPath);
        // The XML element name for stop symbol is "StopSymbol" inside "CodabarParameters"
        XElement stopSymbolElement = doc.Root?
            .Descendants("CodabarParameters")
            .Elements("StopSymbol")
            .FirstOrDefault();

        if (stopSymbolElement == null)
        {
            Console.WriteLine("StopSymbol element not found in XML.");
            return;
        }

        stopSymbolElement.Value = "B";
        doc.Save(xmlPath);

        // 3. Import the modified XML to create a new generator instance
        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Ensure the code text uses the new stop symbol (B)
            modifiedGenerator.CodeText = "A123456B";

            // 4. Generate and save the barcode image with the new stop character
            modifiedGenerator.Save(imagePath);
        }

        Console.WriteLine($"Barcode image saved to: {imagePath}");
    }
}