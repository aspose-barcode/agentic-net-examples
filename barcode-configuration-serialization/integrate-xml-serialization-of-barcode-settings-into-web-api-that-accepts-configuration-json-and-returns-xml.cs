// Title: Barcode Settings XML Serialization from JSON Configuration
// Description: Demonstrates converting a JSON barcode configuration into Aspose.BarCode settings, exporting them to XML, and returning the XML content.
// Category-Description: This example belongs to the Aspose.BarCode configuration serialization category, showcasing how to map JSON payloads to barcode generation parameters, serialize those settings to XML, and use the output in web API scenarios. It highlights key API classes such as BarcodeGenerator, EncodeTypes, and the Parameters property, which developers frequently employ when building services that need dynamic barcode configuration.
// Prompt: Integrate XML serialization of barcode settings into a web API that accepts configuration JSON and returns XML.
// Tags: barcode, symbology, json, xml, serialization, aspose.barcode, configuration, webapi

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Provides a console demonstration of converting a JSON barcode configuration
/// into Aspose.BarCode settings, exporting those settings to XML, and outputting
/// the XML content. In a real web API, the JSON would come from the request body
/// and the XML would be returned as the response.
/// </summary>
class Program
{
    /// <summary>
    /// Simple DTO that matches the expected JSON payload for barcode configuration.
    /// </summary>
    private class BarcodeConfig
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string BarColor { get; set; }      // Color name, e.g., "Green"
        public string BackColor { get; set; }     // Color name, e.g., "White"
        public float XDimension { get; set; }     // In points
    }

    /// <summary>
    /// Entry point of the demonstration. Deserializes a JSON request, configures a
    /// BarcodeGenerator, exports the settings to XML, and writes the XML to the console.
    /// </summary>
    static void Main()
    {
        // Simulated incoming JSON request payload
        string jsonRequest = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""Sample123"",
            ""BarColor"": ""Green"",
            ""BackColor"": ""White"",
            ""XDimension"": 2.0
        }";

        // Deserialize JSON into a BarcodeConfig object
        BarcodeConfig config = JsonSerializer.Deserialize<BarcodeConfig>(jsonRequest);
        if (config == null)
        {
            Console.WriteLine("Failed to parse JSON configuration.");
            return;
        }

        // Resolve the symbology name to a BaseEncodeType using reflection
        var field = typeof(EncodeTypes).GetField(config.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {config.Symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a barcode generator with the resolved symbology
        using (var generator = new BarcodeGenerator(encodeType, string.Empty))
        {
            // Set the code text, specifying UTF-8 encoding explicitly
            generator.SetCodeText(config.CodeText, Encoding.UTF8);

            // Apply visual appearance settings if provided
            if (!string.IsNullOrEmpty(config.BarColor))
                generator.Parameters.Barcode.BarColor = Color.FromName(config.BarColor);
            if (!string.IsNullOrEmpty(config.BackColor))
                generator.Parameters.BackColor = Color.FromName(config.BackColor);

            // Set the module size (X dimension) in points
            generator.Parameters.Barcode.XDimension.Point = config.XDimension;

            // Export the configured barcode settings to an XML file
            string xmlPath = Path.Combine(Path.GetTempPath(), "barcodeConfig.xml");
            generator.ExportToXml(xmlPath);

            // Read the generated XML content
            string xmlContent = File.ReadAllText(xmlPath, Encoding.UTF8);

            // Output the XML content (simulating an API response)
            Console.WriteLine("=== Exported Barcode Settings XML ===");
            Console.WriteLine(xmlContent);
        }
    }
}