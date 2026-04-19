using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    // Simple request model
    class BarcodeRequest
    {
        public string Symbology { get; set; }
        public string CodeText { get; set; }
    }

    static void Main()
    {
        // Simulated JSON payload
        string jsonPayload = @"{ ""Symbology"": ""Code128"", ""CodeText"": ""123ABC"" }";

        // Deserialize request
        BarcodeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
            if (request == null || string.IsNullOrWhiteSpace(request.Symbology) || string.IsNullOrWhiteSpace(request.CodeText))
                throw new ArgumentException("Invalid request payload.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing request: {ex.Message}");
            return;
        }

        // Resolve symbology to EncodeTypes
        BaseEncodeType encodeType = ResolveEncodeType(request.Symbology);

        // Generate barcode with checksum enabled
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
        {
            // Enable checksum generation where applicable
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save to memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] pngBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(pngBytes);
                Console.WriteLine("Base64 PNG Image:");
                Console.WriteLine(base64);
            }
        }
    }

    // Maps a string symbology name to an EncodeTypes value.
    static BaseEncodeType ResolveEncodeType(string name)
    {
        if (string.Equals(name, "Code128", StringComparison.OrdinalIgnoreCase))
            return EncodeTypes.Code128;
        if (string.Equals(name, "EAN13", StringComparison.OrdinalIgnoreCase))
            return EncodeTypes.EAN13;
        if (string.Equals(name, "Code39", StringComparison.OrdinalIgnoreCase))
            return EncodeTypes.Code39;
        // Add more mappings as needed.

        throw new ArgumentException($"Unsupported symbology: {name}");
    }
}