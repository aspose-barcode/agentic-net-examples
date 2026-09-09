// Title: Export BarcodeGenerator Configuration to XML
// Description: Demonstrates exporting a configured BarcodeGenerator's state to an XML file and generating a barcode image for verification.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure a BarcodeGenerator, export its settings using ExportToXml(string), and save a barcode image. It highlights key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, which developers commonly use for creating, customizing, and persisting barcode configurations in .NET applications.
// Prompt: Export a configured BarcodeGenerator state to an XML file using ExportToXml(string) overload.
// Tags: barcode symbology, export, xml, configuration, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a configured BarcodeGenerator state to XML and creating a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary folder, configures a BarcodeGenerator, exports its settings to XML,
    /// saves a barcode image, and writes the output paths to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for output files
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeGenXmlDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Define paths for the generated XML configuration and barcode image
        string xmlPath = Path.Combine(outputFolder, "generatorConfig.xml");
        string imagePath = Path.Combine(outputFolder, "barcode.png");

        // Configure the BarcodeGenerator and export its state
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set barcode visual properties
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarColor = Color.Green;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Helvetica";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Pixels = 12f;

            // Export the current configuration to an XML file
            generator.ExportToXml(xmlPath);

            // Generate and save a barcode image to verify the configuration
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Output the locations of the generated files
        Console.WriteLine("Barcode generator state exported to XML:");
        Console.WriteLine(xmlPath);
        Console.WriteLine("Generated barcode image:");
        Console.WriteLine(imagePath);
    }
}