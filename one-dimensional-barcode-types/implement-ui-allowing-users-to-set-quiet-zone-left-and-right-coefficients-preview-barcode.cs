// Title: Generate Code 16K barcode with custom quiet zone coefficients
// Description: Shows how to set left and right quiet zone coefficients for a Code 16K barcode using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure quiet zone parameters for barcodes. It uses the BarcodeGenerator class and EncodeTypes.Code16K to customize barcode appearance. Developers often need to adjust quiet zones to meet scanner requirements or layout constraints, and this snippet illustrates the typical API usage for such adjustments.
// Prompt: Implement UI allowing users to set quiet zone left and right coefficients, preview barcode.
// Tags: code16k, quietzone, barcode, generation, png, aspose.barcode, encode-types, image-output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code 16K barcode with user‑defined quiet zone coefficients
/// and saving the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Parses optional command‑line arguments for quiet zone
    /// coefficients, creates a temporary output folder, generates the barcode, and writes the
    /// output path to the console.
    /// </summary>
    /// <param name="args">
    /// Optional arguments: <c>args[0]</c> – left quiet zone coefficient (minimum 10),
    /// <c>args[1]</c> – right quiet zone coefficient (minimum 1).
    /// </param>
    static void Main(string[] args)
    {
        // Default quiet zone coefficients (left = 10, right = 10)
        int leftCoef = 10;
        int rightCoef = 10;

        // If two arguments are supplied, attempt to parse them and enforce minimum values
        if (args.Length >= 2)
        {
            if (int.TryParse(args[0], out int parsedLeft) && parsedLeft >= 10)
                leftCoef = parsedLeft;

            if (int.TryParse(args[1], out int parsedRight) && parsedRight >= 1)
                rightCoef = parsedRight;
        }

        // Create a unique temporary folder for the generated barcode image
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodePreview_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Build the output file name that reflects the chosen coefficients
        string outputPath = Path.Combine(outputFolder, $"Code16K_QZL{leftCoef}_QZR{rightCoef}.png");

        // Generate the Code 16K barcode with the specified quiet zone settings
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.BarCode"))
        {
            // Set basic barcode appearance
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply custom quiet zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode generated:");
        Console.WriteLine(outputPath);
    }
}