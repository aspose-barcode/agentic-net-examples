// Title: Generate barcode image with custom dimensions via console arguments
// Description: Demonstrates how to set barcode image size using width, height, and measurement unit, then output the PNG bytes.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Developers often need to create barcodes with specific dimensions for web APIs, reports, or print media; this snippet shows how to control size units (points, pixels, inches, millimeters) and disable auto‑sizing.
// Prompt: Create web API endpoint accepting width, height, and unit, generating barcode and returning image bytes.
// Tags: barcode, code128, image generation, png, dimensions, unit conversion, aspnet, aspnetcore, aspnet-webapi

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with explicit image dimensions supplied via command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Parses width, height, and unit arguments,
    /// configures the barcode generator, and outputs the PNG image bytes.
    /// </summary>
    static void Main()
    {
        // The snippet runner cannot host an HTTP server, so we demonstrate the core barcode logic.
        // Width, height and unit are taken from command‑line arguments; defaults are used if missing.

        float widthValue = 300f;   // default width
        float heightValue = 150f;  // default height
        string unit = "pt";        // default unit (points)

        // Retrieve command‑line arguments; args[0] is the executable name.
        string[] args = Environment.GetCommandLineArgs();

        // Parse optional width argument.
        if (args.Length > 1 && float.TryParse(args[1], out float w))
            widthValue = w;

        // Parse optional height argument.
        if (args.Length > 2 && float.TryParse(args[2], out float h))
            heightValue = h;

        // Parse optional unit argument.
        if (args.Length > 3)
            unit = args[3].ToLowerInvariant();

        // Create the barcode generator for Code128 with a sample codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set explicit image size using the requested unit.
            switch (unit)
            {
                case "pt":
                case "point":
                case "points":
                    generator.Parameters.ImageWidth.Point = widthValue;
                    generator.Parameters.ImageHeight.Point = heightValue;
                    break;
                case "px":
                case "pixel":
                case "pixels":
                    generator.Parameters.ImageWidth.Pixels = widthValue;
                    generator.Parameters.ImageHeight.Pixels = heightValue;
                    break;
                case "in":
                case "inch":
                case "inches":
                    generator.Parameters.ImageWidth.Inches = widthValue;
                    generator.Parameters.ImageHeight.Inches = heightValue;
                    break;
                case "mm":
                case "millimeter":
                case "millimeters":
                    generator.Parameters.ImageWidth.Millimeters = widthValue;
                    generator.Parameters.ImageHeight.Millimeters = heightValue;
                    break;
                default:
                    // Fallback to points if the unit is unsupported.
                    Console.WriteLine($"Unsupported unit '{unit}'. Using points as fallback.");
                    generator.Parameters.ImageWidth.Point = widthValue;
                    generator.Parameters.ImageHeight.Point = heightValue;
                    break;
            }

            // Ensure AutoSizeMode is None so that the explicit size is respected.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Generate the barcode image into a memory stream.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Output the size of the generated image byte array.
                Console.WriteLine($"Generated barcode image bytes: {imageBytes.Length}");

                // Optionally, write the image to a file for verification.
                File.WriteAllBytes("barcode.png", imageBytes);
                Console.WriteLine("Barcode saved as 'barcode.png' in the current directory.");
            }
        }
    }
}