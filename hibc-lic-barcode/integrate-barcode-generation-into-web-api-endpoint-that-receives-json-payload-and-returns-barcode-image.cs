// Title: Generate Barcode Image from JSON Request in Console Demo
// Description: Demonstrates how to deserialize a JSON payload containing barcode parameters, generate a barcode using Aspose.BarCode, and output the image as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and image export APIs. Typical scenarios include creating barcodes on-the-fly for web services, reports, or mobile apps. Developers often need to map incoming data (e.g., JSON) to barcode symbologies and return the generated image in a format suitable for client consumption.
// Prompt: Integrate barcode generation into a web API endpoint that receives JSON payload and returns the barcode image.
// Tags: barcode, symbology, generation, png, base64, json, aspose.barcode, aspnet

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Represents a request payload containing barcode symbology and text.
/// </summary>
public class BarcodeRequest
{
    public string Symbology { get; set; }
    public string CodeText { get; set; }
}

/// <summary>
/// Console demo that simulates a web API endpoint for barcode generation.
/// </summary>
public class Program
{
    /// <summary>
    /// Entry point that processes a sample JSON request, generates a barcode, and writes the Base64 image to the console.
    /// </summary>
    public static void Main()
    {
        // Sample JSON payload simulating a web API request
        string jsonPayload = "{\"Symbology\":\"Code128\",\"CodeText\":\"1234567890\"}";

        // Deserialize the request payload into a strongly‑typed object
        BarcodeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Symbology) ||
                string.IsNullOrWhiteSpace(request.CodeText))
            {
                Console.WriteLine("Invalid request payload.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JSON deserialization error: {ex.Message}");
            return;
        }

        // Resolve the symbology name to the corresponding BaseEncodeType using reflection
        var field = typeof(EncodeTypes).GetField(request.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.Symbology}");
            return;
        }

        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Create a BarcodeGenerator with the resolved symbology and provided code text
        using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
        {
            // Set visual appearance: black bars on white background
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Encode the PNG image as a Base64 string for transmission
                string base64Image = Convert.ToBase64String(imageBytes);

                // Simulated API response: output the Base64‑encoded PNG image
                Console.WriteLine(base64Image);
            }
        }
    }
}