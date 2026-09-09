// Title: Export barcode generation settings to XML using a MemoryStream
// Description: Demonstrates how to serialize Aspose.BarCode generation settings to an in‑memory XML representation.
// Category-Description: This example belongs to the Aspose.BarCode configuration serialization category. It shows how to use BarcodeGenerator, its Parameters, and the ExportToXml(Stream) method to capture settings without writing to disk. Developers often need to store or transmit barcode configuration as XML for later reuse, auditing, or integration with other systems.
// Prompt: Serialize barcode generation settings to a MemoryStream by calling ExportToXml(Stream) method directly.
// Tags: barcode, qrcode, export, xml, memorystream, aspose.barcode, generation

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a QR barcode generator, modifies a setting,
/// and exports the generator's configuration to an XML string using a MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the barcode generation settings export.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with QR symbology and sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Adjust the X dimension (module size) of the barcode.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Create an in‑memory stream to hold the exported XML.
            using (MemoryStream ms = new MemoryStream())
            {
                // Serialize the generator's settings directly into the MemoryStream.
                generator.ExportToXml(ms);

                // Reset the stream position to the beginning for reading.
                ms.Position = 0;

                // Read the XML content from the stream using a StreamReader.
                using (StreamReader reader = new StreamReader(ms, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    string xml = reader.ReadToEnd();

                    // Output the exported XML to the console.
                    Console.WriteLine("Exported Barcode Generation Settings (XML):");
                    Console.WriteLine(xml);
                }
            }
        }
    }
}