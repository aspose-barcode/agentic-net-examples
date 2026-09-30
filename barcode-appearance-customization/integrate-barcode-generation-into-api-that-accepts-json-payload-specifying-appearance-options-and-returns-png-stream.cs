// Title: Generate Barcode PNG from JSON Request
// Description: Demonstrates how to deserialize a JSON payload describing barcode appearance, generate a barcode using Aspose.BarCode, and return the image as a PNG stream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and image format classes. It illustrates typical scenarios such as creating barcodes from client‑provided data, customizing colors, dimensions, and padding, and delivering the result as a memory stream for API responses. Developers building web services or micro‑APIs often need this pattern to produce on‑the‑fly barcode images.
// Prompt: Integrate barcode generation into an API that accepts JSON payload specifying appearance options and returns a PNG stream.
// Tags: barcode, generation, json, png, aspose.barcode, encode-types, memorystream, api

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation from a JSON request and saving the result as a PNG file.
/// </summary>
class Program
{
    // Model representing the JSON payload
    private class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string ForeColorHex { get; set; }          // e.g. "#FF0000"
        public string BackColorHex { get; set; }          // e.g. "#FFFFFF"
        public float? XDimension { get; set; }            // module size in points
        public float? BarHeight { get; set; }             // height in points
        public float? Padding { get; set; }               // uniform padding in points
    }

    /// <summary>
    /// Application entry point. Generates a barcode from a sample JSON payload and writes the PNG to disk.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload describing barcode appearance
        string jsonPayload = @"
        {
            ""Symbology"": ""QR"",
            ""CodeText"": ""https://example.com"",
            ""ForeColorHex"": ""#0000FF"",
            ""BackColorHex"": ""#FFFFFF"",
            ""XDimension"": 2.5,
            ""BarHeight"": 50,
            ""Padding"": 5
        }";

        try
        {
            // Generate the barcode image as a memory stream
            using (MemoryStream barcodeStream = GenerateBarcode(jsonPayload))
            {
                // Write the PNG stream to a file for verification
                const string outputPath = "barcode.png";
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    barcodeStream.CopyTo(file);
                }
                Console.WriteLine($"Barcode image saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Generates a PNG barcode image based on the JSON request and returns a MemoryStream
    private static MemoryStream GenerateBarcode(string json)
    {
        // Deserialize JSON payload into a strongly‑typed request object
        BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(json);
        if (request == null)
            throw new ArgumentException("Invalid JSON payload.");

        // Validate required fields
        if (string.IsNullOrWhiteSpace(request.Symbology))
            throw new ArgumentException("Symbology must be specified.");

        if (string.IsNullOrWhiteSpace(request.CodeText))
            throw new ArgumentException("CodeText must be specified.");

        // Resolve symbology name to BaseEncodeType via reflection
        var field = typeof(EncodeTypes).GetField(request.Symbology);
        if (field == null)
            throw new ArgumentException($"Unknown symbology: {request.Symbology}");

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Prepare the barcode generator with the specified symbology and data
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
        {
            // Apply optional appearance settings
            if (!string.IsNullOrWhiteSpace(request.ForeColorHex))
                generator.Parameters.Barcode.BarColor = ParseColor(request.ForeColorHex);

            if (!string.IsNullOrWhiteSpace(request.BackColorHex))
                generator.Parameters.BackColor = ParseColor(request.BackColorHex);

            if (request.XDimension.HasValue)
                generator.Parameters.Barcode.XDimension.Point = request.XDimension.Value;

            if (request.BarHeight.HasValue)
            {
                if (request.BarHeight.Value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(request.BarHeight), "BarHeight must be greater than zero.");
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

            // Save the barcode image to a memory stream in PNG format
            var ms = new MemoryStream();
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for the caller
            return ms;
        }
    }

    // Helper to convert a hex color string to Aspose.Drawing.Color
    private static Color ParseColor(string hex)
    {
        if (string.IsNullOrWhiteSpace(hex))
            throw new ArgumentException("Color hex string cannot be null or empty.");

        // Remove leading '#', if present
        string clean = hex.TrimStart('#');

        // Support 6-digit (RRGGBB) and 8-digit (AARRGGBB) formats
        if (clean.Length == 6)
            clean = "FF" + clean; // Assume fully opaque

        if (clean.Length != 8)
            throw new ArgumentException($"Invalid color hex format: {hex}");

        int argb = Convert.ToInt32(clean, 16);
        return Color.FromArgb(argb);
    }
}