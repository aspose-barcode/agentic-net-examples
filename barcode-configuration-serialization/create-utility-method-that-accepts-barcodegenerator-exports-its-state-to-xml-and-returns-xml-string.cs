// Title: Export BarcodeGenerator state to XML string
// Description: Demonstrates how to export the configuration of an Aspose.BarCode BarcodeGenerator to an XML representation and retrieve it as a string.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category, illustrating the use of BarcodeGenerator, its Parameters, and the ExportToXml method. Developers often need to persist barcode settings for later reuse, configuration auditing, or integration with external systems. The snippet shows a typical pattern for capturing generator state in XML format.
// Prompt: Create a utility method that accepts a BarcodeGenerator, exports its state to XML, and returns the XML string.
// Tags: barcode, export, xml, serialization, aspose.barcode, generatortoxml, code128

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a BarcodeGenerator's configuration to an XML string.
/// </summary>
class Program
{
    /// <summary>
    /// Exports the provided <see cref="BarcodeGenerator"/> instance to an XML string.
    /// </summary>
    /// <param name="generator">The barcode generator whose state will be serialized.</param>
    /// <returns>XML string representing the generator's configuration.</returns>
    static string ExportGeneratorToXml(BarcodeGenerator generator)
    {
        // Create a memory stream to hold the XML data.
        using (MemoryStream ms = new MemoryStream())
        {
            // Write the generator's state into the stream in XML format.
            generator.ExportToXml(ms);
            // Reset stream position to the beginning for reading.
            ms.Position = 0;
            // Read the entire XML content as a UTF-8 string.
            using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }

    /// <summary>
    /// Entry point of the example. Creates a BarcodeGenerator, customizes a parameter, and outputs the exported XML.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with sample data.
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Adjust the X-dimension (module width) to 2 pixels.
            gen.Parameters.Barcode.XDimension.Pixels = 2f;
            // Export the generator's configuration to XML.
            string xml = ExportGeneratorToXml(gen);
            // Display the resulting XML.
            Console.WriteLine("Exported XML:");
            Console.WriteLine(xml);
        }
    }
}