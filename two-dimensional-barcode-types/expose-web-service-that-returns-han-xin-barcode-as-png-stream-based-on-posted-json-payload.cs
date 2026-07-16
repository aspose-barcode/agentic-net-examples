// Title: Han Xin barcode generation as PNG stream from JSON payload
// Description: Demonstrates how to accept a JSON payload, generate a Han Xin barcode, and return the PNG image as a Base64 string (simulating a web service response).
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.HanXin, setting error correction levels, and exporting to PNG via BarCodeImageFormat. Developers creating web APIs or services that need to produce Han Xin barcodes can follow this pattern to serialize the image for HTTP responses.
// Prompt: Expose a web service that returns Han Xin barcode as PNG stream based on posted JSON payload.
// Tags: hanxin, barcode, generation, png, json, aspnet, aspose.barcode, imageformat

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Simulates a web service that generates a Han Xin barcode PNG from a JSON payload
/// and outputs it as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses JSON payload, creates barcode, and writes Base64 PNG to console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may contain the JSON payload.</param>
    static void Main(string[] args)
    {
        // Use the first command‑line argument as JSON payload; fall back to a default example if none provided.
        // Example payload: {"CodeText":"Hello HanXin","ErrorLevel":"L2"}
        string jsonPayload = args.Length > 0 ? args[0] : "{\"CodeText\":\"Hello HanXin\",\"ErrorLevel\":\"L2\"}";

        // Deserialize the JSON into a strongly‑typed payload object.
        Payload? payload;
        try
        {
            payload = JsonSerializer.Deserialize<Payload>(jsonPayload);
        }
        catch (JsonException)
        {
            Console.WriteLine("Invalid JSON payload.");
            return;
        }

        // Validate required fields.
        if (payload == null || string.IsNullOrWhiteSpace(payload.CodeText))
        {
            Console.WriteLine("Payload must contain a non‑empty CodeText.");
            return;
        }

        // Create the barcode generator for Han Xin symbology with the supplied text.
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, payload.CodeText))
        {
            // If an error correction level is provided, attempt to parse and apply it.
            if (!string.IsNullOrWhiteSpace(payload.ErrorLevel))
            {
                if (Enum.TryParse<HanXinErrorLevel>(payload.ErrorLevel, out var level))
                {
                    generator.Parameters.Barcode.HanXin.ErrorLevel = level;
                }
                else
                {
                    Console.WriteLine($"Unknown ErrorLevel '{payload.ErrorLevel}'. Using default.");
                }
            }

            // Generate the PNG image into a memory stream.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Convert the image bytes to a Base64 string and output it.
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine(base64);
            }
        }
    }

    // Simple DTO representing the expected JSON payload.
    private class Payload
    {
        public string? CodeText { get; set; }
        public string? ErrorLevel { get; set; }
    }
}