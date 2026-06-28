using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and verification of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    // Simple model representing the expected HTTP JSON payload
    private class Gs1CompositeRequest
    {
        public string Linear { get; set; }
        public string TwoD { get; set; }
    }

    /// <summary>
    /// Entry point of the application. Generates a GS1 Composite barcode from a JSON payload,
    /// saves it to a file, and then reads it back to verify the content.
    /// </summary>
    static void Main()
    {
        // Sample JSON payload (simulating an HTTP request body)
        string jsonPayload = @"{ ""Linear"": ""(01)03212345678906"", ""TwoD"": ""(21)A1B2C3D4E5F6G7H8"" }";

        // Deserialize the payload into a strongly‑typed request object
        Gs1CompositeRequest request = JsonSerializer.Deserialize<Gs1CompositeRequest>(jsonPayload);
        if (request == null || string.IsNullOrWhiteSpace(request.Linear) || string.IsNullOrWhiteSpace(request.TwoD))
        {
            Console.WriteLine("Invalid request payload.");
            return;
        }

        // Combine linear and 2D parts using the required '|' separator for GS1 Composite
        string codetext = $"{request.Linear}|{request.TwoD}";

        // Determine the output file path for the generated barcode image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1composite.png");

        // Generate the GS1 Composite barcode
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Configure the linear component (GS1 Code128) and the 2D component (CC_A)
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Example additional settings for the barcode appearance
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;          // Aspect ratio of the 2D component
            generator.Parameters.Barcode.XDimension.Pixels = 3f;          // X‑Dimension for both components
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;        // Height of the linear component

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        Console.WriteLine($"Barcode image saved to: {outputPath}");

        // Verify the generated barcode by reading it back from the saved image
        using (var reader = new BarCodeReader(outputPath, DecodeType.GS1CompositeBar))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
            }
        }
    }
}