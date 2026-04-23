using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const string xmlFile = "barcode_state.xml";
        const string outputImage = "updated_barcode.png";

        // Step 1: Create a barcode generator with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Export the current barcode settings to XML
            bool exported = generator.ExportToXml(xmlFile);
            if (!exported)
            {
                Console.WriteLine("Failed to export barcode settings to XML.");
                return;
            }
        }

        // Step 2: Modify the exported XML – set BarWidthReduction to 10 (points)
        if (!File.Exists(xmlFile))
        {
            Console.WriteLine($"XML file '{xmlFile}' not found.");
            return;
        }

        XDocument doc = XDocument.Load(xmlFile);
        // The XML element name matches the property name
        XElement reductionElement = doc.Root?.Descendants("BarWidthReduction").FirstOrDefault();
        if (reductionElement == null)
        {
            Console.WriteLine("BarWidthReduction element not found in XML.");
            return;
        }
        reductionElement.Value = "10"; // 10 points (approximately 10 percent reduction)

        // Save the modified XML back to the same file
        doc.Save(xmlFile);

        // Step 3: Re‑import the barcode generator from the modified XML
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlFile))
        {
            if (importedGenerator == null)
            {
                Console.WriteLine("Failed to import barcode from XML.");
                return;
            }

            // Generate and save the updated barcode image
            importedGenerator.Save(outputImage);
        }

        Console.WriteLine($"Updated barcode saved to '{outputImage}'.");
    }
}