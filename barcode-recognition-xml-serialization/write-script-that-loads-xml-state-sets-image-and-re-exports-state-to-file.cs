// Title: Load barcode generator state from XML, generate image, and export state
// Description: Demonstrates loading a barcode generator configuration from an XML file, generating a barcode image, and exporting the (potentially modified) state back to XML.
// Prompt: Write a script that loads an XML state, sets an image, and re‑exports the state to a file.
// Tags: barcode symbology, generation, xml, export, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that loads a barcode generator state from an XML file,
/// generates a barcode image, and re‑exports the (possibly modified) state to a new XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the input XML file containing the barcode generator state
        string inputXmlPath = "barcode_state.xml";

        // Path where the generated barcode image will be saved
        string outputImagePath = "generated_barcode.png";

        // Path where the (potentially modified) generator state will be exported
        string outputXmlPath = "exported_state.xml";

        // Verify that the input XML file exists before proceeding
        if (!File.Exists(inputXmlPath))
        {
            Console.WriteLine($"Input XML file not found: {inputXmlPath}");
            return;
        }

        // Load the BarcodeGenerator configuration from the XML file
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(inputXmlPath))
        {
            // Generate the barcode image based on the loaded configuration
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                // Save the generated image as a PNG file
                bitmap.Save(outputImagePath, ImageFormat.Png);
            }

            // Export the current generator state back to an XML file
            generator.ExportToXml(outputXmlPath);
        }

        Console.WriteLine("Barcode image generated and state exported successfully.");
    }
}