// Title: Serialize and Reload Barcode Generator Settings via XML
// Description: Demonstrates how to export barcode generation settings, including checksum enablement, to an XML file and later import them to recreate the barcode.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the use of BarcodeGenerator, its Parameters, and the ExportToXml/ImportFromXml methods to persist and restore settings. Typical scenarios include saving configuration for later reuse, sharing settings across services, or version‑controlling barcode definitions. Developers often need to serialize settings to XML or JSON to integrate barcode generation into automated pipelines or configuration‑driven applications.
// Prompt: Serialize barcode generation settings, including IsChecksumEnabled, to XML and reload them later.
// Tags: barcode symbology, serialization, xml, checksum, generation, aspose.barcode

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
        // Create a unique temporary directory for all demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the XML settings and the two barcode images
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string imgPathOriginal = Path.Combine(tempDir, "barcode_original.png");
        string imgPathLoaded = Path.Combine(tempDir, "barcode_loaded.png");

        // --------------------------------------------------------------------
        // Generate the original barcode, enable checksum, save the image,
        // and export the generator's configuration to an XML file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Enable checksum calculation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image as PNG
            generator.Save(imgPathOriginal, BarCodeImageFormat.Png);

            // Export the current generator settings (including checksum flag) to XML
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the previously saved XML settings and generate a second image
        // using the restored configuration.
        // --------------------------------------------------------------------
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode image generated from the imported settings
            loadedGenerator.Save(imgPathLoaded, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for verification
        Console.WriteLine("Original barcode image: " + imgPathOriginal);
        Console.WriteLine("Exported XML settings: " + xmlPath);
        Console.WriteLine("Loaded barcode image: " + imgPathLoaded);
    }
}