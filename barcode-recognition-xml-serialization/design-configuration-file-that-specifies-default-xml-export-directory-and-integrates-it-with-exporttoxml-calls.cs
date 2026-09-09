// Title: Export Barcode Generator State to XML Using Configured Directory
// Description: Demonstrates reading a default export directory from a configuration file, generating a QR barcode, exporting its state to XML, and recreating the barcode image from the XML.
// Category-Description: This example belongs to the Aspose.BarCode generation and persistence category. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat for creating barcodes, as well as ExportToXml and ImportFromXml for persisting and restoring generator state. Typical use cases include saving barcode configurations for later reuse, batch processing, and integrating barcode generation into automated workflows. Developers often need to manage export locations via configuration files to keep environments flexible and maintainable.
/// Prompt: Design a configuration file that specifies the default XML export directory and integrates it with ExportToXml calls.
/// Tags: barcode symbology, generation, export, xml, configuration, aspose.barcode, qr

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides a simple demonstration of configuring an export directory,
/// generating a QR barcode, exporting its state to XML, and recreating the barcode image from the XML.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the configuration handling,
    /// barcode generation, XML export/import, and image saving steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Define base paths and configuration file location
        // --------------------------------------------------------------------
        string basePath = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        string configPath = Path.Combine(basePath, "config.txt");
        string defaultExportDir = Path.Combine(basePath, "ExportedXml");

        // --------------------------------------------------------------------
        // Ensure the base directory exists
        // --------------------------------------------------------------------
        if (!Directory.Exists(basePath))
            Directory.CreateDirectory(basePath);

        // --------------------------------------------------------------------
        // Create a simple config file with the default export directory if it does not exist
        // --------------------------------------------------------------------
        if (!File.Exists(configPath))
        {
            Directory.CreateDirectory(defaultExportDir);
            File.WriteAllText(configPath, defaultExportDir);
        }

        // --------------------------------------------------------------------
        // Read the export directory from the config file
        // --------------------------------------------------------------------
        string exportDir = File.ReadAllText(configPath).Trim();
        if (string.IsNullOrEmpty(exportDir))
        {
            Console.WriteLine("Export directory not specified in config. Using default.");
            exportDir = defaultExportDir;
        }

        // --------------------------------------------------------------------
        // Ensure the export directory exists
        // --------------------------------------------------------------------
        if (!Directory.Exists(exportDir))
            Directory.CreateDirectory(exportDir);

        // --------------------------------------------------------------------
        // Generate a QR barcode and export its generator state to an XML file
        // --------------------------------------------------------------------
        string xmlFilePath = Path.Combine(exportDir, "barcode_state.xml");
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose.BarCode"))
        {
            // Set a specific X-dimension for better visual quality
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.ExportToXml(xmlFilePath);
        }

        Console.WriteLine($"Barcode generation state exported to: {xmlFilePath}");

        // --------------------------------------------------------------------
        // Import the barcode generator state from the XML and save the barcode image
        // --------------------------------------------------------------------
        string imagePath = Path.Combine(exportDir, "barcode_image.png");
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlFilePath))
        {
            importedGenerator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image generated from imported state: {imagePath}");
    }
}