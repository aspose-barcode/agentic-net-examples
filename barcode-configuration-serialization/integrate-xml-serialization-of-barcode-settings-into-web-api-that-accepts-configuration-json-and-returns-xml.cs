using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode.Generation;

class Program
{
    // Model representing the JSON payload
    private class BarcodeConfig
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? XDimension { get; set; }
        public float? BarHeight { get; set; }
        public PaddingConfig Padding { get; set; }
    }

    private class PaddingConfig
    {
        public float? Top { get; set; }
        public float? Bottom { get; set; }
        public float? Left { get; set; }
        public float? Right { get; set; }
    }

    static void Main()
    {
        // Simulated JSON request payload
        string jsonPayload = @"
        {
            ""Symbology"": ""Code128"",
            ""CodeText"": ""123ABC"",
            ""XDimension"": 2.0,
            ""BarHeight"": 40.0,
            ""Padding"": { ""Top"": 5.0, ""Bottom"": 5.0, ""Left"": 5.0, ""Right"": 5.0 }
        }";

        // Deserialize JSON to config object
        BarcodeConfig config = JsonSerializer.Deserialize<BarcodeConfig>(jsonPayload, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (config == null)
            throw new ArgumentException("Invalid JSON payload.");

        // Resolve symbology string to BaseEncodeType
        BaseEncodeType encodeType = ResolveEncodeType(config.Symbology);

        // Create generator with resolved type and codetext
        using (var generator = new BarcodeGenerator(encodeType, config.CodeText))
        {
            // Apply optional XDimension
            if (config.XDimension.HasValue)
                generator.Parameters.Barcode.XDimension.Point = config.XDimension.Value;

            // Apply optional BarHeight
            if (config.BarHeight.HasValue)
                generator.Parameters.Barcode.BarHeight.Point = config.BarHeight.Value;

            // Apply optional padding
            if (config.Padding != null)
            {
                var padding = generator.Parameters.Barcode.Padding;
                if (config.Padding.Top.HasValue)
                    padding.Top.Point = config.Padding.Top.Value;
                if (config.Padding.Bottom.HasValue)
                    padding.Bottom.Point = config.Padding.Bottom.Value;
                if (config.Padding.Left.HasValue)
                    padding.Left.Point = config.Padding.Left.Value;
                if (config.Padding.Right.HasValue)
                    padding.Right.Point = config.Padding.Right.Value;
            }

            // Export settings to XML using a memory stream
            using (var xmlStream = new MemoryStream())
            {
                bool exported = generator.ExportToXml(xmlStream);
                if (!exported)
                    throw new InvalidOperationException("Failed to export barcode settings to XML.");

                // Reset stream position and read XML string
                xmlStream.Position = 0;
                using (var reader = new StreamReader(xmlStream, Encoding.UTF8))
                {
                    string xmlResult = reader.ReadToEnd();
                    // Output XML (simulating API response)
                    Console.WriteLine(xmlResult);
                }
            }
        }
    }

    // Simple mapper from string to EncodeTypes fields
    private static BaseEncodeType ResolveEncodeType(string symbology)
    {
        return symbology?.Trim().ToUpperInvariant() switch
        {
            "CODE128" => EncodeTypes.Code128,
            "CODE39" => EncodeTypes.Code39,
            "EAN13" => EncodeTypes.EAN13,
            "QR" => EncodeTypes.QR,
            // Add more mappings as needed
            _ => throw new ArgumentOutOfRangeException(nameof(symbology), $"Unsupported symbology: {symbology}")
        };
    }
}