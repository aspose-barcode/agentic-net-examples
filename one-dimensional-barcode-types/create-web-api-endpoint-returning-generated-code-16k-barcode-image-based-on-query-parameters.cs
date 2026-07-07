// Title: Generate Code 16K barcode image
// Description: Demonstrates generating a Code 16K barcode image using Aspose.BarCode and saving it to a file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code16K. Typical use cases include creating barcode images for inventory, shipping, or product labeling in .NET applications. Developers often need to customize parameters such as aspect ratio and output format before saving the image.
// Prompt: Create web API endpoint returning generated Code 16K barcode image based on query parameters.
// Tags: code16k, barcode, generation, png, aspose.barcode, aspnetcore, webapi

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code 16K barcode image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode based on optional command‑line arguments.
    /// </summary>
    /// <param name="args">
    /// args[0] – barcode text (default "1234567890")
    /// args[1] – aspect ratio as a positive float (default 1.0)
    /// args[2] – output file path (default "code16k.png")
    /// </param>
    static void Main(string[] args)
    {
        // Default values for barcode generation
        string codeText = "1234567890";
        float aspectRatio = 1.0f;
        string outputPath = "code16k.png";

        // Override defaults with command‑line arguments when provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];

        if (args.Length > 1 && float.TryParse(args[1], out float parsedRatio))
        {
            if (parsedRatio <= 0f)
                throw new ArgumentOutOfRangeException(nameof(aspectRatio), "Aspect ratio must be positive.");
            aspectRatio = parsedRatio;
        }

        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            outputPath = args[2];

        // Initialize the barcode generator for the Code 16K symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Apply the specified aspect ratio to the Code 16K modules
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

            // Save the generated barcode as a PNG image to the specified path
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Code16K barcode saved to '{outputPath}'.");
    }
}