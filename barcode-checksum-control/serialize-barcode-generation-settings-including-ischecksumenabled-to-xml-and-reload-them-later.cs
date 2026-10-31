// Title: Serialize barcode generator settings to XML and reload
// Description: Demonstrates how to configure a barcode generator, export its settings to an XML file, and later import those settings to recreate the barcode.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showing how to use BarcodeGenerator, its Parameters, and the ExportToXml / ImportFromXml methods. Developers often need to persist barcode generation settings for reuse, versioning, or sharing across applications, and this snippet illustrates the typical workflow for saving and reloading generator configurations.
// Prompt: Serialize barcode generation settings, including IsChecksumEnabled, to XML and reload them later.
// Tags: barcode, serialization, xml, checksum, code39, aspnet, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a barcode, saves its configuration to XML,
/// reloads the configuration, and generates the barcode again.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, XML export/import,
    /// and saves the resulting images to the temporary folder.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory for all output files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempDir);

        // Define file paths for the XML configuration and the two barcode images
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string imgOriginal = Path.Combine(tempDir, "original.png");
        string imgLoaded = Path.Combine(tempDir, "loaded.png");

        // --------------------------------------------------------------------
        // Create a barcode generator, configure checksum and other parameters,
        // save the barcode image, and export the generator settings to XML.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "Sample123"))
        {
            // Enable checksum calculation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Save the generated barcode as a PNG image
            generator.Save(imgOriginal, BarCodeImageFormat.Png);

            // Export the current generator configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // --------------------------------------------------------------------
        // Import the previously saved generator settings from XML and generate
        // the barcode again, saving it to a separate image file.
        // --------------------------------------------------------------------
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode generated from the imported settings
            loadedGenerator.Save(imgLoaded, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for verification
        Console.WriteLine("Original image: " + imgOriginal);
        Console.WriteLine("Loaded image: " + imgLoaded);
        Console.WriteLine("XML configuration: " + xmlPath);
    }
}