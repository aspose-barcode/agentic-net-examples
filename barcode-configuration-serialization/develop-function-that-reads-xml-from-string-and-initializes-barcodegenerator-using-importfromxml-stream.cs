// Title: Initialize BarcodeGenerator from XML string
// Description: Demonstrates reading barcode configuration XML from a string and creating a BarcodeGenerator via ImportFromXml.
// Category-Description: This example belongs to the Aspose.BarCode XML configuration category, illustrating how to use the BarcodeGenerator.ImportFromXml(Stream) method. It shows developers how to define barcode settings (such as symbology and text) in XML, load it from memory, and generate a barcode image. Typical use cases include dynamic barcode creation from stored XML templates or configuration files.
// Prompt: Develop a function that reads XML from a string and initializes a BarcodeGenerator using ImportFromXml(Stream).
// Tags: code128, barcode generation, xml import, importfromxml, aspose.barcode, png output

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a BarcodeGenerator from an XML string using ImportFromXml.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode from XML and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // XML definition for a Code128 barcode with sample text.
        string xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <EncodeType>Code128</EncodeType>
  <CodeText>1234567890</CodeText>
</BarcodeGenerator>";

        try
        {
            // Create a BarcodeGenerator instance from the XML string.
            using (var generator = CreateGeneratorFromXml(xml))
            {
                // Save the generated barcode image to verify successful creation.
                generator.Save("generated_from_xml.png");
                Console.WriteLine("Barcode generated and saved as generated_from_xml.png");
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Reads XML content from a string, wraps it in a MemoryStream, and imports it into a new BarcodeGenerator.
    /// </summary>
    /// <param name="xmlContent">The XML string containing barcode configuration.</param>
    /// <returns>A BarcodeGenerator initialized with the settings defined in the XML.</returns>
    static BarcodeGenerator CreateGeneratorFromXml(string xmlContent)
    {
        // Convert the XML string to a UTF-8 byte array.
        byte[] bytes = Encoding.UTF8.GetBytes(xmlContent);

        // Use a MemoryStream to provide the XML data to ImportFromXml.
        using (var stream = new MemoryStream(bytes))
        {
            // ImportFromXml parses the XML and returns a configured BarcodeGenerator instance.
            BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(stream);
            return generator;
        }
    }
}