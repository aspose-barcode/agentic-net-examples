// Title: Export and Import Barcode Generation Settings via XML MemoryStream
// Description: Demonstrates how to serialize barcode generation settings to a MemoryStream using ExportToXml and then import them back to generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the use of BarcodeGenerator, its Parameters, ExportToXml(Stream) and ImportFromXml(Stream) methods to persist and restore barcode settings. Typical scenarios include saving configuration for later reuse, transferring settings between services, or version‑controlling barcode definitions. Developers working with barcode creation, configuration persistence, or XML‑based workflows will find these APIs essential.
// Prompt: Serialize barcode generation settings to a MemoryStream by calling ExportToXml(Stream) method directly.
// Tags: barcode, qr, export, xml, memorystream, aspose.barcode, generation, import

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of exporting barcode generation settings to XML in a memory stream,
/// then importing those settings to generate a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR barcode, exports its settings to a MemoryStream,
    /// imports the settings back, and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator with QR symbology and sample text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Configure a sample parameter: set the X‑dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Create a memory stream to hold the exported XML
            using (MemoryStream ms = new MemoryStream())
            {
                // Serialize the generator's settings directly into the memory stream
                generator.ExportToXml(ms);
                Console.WriteLine($"Exported XML size: {ms.Length} bytes");

                // Reset the stream position to the beginning for subsequent reading
                ms.Position = 0;

                // Import the settings from the memory stream into a new generator instance
                using (BarcodeGenerator imported = BarcodeGenerator.ImportFromXml(ms))
                {
                    // Define the output path for the generated barcode image
                    string outputPath = Path.Combine(Path.GetTempPath(), "barcode_from_xml.png");

                    // Save the barcode image using the imported settings
                    imported.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Barcode image saved to: {outputPath}");
                }
            }
        }
    }
}