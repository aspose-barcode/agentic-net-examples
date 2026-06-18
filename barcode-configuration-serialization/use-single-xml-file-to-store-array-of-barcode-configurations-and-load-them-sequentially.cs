using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeBatchFromXml
{
    // Simple DTO for a single barcode configuration
    public class BarcodeConfig
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
    }

    // Container for an array of configurations
    [XmlRoot("BarcodeConfigurations")]
    public class BarcodeConfigCollection
    {
        [XmlElement("BarcodeConfig")]
        public List<BarcodeConfig> Items { get; set; } = new List<BarcodeConfig>();
    }

    /// <summary>
    /// Entry point for the barcode batch generation application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that orchestrates loading configurations, generating barcodes, and handling errors.
        /// </summary>
        static void Main()
        {
            const string xmlPath = "barcodeConfigs.xml";

            // Ensure the XML file exists; if not, create a sample file
            if (!File.Exists(xmlPath))
            {
                CreateSampleXml(xmlPath);
                Console.WriteLine($"Sample configuration file created at '{Path.GetFullPath(xmlPath)}'.");
            }

            // Load configurations from XML
            BarcodeConfigCollection configCollection = LoadConfigurations(xmlPath);
            if (configCollection == null || configCollection.Items.Count == 0)
            {
                Console.WriteLine("No barcode configurations found.");
                return;
            }

            // Process each configuration sequentially
            int index = 1;
            foreach (var cfg in configCollection.Items)
            {
                // Resolve symbology name to BaseEncodeType via reflection
                var field = typeof(EncodeTypes).GetField(cfg.Symbology);
                if (field == null)
                {
                    Console.WriteLine($"[#{index}] Unknown symbology '{cfg.Symbology}'. Skipping.");
                    index++;
                    continue;
                }

                // Cast the reflected value to the appropriate enum type
                var encodeType = (BaseEncodeType)field.GetValue(null);
                string outputFile = $"barcode_{index}_{cfg.Symbology}.png";

                try
                {
                    // Generate the barcode using Aspose.BarCode
                    using (var generator = new BarcodeGenerator(encodeType, cfg.CodeText))
                    {
                        // Example: enable checksum (optional)
                        generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

                        // Save the barcode image to a PNG file
                        generator.Save(outputFile);
                    }

                    Console.WriteLine($"[#{index}] Generated '{outputFile}' for symbology '{cfg.Symbology}'.");
                }
                catch (Exception ex)
                {
                    // Report any errors that occur during generation
                    Console.WriteLine($"[#{index}] Error generating barcode: {ex.Message}");
                }

                index++;
            }
        }

        // Creates a sample XML file with a few barcode configurations
        private static void CreateSampleXml(string path)
        {
            var sample = new BarcodeConfigCollection();
            sample.Items.Add(new BarcodeConfig { Symbology = "Code128", CodeText = "Sample123" });
            sample.Items.Add(new BarcodeConfig { Symbology = "QR", CodeText = "https://example.com" });
            sample.Items.Add(new BarcodeConfig { Symbology = "DataMatrix", CodeText = "DM12345" });

            var serializer = new XmlSerializer(typeof(BarcodeConfigCollection));
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(stream, sample);
            }
        }

        // Deserializes the XML file into a collection of configurations
        private static BarcodeConfigCollection LoadConfigurations(string path)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(BarcodeConfigCollection));
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return (BarcodeConfigCollection)serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                // Log deserialization errors and return null to indicate failure
                Console.WriteLine($"Failed to load configurations: {ex.Message}");
                return null;
            }
        }
    }
}