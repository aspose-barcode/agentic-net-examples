// Title: Barcode checksum configuration via XML files
// Description: Demonstrates creating per‑symbology checksum defaults, storing them in XML, and loading them when generating barcodes.
// Category-Description: This example belongs to the Aspose.BarCode configuration management category. It shows how to use EncodeTypes, BarcodeGenerator, and the ExportToXml / ImportFromXml APIs to persist and reuse barcode settings such as checksum enablement. Developers often need to maintain consistent barcode parameters across applications, and this pattern provides a reusable approach for default configuration files.
// Prompt: Create a configuration file storing default checksum settings per symbology and load it during barcode initialization.
// Tags: barcode, checksum, configuration, xml, aspnet, aspose.barcode, generation, encode-types

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating and using XML configuration files for default checksum settings per symbology.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates configuration files, then loads them to produce sample barcode images.
    /// </summary>
    static void Main()
    {
        // Define base output directory and subdirectory for configuration files
        string baseOutput = Path.Combine(Directory.GetCurrentDirectory(), "output");
        string configDir = Path.Combine(baseOutput, "config");
        Directory.CreateDirectory(baseOutput);
        Directory.CreateDirectory(configDir);

        // Define default checksum settings per symbology
        var defaultSettings = new Dictionary<string, EnableChecksum>
        {
            { "Code128", EnableChecksum.Yes },          // checksum always required
            { "Code39FullASCII", EnableChecksum.No },   // optional checksum disabled
            { "EAN13", EnableChecksum.Yes }             // checksum required
        };

        // -----------------------------------------------------------------
        // Create configuration XML files for each symbology
        // -----------------------------------------------------------------
        foreach (var kvp in defaultSettings)
        {
            string symbologyName = kvp.Key;
            EnableChecksum checksumSetting = kvp.Value;

            // Resolve symbology name to BaseEncodeType via reflection
            var field = typeof(EncodeTypes).GetField(symbologyName);
            if (field == null)
            {
                Console.WriteLine($"Unknown symbology: {symbologyName}");
                continue;
            }
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create a generator, apply the checksum setting, and export to XML
            using (var generator = new BarcodeGenerator(encodeType))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
                string xmlPath = Path.Combine(configDir, $"{symbologyName}.xml");
                generator.ExportToXml(xmlPath);
                Console.WriteLine($"Exported config for {symbologyName} to {xmlPath}");
            }
        }

        // -----------------------------------------------------------------
        // Load each configuration file and generate a sample barcode image
        // -----------------------------------------------------------------
        foreach (var kvp in defaultSettings)
        {
            string symbologyName = kvp.Key;
            string xmlPath = Path.Combine(configDir, $"{symbologyName}.xml");

            // Verify that the configuration file exists
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"Config file missing: {xmlPath}");
                continue;
            }

            // Import generator settings from the XML configuration
            BarcodeGenerator loadedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
            if (loadedGenerator == null)
            {
                Console.WriteLine($"Failed to import config for {symbologyName}");
                continue;
            }

            // Set a sample CodeText appropriate for the current symbology
            string sampleText = symbologyName switch
            {
                "Code128" => "ABC123456",
                "Code39FullASCII" => "CODE39*",
                "EAN13" => "1234567890128",
                _ => "Sample"
            };
            loadedGenerator.CodeText = sampleText;

            // Save the generated barcode image to the output directory
            string imagePath = Path.Combine(baseOutput, $"{symbologyName}.png");
            loadedGenerator.Save(imagePath);
            Console.WriteLine($"Generated barcode for {symbologyName} at {imagePath}");

            // Clean up the imported generator instance
            loadedGenerator.Dispose();
        }

        Console.WriteLine("Processing completed.");
    }
}