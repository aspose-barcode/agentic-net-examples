// Title: Export and Import Barcode Generator Configuration via XML
// Description: Demonstrates exporting a configured barcode generator to an XML file and importing it to recreate the barcode in another application.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to persist barcode settings using the BarcodeGenerator class. Developers often need to share or reuse barcode configurations across projects or environments; exporting to XML and later importing provides a portable, human‑readable format. Typical use cases include configuration versioning, deployment pipelines, and cross‑application barcode generation.
// Prompt: Export current barcode configuration to an XML file, then import it in another application.
// Tags: barcode, export, import, xml, configuration, generation, qrcode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that exports a barcode generator's configuration to XML,
/// then imports the configuration to generate the same barcode again.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, saves its image,
    /// exports the generator settings to XML, imports the settings,
    /// and generates a second image from the imported configuration.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare temporary working directory and file paths
        // ------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(workDir);
        string xmlPath = Path.Combine(workDir, "generatorConfig.xml");
        string originalPath = Path.Combine(workDir, "barcode_original.png");
        string loadedPath = Path.Combine(workDir, "barcode_loaded.png");

        // ------------------------------------------------------------
        // Create a barcode generator, configure it, save the image,
        // and export the configuration to an XML file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the X dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated QR code as a PNG image
            generator.Save(originalPath, BarCodeImageFormat.Png);

            // Export the current generator configuration to XML
            generator.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Import the previously saved configuration and generate a new barcode
        // ------------------------------------------------------------
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the barcode generated from the imported configuration
            loadedGenerator.Save(loadedPath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Output the locations of the generated files
        // ------------------------------------------------------------
        Console.WriteLine($"Original barcode saved to: {originalPath}");
        Console.WriteLine($"Configuration exported to XML: {xmlPath}");
        Console.WriteLine($"Barcode generated from imported config saved to: {loadedPath}");
    }
}