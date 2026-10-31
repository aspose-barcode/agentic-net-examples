// Title: Import BarcodeGenerator from XML string using Aspose.BarCode
// Description: Demonstrates exporting a BarcodeGenerator to an XML string and re‑initializing it from that string using ImportFromXml(Stream).
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category. It showcases the ExportToXml and ImportFromXml APIs, which are essential for persisting barcode settings, sharing configurations across services, or version‑controlling barcode definitions. Developers working with QR codes, DataMatrix, or other symbologies often need to serialize generator parameters to XML and later reconstruct them without recreating settings manually.
// Prompt: Develop a function that reads XML from a string and initializes a BarcodeGenerator using ImportFromXml(Stream).
// Tags: barcode symbology, import, export, xml, aspose.barcode, qrcode, png, stream

using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that shows how to export a BarcodeGenerator to XML and import it back from a string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a QR code generator, exports its configuration to XML, re‑imports it, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Create a sample barcode generator (QR code) and customize a parameter
        var originalGenerator = new BarcodeGenerator(EncodeTypes.QR, "SampleText");
        originalGenerator.Parameters.Barcode.XDimension.Pixels = 4f;

        // Export the generator's state to an XML string using a memory stream
        string xml;
        using (var exportStream = new MemoryStream())
        {
            originalGenerator.ExportToXml(exportStream);
            exportStream.Position = 0; // Reset stream position for reading
            using (var reader = new StreamReader(exportStream, Encoding.UTF8))
            {
                xml = reader.ReadToEnd();
            }
        }

        Console.WriteLine("Exported XML:");
        Console.WriteLine(xml);

        // Import a new generator instance from the XML string
        BarcodeGenerator importedGenerator = ImportGeneratorFromXmlString(xml);
        if (importedGenerator == null)
        {
            Console.WriteLine("Failed to import BarcodeGenerator from XML.");
            return;
        }

        // Save the generated barcode image to a temporary file
        string outputPath = Path.Combine(Path.GetTempPath(), "ImportedBarcode.png");
        importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }

    /// <summary>
    /// Creates a BarcodeGenerator by importing its configuration from an XML string.
    /// </summary>
    /// <param name="xmlString">The XML representation of a BarcodeGenerator.</param>
    /// <returns>A new BarcodeGenerator instance, or null if the import fails.</returns>
    static BarcodeGenerator ImportGeneratorFromXmlString(string xmlString)
    {
        if (string.IsNullOrEmpty(xmlString))
        {
            Console.WriteLine("Provided XML string is null or empty.");
            return null;
        }

        // Convert the XML string to a UTF‑8 byte array and wrap it in a MemoryStream
        byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
        using (var stream = new MemoryStream(bytes))
        {
            try
            {
                // Import the generator configuration from the stream
                return BarcodeGenerator.ImportFromXml(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during ImportFromXml: {ex.Message}");
                return null;
            }
        }
    }
}