// Title: Clone BarcodeGenerator Configuration Using ExportToXml and ImportFromXml
// Description: Demonstrates how to export a BarcodeGenerator's settings to XML and import them into a new instance, effectively cloning the configuration.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing the use of ExportToXml and ImportFromXml methods of BarcodeGenerator. Developers often need to persist barcode settings, share them across applications, or duplicate generators with identical parameters. The example highlights key classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, useful for scenarios like batch processing or template-based barcode creation.
// Prompt: Chain ExportToXml and ImportFromXml calls to clone a BarcodeGenerator configuration into a new object.
// Tags: code128, export, import, xml, clone, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates cloning a BarcodeGenerator configuration via XML export/import.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates an original barcode, saves it, clones its configuration, and saves the cloned barcode.
    /// </summary>
    static void Main()
    {
        // Create an original barcode generator with Code128 symbology and sample data
        using (var originalGenerator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Configure visual appearance and caption for the original barcode
            originalGenerator.Parameters.Barcode.BarColor = Color.Blue;
            originalGenerator.Parameters.Barcode.XDimension.Point = 2f;
            originalGenerator.Parameters.CaptionAbove.Text = "Original Barcode";
            originalGenerator.Parameters.CaptionAbove.Alignment = TextAlignment.Center;
            originalGenerator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            originalGenerator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

            // Save the original barcode image to a temporary PNG file
            string originalPath = Path.Combine(Path.GetTempPath(), "original.png");
            originalGenerator.Save(originalPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Original barcode saved to: {originalPath}");

            // Export the generator's configuration to an XML file for later reuse
            string xmlPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.xml");
            originalGenerator.ExportToXml(xmlPath);

            // Import the saved XML configuration into a new generator instance (clone)
            using (var clonedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
            {
                // Modify the caption to demonstrate that this is a separate instance
                clonedGenerator.Parameters.CaptionAbove.Text = "Cloned Barcode";

                // Save the cloned barcode image to a temporary PNG file
                string clonedPath = Path.Combine(Path.GetTempPath(), "cloned.png");
                clonedGenerator.Save(clonedPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Cloned barcode saved to: {clonedPath}");
            }
        }
    }
}