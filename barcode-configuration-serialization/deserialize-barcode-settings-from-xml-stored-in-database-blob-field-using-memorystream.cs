// Title: Deserialize barcode settings from XML stored in a database BLOB using MemoryStream
// Description: Demonstrates exporting barcode generator settings to XML, storing them as a byte array (simulating a DB BLOB), and importing the settings back via a MemoryStream to generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode settings serialization category. It shows how to use BarcodeGenerator.ExportToXml and BarcodeGenerator.ImportFromXml together with MemoryStream to persist and restore barcode configuration. Typical use cases include saving barcode settings in a database, transferring them between services, or caching them for later reuse. Developers working with barcode generation often need to serialize settings for persistence or versioning, and this snippet illustrates the key API classes involved.
// Prompt: Deserialize barcode settings from XML stored in a database BLOB field using a MemoryStream.
// Tags: barcode, serialization, xml, memorystream, database, aspose.barcode, qrcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that serializes barcode generator settings to XML,
/// stores them in a simulated database BLOB, and deserializes them back
/// to generate a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs export, simulated storage,
    /// import, and image generation using Aspose.BarCode.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Step 1: Create a barcode generator and configure its parameters.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the X‑dimension (module size) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Step 2: Export the generator settings to XML using a MemoryStream.
            // This simulates writing the XML to a BLOB column in a database.
            using (var exportStream = new MemoryStream())
            {
                generator.ExportToXml(exportStream);
                byte[] xmlBlob = exportStream.ToArray(); // Simulated BLOB data.

                // Step 3: Load the XML from the simulated BLOB into a new MemoryStream.
                using (var importStream = new MemoryStream(xmlBlob))
                {
                    // Ensure the stream position is at the beginning before import.
                    importStream.Position = 0;

                    // Step 4: Import barcode generator settings from the XML stream.
                    using (var importedGenerator = BarcodeGenerator.ImportFromXml(importStream))
                    {
                        // Step 5: Generate and save the barcode image using the imported settings.
                        string outputPath = Path.Combine(Path.GetTempPath(), "ImportedBarcode.png");
                        importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"Barcode image saved to: {outputPath}");
                    }
                }
            }
        }
    }
}