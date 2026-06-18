using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode image from a JSON payload using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Model representing the JSON payload for barcode generation.
    /// </summary>
    private class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? BarHeight { get; set; }
        public float? ImageWidth { get; set; }
        public float? ImageHeight { get; set; }
        public float? RotationAngle { get; set; }
        public float? Padding { get; set; }
        public string BarColor { get; set; }   // Hex, e.g. "#FF0000"
        public string BackColor { get; set; }  // Hex, e.g. "#FFFFFF"
    }

    /// <summary>
    /// Entry point of the application. Parses a JSON payload, configures a barcode generator,
    /// and outputs the generated PNG image as a Base64 string.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload; replace with actual input as needed.
        string json = @"{
            ""Symbology"": ""Code128"",
            ""CodeText"": ""1234567890"",
            ""BarHeight"": 40,
            ""ImageWidth"": 300,
            ""ImageHeight"": 150,
            ""RotationAngle"": 0,
            ""Padding"": 10,
            ""BarColor"": ""#0000FF"",
            ""BackColor"": ""#FFFFFF""
        }";

        // Parse JSON into a BarcodeRequest object.
        BarcodeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<BarcodeRequest>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (request == null)
                throw new ArgumentException("Invalid JSON payload.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing JSON: {ex.Message}");
            return;
        }

        // Resolve the symbology name to a BaseEncodeType using reflection.
        var field = typeof(EncodeTypes).GetField(request.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.Symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create the barcode generator and apply appearance options.
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText ?? string.Empty))
        {
            // Set bar height (requires AutoSizeMode.None).
            if (request.BarHeight.HasValue)
            {
                if (request.BarHeight.Value <= 0f)
                {
                    Console.WriteLine("BarHeight must be greater than zero.");
                    return;
                }
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                generator.Parameters.Barcode.BarHeight.Point = request.BarHeight.Value;
            }

            // Set image dimensions (used when AutoSizeMode is Interpolation).
            if (request.ImageWidth.HasValue)
                generator.Parameters.ImageWidth.Point = request.ImageWidth.Value;
            if (request.ImageHeight.HasValue)
                generator.Parameters.ImageHeight.Point = request.ImageHeight.Value;

            // Apply rotation angle if provided.
            if (request.RotationAngle.HasValue)
                generator.Parameters.RotationAngle = request.RotationAngle.Value;

            // Apply uniform padding to all sides if provided.
            if (request.Padding.HasValue)
            {
                generator.Parameters.Barcode.Padding.Left.Point = request.Padding.Value;
                generator.Parameters.Barcode.Padding.Top.Point = request.Padding.Value;
                generator.Parameters.Barcode.Padding.Right.Point = request.Padding.Value;
                generator.Parameters.Barcode.Padding.Bottom.Point = request.Padding.Value;
            }

            // Set bar and background colors from hex strings.
            if (!string.IsNullOrEmpty(request.BarColor))
                generator.Parameters.Barcode.BarColor = Color.FromArgb(Convert.ToInt32(request.BarColor.TrimStart('#'), 16));
            if (!string.IsNullOrEmpty(request.BackColor))
                generator.Parameters.BackColor = Color.FromArgb(Convert.ToInt32(request.BackColor.TrimStart('#'), 16));

            // Save the generated barcode to a memory stream as PNG.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] pngBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(pngBytes);
                Console.WriteLine("Generated PNG (Base64):");
                Console.WriteLine(base64);
            }
        }
    }
}