// Title: Clone BarcodeGenerator configuration via XML export/import
// Description: Demonstrates exporting a BarcodeGenerator's settings to XML and importing them into a new instance, effectively cloning the configuration.
// Prompt: Chain ExportToXml and ImportFromXml calls to clone a BarcodeGenerator configuration into a new object.
// Tags: barcode symbology, configuration cloning, xml, export, import, aspose.barcodes, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that shows how to clone a BarcodeGenerator configuration
/// by exporting it to XML and then importing it into a new generator instance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Performs the export, import, and image generation steps.
    /// </summary>
    static void Main()
    {
        // Define file paths for the temporary XML configuration and the output images
        string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "barcodeConfig.xml");
        string originalImagePath = Path.Combine(Directory.GetCurrentDirectory(), "original.png");
        string clonedImagePath = Path.Combine(Directory.GetCurrentDirectory(), "cloned.png");

        // Create and configure the original barcode generator
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set some non-default parameters to demonstrate cloning
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;
            generator.Parameters.Resolution = 150;

            // Save the original barcode image to file
            generator.Save(originalImagePath);

            // Export the current configuration to an XML file for later import
            generator.ExportToXml(xmlPath);
        }

        // Import the configuration from the XML file into a new generator instance
        using (BarcodeGenerator clonedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Save the cloned barcode image (should be identical to the original)
            clonedGenerator.Save(clonedImagePath);
        }

        // Clean up the temporary XML file used for cloning
        if (File.Exists(xmlPath))
        {
            File.Delete(xmlPath);
        }

        // Output the locations of the generated images for verification
        Console.WriteLine("Original barcode saved to: " + originalImagePath);
        Console.WriteLine("Cloned barcode saved to: " + clonedImagePath);
    }
}