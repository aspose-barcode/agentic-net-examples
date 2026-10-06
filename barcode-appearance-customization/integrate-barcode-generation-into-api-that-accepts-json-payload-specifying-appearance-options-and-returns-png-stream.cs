// Title: Generate a barcode from JSON request and output PNG as Base64
// Description: Demonstrates how to parse a JSON payload describing barcode appearance, create the barcode with Aspose.BarCode, and return the image as a PNG stream (Base64). Useful for API endpoints that need dynamic barcode generation.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and image format settings. Developers often need to generate barcodes on the fly in web services, customizing colors, dimensions, resolution, rotation, and padding. The snippet illustrates typical API usage for creating and exporting barcodes as PNG.
// Prompt: Integrate barcode generation into an API that accepts JSON payload specifying appearance options and returns a PNG stream.
// Tags: barcode, generation, json, png, aspose.barcode, encode-types, color, resolution, rotation, padding

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Globalization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation from a JSON request and outputs a PNG image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Represents the JSON payload that specifies barcode generation options.
    /// </summary>
    class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string BarColor { get; set; }          // Hex, e.g. "#FF0000"
        public string BackColor { get; set; }         // Hex
        public float? XDimension { get; set; }        // Points
        public float? Resolution { get; set; }        // DPI
        public float? RotationAngle { get; set; }     // Degrees
        public float? Padding { get; set; }           // Points (applied to all sides)
    }

    /// <summary>
    /// Parses a hexadecimal color string into an Aspose.Drawing.Color.
    /// Returns Color.Empty if the input is null or whitespace.
    /// </summary>
    static Aspose.Drawing.Color ParseHexColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            return Aspose.Drawing.Color.Empty;

        hex = hex.TrimStart('#');
        if (hex.Length == 6)
        {
            int argb = int.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return Aspose.Drawing.Color.FromArgb(255, (argb >> 16) & 0xFF, (argb >> 8) & 0xFF, argb & 0xFF);
        }
        if (hex.Length == 8)
        {
            int argb = int.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            return Aspose.Drawing.Color.FromArgb((argb >> 24) & 0xFF, (argb >> 16) & 0xFF, (argb >> 8) & 0xFF, argb & 0xFF);
        }
        throw new ArgumentException($"Invalid color format: #{hex}");
    }

    /// <summary>
    /// Resolves a symbology name (e.g., "Code128") to the corresponding EncodeTypes value.
    /// </summary>
    static BaseEncodeType ResolveSymbology(string name)
    {
        var field = typeof(EncodeTypes).GetField(name);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {name}");
        return (BaseEncodeType)field.GetValue(null);
    }

    /// <summary>
    /// Entry point that parses a sample JSON payload, configures the barcode generator, and writes the PNG image as Base64 to the console.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload representing a client request
        string json = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""Sample123"",
            ""BarColor"": ""#0000FF"",
            ""BackColor"": ""#FFFFFF"",
            ""XDimension"": 2.0,
            ""Resolution"": 300.0,
            ""RotationAngle"": 0.0,
            ""Padding"": 5.0
        }";

        // Deserialize JSON into a strongly‑typed request object
        BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(json);
        if (request == null)
        {
            Console.WriteLine("Failed to parse request.");
            return;
        }

        // Resolve the requested symbology to an EncodeTypes value
        BaseEncodeType encodeType;
        try
        {
            encodeType = ResolveSymbology(request.Symbology);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
        {
            if (!string.IsNullOrWhiteSpace(request.BarColor))
                generator.Parameters.Barcode.BarColor = ParseHexColor(request.BarColor);
            if (!string.IsNullOrWhiteSpace(request.BackColor))
                generator.Parameters.BackColor = ParseHexColor(request.BackColor);
            if (request.XDimension.HasValue)
                generator.Parameters.Barcode.XDimension.Point = request.XDimension.Value;
            if (request.Resolution.HasValue)
                generator.Parameters.Resolution = request.Resolution.Value;
            if (request.RotationAngle.HasValue)
                generator.Parameters.RotationAngle = request.RotationAngle.Value;
            if (request.Padding.HasValue)
            {
                float pad = request.Padding.Value;
                generator.Parameters.Barcode.Padding.Left.Point = pad;
                generator.Parameters.Barcode.Padding.Top.Point = pad;
                generator.Parameters.Barcode.Padding.Right.Point = pad;
                generator.Parameters.Barcode.Padding.Bottom.Point = pad;
            }

            // Generate the PNG image into a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] pngBytes = ms.ToArray();

                // Convert the PNG bytes to a Base64 string for easy transport
                string base64 = Convert.ToBase64String(pngBytes);
                Console.WriteLine("Generated PNG Base64:");
                Console.WriteLine(base64);
            }
        }
    }
}