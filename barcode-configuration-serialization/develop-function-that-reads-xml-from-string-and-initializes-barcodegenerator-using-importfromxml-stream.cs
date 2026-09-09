// Title: Initialize BarcodeGenerator from XML string using ImportFromXml
// Description: Demonstrates reading barcode configuration XML from a string and creating a BarcodeGenerator via ImportFromXml(Stream). The example shows exporting a generator to XML, then re-importing it.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category, illustrating how to persist and restore barcode settings using XML. It uses BarcodeGenerator, EncodeTypes, and ExportToXml/ImportFromXml methods, common for scenarios like saving barcode templates, sharing configurations, or recreating barcodes across applications. Developers often need to serialize generator parameters to XML for storage or transmission and later reconstruct the generator without manually setting each property.
// Prompt: Develop a function that reads XML from a string and initializes a BarcodeGenerator using ImportFromXml(Stream).
// Tags: barcode, xml, import, export, qrcode, generation, aspose.barcode, stream

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates importing a BarcodeGenerator configuration from an XML string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Exports a QR code generator to XML, then re-imports it and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Create an initial barcode generator and export its state to an XML string
        using (var originalGen = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Adjust a specific parameter (pixel size of X-dimension)
            originalGen.Parameters.Barcode.XDimension.Pixels = 4f;

            string xml;
            // Export the generator configuration to a memory stream, then read it as a UTF-8 string
            using (var ms = new MemoryStream())
            {
                originalGen.ExportToXml(ms);
                ms.Position = 0;
                using (var sr = new StreamReader(ms, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    xml = sr.ReadToEnd();
                }
            }

            // Initialize a new generator from the XML string
            using (var importedGen = InitializeGeneratorFromXmlString(xml))
            {
                // Save the regenerated barcode image to a temporary file
                string outputPath = Path.Combine(Path.GetTempPath(), "barcode_from_xml.png");
                importedGen.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
    }

    /// <summary>
    /// Creates a BarcodeGenerator by importing configuration from an XML string.
    /// </summary>
    /// <param name="xml">The XML representation of the barcode generator settings.</param>
    /// <returns>A new BarcodeGenerator instance configured according to the XML.</returns>
    static BarcodeGenerator InitializeGeneratorFromXmlString(string xml)
    {
        // Convert the XML string to a UTF-8 byte array and wrap it in a MemoryStream
        byte[] bytes = Encoding.UTF8.GetBytes(xml);
        var ms = new MemoryStream(bytes);
        ms.Position = 0;

        // Import the generator configuration from the stream
        var generator = BarcodeGenerator.ImportFromXml(ms);

        // Clean up the temporary stream
        ms.Dispose();

        return generator;
    }
}