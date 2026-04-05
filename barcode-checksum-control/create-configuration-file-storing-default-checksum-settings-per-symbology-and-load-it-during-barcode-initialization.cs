using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Aspose.BarCode.Generation;

namespace BarcodeChecksumConfigDemo
{
    // Represents a checksum setting for a specific symbology.
    public class ChecksumConfigItem
    {
        public string Symbology { get; set; }
        public EnableChecksum ChecksumSetting { get; set; }
    }

    // Container for all checksum configurations.
    [XmlRoot("ChecksumConfig")]
    public class ChecksumConfig
    {
        [XmlElement("Item")]
        public List<ChecksumConfigItem> Items { get; set; } = new List<ChecksumConfigItem>();
    }

    class Program
    {
        static void Main()
        {
            const string configFile = "checksumConfig.xml";

            // Create default configuration if it does not exist.
            if (!File.Exists(configFile))
            {
                CreateDefaultConfig(configFile);
            }

            // Load configuration.
            ChecksumConfig config = LoadConfig(configFile);

            // Example: generate a Code39 barcode using the loaded checksum setting.
            GenerateBarcodeWithConfig(
                EncodeTypes.Code39,
                "12345",
                GetChecksumSetting(config, "Code39"),
                "code39.png");

            // Example: generate an EAN13 barcode using the loaded checksum setting.
            GenerateBarcodeWithConfig(
                EncodeTypes.EAN13,
                "1234567890128",
                GetChecksumSetting(config, "EAN13"),
                "ean13.png");
        }

        // Retrieves the checksum setting for a given symbology name; defaults to EnableChecksum.Default.
        private static EnableChecksum GetChecksumSetting(ChecksumConfig config, string symbology)
        {
            var item = config.Items.FirstOrDefault(i => string.Equals(i.Symbology, symbology, StringComparison.OrdinalIgnoreCase));
            return item?.ChecksumSetting ?? EnableChecksum.Default;
        }

        // Generates a barcode, applies the checksum setting, and saves the image.
        private static void GenerateBarcodeWithConfig(BaseEncodeType type, string codeText, EnableChecksum checksumSetting, string outputFile)
        {
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                generator.Parameters.Barcode.IsChecksumEnabled = checksumSetting;
                // Optionally show checksum digit when supported.
                generator.Parameters.Barcode.ChecksumAlwaysShow = true;
                generator.Save(outputFile);
                Console.WriteLine($"Saved {outputFile} with checksum setting: {checksumSetting}");
            }
        }

        // Creates a default XML configuration file with sample checksum settings.
        private static void CreateDefaultConfig(string filePath)
        {
            var defaultConfig = new ChecksumConfig
            {
                Items = new List<ChecksumConfigItem>
                {
                    new ChecksumConfigItem { Symbology = "Code39", ChecksumSetting = EnableChecksum.Yes },
                    new ChecksumConfigItem { Symbology = "Code128", ChecksumSetting = EnableChecksum.Default },
                    new ChecksumConfigItem { Symbology = "EAN13", ChecksumSetting = EnableChecksum.Default },
                    new ChecksumConfigItem { Symbology = "ITF14", ChecksumSetting = EnableChecksum.No }
                }
            };

            var serializer = new XmlSerializer(typeof(ChecksumConfig));
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(stream, defaultConfig);
            }

            Console.WriteLine($"Created default checksum configuration at '{filePath}'.");
        }

        // Loads the XML configuration file into a ChecksumConfig object.
        private static ChecksumConfig LoadConfig(string filePath)
        {
            var serializer = new XmlSerializer(typeof(ChecksumConfig));
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return (ChecksumConfig)serializer.Deserialize(stream);
            }
        }
    }
}