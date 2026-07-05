// Title: Barcode checksum configuration example
// Description: Demonstrates creating per‑symbology XML configuration files that store default checksum settings and loading one of them during barcode generation.
// Prompt: Create a configuration file storing default checksum settings per symbology and load it during barcode initialization.
// Tags: barcode symbology, checksum, configuration, xml, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates XML configuration files for barcode checksum settings
/// and shows how to load a configuration during barcode generation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a folder to store the generated XML configuration files
        // ------------------------------------------------------------
        string configDir = Path.Combine(Directory.GetCurrentDirectory(), "Config");
        if (!Directory.Exists(configDir))
        {
            Directory.CreateDirectory(configDir);
        }

        // ------------------------------------------------------------
        // Define a set of symbologies together with their default checksum preferences
        // ------------------------------------------------------------
        var symbologySettings = new (BaseEncodeType type, EnableChecksum checksum)[]
        {
            (EncodeTypes.Code128, EnableChecksum.Yes),          // Code128 always uses checksum
            (EncodeTypes.Code39FullASCII, EnableChecksum.No),   // Example: disable checksum for Code39FullASCII
            (EncodeTypes.EAN13, EnableChecksum.Yes)            // EAN13 requires checksum
        };

        // ------------------------------------------------------------
        // Generate an XML configuration file for each symbology
        // ------------------------------------------------------------
        foreach (var (type, checksum) in symbologySettings)
        {
            string configPath = Path.Combine(configDir, $"{type.TypeName}_checksum.xml");

            // Use a placeholder codetext; it will be replaced after loading
            using (var generator = new BarcodeGenerator(type, "123456"))
            {
                // Apply the default checksum setting for this symbology
                generator.Parameters.Barcode.IsChecksumEnabled = checksum;

                // Export the current generator settings to an XML file
                generator.ExportToXml(configPath);
            }
        }

        // ------------------------------------------------------------
        // Demonstrate loading a configuration file and generating a barcode
        // ------------------------------------------------------------
        // Choose a symbology to demonstrate (Code128 in this case)
        BaseEncodeType demoType = EncodeTypes.Code128;
        string demoConfigPath = Path.Combine(configDir, $"{demoType.TypeName}_checksum.xml");

        if (!File.Exists(demoConfigPath))
        {
            Console.WriteLine($"Configuration file not found: {demoConfigPath}");
            return;
        }

        // Load the configuration, set the actual codetext, and save the barcode image
        using (var generator = BarcodeGenerator.ImportFromXml(demoConfigPath))
        {
            // Assign the real codetext to encode
            generator.CodeText = "ABC1234567890";

            // Save the generated barcode image to the current directory
            string outputImage = Path.Combine(Directory.GetCurrentDirectory(), "barcode_demo.png");
            generator.Save(outputImage);
            Console.WriteLine($"Barcode saved to: {outputImage}");
        }
    }
}