// Title: Barcode generation API simulation with JSON payload
// Description: Demonstrates how to accept a JSON request describing barcode appearance, generate the barcode using Aspose.BarCode, and return a PNG byte array.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image formatting options. It shows typical use cases such as creating QR codes or linear barcodes with custom dimensions, colors, and padding, which developers often need when building web APIs that serve barcode images.
// Prompt: Integrate barcode generation into an API that accepts JSON payload specifying appearance options and returns a PNG stream.
// Tags: barcode, generation, json, api, png, aspose.barcode, encode-types, appearance

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeApiSimulation
{
    /// <summary>
    /// Represents the JSON payload for barcode generation.
    /// </summary>
    public class BarcodeRequest
    {
        public string Symbology { get; set; }          // e.g., "Code128", "QR"
        public string CodeText { get; set; }           // Text to encode
        public AppearanceOptions Appearance { get; set; } // Optional appearance settings
    }

    /// <summary>
    /// Appearance options that can be supplied in the JSON payload.
    /// </summary>
    public class AppearanceOptions
    {
        public float? ImageWidth { get; set; }         // Width in points
        public float? ImageHeight { get; set; }        // Height in points
        public string ForegroundColor { get; set; }    // Hex color, e.g., "#FF0000"
        public string BackgroundColor { get; set; }    // Hex color, e.g., "#FFFFFF"
        public float? Padding { get; set; }            // Uniform padding in points
    }

    /// <summary>
    /// Simulates an API that receives a JSON payload describing barcode parameters,
    /// generates the barcode image, and returns the PNG data.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point demonstrating the JSON deserialization, barcode generation,
        /// and saving the resulting PNG to disk.
        /// </summary>
        static void Main()
        {
            // Sample JSON payload (in a real scenario this would come from an HTTP request)
            string jsonPayload = @"
            {
                ""Symbology"": ""QR"",
                ""CodeText"": ""https://example.com"",
                ""Appearance"": {
                    ""ImageWidth"": 300,
                    ""ImageHeight"": 300,
                    ""ForegroundColor"": ""#0000FF"",
                    ""BackgroundColor"": ""#FFFFFF"",
                    ""Padding"": 5
                }
            }";

            // Deserialize the JSON into a request object
            BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);

            // Generate the barcode and obtain the PNG bytes
            byte[] pngData = GenerateBarcode(request);

            // Write the PNG to a file for verification
            const string outputPath = "generated_barcode.png";
            File.WriteAllBytes(outputPath, pngData);
            Console.WriteLine($"Barcode image saved to '{outputPath}'. Size: {pngData.Length} bytes.");

            // In an actual API the pngData would be written to the HTTP response stream.
        }

        /// <summary>
        /// Generates a barcode based on the request and returns PNG bytes.
        /// </summary>
        /// <param name="request">The barcode generation request.</param>
        /// <returns>Byte array containing the PNG image.</returns>
        static byte[] GenerateBarcode(BarcodeRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request cannot be null.");

            // Resolve the symbology name to a BaseEncodeType using reflection
            var field = typeof(EncodeTypes).GetField(request.Symbology);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {request.Symbology}");

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create the generator with the resolved type and code text
            using (var generator = new BarcodeGenerator(encodeType, request.CodeText ?? string.Empty))
            {
                // Apply appearance options if provided
                if (request.Appearance != null)
                {
                    var ap = request.Appearance;

                    // Set image dimensions (using .Point as required)
                    if (ap.ImageWidth.HasValue)
                        generator.Parameters.ImageWidth.Point = ap.ImageWidth.Value;
                    if (ap.ImageHeight.HasValue)
                        generator.Parameters.ImageHeight.Point = ap.ImageHeight.Value;

                    // Set foreground (bar) color
                    if (!string.IsNullOrWhiteSpace(ap.ForegroundColor))
                        generator.Parameters.Barcode.BarColor = ParseColor(ap.ForegroundColor);

                    // Set background color
                    if (!string.IsNullOrWhiteSpace(ap.BackgroundColor))
                        generator.Parameters.BackColor = ParseColor(ap.BackgroundColor);

                    // Uniform padding on all sides
                    if (ap.Padding.HasValue)
                    {
                        generator.Parameters.Barcode.Padding.Left.Point = ap.Padding.Value;
                        generator.Parameters.Barcode.Padding.Top.Point = ap.Padding.Value;
                        generator.Parameters.Barcode.Padding.Right.Point = ap.Padding.Value;
                        generator.Parameters.Barcode.Padding.Bottom.Point = ap.Padding.Value;
                    }
                }

                // Use interpolation mode to respect explicit ImageWidth/ImageHeight if set
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Save to a memory stream as PNG
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Converts a hex color string to an Aspose.Drawing.Color.
        /// </summary>
        /// <param name="hex">Hex color string (e.g., "#FF0000" or "FF0000FF").</param>
        /// <returns>Corresponding Color object.</returns>
        static Aspose.Drawing.Color ParseColor(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Color string cannot be null or empty.");

            // Remove leading '#'
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            // Support RGB (6 chars) or ARGB (8 chars)
            if (hex.Length == 6)
                hex = "FF" + hex; // Assume fully opaque

            if (hex.Length != 8)
                throw new ArgumentException($"Invalid color format: #{hex}");

            uint argb = Convert.ToUInt32(hex, 16);
            byte a = (byte)((argb & 0xFF000000) >> 24);
            byte r = (byte)((argb & 0x00FF0000) >> 16);
            byte g = (byte)((argb & 0x0000FF00) >> 8);
            byte b = (byte)(argb & 0x000000FF);
            return Aspose.Drawing.Color.FromArgb(a, r, g, b);
        }
    }
}