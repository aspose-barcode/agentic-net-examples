// Title: Barcode generation from JSON payload in a console demo
// Description: Demonstrates how to deserialize a JSON request, map its properties to Aspose.BarCode settings, and generate a PNG barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and rendering options. Developers often need to create barcodes dynamically from client data in web APIs or services, and this snippet shows the typical workflow of parsing input, configuring parameters, and producing an image.
// Prompt: Integrate barcode generation into a web API endpoint that receives JSON payload and returns the barcode image.
// Tags: barcode, generation, json, deserialization, aspose.barcode, aspnet core, api, png, image

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation based on a JSON request payload.
/// </summary>
class Program
{
    // Model representing the expected JSON payload
    public class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? XDimension { get; set; }
        public float? BarHeight { get; set; }
        public string BarColor { get; set; }
    }

    /// <summary>
    /// Entry point that simulates receiving a JSON payload, creates a barcode, and outputs the image.
    /// </summary>
    static void Main()
    {
        // Simulated incoming JSON request
        string json = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""123ABC"",
            ""XDimension"": 2.0,
            ""BarHeight"": 50.0,
            ""BarColor"": ""Blue""
        }";

        // Deserialize the JSON payload
        BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(json);
        if (request == null)
        {
            Console.WriteLine("Invalid request payload.");
            return;
        }

        // Resolve the symbology name to a BaseEncodeType using reflection
        var field = typeof(EncodeTypes).GetField(request.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.Symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create the barcode generator with the resolved type and provided code text
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText ?? string.Empty))
        {
            // Apply optional parameters if they are present
            if (request.XDimension.HasValue)
                generator.Parameters.Barcode.XDimension.Point = request.XDimension.Value;

            if (request.BarHeight.HasValue && request.BarHeight.Value > 0)
                generator.Parameters.Barcode.BarHeight.Point = request.BarHeight.Value;

            if (!string.IsNullOrEmpty(request.BarColor))
            {
                // Map color name to Aspose.Drawing.Color static property (e.g., Color.Blue)
                var colorProp = typeof(Color).GetProperty(request.BarColor);
                if (colorProp != null)
                {
                    generator.Parameters.Barcode.BarColor = (Color)colorProp.GetValue(null);
                }
            }

            // Generate the barcode image into a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0;

                // Output the image as a Base64 string (simulating an HTTP response body)
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("Barcode image (Base64 PNG):");
                Console.WriteLine(base64);

                // Also write the image to a file for local verification
                File.WriteAllBytes("output.png", ms.ToArray());
            }
        }
    }
}