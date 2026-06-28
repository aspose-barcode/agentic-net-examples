using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode from JSON input.
/// </summary>
class Program
{
    /// <summary>
    /// Represents the expected JSON payload containing linear and 2‑D components.
    /// </summary>
    private class Gs1CompositeRequest
    {
        public string Linear { get; set; }
        public string TwoD { get; set; }
    }

    /// <summary>
    /// Entry point of the application.
    /// Reads a JSON payload (from file or default), validates it, generates a GS1 Composite barcode,
    /// saves the image, and prints its Base64 representation.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument may be a path to a JSON file.</param>
    static void Main(string[] args)
    {
        // Default JSON payload used when no file is supplied.
        string jsonPayload = @"{ ""Linear"": ""(01)03212345678906"", ""TwoD"": ""(21)A1B2C3D4E5F6G7H8"" }";

        // If a command‑line argument is provided and points to an existing file, read its contents.
        if (args.Length > 0 && File.Exists(args[0]))
        {
            try
            {
                jsonPayload = File.ReadAllText(args[0]);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read JSON file: {ex.Message}");
                return;
            }
        }

        // Attempt to deserialize the JSON payload into a request object.
        Gs1CompositeRequest request;
        try
        {
            request = JsonSerializer.Deserialize<Gs1CompositeRequest>(jsonPayload);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid JSON payload: {ex.Message}");
            return;
        }

        // Ensure both required fields are present; otherwise fall back to sample values.
        if (string.IsNullOrWhiteSpace(request?.Linear) || string.IsNullOrWhiteSpace(request?.TwoD))
        {
            Console.WriteLine("Both 'Linear' and 'TwoD' fields must be provided. Using fallback sample values.");
            request = new Gs1CompositeRequest
            {
                Linear = "(01)03212345678906",
                TwoD = "(21)A1B2C3D4E5F6G7H8"
            };
        }

        // Combine linear and 2‑D parts with the '|' separator required for GS1 Composite barcodes.
        string codetext = $"{request.Linear}|{request.TwoD}";

        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1composite.png");

        // Generate the barcode using Aspose.BarCode.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Set component types: linear part as GS1‑Code128, 2‑D part as CC‑A.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Additional optional settings.
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;          // Aspect ratio of the 2‑D component.
            generator.Parameters.Barcode.XDimension.Pixels = 3f;          // Module size for both components.
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;        // Height of the linear component.

            // Save the generated barcode image as PNG.
            generator.Save(outputPath);
        }

        // Read the saved image file and output its Base64 representation.
        try
        {
            byte[] imageBytes = File.ReadAllBytes(outputPath);
            string base64 = Convert.ToBase64String(imageBytes);
            Console.WriteLine($"Barcode image saved to: {outputPath}");
            Console.WriteLine("Base64 PNG:");
            Console.WriteLine(base64);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to read generated image: {ex.Message}");
        }
    }
}