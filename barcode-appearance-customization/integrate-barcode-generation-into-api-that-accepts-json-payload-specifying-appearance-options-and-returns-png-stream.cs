// Title: Generate Barcode PNG from JSON Payload via Aspose.BarCode
// Description: Demonstrates how to deserialize a JSON request that defines barcode appearance options, generate a barcode using Aspose.BarCode, and return the image as a PNG stream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and image format classes to create barcodes programmatically. Typical use cases include API endpoints that accept client‑specified barcode parameters and need to produce image streams for web or mobile consumption. Developers often need to map JSON payloads to generator settings, handle color parsing, and output PNG data without writing to disk.
// Prompt: Integrate barcode generation into an API that accepts JSON payload specifying appearance options and returns a PNG stream.
// Tags: barcode, symbology, generation, json, png, aspose.barcode, apigeneration, color, encoding

using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeApiConsole
{
    /// <summary>
    /// Console application that demonstrates barcode generation from a JSON payload.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Deserializes a sample JSON request, generates a barcode PNG, and saves it to a temporary file.
        /// </summary>
        static void Main()
        {
            // Sample JSON payload with appearance options
            string jsonPayload = @"
            {
                ""Symbology"": ""Code128"",
                ""CodeText"": ""Sample123"",
                ""ForeColor"": ""#0000FF"",
                ""BackColor"": ""#FFFFFF"",
                ""XDimension"": 2.0,
                ""BarHeight"": 50.0,
                ""RotationAngle"": 0.0,
                ""Padding"": 5.0
            }";

            try
            {
                // Generate barcode PNG stream from JSON
                using (MemoryStream pngStream = GenerateBarcodeFromJson(jsonPayload))
                {
                    // Write the PNG stream to a file for verification
                    string outputPath = Path.Combine(Path.GetTempPath(), "generated_barcode.png");
                    using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        pngStream.CopyTo(file);
                    }

                    Console.WriteLine($"Barcode PNG generated. File saved to: {outputPath}");
                    Console.WriteLine($"Stream length: {pngStream.Length} bytes");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static MemoryStream GenerateBarcodeFromJson(string json)
        {
            // Deserialize JSON payload into a strongly‑typed request object
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(json, options);
            if (request == null)
                throw new ArgumentException("Invalid JSON payload.");

            // Resolve symbology name to BaseEncodeType using reflection
            FieldInfo field = typeof(EncodeTypes).GetField(request.Symbology);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {request.Symbology}");
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create barcode generator with specified symbology and code text
            using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
            {
                // Apply foreground and background colors
                generator.Parameters.Barcode.BarColor = ParseColor(request.ForeColor);
                generator.Parameters.BackColor = ParseColor(request.BackColor);

                // Set X dimension and bar height
                generator.Parameters.Barcode.XDimension.Point = (float)request.XDimension;
                generator.Parameters.Barcode.BarHeight.Point = (float)request.BarHeight;

                // Set rotation angle
                generator.Parameters.RotationAngle = (float)request.RotationAngle;

                // Apply uniform padding on all sides
                generator.Parameters.Barcode.Padding.Left.Point = (float)request.Padding;
                generator.Parameters.Barcode.Padding.Top.Point = (float)request.Padding;
                generator.Parameters.Barcode.Padding.Right.Point = (float)request.Padding;
                generator.Parameters.Barcode.Padding.Bottom.Point = (float)request.Padding;

                // Generate PNG into a memory stream
                var pngStream = new MemoryStream();
                generator.Save(pngStream, BarCodeImageFormat.Png);
                pngStream.Position = 0; // Reset stream position for reading
                return pngStream;
            }
        }

        static Color ParseColor(string hex)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Color string is null or empty.");

            // Remove leading '#' and ensure ARGB format
            string clean = hex.TrimStart('#');
            if (clean.Length == 6)
                clean = "FF" + clean; // add alpha if missing

            // Parse hexadecimal string to integer
            if (!int.TryParse(clean, System.Globalization.NumberStyles.HexNumber, null, out int argb))
                throw new ArgumentException($"Invalid color hex: {hex}");

            // Convert ARGB integer to Color
            return Color.FromArgb(argb);
        }

        // DTO representing the expected JSON structure for barcode generation
        class BarcodeRequest
        {
            public string Symbology { get; set; }
            public string CodeText { get; set; }
            public string ForeColor { get; set; }
            public string BackColor { get; set; }
            public double XDimension { get; set; }
            public double BarHeight { get; set; }
            public double RotationAngle { get; set; }
            public double Padding { get; set; }
        }
    }
}