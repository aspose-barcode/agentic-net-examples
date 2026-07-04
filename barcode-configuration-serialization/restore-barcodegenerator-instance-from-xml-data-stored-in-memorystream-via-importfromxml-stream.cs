// Title: Restore BarcodeGenerator from XML in MemoryStream
// Description: Demonstrates exporting a BarcodeGenerator's settings to XML, storing it in a MemoryStream, and recreating the generator via ImportFromXml.
// Prompt: Restore a BarcodeGenerator instance from XML data stored in a MemoryStream via ImportFromXml(Stream).
// Tags: barcode, symbology, code128, xml, import, export, memorystream, aspose.barcodes, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows how to export a <see cref="BarcodeGenerator"/> configuration to XML,
/// store it in a <see cref="MemoryStream"/>, and then restore a new generator instance from that XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, exports its settings to XML,
    /// imports the settings back, and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with Code128 symbology and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Optionally configure image dimensions.
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Export the generator's configuration to an in‑memory XML stream.
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);

                // Rewind the stream so it can be read from the beginning.
                xmlStream.Position = 0;

                // Create a new generator instance by importing the XML data.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Save the barcode image generated from the imported settings.
                    string outputPath = "imported_barcode.png";
                    importedGenerator.Save(outputPath);
                    Console.WriteLine($"Barcode image saved to: {outputPath}");
                }
            }
        }
    }
}