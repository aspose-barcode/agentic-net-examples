// Title: Clone BarcodeGenerator configuration using ExportToXml and ImportFromXml
// Description: Demonstrates how to export a BarcodeGenerator's settings to XML, then import them into a new instance, effectively cloning the configuration. Shows saving both original and cloned barcodes as PNG files.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist and reuse barcode generator settings via XML. It uses the BarcodeGenerator class together with ExportToXml, ImportFromXml, and image saving methods. Developers often need to clone or transfer barcode configurations across applications or sessions, and this pattern provides a straightforward way to achieve that.
// Prompt: Chain ExportToXml and ImportFromXml calls to clone a BarcodeGenerator configuration into a new object.
// Tags: barcode, cloning, exporttoxml, importfromxml, configuration, qrcode, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates cloning a BarcodeGenerator configuration using XML export/import.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an original QR code, exports its configuration to XML,
    /// imports it into a new generator, and saves both images.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create the original BarcodeGenerator with QR symbology and sample text
        using (BarcodeGenerator original = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Configure visual properties of the original barcode
            original.Parameters.Barcode.XDimension.Pixels = 4f;
            original.Parameters.Barcode.BarColor = Color.Blue;
            original.Parameters.BackColor = Color.White;

            // Save the original barcode image to PNG
            string originalPath = Path.Combine(outputDir, "original.png");
            original.Save(originalPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Original barcode saved to: {originalPath}");

            // Export the original generator's configuration to an in‑memory XML stream
            using (MemoryStream ms = new MemoryStream())
            {
                original.ExportToXml(ms);
                ms.Position = 0; // Reset stream position for reading

                // Import the XML configuration into a new BarcodeGenerator instance (clone)
                using (BarcodeGenerator cloned = BarcodeGenerator.ImportFromXml(ms))
                {
                    // Save the cloned barcode image to PNG
                    string clonedPath = Path.Combine(outputDir, "cloned.png");
                    cloned.Save(clonedPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Cloned barcode saved to: {clonedPath}");
                }
            }
        }
    }
}