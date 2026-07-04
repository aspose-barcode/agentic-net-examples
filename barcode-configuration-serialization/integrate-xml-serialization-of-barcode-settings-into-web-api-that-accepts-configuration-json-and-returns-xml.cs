// Title: XML serialization of barcode settings via a web‑API style console demo
// Description: Demonstrates deserializing barcode configuration from JSON, generating a barcode, and exporting its settings to XML.
// Prompt: Integrate XML serialization of barcode settings into a web API that accepts configuration JSON and returns XML.
// Tags: barcode, symbology, serialization, xml, json, aspose.barcode, webapi

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Simple DTO that represents the JSON payload received by the API.
/// </summary>
class Config
{
    /// <summary>
    /// The name of the barcode symbology (e.g., "Code128").
    /// </summary>
    public string Symbology { get; set; }

    /// <summary>
    /// The text to encode in the barcode.
    /// </summary>
    public string CodeText { get; set; }
}

/// <summary>
/// Console application that mimics the core logic of a web API endpoint.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that deserializes JSON, generates a barcode, and returns its settings as XML.
    /// </summary>
    static void Main()
    {
        // NOTE: The snippet runner is a console application. In a real web API this logic would be inside a controller action.

        // Sample JSON configuration (normally this would come from an HTTP request body)
        string jsonConfig = @"{ ""Symbology"": ""Code128"", ""CodeText"": ""Sample123"" }";

        // Deserialize JSON to a configuration object
        Config config = JsonSerializer.Deserialize<Config>(jsonConfig);
        if (config == null)
        {
            Console.WriteLine("Invalid configuration.");
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

        // Create the barcode generator with the resolved type and codetext
        using (var generator = new BarcodeGenerator(encodeType, config.CodeText))
        {
            // Example of setting a barcode property (optional)
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Export the barcode settings to XML (in-memory)
            using (var memoryStream = new MemoryStream())
            {
                bool exported = generator.ExportToXml(memoryStream);
                if (!exported)
                {
                    Console.WriteLine("Failed to export barcode settings to XML.");
                    return;
                }

                // Reset stream position and read the XML content
                memoryStream.Position = 0;
                using (var reader = new StreamReader(memoryStream))
                {
                    string xmlOutput = reader.ReadToEnd();
                    Console.WriteLine("Exported XML:");
                    Console.WriteLine(xmlOutput);
                }
            }
        }
    }
}