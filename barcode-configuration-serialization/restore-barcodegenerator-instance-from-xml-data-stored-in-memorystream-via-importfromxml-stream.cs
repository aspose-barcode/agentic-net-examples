// Title: Restore BarcodeGenerator from XML using ImportFromXml
// Description: Demonstrates exporting a BarcodeGenerator's settings to XML stored in a MemoryStream and then restoring a new instance via ImportFromXml.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to serialize and deserialize barcode generator settings using XML. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers use to persist barcode configurations, share them across services, or recreate barcodes without reapplying settings manually.
// Prompt: Restore a BarcodeGenerator instance from XML data stored in a MemoryStream via ImportFromXml(Stream).
// Tags: barcode, code128, xml, import, export, memorystream, aspose.barcode, generator, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a BarcodeGenerator's configuration to XML,
/// then restores a new generator instance from that XML using a MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs export, import, and saves the restored barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize the original barcode generator with Code128 symbology and sample text.
        using (BarcodeGenerator originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Create a memory stream to hold the XML representation of the generator.
            using (MemoryStream xmlStream = new MemoryStream())
            {
                // Export the generator's settings to the memory stream as XML.
                originalGenerator.ExportToXml(xmlStream);

                // Reset the stream position to the beginning so it can be read.
                xmlStream.Position = 0;

                // Import a new BarcodeGenerator instance from the XML data in the stream.
                BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream);

                // Save the restored barcode image to a PNG file.
                importedGenerator.Save("restored.png", BarCodeImageFormat.Png);

                // Release resources used by the imported generator.
                importedGenerator.Dispose();

                // Inform the user that the process completed successfully.
                Console.WriteLine("Barcode restored from XML and saved as 'restored.png'.");
            }
        }
    }
}