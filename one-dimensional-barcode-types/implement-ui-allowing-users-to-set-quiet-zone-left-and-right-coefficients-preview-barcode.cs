// Title: Generate Code16K barcode with configurable quiet zone coefficients
// Description: Demonstrates how to set left and right quiet zone coefficients for a Code16K barcode using Aspose.BarCode and save the image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings such as XDimension and quiet zone coefficients. Developers often need to customize quiet zones to meet scanner requirements or layout constraints, and this snippet shows typical command‑line handling and file output for such scenarios. Suitable for searches about configuring quiet zones in Code16K barcodes with Aspose.
// Prompt: Implement UI allowing users to set quiet zone left and right coefficients, preview barcode.
// Tags: code16k, quietzone, barcode generation, aspose.barcode, c#, console

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code16K barcode with user‑specified quiet zone coefficients.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses optional command‑line arguments for quiet zone coefficients,
    /// validates them, generates the barcode, and saves it to a temporary folder.
    /// </summary>
    /// <param name="args">Optional arguments: left coefficient, right coefficient.</param>
    static void Main(string[] args)
    {
        // Default quiet zone coefficients
        int leftCoef = 10;
        int rightCoef = 10;

        // ------------------------------------------------------------
        // Parse command‑line arguments if provided (left then right)
        // ------------------------------------------------------------
        if (args.Length >= 1 && int.TryParse(args[0], out int parsedLeft))
        {
            leftCoef = parsedLeft;
        }
        if (args.Length >= 2 && int.TryParse(args[1], out int parsedRight))
        {
            rightCoef = parsedRight;
        }

        // ------------------------------------------------------------
        // Validate coefficients (Code16K requires left >= 10, right >= 1)
        // ------------------------------------------------------------
        if (leftCoef < 10)
        {
            Console.WriteLine($"QuietZoneLeftCoef must be >= 10. Using default 10.");
            leftCoef = 10;
        }
        if (rightCoef < 1)
        {
            Console.WriteLine($"QuietZoneRightCoef must be >= 1. Using default 1.");
            rightCoef = 1;
        }

        // ------------------------------------------------------------
        // Prepare output folder and file path
        // ------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo");
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, $"Code16K_QZ_L{leftCoef}_R{rightCoef}.png");

        // ------------------------------------------------------------
        // Generate the barcode with the specified parameters
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, "Aspose.Barcode"))
        {
            // Set basic barcode appearance
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Apply user‑defined quiet zone coefficients
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftCoef;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightCoef;

            // Save the generated barcode image as PNG
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}