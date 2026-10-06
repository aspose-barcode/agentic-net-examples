// Title: Load and Update Barcode Settings XML – Modify XDimension
// Description: Demonstrates loading barcode generation settings from an XML file, changing the XDimension property, and saving the updated configuration back to XML.
// Category-Description: This example belongs to the Aspose.BarCode generation suite, focusing on importing and exporting barcode settings via XML. It showcases the BarcodeGenerator class, its Parameters.Barcode.XDimension property, and the ImportFromXml/ExportToXml methods. Developers commonly use these APIs to persist configuration, adjust parameters programmatically, and integrate barcode settings into automated workflows.
// Prompt: Load barcode settings from an XML file, modify XDimension, and re‑save the updated XML.
// Tags: barcode, xml, xdimension, generation, export, import, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that loads barcode settings from an XML file, updates the XDimension,
/// and writes the modified settings back to a new XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments:
    /// 0 – input XML path (default: "barcodeSettings.xml")
    /// 1 – output XML path (default: "barcodeSettingsUpdated.xml")
    /// 2 – new XDimension value (default: 3.0)
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Resolve input, output, and new XDimension values from arguments or defaults.
        string inputPath = args.Length > 0 ? args[0] : "barcodeSettings.xml";
        string outputPath = args.Length > 1 ? args[1] : "barcodeSettingsUpdated.xml";
        float newXDimension = args.Length > 2 ? float.Parse(args[2]) : 3f;

        // If the input XML does not exist, create a sample barcode generator and export its settings.
        if (!File.Exists(inputPath))
        {
            // Create a sample generator with Code128 symbology and a sample value.
            using (BarcodeGenerator sampleGen = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Set an initial XDimension value.
                sampleGen.Parameters.Barcode.XDimension.Point = 2f;
                // Export the generator's settings to the specified input XML file.
                sampleGen.ExportToXml(inputPath);
            }
        }

        // Import the existing settings from the input XML file.
        using (BarcodeGenerator gen = BarcodeGenerator.ImportFromXml(inputPath))
        {
            // Update the XDimension to the new value provided by the user.
            gen.Parameters.Barcode.XDimension.Point = newXDimension;
            // Export the modified settings to the output XML file.
            gen.ExportToXml(outputPath);
        }

        // Inform the user about the performed operations.
        Console.WriteLine($"Loaded settings from: {inputPath}");
        Console.WriteLine($"Modified XDimension to: {newXDimension}");
        Console.WriteLine($"Saved updated settings to: {outputPath}");
    }
}