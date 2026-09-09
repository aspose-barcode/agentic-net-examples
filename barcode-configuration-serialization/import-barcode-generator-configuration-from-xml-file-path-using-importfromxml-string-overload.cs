// Title: Import barcode generator configuration from XML file
// Description: Demonstrates exporting a barcode generator's settings to an XML file and then importing that configuration to generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showing how to persist and reuse barcode generator settings via XML. It uses BarcodeGenerator, EncodeTypes, and ExportToXml/ImportFromXml methods. Typical use cases include saving generator presets, sharing configurations across applications, or automating batch barcode creation. Developers often need to export settings for version control or to apply consistent parameters in different environments.
/// Prompt: Import barcode generator configuration from an XML file path using ImportFromXml(string) overload.
/// Tags: barcode, configuration, xml, import, export, qrcode, aspnet, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a barcode generator configuration to XML,
/// then imports the configuration to generate and save a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs export, import, and image generation.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the example files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeGenImport_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define paths for the XML configuration and the output PNG image
        string xmlPath = Path.Combine(tempFolder, "generator.xml");
        string outputPath = Path.Combine(tempFolder, "generated.png");

        // Step 1: Create a barcode generator, configure it, and export its settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample"))
        {
            // Set the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Export the current configuration to the specified XML file
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Import the generator configuration from the previously saved XML file
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Generate the barcode image and save it as PNG
            importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for verification
        Console.WriteLine($"XML configuration saved to: {xmlPath}");
        Console.WriteLine($"Generated barcode image saved to: {outputPath}");
    }
}