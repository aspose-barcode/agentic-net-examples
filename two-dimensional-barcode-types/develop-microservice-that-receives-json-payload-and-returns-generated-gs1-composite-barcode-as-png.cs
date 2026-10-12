// Title: Generate GS1 Composite barcode from JSON payload and return PNG as Base64
// Description: Demonstrates how to parse a JSON request, configure linear and 2‑D components, generate a GS1 Composite barcode, save it as PNG, and output the image as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on composite symbologies (GS1 Composite). It showcases the use of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to combine linear and 2‑D components. Developers building microservices or APIs that need to produce barcode images on‑the‑fly will find this pattern useful for handling JSON input and returning image data.
// Prompt: Develop a microservice that receives JSON payload and returns generated GS1 Composite barcode as PNG.
// Tags: barcode, gs1 composite, json, png, base64, aspose.barcode, generation

using System;
using System.IO;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Represents the JSON payload containing barcode data and symbology types.
/// </summary>
class Payload
{
    public string Linear { get; set; }
    public string TwoD { get; set; }
    public string LinearType { get; set; }
    public string TwoDType { get; set; }
}

/// <summary>
/// Entry point for the console application that simulates a microservice request.
/// </summary>
class Program
{
    /// <summary>
    /// Parses a sample JSON payload, generates a GS1 Composite barcode, saves it as PNG,
    /// and writes the image as a Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload simulating an incoming request body
        string json = @"{
            ""Linear"": ""(01)01234567890128"",
            ""TwoD"": ""(21)ABC123"",
            ""LinearType"": ""GS1Code128"",
            ""TwoDType"": ""CC_B""
        }";

        // Deserialize JSON into a strongly‑typed object
        Payload payload = JsonSerializer.Deserialize<Payload>(json);
        if (payload == null)
        {
            Console.WriteLine("Failed to parse JSON payload.");
            return;
        }

        // Resolve the linear component symbology using reflection on EncodeTypes
        FieldInfo linearField = typeof(EncodeTypes).GetField(payload.LinearType);
        if (linearField == null)
        {
            Console.WriteLine($"Unknown linear symbology: {payload.LinearType}");
            return;
        }
        BaseEncodeType linearEncode = (BaseEncodeType)linearField.GetValue(null);

        // Parse the 2‑D component type from its string representation
        if (!Enum.TryParse<TwoDComponentType>(payload.TwoDType, out TwoDComponentType twoDComp))
        {
            Console.WriteLine($"Invalid TwoDComponentType: {payload.TwoDType}");
            return;
        }

        // Combine linear and 2‑D code texts using the '|' separator required by GS1 Composite
        string combinedCodeText = $"{payload.Linear}|{payload.TwoD}";

        // Determine the output file path for the generated PNG
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1composite.png");

        // Create and configure the barcode generator for GS1 Composite
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = linearEncode;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = twoDComp;
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the PNG file and convert it to a Base64 string for transmission
        byte[] pngBytes = File.ReadAllBytes(outputPath);
        string base64 = Convert.ToBase64String(pngBytes);

        // Output the file location and Base64 representation
        Console.WriteLine("Generated GS1 Composite barcode saved to: " + outputPath);
        Console.WriteLine("Base64 PNG:");
        Console.WriteLine(base64);
    }
}