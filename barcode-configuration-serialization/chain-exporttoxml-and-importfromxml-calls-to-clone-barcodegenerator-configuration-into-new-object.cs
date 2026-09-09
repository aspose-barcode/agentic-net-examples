// Title: Clone BarcodeGenerator Configuration via XML Export/Import
// Description: Demonstrates exporting a BarcodeGenerator's settings to XML and importing them into a new generator to clone the configuration, then saving both barcodes as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to persist and reuse barcode generator settings using ExportToXml and ImportFromXml. It highlights key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use to create, configure, and serialize barcode definitions for reuse across applications or environments.
// Prompt: Chain ExportToXml and ImportFromXml calls to clone a BarcodeGenerator configuration into a new object.
// Tags: barcode symbology, configuration cloning, export to xml, import from xml, aspose.barcode, qrcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates cloning a BarcodeGenerator configuration by exporting to XML and importing back,
/// then saving the original and cloned barcodes as PNG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, exports its configuration to XML,
    /// imports the configuration into a new generator, and saves both images.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary directory to store generated files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeCloneDemo");
        Directory.CreateDirectory(tempDir);

        // Define file paths for the XML configuration and the PNG images
        string xmlPath = Path.Combine(tempDir, "generator.xml");
        string originalImagePath = Path.Combine(tempDir, "original.png");
        string clonedImagePath = Path.Combine(tempDir, "cloned.png");

        // Create the original barcode generator and configure its appearance
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleText"))
        {
            // Set the module size and barcode color
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Parameters.Barcode.BarColor = Color.Green;

            // Export the current configuration to an XML file
            generator.ExportToXml(xmlPath);

            // Save the generated barcode image to PNG format
            generator.Save(originalImagePath, BarCodeImageFormat.Png);
        }

        // Import the saved XML configuration into a new generator instance
        using (var clonedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the cloned barcode image to PNG format
            clonedGenerator.Save(clonedImagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine($"Original barcode saved to: {originalImagePath}");
        Console.WriteLine($"Cloned barcode saved to: {clonedImagePath}");
        Console.WriteLine($"Configuration XML saved to: {xmlPath}");
    }
}