using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // File paths
        string xmlPath = "barcode_state.xml";
        string originalImagePath = "barcode_original.png";
        string modifiedImagePath = "barcode_modified.png";

        // Create a barcode generator, set code text and initial BarWidthReduction (default 0)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Ensure BarWidthReduction is explicitly set to 0 points
            generator.Parameters.Barcode.BarWidthReduction.Point = 0f;

            // Save the original barcode image
            generator.Save(originalImagePath);

            // Export the barcode settings to XML
            generator.ExportToXml(xmlPath);
        }

        // Modify the exported XML to change BarWidthReduction to 0.5 points
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);
            XElement reductionElement = doc.Descendants("BarWidthReduction").FirstOrDefault();
            if (reductionElement != null)
            {
                reductionElement.Value = "0.5";
                doc.Save(xmlPath);
            }
            else
            {
                Console.WriteLine("BarWidthReduction element not found in XML.");
                return;
            }
        }
        else
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // Import the modified XML and generate a new barcode image to observe the change
        using (var modifiedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode image with the modified BarWidthReduction
            modifiedGenerator.Save(modifiedImagePath);
        }

        Console.WriteLine("Original barcode saved to: " + Path.GetFullPath(originalImagePath));
        Console.WriteLine("Modified barcode saved to: " + Path.GetFullPath(modifiedImagePath));
    }
}