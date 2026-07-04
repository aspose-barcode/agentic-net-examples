// Title: Barcode Generation API Example
// Description: Demonstrates how to accept a JSON payload with appearance options, generate a barcode, and return a PNG byte stream.
// Prompt: Integrate barcode generation into an API that accepts JSON payload specifying appearance options and returns a PNG stream.
// Tags: barcode, generation, json, api, png, aspose

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeApiSimulation
{
    // DTO for JSON payload
    public class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string ForeColor { get; set; }          // Hex, e.g. "#FF0000"
        public string BackColor { get; set; }          // Hex, e.g. "#FFFFFF"
        public float? ImageWidth { get; set; }         // Points
        public float? ImageHeight { get; set; }        // Points
        public float? XDimension { get; set; }         // Points
        public float? BarHeight { get; set; }          // Points (used only when AutoSizeMode = None)
        public string AutoSizeMode { get; set; }       // "None", "Interpolation", "Nearest"
    }

    /// <summary>
    /// Simulates an API that generates barcodes from JSON requests.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Parses a hex color string (e.g. "#RRGGBB") into an Aspose.Drawing.Color.
        /// </summary>
        /// <param name="hex">Hexadecimal color string.</param>
        /// <returns>Corresponding Color object.</returns>
        private static Color ParseColor(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                throw new ArgumentException("Color string is null or empty.");

            // Remove leading '#' if present
            string clean = hex.TrimStart('#');
            if (clean.Length != 6)
                throw new ArgumentException($"Invalid color format: {hex}");

            // Convert each component from hex to int
            int r = Convert.ToInt32(clean.Substring(0, 2), 16);
            int g = Convert.ToInt32(clean.Substring(2, 2), 16);
            int b = Convert.ToInt32(clean.Substring(4, 2), 16);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Generates a barcode image from a JSON payload and returns the PNG bytes.
        /// </summary>
        /// <param name="jsonPayload">JSON string containing barcode parameters.</param>
        /// <returns>Byte array with PNG image data.</returns>
        private static byte[] GenerateBarcodeFromJson(string jsonPayload)
        {
            // Deserialize request
            BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
            if (request == null)
                throw new ArgumentException("Invalid JSON payload.");

            // Resolve symbology name to EncodeTypes field via reflection
            var field = typeof(EncodeTypes).GetField(request.Symbology);
            if (field == null)
                throw new ArgumentException($"Unknown symbology: {request.Symbology}");
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create generator with codetext
            using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
            {
                // Set foreground color if provided
                if (!string.IsNullOrWhiteSpace(request.ForeColor))
                    generator.Parameters.Barcode.BarColor = ParseColor(request.ForeColor);

                // Set background color if provided
                if (!string.IsNullOrWhiteSpace(request.BackColor))
                    generator.Parameters.BackColor = ParseColor(request.BackColor);

                // Set AutoSizeMode if provided
                if (!string.IsNullOrWhiteSpace(request.AutoSizeMode))
                {
                    if (Enum.TryParse<AutoSizeMode>(request.AutoSizeMode, out var mode))
                        generator.Parameters.AutoSizeMode = mode;
                    else
                        throw new ArgumentException($"Invalid AutoSizeMode: {request.AutoSizeMode}");
                }

                // Set image width/height if provided
                if (request.ImageWidth.HasValue)
                    generator.Parameters.ImageWidth.Point = request.ImageWidth.Value;
                if (request.ImageHeight.HasValue)
                    generator.Parameters.ImageHeight.Point = request.ImageHeight.Value;

                // Set XDimension if provided
                if (request.XDimension.HasValue)
                    generator.Parameters.Barcode.XDimension.Point = request.XDimension.Value;

                // Set BarHeight only when AutoSizeMode is None
                if (request.BarHeight.HasValue && generator.Parameters.AutoSizeMode == AutoSizeMode.None)
                    generator.Parameters.Barcode.BarHeight.Point = request.BarHeight.Value;

                // Generate PNG into memory stream and return bytes
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Entry point that demonstrates barcode generation using a sample JSON payload.
        /// </summary>
        static void Main()
        {
            // Sample JSON payload representing an API request
            string sampleJson = @"{
                ""Symbology"": ""Code128"",
                ""CodeText"": ""1234567890"",
                ""ForeColor"": ""#0000FF"",
                ""BackColor"": ""#FFFFFF"",
                ""ImageWidth"": 300,
                ""ImageHeight"": 150,
                ""XDimension"": 2,
                ""AutoSizeMode"": ""Interpolation""
            }";

            try
            {
                // Generate PNG bytes from the JSON request
                byte[] pngBytes = GenerateBarcodeFromJson(sampleJson);

                // Simulate API response by outputting Base64 string
                string base64 = Convert.ToBase64String(pngBytes);
                Console.WriteLine("PNG Base64:");
                Console.WriteLine(base64);
            }
            catch (Exception ex)
            {
                // Output any errors encountered during processing
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}