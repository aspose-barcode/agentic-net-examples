// Title: XML Serialization of Barcode Settings via JSON Input
// Description: Demonstrates deserializing barcode configuration from JSON, generating a barcode with Aspose.BarCode, and exporting the generator settings to XML.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It showcases the use of BarcodeGenerator, EncodeTypes, and the ExportToXml method to convert runtime barcode settings into an XML representation. Typical scenarios include persisting barcode configurations, transmitting settings between services, or debugging generation parameters. Developers working with barcode creation and configuration persistence often need to serialize settings to XML or JSON for storage or API communication.
// Prompt: Integrate XML serialization of barcode settings into a web API that accepts configuration JSON and returns XML.
// Tags: barcode, symbology, json, xml, serialization, generation, aspose.barcode

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates converting a JSON barcode configuration into an XML representation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Represents the JSON payload structure expected by the API.
    /// </summary>
    class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? XDimension { get; set; }
    }

    /// <summary>
    /// Entry point of the example. Deserializes JSON, creates a barcode generator, and exports its configuration to XML.
    /// </summary>
    static void Main()
    {
        // Simulated JSON payload received by a web API
        string jsonPayload = @"{
            ""Symbology"": ""QR"",
            ""CodeText"": ""Hello, Aspose!"",
            ""XDimension"": 2.5
        }";

        // Deserialize JSON to request object
        BarcodeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Validate required fields
        if (request == null || string.IsNullOrWhiteSpace(request.Symbology) || string.IsNullOrWhiteSpace(request.CodeText))
        {
            Console.WriteLine("Invalid request data.");
            return;
        }

        // Resolve symbology name to BaseEncodeType using reflection
        var field = typeof(EncodeTypes).GetField(request.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.Symbology}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create barcode generator with provided settings
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
        {
            // Apply optional XDimension if supplied
            if (request.XDimension.HasValue)
            {
                generator.Parameters.Barcode.XDimension.Point = request.XDimension.Value;
            }

            // Export generation state to XML in memory
            using (var ms = new MemoryStream())
            {
                generator.ExportToXml(ms);
                ms.Position = 0;

                // Read XML string from memory stream
                using (var reader = new StreamReader(ms))
                {
                    string xml = reader.ReadToEnd();
                    Console.WriteLine("=== Generated Barcode Configuration XML ===");
                    Console.WriteLine(xml);
                }
            }
        }
    }
}