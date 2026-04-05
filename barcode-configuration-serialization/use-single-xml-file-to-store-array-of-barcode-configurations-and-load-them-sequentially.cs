using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the XML file that contains multiple barcode configurations.
        const string xmlFilePath = "barcodes.xml";

        if (!File.Exists(xmlFilePath))
        {
            Console.WriteLine($"XML file not found: {xmlFilePath}");
            return;
        }

        // Load the XML document.
        XDocument doc = XDocument.Load(xmlFilePath);
        int index = 0;

        // Each <BarcodeConfig> element should contain a full BarcodeGenerator XML definition
        // as produced by BarcodeGenerator.ExportToXml().
        foreach (XElement configElement in doc.Root.Elements("BarcodeConfig"))
        {
            // Convert the XML fragment to a memory stream.
            byte[] xmlBytes = Encoding.UTF8.GetBytes(configElement.ToString());
            using (var xmlStream = new MemoryStream(xmlBytes))
            {
                // Import the barcode configuration from the XML stream.
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Save the generated barcode image.
                    string outputFile = $"barcode_{index}.png";
                    generator.Save(outputFile);
                    Console.WriteLine($"Generated: {outputFile}");
                }
            }
            index++;
        }

        Console.WriteLine("All barcodes processed.");
    }
}