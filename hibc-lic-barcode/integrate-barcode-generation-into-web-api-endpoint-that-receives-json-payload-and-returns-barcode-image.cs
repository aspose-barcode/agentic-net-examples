// Title: Web API Barcode Generation Example
// Description: Demonstrates generating a barcode image from a JSON payload using Aspose.BarCode and returning it as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcodes in server-side applications. Typical use cases include web services that accept barcode parameters via JSON, generate images on the fly, and deliver them to clients. Developers often need to map symbology names to EncodeTypes, configure image settings, and serialize the result for HTTP responses.
// Prompt: Integrate barcode generation into a web API endpoint that receives JSON payload and returns the barcode image.
// Tags: barcode, symbology, generation, png, base64, aspnet, aspose.barcode, json, webapi

using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Represents the JSON payload received by the web API containing barcode parameters.
/// </summary>
public class BarcodeRequest
{
    /// <summary>
    /// Gets or sets the symbology name (e.g., "QR", "Code128").
    /// </summary>
    public string Symbology { get; set; }

    /// <summary>
    /// Gets or sets the text to encode in the barcode.
    /// </summary>
    public string CodeText { get; set; }
}

/// <summary>
/// Demonstrates processing a JSON request, generating a barcode, and outputting the image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application that simulates a web API barcode generation endpoint.
    /// </summary>
    static void Main()
    {
        // Simulated JSON payload received by a web API endpoint
        string jsonPayload = "{\"symbology\":\"QR\",\"codeText\":\"Hello World\"}";

        // Parse JSON into a request object
        BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
        if (request == null || string.IsNullOrWhiteSpace(request.Symbology) || string.IsNullOrWhiteSpace(request.CodeText))
        {
            Console.WriteLine("Invalid request payload.");
            return;
        }

        // Resolve symbology name to a BaseEncodeType using reflection
        FieldInfo field = typeof(EncodeTypes).GetField(request.Symbology);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {request.Symbology}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Generate the barcode and output as Base64 string
        using (var memoryStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(encodeType, request.CodeText))
            {
                // Example parameter customizations
                generator.Parameters.Resolution = 300f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                try
                {
                    // Save the generated barcode image to the memory stream in PNG format
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Barcode generation failed: {ex.Message}");
                    return;
                }
            }

            // Convert the image bytes to a Base64 string for transmission
            string base64Image = Convert.ToBase64String(memoryStream.ToArray());
            Console.WriteLine(base64Image);
        }
    }
}