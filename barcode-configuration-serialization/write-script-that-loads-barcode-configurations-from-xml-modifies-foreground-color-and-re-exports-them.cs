// Title: Load, modify, and re-export barcode configuration via XML
// Description: Demonstrates loading a barcode configuration from an XML file, changing the bar color, and saving both the image and updated XML.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It showcases the BarcodeGenerator class together with its ExportToXml and ImportFromXml methods, which are commonly used to persist barcode settings, adjust visual properties such as colors, and regenerate barcodes without recreating the generator from scratch. Developers working with batch barcode processing, dynamic styling, or configuration versioning often rely on these APIs.
// Prompt: Write a script that loads barcode configurations from XML, modifies the foreground color, and re‑exports them.
// Tags: barcode symbology, configuration, xml, color, export, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a barcode, exports its configuration to XML,
/// reloads the configuration, changes the bar color, and re‑exports both the
/// modified image and XML configuration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the create‑export‑modify‑re‑export workflow.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define temporary file paths for the original and modified assets
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.xml");
        string modifiedXmlPath = Path.Combine(Path.GetTempPath(), "barcodeConfigModified.xml");
        string originalImagePath = Path.Combine(Path.GetTempPath(), "barcode.png");
        string modifiedImagePath = Path.Combine(Path.GetTempPath(), "barcode_modified.png");

        // ------------------------------------------------------------
        // Step 1: Create a barcode, set its initial color, save the image,
        // and export the generator configuration to an XML file.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the initial foreground (bar) color to black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the barcode image as PNG
            generator.Save(originalImagePath, BarCodeImageFormat.Png);

            // Export the current generator settings to XML
            generator.ExportToXml(xmlPath);
        }

        // Verify that the XML configuration file was successfully created
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine("Failed to create the initial XML configuration.");
            return;
        }

        // ------------------------------------------------------------
        // Step 2: Load the previously saved configuration, modify the bar
        // color, save the updated image, and export the new configuration.
        // ------------------------------------------------------------
        using (var loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Change the foreground (bar) color to blue
            loadedGenerator.Parameters.Barcode.BarColor = Color.Blue;

            // Save the modified barcode image to verify the color change
            loadedGenerator.Save(modifiedImagePath, BarCodeImageFormat.Png);

            // Export the modified generator settings to a new XML file
            loadedGenerator.ExportToXml(modifiedXmlPath);
        }

        // Output the locations of the generated files for reference
        Console.WriteLine("Original barcode image saved to: " + originalImagePath);
        Console.WriteLine("Modified barcode image saved to: " + modifiedImagePath);
        Console.WriteLine("Original XML configuration saved to: " + xmlPath);
        Console.WriteLine("Modified XML configuration saved to: " + modifiedXmlPath);
    }
}