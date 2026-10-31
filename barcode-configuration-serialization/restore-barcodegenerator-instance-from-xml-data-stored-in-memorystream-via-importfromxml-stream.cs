// Title: Restore BarcodeGenerator from XML using MemoryStream
// Description: Demonstrates exporting a BarcodeGenerator configuration to XML, storing it in a MemoryStream, and restoring the generator via ImportFromXml.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on persisting and reloading barcode settings. It showcases the ExportToXml and ImportFromXml APIs of the BarcodeGenerator class, which are commonly used for saving configuration, sharing across services, or recreating barcodes without re‑defining parameters. Developers working with QR codes, PDF417, or other symbologies often need to serialize generator state for later reuse or for version‑controlled configuration files.
// Prompt: Restore a BarcodeGenerator instance from XML data stored in a MemoryStream via ImportFromXml(Stream).
// Tags: barcode symbology, import, export, xml, memorystream, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a <see cref="BarcodeGenerator"/> configuration to XML,
/// stores it in a <see cref="MemoryStream"/>, and then restores a new generator instance
/// from that XML data using <c>ImportFromXml</c>. The restored generator is used to
/// create a PNG image of the barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑import workflow and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary output directory for the generated image file.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir);

        // --------------------------------------------------------------------
        // Create and configure a BarcodeGenerator instance.
        // --------------------------------------------------------------------
        // Encode a QR code with the text "SampleText".
        BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText");
        // Set the X‑dimension (module size) to 4 pixels.
        generator.Parameters.Barcode.XDimension.Pixels = 4f;

        // --------------------------------------------------------------------
        // Export the generator's state to a MemoryStream as XML.
        // --------------------------------------------------------------------
        using (MemoryStream ms = new MemoryStream())
        {
            generator.ExportToXml(ms);
            // Reset the stream position to the beginning before reading.
            ms.Position = 0;

            // ----------------------------------------------------------------
            // Import a new BarcodeGenerator instance from the XML data in the stream.
            // ----------------------------------------------------------------
            BarcodeGenerator loadedGenerator = BarcodeGenerator.ImportFromXml(ms);

            // ----------------------------------------------------------------
            // Save the barcode image produced by the restored generator.
            // ----------------------------------------------------------------
            string imagePath = Path.Combine(outputDir, "BarcodeFromStream.png");
            loadedGenerator.Save(imagePath, BarCodeImageFormat.Png);

            Console.WriteLine("Barcode image saved to: " + imagePath);
        }
    }
}