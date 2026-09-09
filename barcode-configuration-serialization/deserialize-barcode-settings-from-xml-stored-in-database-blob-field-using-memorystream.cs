// Title: Deserialize Barcode Generator Settings from XML BLOB using MemoryStream
// Description: Demonstrates how to export Aspose.BarCode generator settings to XML, store them as a byte array (simulating a database BLOB), and later import the settings to generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode settings serialization category. It shows how to use the BarcodeGenerator class together with ExportToXml and ImportFromXml methods to persist and restore barcode configuration. Typical use cases include saving barcode settings in a database, sharing configurations across services, or version‑controlling barcode definitions. Developers working with barcode generation often need to serialize settings for later reuse, and this snippet provides a clear pattern for doing so.
// Prompt: Deserialize barcode settings from XML stored in a database BLOB field using a MemoryStream.
// Tags: barcode, symbology, serialization, xml, memorystream, aspose.barcode, import, export, settings, qr, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that serializes barcode generator settings to XML,
/// stores them as a byte array (simulating a DB BLOB), and deserializes them
/// to generate a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the export‑to‑XML, storage‑as‑BLOB, and import‑from‑XML workflow.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // ------------------------------------------------------------
        // Step 1: Create a barcode generator and export its settings to XML.
        // The resulting XML is stored in a byte array to simulate a BLOB field.
        // ------------------------------------------------------------
        byte[] xmlBlob;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Adjust a specific parameter (X‑dimension) before exporting
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Write the XML representation to a memory stream
            using (var ms = new MemoryStream())
            {
                generator.ExportToXml(ms);
                xmlBlob = ms.ToArray(); // Byte array now represents the stored BLOB
            }
        }

        // ------------------------------------------------------------
        // Step 2: Retrieve the XML BLOB and deserialize the settings.
        // ------------------------------------------------------------
        using (var ms = new MemoryStream(xmlBlob))
        {
            // Ensure the stream position is at the beginning
            ms.Position = 0;

            // Import the generator settings from the XML stream
            using (var importedGenerator = BarcodeGenerator.ImportFromXml(ms))
            {
                // Generate the barcode image using the deserialized configuration
                string imagePath = Path.Combine(outputDir, "DeserializedBarcode.png");
                importedGenerator.Save(imagePath, BarCodeImageFormat.Png);
                Console.WriteLine("Barcode image saved to: " + imagePath);
            }
        }

        // Optional clean‑up: delete the temporary directory.
        // Directory.Delete(outputDir, true);
    }
}