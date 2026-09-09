// Title: Generate QR barcode, export configuration to XML, and regenerate from XML
// Description: Demonstrates creating a QR barcode image, saving it as PNG, exporting the generator settings to an XML file, and reproducing the same barcode by importing the XML configuration.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration management category. It showcases the BarcodeGenerator class for creating barcodes, the ExportToXml method for persisting settings, and the ImportFromXml method for reproducible output. Developers often need to store barcode parameters for later reuse, batch processing, or version‑controlled configurations, making this pattern essential for reliable barcode production pipelines.
// Prompt: Convert an existing barcode image generation workflow to use saved XML configurations for reproducible output.
// Tags: qr, barcode, xml, export, import, generation, png, aspose.barcode, configuration

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, configuration export to XML, and regeneration from the saved XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR barcode, saves it, exports its settings to XML, and recreates the barcode from that XML.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for all generated files.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo");
        Directory.CreateDirectory(outputDir);

        // Define file paths for the original image, XML configuration, and the regenerated image.
        string originalImagePath = Path.Combine(outputDir, "original.png");
        string xmlConfigPath = Path.Combine(outputDir, "config.xml");
        string loadedImagePath = Path.Combine(outputDir, "loaded.png");

        // ------------------------------------------------------------
        // Generate a QR barcode, customize its appearance, save the image,
        // and export the generator's configuration to an XML file.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            // Set the module size (X dimension) and barcode color.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Save the barcode as a PNG image.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the current generator settings to XML for later reuse.
            generator.ExportToXml(xmlConfigPath);
        }

        // ------------------------------------------------------------
        // Import the previously saved XML configuration and generate the
        // same barcode again, saving it to a new image file.
        // ------------------------------------------------------------
        using (var generator = BarcodeGenerator.ImportFromXml(xmlConfigPath))
        {
            generator.Save(loadedImagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files.
        Console.WriteLine("Original barcode saved to: " + originalImagePath);
        Console.WriteLine("XML configuration saved to: " + xmlConfigPath);
        Console.WriteLine("Barcode regenerated from XML saved to: " + loadedImagePath);
    }
}