// Title: Generate Code 16K barcode PNG with custom quiet zones
// Description: Demonstrates creating a Code 16K barcode image, saving it as a PNG file, and allowing the left and right quiet‑zone coefficients to be set via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.Code16K. Typical use cases include producing high‑density barcodes for packaging, inventory, or shipping labels where precise quiet‑zone control is required. Developers often need to adjust quiet zones, X‑dimension, and output formats, which this sample covers.
// Prompt: Create PowerShell module accepting barcode data, outputting Code 16K PNG with specified quiet zones.
// Tags: code16k, barcode, generation, png, quietzone, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Entry point for the console application that generates a Code 16K barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code 16K barcode PNG file using optional command‑line arguments for data and quiet‑zone coefficients.
    /// </summary>
    /// <param name="args">
    /// args[0] – barcode data (optional, defaults to "Aspose.BarCode").
    /// args[1] – left quiet‑zone coefficient (optional, minimum 10).
    /// args[2] – right quiet‑zone coefficient (optional, minimum 1).
    /// </param>
    static void Main(string[] args)
    {
        // Determine barcode data; use default if not provided.
        string data = args.Length > 0 ? args[0] : "Aspose.BarCode";

        // Default quiet‑zone coefficients.
        int leftQuiet = 10;
        int rightQuiet = 10;

        // Parse left quiet‑zone coefficient if supplied, enforce minimum of 10.
        if (args.Length > 1 && int.TryParse(args[1], out int l))
        {
            leftQuiet = Math.Max(l, 10);
        }

        // Parse right quiet‑zone coefficient if supplied, enforce minimum of 1.
        if (args.Length > 2 && int.TryParse(args[2], out int r))
        {
            rightQuiet = Math.Max(r, 1);
        }

        // Build full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code16K.png");

        // Create the barcode generator for Code 16K symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, data))
        {
            // Set the X‑dimension (module width) to 2 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Apply custom quiet‑zone coefficients.
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftQuiet;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightQuiet;

            // Save the barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}