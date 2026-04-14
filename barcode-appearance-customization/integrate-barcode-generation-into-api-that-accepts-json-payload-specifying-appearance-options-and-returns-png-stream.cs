using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class BarcodeRequest
{
    public string Symbology { get; set; }
    public string CodeText { get; set; }
    public string BarColor { get; set; }          // Hex string, e.g. "#FF0000"
    public string BackColor { get; set; }         // Hex string, e.g. "#FFFFFF"
    public float? BarHeight { get; set; }         // In points
    public float? XDimension { get; set; }        // In points
}

class Program
{
    static void Main()
    {
        // Sample JSON payload
        string jsonPayload = @"{
            ""Symbology"": ""QR"",
            ""CodeText"": ""https://example.com"",
            ""BarColor"": ""#0000FF"",
            ""BackColor"": ""#FFFFFF"",
            ""BarHeight"": 40,
            ""XDimension"": 2
        }";

        // Deserialize request
        BarcodeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
            if (request == null)
                throw new ArgumentException("Deserialized request is null.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid JSON payload: {ex.Message}");
            return;
        }

        // Resolve symbology
        if (!EncodeTypes.TryParse(request.Symbology, out BaseEncodeType encodeType))
        {
            Console.WriteLine($"Unsupported symbology: {request.Symbology}");
            return;
        }

        // Create generator
        using (var generator = new BarcodeGenerator(encodeType))
        {
            generator.CodeText = request.CodeText ?? string.Empty;

            // Apply optional appearance settings
            if (!string.IsNullOrWhiteSpace(request.BarColor))
                generator.Parameters.Barcode.BarColor = ParseHexColor(request.BarColor);

            if (!string.IsNullOrWhiteSpace(request.BackColor))
                generator.Parameters.BackColor = ParseHexColor(request.BackColor);

            if (request.BarHeight.HasValue)
                generator.Parameters.Barcode.BarHeight.Point = request.BarHeight.Value;

            if (request.XDimension.HasValue)
                generator.Parameters.Barcode.XDimension.Point = request.XDimension.Value;

            // Save to memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                string base64Png = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine("PNG Base64 Output:");
                Console.WriteLine(base64Png);
            }
        }
    }

    // Helper to convert "#RRGGBB" or "RRGGBB" to Aspose.Drawing.Color
    static Color ParseHexColor(string hex)
    {
        string clean = hex.Trim().TrimStart('#');
        if (clean.Length == 6)
        {
            int rgb = Convert.ToInt32(clean, 16);
            int argb = (0xFF << 24) | rgb; // Opaque
            return Color.FromArgb(argb);
        }
        else if (clean.Length == 8)
        {
            int argb = Convert.ToInt32(clean, 16);
            return Color.FromArgb(argb);
        }
        else
        {
            // Fallback to black if format is invalid
            return Color.Black;
        }
    }
}