// Title: Generate Code 16K Barcode Image via Console Parameters
// Description: Demonstrates generating a Code 16K barcode image using Aspose.BarCode and saving it to a temporary folder. The example shows how to configure aspect ratio, X‑dimension, and quiet zones based on input values.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to create barcodes with the BarcodeGenerator class. Typical use cases include producing barcode images for web APIs, reports, or inventory systems. Developers often need to set symbology‑specific parameters such as aspect ratio and quiet zone coefficients to meet scanning requirements.
// Prompt: Create web API endpoint returning generated Code 16K barcode image based on query parameters.
// Tags: code16k, barcode, generation, image, png, aspnet, aspnetcore, aspnet-webapi, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code 16K barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that parses arguments, configures the barcode generator, and saves the image.
    /// </summary>
    /// <param name="args">Command‑line arguments representing barcode text and optional parameters.</param>
    static void Main(string[] args)
    {
        // Parse sample request parameters (in a real web API these would come from the query string)
        string codeText = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]) ? args[0] : "Aspose.Barcode";
        float aspectRatio = args.Length > 1 && float.TryParse(args[1], out var ar) && ar > 0 ? ar : 10f;
        float xDimensionPixels = args.Length > 2 && float.TryParse(args[2], out var xd) && xd > 0 ? xd : 2f;
        int quietLeft = args.Length > 3 && int.TryParse(args[3], out var ql) && ql >= 10 ? ql : 10;
        int quietRight = args.Length > 4 && int.TryParse(args[4], out var qr) && qr >= 1 ? qr : 1;

        // Determine output directory inside the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        // Build a unique file name for the generated barcode image
        string outputPath = Path.Combine(outputDir, $"Code16K_{Guid.NewGuid():N}.png");

        // Initialize the barcode generator with Code16K symbology and the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Configure barcode appearance
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;          // Width of the smallest bar element
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;           // Height‑to‑width ratio
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietLeft;       // Left quiet zone coefficient
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietRight;     // Right quiet zone coefficient

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the generated barcode image
        Console.WriteLine($"Barcode generated: {outputPath}");
    }
}