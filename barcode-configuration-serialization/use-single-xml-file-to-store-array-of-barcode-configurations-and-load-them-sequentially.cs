using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Path for the combined XML file that will hold all barcode configurations
        string xmlFilePath = "BarcodesConfig.xml";

        // Sample barcode configurations (symbology type and code text)
        var configs = new List<(BaseEncodeType type, string text)>
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "Hello World"),
            (EncodeTypes.EAN13, "123456789012")
        };

        // -----------------------------------------------------------------
        // Step 1: Create individual BarcodeGenerator instances, export each
        // to XML (in memory) and combine them into a single XML document.
        // -----------------------------------------------------------------
        var root = new XElement("Barcodes");

        foreach (var (type, text) in configs)
        {
            using (var generator = new BarcodeGenerator(type, text))
            {
                // Optional: set image size using unit members
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Export the generator's settings to a memory stream
                using (var ms = new MemoryStream())
                {
                    bool exported = generator.ExportToXml(ms);
                    if (!exported)
                    {
                        Console.WriteLine($"Failed to export configuration for {type}.");
                        continue;
                    }

                    ms.Position = 0;
                    // Load the exported XML fragment as an XElement
                    XElement generatorXml = XElement.Load(ms);
                    // Add the fragment to the combined document
                    root.Add(generatorXml);
                }
            }
        }

        // Save the combined XML document to a file
        var combinedDoc = new XDocument(root);
        combinedDoc.Save(xmlFilePath);
        Console.WriteLine($"Combined configuration saved to '{xmlFilePath}'.");

        // -----------------------------------------------------------------
        // Step 2: Load the combined XML file, parse each barcode configuration,
        // import it using ImportFromXml, generate the barcode image and save it.
        // -----------------------------------------------------------------
        if (!File.Exists(xmlFilePath))
        {
            Console.WriteLine("Configuration file not found.");
            return;
        }

        var loadedDoc = XDocument.Load(xmlFilePath);
        int index = 1;
        foreach (var generatorElement in loadedDoc.Root.Elements())
        {
            // Convert the XElement back to a stream for ImportFromXml
            using (var ms = new MemoryStream())
            {
                generatorElement.Save(ms);
                ms.Position = 0;

                // Import the barcode generator from the XML fragment
                using (var generator = BarcodeGenerator.ImportFromXml(ms))
                {
                    if (generator == null)
                    {
                        Console.WriteLine($"Failed to import configuration #{index}.");
                        continue;
                    }

                    // Ensure the image size is set (optional, can be omitted if already set)
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    // Save the generated barcode image
                    string outputFile = $"Barcode_{index}.png";
                    generator.Save(outputFile);
                    Console.WriteLine($"Generated barcode saved to '{outputFile}'.");
                }
            }

            index++;
        }

        Console.WriteLine("Processing completed.");
    }
}