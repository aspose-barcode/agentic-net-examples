// Title: Generate default configuration XML for postal barcode symbologies
// Description: This example creates XML configuration files that define default XDimension, BarHeight, and FilledBars settings for a set of postal barcode types.
// Category-Description: Demonstrates how to use Aspose.BarCode Generation API to export barcode generator parameters to XML. Typical use cases include establishing standard barcode appearance for postal services such as Planet, RM4SCC, Postnet, DutchKIX, OneCode, and AustraliaPost. Developers often need to create reusable configuration files to ensure consistent barcode rendering across applications.
// Prompt: Create a configuration file that defines default XDimension, BarHeight, and FilledBars for all postal barcode operations.
// Tags: postal barcode, configuration, xdimension, barheight, filledbars, aspose.barcode, xml export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting default barcode generation settings to XML for various postal symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates configuration XML files for each postal barcode type with predefined XDimension, BarHeight, and FilledBars values.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory to store the generated XML configuration files.
        string outputDir = Path.Combine(Path.GetTempPath(), "PostalConfigDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the set of postal barcode types, sample text, and a friendly name for each configuration.
        var configs = new (BaseEncodeType encode, string text, string name)[]
        {
            (EncodeTypes.Planet, "123456", "Planet"),
            (EncodeTypes.RM4SCC, "123456", "RM4SCC"),
            (EncodeTypes.Postnet, "123456", "Postnet"),
            (EncodeTypes.DutchKIX, "123456", "DutchKIX"),
            (EncodeTypes.OneCode, "12345678901234567890", "OneCode"),
            (EncodeTypes.AustraliaPost, "1100000000", "AustraliaPost")
        };

        // Iterate over each barcode configuration, set default parameters, and export to XML.
        foreach (var cfg in configs)
        {
            using (var generator = new BarcodeGenerator(cfg.encode, cfg.text))
            {
                // Set default visual parameters for the barcode.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;
                generator.Parameters.Barcode.FilledBars = true;

                // Build the full path for the XML file and export the configuration.
                string xmlPath = Path.Combine(outputDir, $"{cfg.name}Config.xml");
                generator.ExportToXml(xmlPath);

                // Inform the user about the generated file.
                Console.WriteLine($"Exported {cfg.name} configuration to: {xmlPath}");
            }
        }

        // Final status message.
        Console.WriteLine("All postal barcode default configurations have been generated.");
    }
}