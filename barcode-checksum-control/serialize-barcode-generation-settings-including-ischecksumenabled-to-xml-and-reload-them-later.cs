// Title: Serialize and Reload Barcode Generator Settings to XML
// Description: Demonstrates how to export barcode generation settings, including checksum enablement, to an XML file and later import them to recreate the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use BarcodeGenerator, its Parameters, and the ExportToXml/ImportFromXml methods to persist settings. Typical use cases include saving configuration for later reuse, sharing settings across services, or version‑controlling barcode definitions. Developers often need to serialize settings to XML or JSON to maintain consistency across deployments.
// Prompt: Serialize barcode generation settings, including IsChecksumEnabled, to XML and reload them later.
// Tags: barcode, symbology, serialization, xml, checksum, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple demonstration of exporting barcode generator settings to XML,
/// then importing those settings to generate an identical barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, saves its image, exports settings to XML,
    /// reloads the settings, and generates a second image from the imported configuration.
    /// </summary>
    static void Main()
    {
        // Define a temporary working directory for all output files.
        string baseDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo");
        if (!Directory.Exists(baseDir))
        {
            Directory.CreateDirectory(baseDir);
        }

        // Paths for the XML settings file and the two barcode images.
        string xmlPath = Path.Combine(baseDir, "generator.xml");
        string originalImagePath = Path.Combine(baseDir, "original.png");
        string loadedImagePath = Path.Combine(baseDir, "loaded.png");

        // --------------------------------------------------------------------
        // Create a barcode generator, enable checksum, save the image,
        // and export the generator's configuration to an XML file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image in PNG format.
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the current generator settings to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the previously saved XML settings and generate the barcode again.
        // --------------------------------------------------------------------
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode image generated from the imported settings.
            loadedGenerator.Save(loadedImagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for verification.
        Console.WriteLine($"Original barcode image saved to: {originalImagePath}");
        Console.WriteLine($"Generator settings exported to XML: {xmlPath}");
        Console.WriteLine($"Barcode generated from imported settings saved to: {loadedImagePath}");
    }
}