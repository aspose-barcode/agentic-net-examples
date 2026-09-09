// Title: Export and Import Barcode Generator Configuration via XML
// Description: Demonstrates exporting a configured barcode generator to an XML file and importing it to recreate the same barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use the BarcodeGenerator class to export its settings to an XML file with ExportToXml, and later restore those settings using BarcodeGenerator.ImportFromXml. Typical use cases include persisting barcode layouts, sharing configurations across applications, or version‑controlling barcode designs. Developers often need to serialize generator parameters for reuse, migration, or automated testing.
// Prompt: Export current barcode configuration to an XML file, then import it in another application.
// Tags: barcode symbology, export, import, xml, configuration, aspose.barcode, qr, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Shows how to export a barcode generator's configuration to XML and import it later to produce identical barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, saves its image, exports the generator settings to XML,
    /// then imports the settings to generate a second image from the same configuration.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary output directory for the demo files.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the XML configuration and the two barcode images.
        string xmlPath = Path.Combine(outputDir, "generatorConfig.xml");
        string originalImagePath = Path.Combine(outputDir, "original.png");
        string loadedImagePath = Path.Combine(outputDir, "loaded.png");

        // --------------------------------------------------------------------
        // Create a barcode generator, configure its appearance, save the image,
        // and export the full configuration to an XML file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set module size and bar color.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Save the generated QR code as a PNG image.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the current generator settings to XML for later reuse.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the previously saved XML configuration and generate a new image
        // using the same settings. This demonstrates that the configuration round‑trip works.
        // --------------------------------------------------------------------
        using (var importedGen = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            importedGen.Save(loadedImagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for verification.
        Console.WriteLine("Original barcode image: " + originalImagePath);
        Console.WriteLine("Exported XML configuration: " + xmlPath);
        Console.WriteLine("Loaded barcode image: " + loadedImagePath);
    }
}