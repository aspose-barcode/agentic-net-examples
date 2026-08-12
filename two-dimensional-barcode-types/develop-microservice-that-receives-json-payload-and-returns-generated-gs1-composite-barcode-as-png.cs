// Title: Generate GS1 Composite barcode and return as Base64 PNG
// Description: Demonstrates creating a GS1 Composite barcode from a JSON payload and encoding the resulting PNG image as a Base64 string for API responses.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and TwoDComponentType classes to build composite barcodes, a common requirement for supply‑chain and retail applications that need both linear and 2‑D data in a single symbol.
// Prompt: Develop a microservice that receives JSON payload and returns generated GS1 Composite barcode as PNG.
// Tags: gs1, composite, barcode, generation, json, base64, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a GS1 Composite barcode from a JSON payload
/// and returns the barcode image as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Simulates receiving a JSON payload,
    /// generates the barcode, saves it as PNG, and outputs the Base64 string.
    /// </summary>
    static void Main()
    {
        // Simulated incoming JSON payload (would normally come from an HTTP request)
        string jsonPayload = @"{""linear"":""(01)00123456789012"",""twod"":""(21)A12345678""}";

        // Deserialize the JSON into a strongly‑typed object
        var payload = JsonSerializer.Deserialize<Payload>(jsonPayload);
        if (payload == null || string.IsNullOrWhiteSpace(payload.Linear) || string.IsNullOrWhiteSpace(payload.TwoD))
        {
            Console.WriteLine("Invalid payload.");
            return;
        }

        // Combine linear and 2D components using the GS1 Composite separator '|'
        string codeText = $"{payload.Linear}|{payload.TwoD}";

        // Determine output file path (saved in the current working directory)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1composite.png");

        // Generate the GS1 Composite barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Set linear component type to GS1 Code128
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set 2D component type (e.g., CC_A)
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional: adjust PDF417 aspect ratio (used for CC_A)
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

            // X‑Dimension for both components (module size)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Height of the linear component
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Read the generated PNG file and convert it to a Base64 string (simulating an API response)
        byte[] pngBytes = File.ReadAllBytes(outputPath);
        string base64Png = Convert.ToBase64String(pngBytes);
        Console.WriteLine("Generated GS1 Composite barcode (Base64 PNG):");
        Console.WriteLine(base64Png);
    }

    // Helper class matching the JSON structure
    private class Payload
    {
        public string Linear { get; set; }
        public string TwoD { get; set; }
    }
}