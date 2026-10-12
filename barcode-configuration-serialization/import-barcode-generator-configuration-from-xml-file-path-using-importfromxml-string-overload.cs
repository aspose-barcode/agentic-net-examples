// Title: Import barcode generator configuration from XML and generate barcode image
// Description: Demonstrates exporting a BarcodeGenerator's settings to an XML file, then importing that configuration to create a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist and reuse barcode generator settings via XML. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common for scenarios where barcode appearance must be standardized across applications or sessions. Developers often need to export settings for backup, sharing, or dynamic generation based on stored configurations.
// Prompt: Import barcode generator configuration from an XML file path using ImportFromXml(string) overload.
// Tags: code128, import, png, barcodegenerator, aspose.barcodes

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator configuration to XML and importing it to generate a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a temporary directory, exports generator settings to XML, imports them, and saves the barcode as PNG.
    /// </summary>
    static void Main()
    {
        // Build a unique temporary folder for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeGenXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define paths for the XML configuration and the resulting barcode image
        string xmlPath = Path.Combine(tempDir, "generatorConfig.xml");
        string outputPath = Path.Combine(tempDir, "generatedBarcode.png");

        // Create a barcode generator, customize a parameter, and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // Set X-dimension for better readability
            generator.ExportToXml(xmlPath); // Persist the current settings
        }

        // Import the previously saved configuration and generate the barcode image
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            importedGenerator.Save(outputPath, BarCodeImageFormat.Png); // Save as PNG
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode image saved to: " + outputPath);
    }
}