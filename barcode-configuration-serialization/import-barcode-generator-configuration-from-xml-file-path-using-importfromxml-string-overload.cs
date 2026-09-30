// Title: Import Barcode Generator Settings from XML and Generate Image
// Description: Demonstrates exporting a barcode generator configuration to an XML file and then importing it to create a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It showcases the use of BarcodeGenerator, ExportToXml, and ImportFromXml methods to persist and reuse barcode settings. Typical scenarios include sharing barcode templates across applications, version‑controlling barcode designs, and simplifying deployment pipelines. Developers often need to export settings to XML, edit them manually or programmatically, and re‑import them to generate consistent barcodes.
// Prompt: Import barcode generator configuration from an XML file path using ImportFromXml(string) overload.
// Tags: barcode symbology, import, export, xml, generation, png, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to export a barcode generator's configuration to an XML file,
/// import that configuration back, and generate a barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑import workflow and saves the resulting barcode image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define paths for the XML configuration file and the output barcode image
        string xmlPath = Path.Combine(tempDir, "config.xml");
        string outputPath = Path.Combine(tempDir, "barcode.png");

        // Step 1: Create a barcode generator, configure it, and export its settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set a custom barcode color
            generator.Parameters.Barcode.BarColor = Color.Blue;

            // Export the current configuration to an XML file
            generator.ExportToXml(xmlPath);
        }

        // Step 2: Import the configuration from the XML file and generate the barcode image
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the generated barcode as a PNG image
            importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine("Barcode image saved to: " + outputPath);
        // Note: The temporary files are left on disk for inspection.
        // To clean up, uncomment the following line:
        // Directory.Delete(tempDir, true);
    }
}