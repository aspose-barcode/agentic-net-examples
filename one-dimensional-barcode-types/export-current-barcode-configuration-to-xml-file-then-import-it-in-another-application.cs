// Title: Export and Import Barcode Generator Configuration via XML
// Description: Demonstrates exporting a configured BarcodeGenerator to an XML file and then importing that configuration in a new instance to generate identical barcodes.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, illustrating how to persist and reuse barcode generator settings using the ExportToXml and ImportFromXml APIs. Developers working with dynamic barcode generation often need to save generator parameters to files for later reuse, sharing across applications, or version control. The example showcases key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, providing a practical pattern for configuration serialization.
// Prompt: Export current barcode configuration to an XML file, then import it in another application.
// Tags: barcode, configuration, export, import, xml, aspose.barcode, qrcode, generation, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a barcode generator's configuration to XML and importing it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, saves its configuration to XML,
    /// then loads the configuration in a new generator to produce the same barcode image.
    /// </summary>
    static void Main()
    {
        // Prepare temporary directory and file paths
        string baseDir = Path.Combine(Path.GetTempPath(), "BarcodeExportDemo");
        Directory.CreateDirectory(baseDir);
        string xmlPath = Path.Combine(baseDir, "generatorConfig.xml");
        string originalImagePath = Path.Combine(baseDir, "original.png");
        string importedImagePath = Path.Combine(baseDir, "imported.png");

        // Create a barcode generator, configure its appearance, and export settings to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set module size, font size, and bar color
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Pixels = 12f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Persist the current configuration to an XML file
            generator.ExportToXml(xmlPath);

            // Generate and save the original barcode image
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
        }

        // Load the previously saved configuration into a new generator instance
        var importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);

        // Generate and save the barcode image using the imported configuration
        importedGenerator.Save(importedImagePath, BarCodeImageFormat.Png);
        importedGenerator.Dispose();

        // Output the locations of the generated files
        Console.WriteLine("Exported XML: " + xmlPath);
        Console.WriteLine("Original barcode image: " + originalImagePath);
        Console.WriteLine("Imported barcode image: " + importedImagePath);
    }
}