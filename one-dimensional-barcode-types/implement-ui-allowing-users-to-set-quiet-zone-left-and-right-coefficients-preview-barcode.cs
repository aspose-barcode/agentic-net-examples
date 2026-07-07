// Title: Code16K Barcode Quiet Zone Coefficients Demo
// Description: Demonstrates setting left and right quiet zone coefficients for a Code16K barcode and generating a PNG preview.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to customize barcode parameters such as quiet zone coefficients using the BarcodeGenerator class. Typical use cases include fine‑tuning barcode appearance for scanning reliability and layout requirements. Developers often need to adjust these settings when integrating barcodes into UI applications or printed materials.
// Prompt: Implement UI allowing users to set quiet zone left and right coefficients, preview barcode.
// Tags: code16k, quietzone, barcode, generation, png, aspose.barcode, ui

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to configure quiet zone coefficients for a Code16K barcode
/// and generate a preview image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application.
    /// Accepts optional command‑line arguments for left and right quiet zone coefficients,
    /// applies them to the barcode generator, and saves the resulting PNG image.
    /// </summary>
    /// <param name="args">
    /// Optional arguments: args[0] = left quiet zone coefficient,
    /// args[1] = right quiet zone coefficient.
    /// </param>
    static void Main(string[] args)
    {
        // Default quiet zone coefficients (match Aspose defaults)
        int leftCoef = 10;   // left quiet zone coefficient
        int rightCoef = 1;   // right quiet zone coefficient

        // Parse optional command‑line arguments: first = left, second = right
        if (args.Length > 0 && int.TryParse(args[0], out int parsedLeft) && parsedLeft >= 0)
            leftCoef = parsedLeft;
        if (args.Length > 1 && int.TryParse(args[1], out int parsedRight) && parsedRight >= 0)
            rightCoef = parsedRight;

        // Define output file path for the generated barcode image
        string outputPath = "code16k.png";

        // Create a BarcodeGenerator for the Code16K symbology with sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "1234567890"))
        {
            // Apply the user‑specified quiet zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

            // Let Aspose calculate optimal image dimensions
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user about the generated barcode and its location
        Console.WriteLine($"Code16K barcode generated with QuietZoneLeftCoef={leftCoef}, QuietZoneRightCoef={rightCoef}");
        Console.WriteLine($"Image saved to: {Path.GetFullPath(outputPath)}");
    }
}