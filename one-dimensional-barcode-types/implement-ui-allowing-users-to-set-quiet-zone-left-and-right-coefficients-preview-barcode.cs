// Title: Generate Code16K barcode with custom quiet zone coefficients
// Description: Shows how to set left and right quiet zone coefficients for a Code16K barcode using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating the use of BarcodeGenerator and its Parameters to customize barcode appearance. It covers setting quiet zone coefficients, configuring X-dimension, and saving the barcode image. Developers working with barcode rendering often need to adjust quiet zones for scanner compatibility and visual layout.
// Prompt: Implement UI allowing users to set quiet zone left and right coefficients, preview barcode.
// Tags: barcode, code16k, quietzone, generation, png, aspose.barcode, x-dimension

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code16K barcode with custom quiet zone coefficients and saving it as a PNG.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional command‑line arguments for quiet zone coefficients, validates them,
    /// generates the barcode, and writes the image to a temporary folder.
    /// </summary>
    /// <param name="args">Optional arguments: left coefficient, right coefficient.</param>
    static void Main(string[] args)
    {
        // Default quiet zone coefficients
        int leftCoef = 10;
        int rightCoef = 10;

        // Parse left coefficient from first argument, if provided
        if (args.Length >= 1 && int.TryParse(args[0], out int parsedLeft))
        {
            leftCoef = parsedLeft;
        }

        // Parse right coefficient from second argument, if provided
        if (args.Length >= 2 && int.TryParse(args[1], out int parsedRight))
        {
            rightCoef = parsedRight;
        }

        // Validate left coefficient (must be >= 10)
        if (leftCoef < 10)
        {
            Console.WriteLine($"QuietZoneLeftCoef must be >= 10. Using default 10.");
            leftCoef = 10;
        }

        // Validate right coefficient (must be >= 1)
        if (rightCoef < 1)
        {
            Console.WriteLine($"QuietZoneRightCoef must be >= 1. Using default 1.");
            rightCoef = 1;
        }

        // Create a unique temporary directory for the output image
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodePreview_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Build the full output file path, embedding coefficient values in the file name
        string outputPath = Path.Combine(outputDir, $"Code16K_L{leftCoef}_R{rightCoef}.png");

        // Generate the barcode with the specified parameters
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set the X-dimension (module width) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply custom quiet zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user about the generated barcode and its location
        Console.WriteLine($"Barcode generated with QuietZoneLeftCoef={leftCoef}, QuietZoneRightCoef={rightCoef}");
        Console.WriteLine($"Saved to: {outputPath}");
    }
}