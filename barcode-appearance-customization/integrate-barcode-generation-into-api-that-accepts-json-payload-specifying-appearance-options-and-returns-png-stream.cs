// Title: Barcode Generation from JSON Request to PNG Stream
// Description: Demonstrates how to parse a JSON payload describing barcode appearance, generate the barcode with Aspose.BarCode, and return the image as a PNG stream (Base64 for demo).
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator, EncodeTypes, and image format classes to create barcodes dynamically. Typical use cases include web APIs that accept client‑specified barcode parameters and need to return image data. Developers often need to map JSON input to generator settings, handle color conversion, and output PNG streams.
// Prompt: Integrate barcode generation into an API that accepts JSON payload specifying appearance options and returns a PNG stream.
// Tags: barcode, symbology, generation, json, png, aspose.barcode, api, image, encoding

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation from a JSON request and returns a PNG image stream.
/// </summary>
class Program
{
    // Model for JSON payload
    private class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string ForeColorHex { get; set; }      // optional, e.g. "#FF0000"
        public string BackColorHex { get; set; }      // optional
        public float? XDimension { get; set; }        // points, optional
        public float? BarHeight { get; set; }         // points, optional
        public float? Padding { get; set; }           // points, applied uniformly, optional
    }

    /// <summary>
    /// Entry point that parses JSON, configures the generator, and outputs a Base64 PNG.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload representing a client request
        string json = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""Sample123"",
            ""ForeColorHex"": ""#0000FF"",
            ""BackColorHex"": ""#FFFFFF"",
            ""XDimension"": 2.0,
            ""BarHeight"": 50.0,
            ""Padding"": 5.0
        }";

        // Parse JSON into a strongly‑typed request object
        BarcodeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<BarcodeRequest>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to parse JSON: {ex.Message}");
            return;
        }

        // Validate required fields
        if (request == null || string.IsNullOrWhiteSpace(request.Symbology) || string.IsNullOrWhiteSpace(request.CodeText))
        {
            Console.WriteLine("Invalid request payload.");
            return;
        }

        // Resolve symbology name to BaseEncodeType via reflection
        var field = typeof(EncodeTypes).GetField(request.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.Symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create the barcode generator with the resolved symbology
        using (var generator = new BarcodeGenerator(encodeType, string.Empty))
        {
            // Set the code text using UTF‑8 encoding
            generator.SetCodeText(request.CodeText, Encoding.UTF8);

            // Apply optional appearance settings
            if (!string.IsNullOrWhiteSpace(request.ForeColorHex))
            {
                generator.Parameters.Barcode.BarColor = Color.FromArgb(Convert.ToInt32(request.ForeColorHex.Substring(1), 16));
            }

            if (!string.IsNullOrWhiteSpace(request.BackColorHex))
            {
                generator.Parameters.BackColor = Color.FromArgb(Convert.ToInt32(request.BackColorHex.Substring(1), 16));
            }

            if (request.XDimension.HasValue)
            {
                generator.Parameters.Barcode.XDimension.Point = request.XDimension.Value;
            }

            if (request.BarHeight.HasValue && request.BarHeight.Value > 0f)
            {
                generator.Parameters.Barcode.BarHeight.Point = request.BarHeight.Value;
            }

            if (request.Padding.HasValue)
            {
                float pad = request.Padding.Value;
                generator.Parameters.Barcode.Padding.Left.Point = pad;
                generator.Parameters.Barcode.Padding.Top.Point = pad;
                generator.Parameters.Barcode.Padding.Right.Point = pad;
                generator.Parameters.Barcode.Padding.Bottom.Point = pad;
            }

            // Generate PNG image into a memory stream
            using (var ms = new MemoryStream())
            {
                try
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Barcode generation failed: {ex.Message}");
                    return;
                }

                // Convert the PNG bytes to Base64 (simulating an API response stream)
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("Generated PNG (Base64):");
                Console.WriteLine(base64);
            }
        }
    }
}