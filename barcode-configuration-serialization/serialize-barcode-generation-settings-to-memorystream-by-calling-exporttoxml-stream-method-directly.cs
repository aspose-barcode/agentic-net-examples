// Title: Export Barcode Generator Settings to XML via MemoryStream
// Description: Demonstrates how to serialize Aspose.BarCode generation settings to an in‑memory XML stream using the ExportToXml method.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to capture barcode configuration (such as symbology, colors, and fonts) as XML. It uses the BarcodeGenerator, EncodeTypes, and ExportToXml API to persist settings without writing to disk, a common requirement for logging, configuration backup, or transmitting settings over a network. Developers working with barcode creation often need to serialize and deserialize settings for repeatable generation or audit purposes.
// Prompt: Serialize barcode generation settings to a MemoryStream by calling ExportToXml(Stream) method directly.
// Tags: qr, barcode, export, xml, memorystream, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides an example that creates a QR barcode, customizes its appearance,
/// and exports the generator's configuration to an XML document stored in a <see cref="MemoryStream"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, modifies visual parameters,
    /// serializes the settings to XML in memory, and writes the XML to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with QR symbology and sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Customize visual aspects of the barcode.
            generator.Parameters.Barcode.BarColor = Color.Green;                     // Set barcode bar color to green.
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f; // Increase code text font size.

            // Serialize the generator's configuration to an in‑memory XML stream.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);   // Write XML representation to the stream.

                // Rewind the stream so it can be read from the beginning.
                xmlStream.Position = 0;

                // Read the XML content from the memory stream.
                using (StreamReader reader = new StreamReader(xmlStream, leaveOpen: true))
                {
                    string xml = reader.ReadToEnd(); // Retrieve the full XML string.
                    Console.WriteLine("Exported Barcode Settings XML:");
                    Console.WriteLine(xml);
                }
            }
        }
    }
}