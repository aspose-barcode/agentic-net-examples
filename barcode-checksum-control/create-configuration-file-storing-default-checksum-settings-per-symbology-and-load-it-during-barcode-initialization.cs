using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Path to the configuration file
    private const string ConfigFilePath = "checksumConfig.xml";

    static void Main()
    {
        // Step 1: Create default checksum configuration and save it
        CreateDefaultChecksumConfig();

        // Step 2: Load configuration into a dictionary
        var checksumSettings = LoadChecksumConfig();

        // Example usage: generate a barcode for each configured symbology
        foreach (var kvp in checksumSettings)
        {
            string symbologyName = kvp.Key;
            EnableChecksum checksumMode = kvp.Value;

            // Resolve symbology name to BaseEncodeType via reflection
            var prop = typeof(EncodeTypes).GetProperty(symbologyName,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase);
            if (prop == null)
            {
                Console.WriteLine($"Symbology '{symbologyName}' not found. Skipping.");
                continue;
            }

            var encodeType = (BaseEncodeType)prop.GetValue(null);

            // Sample code text (simple placeholder)
            string codeText = "1234567890";

            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply checksum setting from configuration
                generator.Parameters.Barcode.IsChecksumEnabled = checksumMode;

                // Save barcode image
                string fileName = $"{symbologyName}.png";
                generator.Save(fileName);
                Console.WriteLine($"Generated {symbologyName} barcode with checksum mode '{checksumMode}' -> {fileName}");
            }
        }
    }

    // Creates a default configuration file with checksum settings per symbology
    private static void CreateDefaultChecksumConfig()
    {
        var defaults = new List<(string Symbology, EnableChecksum Checksum)>
        {
            ("Code128", EnableChecksum.Yes),
            ("Code39FullASCII", EnableChecksum.Default),
            ("Codabar", EnableChecksum.No),
            ("EAN13", EnableChecksum.Default),
            ("QRCode", EnableChecksum.Default) // QR does not use checksum but kept for completeness
        };

        var doc = new XDocument(
            new XElement("ChecksumConfig",
                defaults.Select(d =>
                    new XElement("Symbology",
                        new XAttribute("Name", d.Symbology),
                        new XAttribute("Checksum", d.Checksum.ToString())))));

        doc.Save(ConfigFilePath);
        Console.WriteLine($"Default checksum configuration saved to '{ConfigFilePath}'.");
    }

    // Loads the configuration file into a dictionary of symbology -> checksum mode
    private static Dictionary<string, EnableChecksum> LoadChecksumConfig()
    {
        var dict = new Dictionary<string, EnableChecksum>(StringComparer.OrdinalIgnoreCase);

        if (!File.Exists(ConfigFilePath))
        {
            Console.WriteLine($"Configuration file '{ConfigFilePath}' not found. No checksum settings will be applied.");
            return dict;
        }

        var doc = XDocument.Load(ConfigFilePath);
        foreach (var elem in doc.Root?.Elements("Symbology") ?? Enumerable.Empty<XElement>())
        {
            var nameAttr = elem.Attribute("Name");
            var checksumAttr = elem.Attribute("Checksum");
            if (nameAttr == null || checksumAttr == null)
                continue;

            if (Enum.TryParse<EnableChecksum>(checksumAttr.Value, out var mode))
            {
                dict[nameAttr.Value] = mode;
            }
            else
            {
                Console.WriteLine($"Invalid checksum value '{checksumAttr.Value}' for symbology '{nameAttr.Value}'. Using default.");
                dict[nameAttr.Value] = EnableChecksum.Default;
            }
        }

        return dict;
    }
}