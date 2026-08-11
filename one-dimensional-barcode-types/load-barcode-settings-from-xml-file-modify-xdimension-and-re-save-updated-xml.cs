// Title: Load and Modify Barcode Settings XML – XDimension Update
// Description: Demonstrates loading barcode generation settings from an XML file, changing the XDimension (module size) and exporting the updated configuration.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on importing and exporting barcode settings via XML. It showcases the use of BarcodeGenerator, EncodeTypes, and the Parameters.Barcode.XDimension property. Typical scenarios include persisting barcode configurations, batch updates, and integrating barcode settings with external configuration files.
// Prompt: Load barcode settings from an XML file, modify XDimension, and re‑save the updated XML.
// Tags: barcode, xml, xdimension, settings, export, import, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that loads barcode settings from an XML file,
/// updates the XDimension (module size), and saves the modified settings
/// back to a new XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs the load‑modify‑save workflow.
    /// </summary>
    static void Main()
    {
        // Define file paths for the source and destination XML files
        string inputXml = "barcodeSettings.xml";
        string outputXml = "barcodeSettings_updated.xml";

        // --------------------------------------------------------------------
        // Create a sample XML file if the expected input does not exist.
        // This ensures the example can run standalone.
        // --------------------------------------------------------------------
        if (!File.Exists(inputXml))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Initialise XDimension to 1 point (default module size)
                generator.Parameters.Barcode.XDimension.Point = 1f;

                // Export the initial settings to an XML file
                generator.ExportToXml(inputXml);
                Console.WriteLine($"Sample XML created at '{inputXml}'.");
            }
        }

        // --------------------------------------------------------------------
        // Verify that the input XML file is present before attempting import.
        // --------------------------------------------------------------------
        if (!File.Exists(inputXml))
        {
            Console.WriteLine($"Input XML file '{inputXml}' not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Import barcode settings from the XML, modify XDimension, and export.
        // --------------------------------------------------------------------
        using (var generator = BarcodeGenerator.ImportFromXml(inputXml))
        {
            // Update the XDimension to 2 points (increase module size)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the updated configuration to a new XML file
            bool saved = generator.ExportToXml(outputXml);
            Console.WriteLine(saved
                ? $"Updated XML saved to '{outputXml}'."
                : $"Failed to save updated XML to '{outputXml}'.");
        }
    }
}