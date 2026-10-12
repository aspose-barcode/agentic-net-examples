// Title: Generate QR barcode and reuse its settings via XML configuration
// Description: Demonstrates creating a QR barcode, saving it as a PNG image, exporting the generator's settings to an XML file, and reproducing the same barcode from that XML configuration.
// Category-Description: This example belongs to the Aspose.BarCode generation and configuration management category. It showcases the BarcodeGenerator class for creating barcodes, the ExportToXml method for persisting generator settings, and the ImportFromXml method for restoring those settings. Developers often need reproducible barcode output across environments or sessions, making XML configuration a practical solution for consistent results.
// Prompt: Convert an existing barcode image generation workflow to use saved XML configurations for reproducible output.
// Tags: qr, barcode, xml, configuration, export, import, image, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, exporting its configuration to XML, and recreating the barcode from that XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR barcode, saves its image, exports the generator settings to XML,
    /// then imports the settings to produce an identical barcode image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for all output files
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the original image, XML configuration, and the restored image
        string originalImagePath = Path.Combine(outputDir, "barcode_original.png");
        string xmlConfigPath = Path.Combine(outputDir, "barcode_config.xml");
        string restoredImagePath = Path.Combine(outputDir, "barcode_from_xml.png");

        // -----------------------------------------------------------------
        // Generate the initial barcode, customize its appearance, save the image,
        // and export the generator's configuration to an XML file.
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            // Set the module size (X dimension) and barcode color
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Color.Green;

            // Save the barcode as a PNG image
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the current generator settings to XML for later reuse
            generator.ExportToXml(xmlConfigPath);
        }

        // -----------------------------------------------------------------
        // Import the previously saved XML configuration and generate the same barcode again.
        // -----------------------------------------------------------------
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlConfigPath))
        {
            // Save the regenerated barcode image using the imported settings
            importedGenerator.Save(restoredImagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for verification
        Console.WriteLine("Original barcode image saved to: " + originalImagePath);
        Console.WriteLine("XML configuration saved to: " + xmlConfigPath);
        Console.WriteLine("Restored barcode image saved to: " + restoredImagePath);
    }
}