using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a barcode, exporting its definition to XML,
/// modifying the XML, and regenerating the barcode with the updated settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define temporary file paths for the original and modified barcode images
        // and their corresponding XML representations.
        // --------------------------------------------------------------------
        string tempDir = Path.GetTempPath();
        string pngPathOriginal = Path.Combine(tempDir, "barcode_original.png");
        string xmlPath = Path.Combine(tempDir, "barcode.xml");
        string xmlPathModified = Path.Combine(tempDir, "barcode_modified.xml");
        string pngPathModified = Path.Combine(tempDir, "barcode_modified.png");

        // --------------------------------------------------------------------
        // Generate a barcode image and export its definition to XML.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a specific bar height (vertical size) for the barcode.
            generator.Parameters.Barcode.BarHeight.Point = 50f;

            // Save the barcode as a PNG image.
            generator.Save(pngPathOriginal);

            // Export the barcode definition to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Load the exported XML, modify (or add) the YDimension attribute,
        // and save the updated XML to a new file.
        // --------------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            XDocument doc = XDocument.Load(xmlPath);
            XElement root = doc.Root;

            if (root != null)
            {
                // Attempt to locate an existing YDimension attribute.
                XAttribute yAttr = root.Attribute("YDimension");

                if (yAttr != null)
                {
                    // Update the existing attribute value.
                    yAttr.Value = "200";
                }
                else
                {
                    // Add a new YDimension attribute with the desired value.
                    root.SetAttributeValue("YDimension", "200");
                }
            }

            // Save the modified XML definition.
            doc.Save(xmlPathModified);
        }
        else
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Import the modified XML definition and generate a new barcode image.
        // --------------------------------------------------------------------
        if (File.Exists(xmlPathModified))
        {
            using (var generatorFromXml = BarcodeGenerator.ImportFromXml(xmlPathModified))
            {
                generatorFromXml.Save(pngPathModified);
            }
        }
        else
        {
            Console.WriteLine("Modified XML file not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Load both the original and modified images to compare their heights.
        // --------------------------------------------------------------------
        if (File.Exists(pngPathOriginal) && File.Exists(pngPathModified))
        {
            using (var imgOriginal = Image.FromFile(pngPathOriginal))
            using (var imgModified = Image.FromFile(pngPathModified))
            {
                Console.WriteLine($"Original image height: {imgOriginal.Height} pixels");
                Console.WriteLine($"Modified image height: {imgModified.Height} pixels");
            }
        }
        else
        {
            Console.WriteLine("One of the generated images was not found.");
        }
    }
}