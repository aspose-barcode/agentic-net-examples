// Title: Serialize Barcode Generator Settings to XML Using MemoryStream
// Description: Demonstrates how to export barcode generation settings to an XML stream and re-import them to create a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode serialization category, illustrating the use of ExportToXml and ImportFromXml methods. It shows how to work with BarcodeGenerator settings, MemoryStream, and XML for persisting configuration. Developers often need to save and load barcode settings for reuse, configuration files, or remote transmission.
// Prompt: Serialize barcode generation settings to a MemoryStream by calling ExportToXml(Stream) method directly.
// Tags: qr, xml, serialization, memorystream, exporttoxml, importfromxml, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting and importing barcode generator settings using XML and MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR barcode, exports its settings to XML, re-imports them, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator with QR symbology and sample text
        var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText");
        // Adjust the X-dimension (module size) in pixels
        generator.Parameters.Barcode.XDimension.Pixels = 4f;

        // Use a MemoryStream to hold the exported XML
        using (var memoryStream = new MemoryStream())
        {
            // Export generator settings to the stream as XML
            generator.ExportToXml(memoryStream);
            // Reset stream position to the beginning for reading
            memoryStream.Position = 0;

            // Read the XML content from the stream for display
            string xmlContent;
            using (var reader = new StreamReader(memoryStream, leaveOpen: true))
            {
                xmlContent = reader.ReadToEnd();
            }

            Console.WriteLine("Exported XML:");
            Console.WriteLine(xmlContent);

            // Reset position again before importing
            memoryStream.Position = 0;

            // Import settings from the XML stream into a new generator instance
            using (var importedGenerator = BarcodeGenerator.ImportFromXml(memoryStream))
            {
                // Define a temporary file path for the barcode image
                string outputPath = Path.Combine(Path.GetTempPath(), "barcode_from_xml.png");
                // Save the barcode as PNG
                importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
    }
}