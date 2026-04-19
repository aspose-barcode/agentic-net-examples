using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeXmlApiSimulation
{
    // Simple configuration model matching expected JSON payload
    public class BarcodeConfig
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public float? XDimension { get; set; }          // in points
        public float? BarHeight { get; set; }           // in points
        public string BarColor { get; set; }            // hex, e.g. "#FF0000"
        public string BackColor { get; set; }           // hex, e.g. "#FFFFFF"
    }

    class Program
    {
        static void Main()
        {
            // Simulated incoming JSON request payload
            string jsonPayload = @"{
                ""Symbology"": ""Code128"",
                ""CodeText"": ""123ABC"",
                ""XDimension"": 2.5,
                ""BarHeight"": 50,
                ""BarColor"": ""#0000FF"",
                ""BackColor"": ""#FFFFFF""
            }";

            // Deserialize JSON to configuration object
            BarcodeConfig config = JsonSerializer.Deserialize<BarcodeConfig>(jsonPayload);

            // Resolve symbology string to EncodeTypes static field via reflection
            Type encodeTypes = typeof(EncodeTypes);
            var fieldInfo = encodeTypes.GetField(config.Symbology);
            if (fieldInfo == null)
                throw new ArgumentException($"Unsupported symbology: {config.Symbology}");

            BaseEncodeType encodeType = (BaseEncodeType)fieldInfo.GetValue(null);

            // Create barcode generator with provided type and codetext
            using (var generator = new BarcodeGenerator(encodeType, config.CodeText))
            {
                // Apply optional parameters if they are present
                if (config.XDimension.HasValue)
                    generator.Parameters.Barcode.XDimension.Point = config.XDimension.Value;

                if (config.BarHeight.HasValue)
                    generator.Parameters.Barcode.BarHeight.Point = config.BarHeight.Value;

                if (!string.IsNullOrWhiteSpace(config.BarColor))
                    generator.Parameters.Barcode.BarColor = ParseHexColor(config.BarColor);

                if (!string.IsNullOrWhiteSpace(config.BackColor))
                    generator.Parameters.BackColor = ParseHexColor(config.BackColor);

                // Export current barcode settings to XML in a memory stream
                using (var xmlStream = new MemoryStream())
                {
                    bool exported = generator.ExportToXml(xmlStream);
                    if (!exported)
                        throw new InvalidOperationException("Failed to export barcode settings to XML.");

                    xmlStream.Position = 0;
                    using (var reader = new StreamReader(xmlStream))
                    {
                        string xmlResult = reader.ReadToEnd();
                        // Simulated API response: XML string
                        Console.WriteLine(xmlResult);
                    }
                }
            }
        }

        // Helper to convert a hex color string to Aspose.Drawing.Color
        private static Color ParseHexColor(string hex)
        {
            // Remove leading '#', support 6 or 8 digit hex (RRGGBB or AARRGGBB)
            string clean = hex.TrimStart('#');
            if (clean.Length == 6)
                clean = "FF" + clean; // assume fully opaque

            if (clean.Length != 8)
                throw new ArgumentException($"Invalid color format: {hex}");

            int argb = Convert.ToInt32(clean, 16);
            return Color.FromArgb(argb);
        }
    }
}