// Title: XML Serialization of Barcode Settings for API Integration
// Description: Demonstrates how to deserialize barcode configuration from JSON, apply settings to an Aspose.BarCode generator, export the generator state to XML, and re-import it to produce a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use BarcodeGenerator, EncodeTypes, and related parameter classes to configure barcodes, serialize the generator state to XML, and deserialize it back. Developers building web APIs or services that need to exchange barcode settings in XML or JSON will find this pattern useful for persisting configurations and reproducing barcodes.
// Prompt: Integrate XML serialization of barcode settings into a web API that accepts configuration JSON and returns XML.
// Tags: barcode, symbology, json, xml, serialization, aspose.barcode, generation

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode configuration deserialization, XML export/import, and image generation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Represents the JSON‑serializable configuration for a barcode.
    /// </summary>
    class BarcodeConfig
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float XDimensionPixels { get; set; }
        public string BarColor { get; set; }
    }

    /// <summary>
    /// Entry point that simulates a web API handling JSON payload, exporting settings to XML, and generating a barcode image.
    /// </summary>
    static void Main()
    {
        // Simulated JSON payload received by a web API
        string jsonPayload = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""Sample123"",
            ""XDimensionPixels"": 2.5,
            ""BarColor"": ""Blue""
        }";

        // Deserialize JSON to a strongly‑typed configuration object
        BarcodeConfig config = JsonSerializer.Deserialize<BarcodeConfig>(jsonPayload);
        if (config == null)
        {
            Console.WriteLine("Failed to parse configuration.");
            return;
        }

        // Resolve the symbology name to the corresponding EncodeTypes field via reflection
        FieldInfo field = typeof(EncodeTypes).GetField(config.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {config.Symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a barcode generator with the resolved symbology and provided code text
        using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, config.CodeText))
        {
            // Apply additional settings from the configuration
            generator.Parameters.Barcode.XDimension.Pixels = config.XDimensionPixels;
            generator.Parameters.Barcode.BarColor = Color.FromName(config.BarColor);

            // Export the generator's state to XML (simulating an API response)
            using (MemoryStream xmlStream = new MemoryStream())
            {
                generator.ExportToXml(xmlStream);
                xmlStream.Position = 0;
                string xmlResult = new StreamReader(xmlStream, Encoding.UTF8).ReadToEnd();
                Console.WriteLine("Exported XML:");
                Console.WriteLine(xmlResult);

                // Import the generator state back from the XML
                using (MemoryStream importStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResult)))
                {
                    using (BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(importStream))
                    {
                        // Save the generated barcode image to a temporary file
                        string outputPath = Path.Combine(Path.GetTempPath(), "generated_barcode.png");
                        importedGenerator.Save(outputPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"Barcode image saved to: {outputPath}");
                    }
                }
            }
        }
    }
}