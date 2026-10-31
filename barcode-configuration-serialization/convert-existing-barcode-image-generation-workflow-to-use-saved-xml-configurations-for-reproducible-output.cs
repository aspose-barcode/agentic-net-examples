// Title: Generate QR barcode using XML configuration for reproducibility
// Description: Demonstrates creating a QR barcode, exporting its settings to XML, and re‑creating the barcode from that XML to ensure identical output.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use BarcodeGenerator, its Parameters, ExportToXml, and ImportFromXml methods to persist and reuse barcode settings. Developers often need reproducible barcode generation across environments, CI pipelines, or when sharing configurations between applications.
// Prompt: Convert an existing barcode image generation workflow to use saved XML configurations for reproducible output.
// Tags: qr, barcode, xml configuration, export, import, aspnet, aspose.barcode, image generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, exporting settings to XML, and re‑creating the barcode from the saved configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR barcode, saves its image and configuration, then regenerates the barcode from the XML.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the original image, XML config, and the image generated from XML
        string originalPath = Path.Combine(outputDir, "original.png");
        string xmlConfigPath = Path.Combine(outputDir, "config.xml");
        string loadedPath = Path.Combine(outputDir, "loaded.png");

        // Create and configure a barcode generator for a QR code
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            // Set visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarColor = Color.Green;

            // Save the barcode image
            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Export the current configuration to an XML file
            generator.ExportToXml(xmlConfigPath);
        }

        // Import the saved XML configuration and generate the barcode again
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlConfigPath))
        {
            // Save the barcode image generated from the imported configuration
            importedGenerator.Save(loadedPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine("Original barcode saved to: " + originalPath);
        Console.WriteLine("XML configuration saved to: " + xmlConfigPath);
        Console.WriteLine("Barcode generated from XML saved to: " + loadedPath);
    }
}