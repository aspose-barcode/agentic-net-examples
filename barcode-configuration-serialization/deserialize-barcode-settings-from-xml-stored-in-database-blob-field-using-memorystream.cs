// Title: Deserialize barcode settings from XML stored in a database BLOB using MemoryStream
// Description: Demonstrates how to export barcode generator settings to XML, store them as a BLOB, and later import them back to recreate the barcode.
// Category-Description: This example belongs to the Aspose.BarCode serialization and deserialization category, showcasing the use of BarcodeGenerator, ExportToXml, and ImportFromXml methods. Developers often need to persist barcode configurations in databases or files and restore them for consistent barcode generation across applications. The snippet illustrates typical workflow for storing settings as XML BLOBs and recreating generators without redefining parameters.
// Prompt: Deserialize barcode settings from XML stored in a database BLOB field using a MemoryStream.
// Tags: barcode symbology, serialization, deserialization, png, aspose.barcode, memorystream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Program demonstrating deserialization of barcode settings from an XML BLOB.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Exports a sample barcode configuration to XML, simulates storing it as a BLOB,
    /// then imports the settings to generate a barcode image.
    /// </summary>
    static void Main()
    {
        // Create a sample barcode generator with Code128 symbology and sample text.
        using (var sampleGenerator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Configure specific barcode parameters.
            sampleGenerator.Parameters.Barcode.XDimension.Point = 2f;
            sampleGenerator.Parameters.Barcode.BarHeight.Point = 40f;

            // Export the generator's settings to a memory stream (simulating a BLOB in a database).
            using (var exportStream = new MemoryStream())
            {
                sampleGenerator.ExportToXml(exportStream);
                byte[] dbBlob = exportStream.ToArray(); // Simulated BLOB data.

                // Deserialize the barcode settings from the XML BLOB using a new memory stream.
                using (var importStream = new MemoryStream(dbBlob))
                {
                    using (var importedGenerator = BarcodeGenerator.ImportFromXml(importStream))
                    {
                        // Generate and save the barcode image using the imported settings.
                        importedGenerator.Save("deserialized_barcode.png", BarCodeImageFormat.Png);
                        Console.WriteLine("Barcode image generated from deserialized settings.");
                    }
                }
            }
        }
    }
}