// Title: Restore BarcodeGenerator from XML using ImportFromXml
// Description: Demonstrates exporting a BarcodeGenerator's configuration to XML stored in a MemoryStream and recreating the generator from that XML.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on persisting and restoring barcode settings via XML. It showcases the ExportToXml and ImportFromXml APIs, which are commonly used for configuration backup, sharing settings across applications, or dynamic barcode generation scenarios. Developers working with barcode creation, customization, and serialization will find these patterns useful.
// Prompt: Restore a BarcodeGenerator instance from XML data stored in a MemoryStream via ImportFromXml(Stream).
// Tags: barcode symbology, import, export, xml, memorystream, aspose.barcode, generation, image, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that exports a BarcodeGenerator's settings to XML,
/// stores the XML in a MemoryStream, and then restores a new generator
/// instance from that XML using ImportFromXml.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs export, display, and import of barcode settings.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with Code128 symbology and sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a visual property (barcode color) to verify that it is restored later.
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Export the generator's configuration to an XML document held in a MemoryStream.
            using (var xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                // Reset stream position to the beginning for reading.
                xmlStream.Position = 0;

                // Optional: read and display the exported XML for debugging or inspection.
                using (var reader = new StreamReader(xmlStream, leaveOpen: true))
                {
                    string xml = reader.ReadToEnd();
                    Console.WriteLine("Exported XML:");
                    Console.WriteLine(xml);
                }

                // Rewind the stream again before importing the settings.
                xmlStream.Position = 0;

                // Create a new BarcodeGenerator instance from the XML data.
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlStream))
                {
                    // Generate the barcode image based on the imported settings.
                    using (var bitmap = importedGenerator.GenerateBarCodeImage())
                    {
                        // Define a temporary file path for the PNG output.
                        string outputPath = Path.Combine(Path.GetTempPath(), "imported_barcode.png");
                        // Save the generated image as PNG.
                        bitmap.Save(outputPath, ImageFormat.Png);
                        Console.WriteLine($"Barcode image saved to: {outputPath}");
                    }
                }
            }
        }
    }
}