// Title: Generate GS1 Composite Barcode and Return as PNG Base64
// Description: Demonstrates creating a GS1 Composite barcode from a JSON payload and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and TwoDComponentType to build composite barcodes, a common requirement for supply‑chain and retail applications that need both linear and 2‑D data in a single symbol. Developers often use these APIs to integrate barcode creation into services, APIs, or batch processing pipelines.
// Prompt: Develop a microservice that receives JSON payload and returns generated GS1 Composite barcode as PNG.
// Tags: gs1 composite barcode, generation, png, base64, json, aspose.barcode, c#

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a GS1 Composite barcode from a JSON request and outputs the image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Model representing the incoming JSON payload with linear and 2‑D components.
    /// </summary>
    public class BarcodeRequest
    {
        public string Linear { get; set; }
        public string TwoD { get; set; }
    }

    /// <summary>
    /// Entry point. Deserializes a sample JSON payload, creates the barcode, saves it, and prints the Base64 representation.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload containing the linear (GS1‑128) and 2‑D (CC‑A) components.
        string jsonPayload = "{\"Linear\":\"(01)01234567890128\",\"TwoD\":\"(21)ABC123\"}";

        // Deserialize the JSON payload into a strongly‑typed request object.
        BarcodeRequest request = JsonSerializer.Deserialize<BarcodeRequest>(jsonPayload);
        if (request == null || string.IsNullOrEmpty(request.Linear) || string.IsNullOrEmpty(request.TwoD))
        {
            Console.WriteLine("Invalid request payload.");
            return;
        }

        // Combine linear and 2‑D components using the pipe separator required by GS1 Composite.
        string codeText = $"{request.Linear}|{request.TwoD}";

        // Define the output file path for the generated PNG image.
        string outputPath = "gs1composite.png";

        // Generate the GS1 Composite barcode using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Configure the linear component to use GS1‑Code128 encoding.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2‑D component to use CC‑A (Composite Component A) encoding.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Allow non‑GS1 characters if needed (set to false for strict GS1 compliance).
            generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

            // Optional visual settings: set module size and colors.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Load the saved PNG file and convert it to a Base64 string for easy transport.
        byte[] imageBytes = File.ReadAllBytes(outputPath);
        string base64 = Convert.ToBase64String(imageBytes);

        // Output the file location and Base64 representation to the console.
        Console.WriteLine($"Generated barcode saved to '{outputPath}'.");
        Console.WriteLine("Base64 PNG:");
        Console.WriteLine(base64);
    }
}