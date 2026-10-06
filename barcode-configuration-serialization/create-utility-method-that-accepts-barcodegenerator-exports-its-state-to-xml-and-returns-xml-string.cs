// Title: Export BarcodeGenerator Settings to XML
// Description: Demonstrates how to export the configuration of an Aspose.BarCode BarcodeGenerator to an XML string.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category, showing how to use BarcodeGenerator, its Parameters, and the ExportToXml method to persist barcode settings. Developers often need to save or transfer barcode configurations for later reuse, debugging, or documentation, and this pattern illustrates the typical workflow for XML serialization of barcode objects.
// Prompt: Create a utility method that accepts a BarcodeGenerator, exports its state to XML, and returns the XML string.
// Tags: barcode symbology, export, xml, serialization, aspose.barcode, generation, utility method

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a BarcodeGenerator's configuration to XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a sample QR barcode generator, adjusts settings, and outputs its XML representation.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for QR code with sample text
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText");

        // Adjust a specific parameter (X dimension) to illustrate custom settings
        generator.Parameters.Barcode.XDimension.Pixels = 4f;

        // Export the generator's current state to an XML string using the utility method
        string xml = ExportGeneratorToXml(generator);

        // Display the resulting XML
        Console.WriteLine("Exported XML:");
        Console.WriteLine(xml);
    }

    /// <summary>
    /// Serializes the provided BarcodeGenerator into an XML string.
    /// </summary>
    /// <param name="generator">The BarcodeGenerator instance to export.</param>
    /// <returns>XML representation of the generator's configuration.</returns>
    static string ExportGeneratorToXml(BarcodeGenerator generator)
    {
        // Use a memory stream to capture the XML output
        using (MemoryStream ms = new MemoryStream())
        {
            // Write the generator's state to the stream in XML format
            generator.ExportToXml(ms);

            // Reset stream position to the beginning for reading
            ms.Position = 0;

            // Read the entire XML content from the stream
            using (StreamReader reader = new StreamReader(ms))
            {
                return reader.ReadToEnd();
            }
        }
    }
}