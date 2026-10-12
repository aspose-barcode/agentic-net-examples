// Title: Restore BarcodeGenerator from XML using ImportFromXml
// Description: Demonstrates exporting a BarcodeGenerator configuration to XML in a MemoryStream and restoring it via ImportFromXml, then saving the barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category, illustrating how to persist and reload barcode generator settings using XML. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use to create, export, import, and render barcodes in various formats.
// Prompt: Restore a BarcodeGenerator instance from XML data stored in a MemoryStream via ImportFromXml(Stream).
// Tags: barcode, qrcode, xml, import, export, memorystream, aspose.barcode, generation, serialization, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a BarcodeGenerator to XML and restoring it from a MemoryStream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR code, exports its configuration to XML, imports it back, and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for QR code with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Set the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Export the generator's configuration to an in‑memory stream as XML
            using (var ms = new MemoryStream())
            {
                generator.ExportToXml(ms);
                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Import a new generator instance from the XML stream
                using (var importedGenerator = BarcodeGenerator.ImportFromXml(ms))
                {
                    // Define output file path in the temporary folder
                    string outputPath = Path.Combine(Path.GetTempPath(), "RestoredBarcode.png");
                    // Save the restored barcode as a PNG image
                    importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
                    // Inform the user where the file was saved
                    Console.WriteLine($"Barcode saved to {outputPath}");
                }
            }
        }
    }
}