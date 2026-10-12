// Title: Import BarcodeGenerator from XML string using a stream
// Description: Shows how to export a BarcodeGenerator to an XML string, then recreate it by importing the XML via a MemoryStream.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration category. It demonstrates using BarcodeGenerator, ExportToXml, and ImportFromXml methods to persist and restore barcode settings. Typical use cases include saving barcode configurations, transferring them between services, or recreating barcodes from stored XML. Developers often need to serialize generator parameters to XML and later reinitialize generators without re‑specifying each property.
// Prompt: Develop a function that reads XML from a string and initializes a BarcodeGenerator using ImportFromXml(Stream).
// Tags: barcode, symbology, xml, import, stream, aspose.barcode, generation, png, exporttoxml, importfromxml

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates importing a <see cref="BarcodeGenerator"/> from an XML string using <c>ImportFromXml</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode, exports its configuration to XML, reimports it, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // Step 1: Create an initial BarcodeGenerator and export its state to an XML string
        string xmlString;
        using (var initialGenerator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Adjust a visual parameter (pixel size of the X dimension)
            initialGenerator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Export the generator's configuration to a memory stream as XML
            using (var exportStream = new MemoryStream())
            {
                initialGenerator.ExportToXml(exportStream);
                exportStream.Position = 0; // Reset stream position for reading

                // Read the XML content from the stream into a string
                using (var reader = new StreamReader(exportStream, Encoding.UTF8, true, 1024, leaveOpen: true))
                {
                    xmlString = reader.ReadToEnd();
                }
            }
        }

        // Step 2: Import a BarcodeGenerator from the XML string using a memory stream
        using (var importStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
        {
            importStream.Position = 0; // Ensure the stream is positioned at the beginning

            // Recreate the generator from the XML configuration
            using (var generator = BarcodeGenerator.ImportFromXml(importStream))
            {
                // Generate the barcode image and save it as PNG
                string outputPath = Path.Combine(outputDir, "ImportedBarcode.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
    }
}