// Title: Restore BarcodeGenerator from XML using ImportFromXml
// Description: Demonstrates how to export a BarcodeGenerator configuration to XML stored in a MemoryStream and then restore it using ImportFromXml, saving the resulting barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration management category. It shows how to serialize a BarcodeGenerator's settings to XML and later deserialize them, using the ExportToXml and ImportFromXml APIs. Developers often need to persist barcode settings, share configurations across services, or recreate generators from stored XML data; this snippet illustrates the typical workflow with EncodeTypes, parameters, and image saving.
// Prompt: Restore a BarcodeGenerator instance from XML data stored in a MemoryStream via ImportFromXml(Stream).
// Tags: barcode, xml, import, export, memorystream, qrcode, aspose.barcode, generation, serialization

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that demonstrates exporting a BarcodeGenerator to XML,
/// restoring it from a MemoryStream, and saving the resulting barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, serializes its settings to XML,
    /// restores the generator from the XML stream, and saves the barcode as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output image.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeGenDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "RestoredBarcode.png");

        // Initialize the original BarcodeGenerator with QR symbology and sample text.
        using (BarcodeGenerator originalGen = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Adjust a specific parameter (X dimension) for demonstration purposes.
            originalGen.Parameters.Barcode.XDimension.Pixels = 4f;

            // Export the generator's configuration to an in‑memory XML stream.
            using (MemoryStream ms = new MemoryStream())
            {
                originalGen.ExportToXml(ms);
                ms.Position = 0; // Reset stream position to the beginning for reading.

                // Import a new BarcodeGenerator instance from the XML stream.
                using (BarcodeGenerator restoredGen = BarcodeGenerator.ImportFromXml(ms))
                {
                    // Save the restored barcode image to the specified file path.
                    restoredGen.Save(outputPath, BarCodeImageFormat.Png);
                }
            }
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine("Barcode saved to: " + outputPath);
    }
}