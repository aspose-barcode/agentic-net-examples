// Title: XML Serialization of Barcode Settings via Web API
// Description: Demonstrates converting barcode configuration JSON into Aspose.BarCode XML settings, suitable for returning from a web API.
// Category-Description: This example belongs to the Aspose.BarCode configuration serialization category, illustrating how to use BarcodeGenerator, EncodeTypes, and ExportToXml to transform runtime barcode settings into XML. Developers building web services often need to accept JSON payloads, configure barcode generation, and expose the resulting configuration as XML for downstream processing or storage.
// Prompt: Integrate XML serialization of barcode settings into a web API that accepts configuration JSON and returns XML.
// Tags: barcode symbology serialization json xml aspose.barcode generation

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates serialization of barcode generator settings to XML based on JSON configuration.
/// </summary>
class Program
{
    // Simple configuration model matching expected JSON structure
    private class BarcodeConfig
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? XDimension { get; set; }
        public float? BarHeight { get; set; }
    }

    /// <summary>
    /// Entry point that parses JSON configuration, creates a BarcodeGenerator, applies settings, and outputs XML.
    /// </summary>
    static void Main()
    {
        // Sample JSON configuration (in a real scenario this would come from an HTTP request)
        string json = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""1234567890"",
            ""XDimension"": 2.0,
            ""BarHeight"": 40.0
        }";

        // Deserialize JSON into configuration object
        BarcodeConfig config = JsonSerializer.Deserialize<BarcodeConfig>(json);
        if (config == null)
        {
            Console.WriteLine("Failed to parse configuration.");
            return;
        }

        // Resolve symbology name to BaseEncodeType using reflection
        BaseEncodeType encodeType = ResolveEncodeType(config.Symbology);
        if (encodeType == null)
        {
            Console.WriteLine($"Unknown symbology: {config.Symbology}");
            return;
        }

        // Create barcode generator with provided codetext
        using (var generator = new BarcodeGenerator(encodeType, config.CodeText ?? string.Empty))
        {
            // Apply optional X dimension setting
            if (config.XDimension.HasValue)
                generator.Parameters.Barcode.XDimension.Point = config.XDimension.Value;

            // Apply optional bar height setting
            if (config.BarHeight.HasValue)
                generator.Parameters.Barcode.BarHeight.Point = config.BarHeight.Value;

            // Export settings to XML using a memory stream
            using (var ms = new MemoryStream())
            {
                bool exported = generator.ExportToXml(ms);
                if (!exported)
                {
                    Console.WriteLine("Export to XML failed.");
                    return;
                }

                // Reset stream position and read XML content
                ms.Position = 0;
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    string xmlOutput = reader.ReadToEnd();
                    // Output XML (could be returned from an API endpoint)
                    Console.WriteLine(xmlOutput);
                }
            }
        }
    }

    // Helper to map symbology string to EncodeTypes field via reflection
    private static BaseEncodeType ResolveEncodeType(string symbologyName)
    {
        if (string.IsNullOrWhiteSpace(symbologyName))
            return null;

        var field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
            return null;

        return field.GetValue(null) as BaseEncodeType;
    }
}