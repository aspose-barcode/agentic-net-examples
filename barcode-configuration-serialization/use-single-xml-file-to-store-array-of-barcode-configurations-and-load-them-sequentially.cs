// Title: Load multiple barcode configurations from a single XML file
// Description: Demonstrates storing several barcode generator settings in one XML file and generating each barcode sequentially.
// Prompt: Use a single XML file to store an array of barcode configurations and load them sequentially.
// Tags: barcode, xml, configuration, generation, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that reads an array of barcode configurations from a single XML file
/// and generates each barcode as a separate PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a sample XML file if missing,
    /// loads each <c>BarcodeGenerator</c> configuration, and saves the resulting images.
    /// </summary>
    static void Main()
    {
        // Path to the XML file that holds multiple barcode configurations.
        const string xmlFilePath = "barcodes.xml";

        // If the XML file does not exist, create it with a few sample configurations.
        if (!File.Exists(xmlFilePath))
        {
            // Prepare the first sample barcode generator (Code128).
            using (var gen1 = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                gen1.Parameters.Barcode.BarColor = Color.Blue;

                // Export the first generator's configuration to XML.
                using (var ms1 = new MemoryStream())
                {
                    gen1.ExportToXml(ms1);
                    string xml1 = Encoding.UTF8.GetString(ms1.ToArray());

                    // Prepare the second sample barcode generator (QR).
                    using (var gen2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
                    {
                        gen2.Parameters.Barcode.BarColor = Color.Green;

                        // Export the second generator's configuration to XML.
                        using (var ms2 = new MemoryStream())
                        {
                            gen2.ExportToXml(ms2);
                            string xml2 = Encoding.UTF8.GetString(ms2.ToArray());

                            // Combine both <BarcodeGenerator> fragments under a single root element.
                            string combinedXml = $"<Barcodes>{xml1}{xml2}</Barcodes>";

                            // Write the combined XML to the file system.
                            File.WriteAllText(xmlFilePath, combinedXml, Encoding.UTF8);
                        }
                    }
                }
            }
        }

        // Load the combined XML file into an XmlDocument.
        var doc = new XmlDocument();
        doc.Load(xmlFilePath);

        // Select each <BarcodeGenerator> node from the document.
        var generatorNodes = doc.SelectNodes("//BarcodeGenerator");
        if (generatorNodes == null || generatorNodes.Count == 0)
        {
            Console.WriteLine("No barcode configurations found in the XML file.");
            return;
        }

        // Iterate over each configuration node and generate the corresponding barcode image.
        int index = 1;
        foreach (XmlNode node in generatorNodes)
        {
            // Convert the node back to its XML representation.
            string nodeXml = node.OuterXml;

            // Load the configuration into a new BarcodeGenerator instance.
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(nodeXml)))
            using (var generator = BarcodeGenerator.ImportFromXml(ms))
            {
                // Define the output file name for the generated barcode.
                string outputFile = $"barcode_{index}.png";

                // Save the barcode image as PNG.
                generator.Save(outputFile);
                Console.WriteLine($"Generated barcode saved to: {outputFile}");
            }

            index++;
        }
    }
}