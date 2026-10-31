// Title: Import barcode generator configuration from XML file
// Description: Demonstrates exporting a barcode generator's settings to an XML file and then importing that configuration to generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showing how to persist and reuse barcode generator settings via XML. It uses BarcodeGenerator, its ExportToXml and ImportFromXml methods, and illustrates typical scenarios such as saving configurations for later reuse or sharing across applications. Developers often need to serialize generator parameters to files and later recreate identical barcodes without re‑coding settings.
// Prompt: Import barcode generator configuration from an XML file path using ImportFromXml(string) overload.
// Tags: barcode, configuration, xml, import, export, qrcode, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that exports a barcode generator configuration to XML,
/// then imports the configuration to create a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑import workflow and saves the resulting image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary working directory
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeGenXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define file paths for the XML configuration and the output image
        string xmlPath = Path.Combine(workDir, "generator.xml");
        string outputPath = Path.Combine(workDir, "generated.png");

        // Step 1: Create a barcode generator, configure it, and export its settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample"))
        {
            // Example configuration: set the X‑dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;

            // Persist the current configuration to the specified XML file
            generator.ExportToXml(xmlPath);
        }

        // Verify that the XML file was successfully created
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create XML configuration file.");
            return;
        }

        // Step 2: Import the generator configuration from the XML file
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Generate the barcode image using the imported settings and save it as PNG
            importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files for reference
        Console.WriteLine($"XML configuration saved at: {xmlPath}");
        Console.WriteLine($"Generated barcode image saved at: {outputPath}");
    }
}