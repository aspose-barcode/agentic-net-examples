// Title: Generate Code 16K barcode PNG with custom quiet zones
// Description: This example creates a Code 16K barcode image in PNG format, allowing the user to specify the barcode text and quiet‑zone coefficients via command‑line arguments.
// Category-Description: Demonstrates Aspose.BarCode barcode generation using the BarcodeGenerator class with EncodeTypes.Code16K. Typical scenarios include producing high‑density barcodes for inventory, shipping labels, or product packaging where precise quiet‑zone control is required. Developers often use this API to customize dimensions, symbology settings, and output formats such as PNG, JPEG, or SVG.
// Prompt: Create PowerShell module accepting barcode data, outputting Code 16K PNG with specified quiet zones.
// Tags: barcode, code16k, generation, png, quietzone, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a Code 16K barcode image (PNG) with configurable quiet zones using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Accepts optional command‑line arguments: barcode text, left quiet‑zone coefficient, right quiet‑zone coefficient, and output file path.
    /// </summary>
    /// <param name="args">Command‑line arguments.</param>
    static void Main(string[] args)
    {
        // Default values for barcode generation
        string codeText = "Aspose.Barcode";
        int quietLeft = 10;   // Minimum left quiet‑zone coefficient
        int quietRight = 10;  // Minimum right quiet‑zone coefficient
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code16K.png");

        // Parse command‑line arguments if provided
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];
        if (args.Length > 1 && int.TryParse(args[1], out int left) && left >= 10)
            quietLeft = left;
        if (args.Length > 2 && int.TryParse(args[2], out int right) && right >= 1)
            quietRight = right;
        if (args.Length > 3 && !string.IsNullOrWhiteSpace(args[3]))
            outputPath = args[3];

        // Ensure the output directory exists
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
            Directory.CreateDirectory(outDir);

        // Generate Code 16K barcode with the specified quiet zones
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set barcode module size (pixel dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply custom quiet‑zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = quietLeft;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = quietRight;

            // Save the barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output summary information to the console
        Console.WriteLine($"Barcode saved to: {outputPath}");
        Console.WriteLine($"Code text: {codeText}");
        Console.WriteLine($"Quiet zone left coefficient: {quietLeft}");
        Console.WriteLine($"Quiet zone right coefficient: {quietRight}");
    }
}