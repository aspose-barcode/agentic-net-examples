// Title: Load and Update Barcode Settings XML
// Description: Demonstrates loading barcode generation settings from an XML file, modifying the XDimension property, and saving the updated configuration back to XML.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to import and export barcode settings via XML. It highlights the use of BarcodeGenerator, its Parameters.Barcode properties, and typical scenarios where developers need to persist or adjust barcode configurations programmatically. Ideal for workflows involving configuration files, batch processing, or dynamic barcode adjustments.
/// Prompt: Load barcode settings from an XML file, modify XDimension, and re‑save the updated XML.
/// Tags: barcode, xdimension, xml, import, export, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of loading barcode settings from an XML file, adjusting the XDimension,
/// and exporting the updated settings back to XML using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a default settings XML if none exists,
    /// updates the XDimension value, and saves the modified configuration.
    /// </summary>
    static void Main()
    {
        // Get the system temporary directory for demonstration files
        string tempDir = Path.GetTempPath();

        // Define paths for the input (original) and output (updated) XML files
        string inputXml = Path.Combine(tempDir, "barcodeSettings.xml");
        string outputXml = Path.Combine(tempDir, "barcodeSettings_updated.xml");

        // Ensure an input XML exists; create a default one if missing
        if (!File.Exists(inputXml))
        {
            // Create a BarcodeGenerator with a sample symbology and value
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set the initial XDimension in pixels
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Export the default settings to an XML file
                generator.ExportToXml(inputXml);
                Console.WriteLine($"Created default XML at: {inputXml}");
            }
        }

        // Load settings from the existing XML, modify XDimension, and save the updated XML
        using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(inputXml))
        {
            // Modify XDimension (example: set to 3 points)
            generator.Parameters.Barcode.XDimension.Point = 3f;

            // Export the modified settings to a new XML file
            generator.ExportToXml(outputXml);
            Console.WriteLine($"Modified XDimension and saved updated XML to: {outputXml}");
        }
    }
}