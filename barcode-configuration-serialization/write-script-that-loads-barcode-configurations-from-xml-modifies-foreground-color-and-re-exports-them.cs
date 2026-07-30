// Title: Modify barcode configuration XML by changing foreground color
// Description: Demonstrates loading a barcode configuration from an XML file, updating the bar color, and exporting the modified configuration.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category, showcasing how to import and export barcode settings using BarcodeGenerator. It highlights common tasks such as adjusting visual properties (e.g., colors) of barcodes programmatically, which developers often need when customizing barcode appearance for different branding or design requirements. Ideal for developers looking to automate barcode style changes across multiple configurations.
// Prompt: Write a script that loads barcode configurations from XML, modifies the foreground color, and re‑exports them.
// Tags: barcode, xml, configuration, color, aspose.barcodes, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Loads a barcode configuration from an XML file, changes the foreground color,
/// and saves the modified configuration to a new XML file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments for input and output file paths.
    /// </summary>
    /// <param name="args">[0] Input XML path, [1] Output XML path (optional).</param>
    static void Main(string[] args)
    {
        // Determine input XML path: first argument or default filename
        string inputPath = args.Length > 0 ? args[0] : "barcodeConfig.xml";

        // Determine output XML path: second argument or default filename
        string outputPath = args.Length > 1 ? args[1] : "barcodeConfig_modified.xml";

        // Verify that the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Import barcode configuration from the specified XML file
        using (var generator = BarcodeGenerator.ImportFromXml(inputPath))
        {
            // Update the barcode's foreground (bar) color to red
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Red;

            // Export the updated configuration to the designated output XML file
            generator.ExportToXml(outputPath);
        }

        // Inform the user that the operation completed successfully
        Console.WriteLine($"Modified barcode configuration saved to: {outputPath}");
    }
}