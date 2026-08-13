// Title: Generate barcode image with custom dimensions via simulated API endpoint
// Description: Demonstrates how to accept width, height, and unit parameters, generate a Code128 barcode using Aspose.BarCode, and return the PNG image bytes as a Base64 string, mimicking a web API response.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcode images with explicit sizing. Typical scenarios include web services that need to produce barcode graphics on‑the‑fly based on client‑supplied dimensions. Developers often need to control image size units (points, pixels, millimeters, inches) and return the binary data directly in HTTP responses.
// Prompt: Create web API endpoint accepting width, height, and unit, generating barcode and returning image bytes.
// Tags: code128, barcode generation, png, image dimensions, autosizemode, aspnet, aspnetcore, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Simulates a web API endpoint that generates a barcode image based on supplied dimensions
/// and returns the image bytes as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that parses command‑line arguments for width, height, and unit,
    /// creates a Code128 barcode with the specified size, and writes the PNG image bytes
    /// to the console as a Base64 string (representing an HTTP response body).
    /// </summary>
    static void Main()
    {
        // Default dimensions for CI environments where no arguments are provided
        float width = 300f;
        float height = 150f;
        string unit = "pt";

        // Retrieve command‑line arguments (args[0] is the executable name)
        string[] args = Environment.GetCommandLineArgs();

        // Override defaults if valid arguments are supplied
        if (args.Length > 1 && float.TryParse(args[1], out float w)) width = w;
        if (args.Length > 2 && float.TryParse(args[2], out float h)) height = h;
        if (args.Length > 3) unit = args[3].ToLowerInvariant();

        // Initialize the barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Disable automatic sizing to enforce explicit dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Apply the requested unit to the image width and height
            switch (unit)
            {
                case "pt":
                    generator.Parameters.ImageWidth.Point = width;
                    generator.Parameters.ImageHeight.Point = height;
                    break;
                case "px":
                    generator.Parameters.ImageWidth.Pixels = width;
                    generator.Parameters.ImageHeight.Pixels = height;
                    break;
                case "mm":
                    generator.Parameters.ImageWidth.Millimeters = width;
                    generator.Parameters.ImageHeight.Millimeters = height;
                    break;
                case "in":
                    generator.Parameters.ImageWidth.Inches = width;
                    generator.Parameters.ImageHeight.Inches = height;
                    break;
                default:
                    throw new ArgumentException($"Unsupported unit '{unit}'. Use pt, px, mm, or in.");
            }

            // Set the barcode content
            generator.CodeText = "123456";

            // Save the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert the image bytes to Base64 to simulate an HTTP response body
                string base64 = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64);
            }
        }
    }
}