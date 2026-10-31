// Title: Export BarcodeGenerator configuration to XML
// Description: Demonstrates configuring a QR code generator, exporting its settings to an XML file, and saving the generated barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to set barcode parameters, export the generator state via ExportToXml(string), and produce an image with Save(string, BarCodeImageFormat). Developers working with barcode creation, configuration persistence, and image output commonly reference these APIs for automation and integration scenarios.
// Prompt: Export a configured BarcodeGenerator state to an XML file using ExportToXml(string) overload.
// Tags: barcode, qr, export, xml, configuration, aspose.barcode, generation, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that configures a QR barcode, exports its configuration to XML,
/// and saves the barcode image to a file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Output");

        // Ensure the output directory exists.
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Build full paths for the XML configuration file and the barcode image.
        string xmlPath = Path.Combine(outputDir, "generatorConfig.xml");
        string imagePath = Path.Combine(outputDir, "barcode.png");

        // Create and configure the BarcodeGenerator for a QR code.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample123"))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Resolution = 300f;

            // Export the current generator configuration to an XML file.
            generator.ExportToXml(xmlPath);

            // Save the generated barcode image as a PNG file.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Inform the user where the files have been saved.
        Console.WriteLine($"Barcode configuration exported to: {xmlPath}");
        Console.WriteLine($"Barcode image saved to: {imagePath}");
    }
}