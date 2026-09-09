// Title: Barcode width reduction example using Aspose.BarCode
// Description: Demonstrates how to reduce the bar width of a Code128 barcode by 20 percent to fit narrow label spaces.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure barcode appearance parameters such as X‑dimension and bar‑width reduction. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to create PNG images. Developers often need to adjust these settings when printing on small labels or tight layouts, making this a common task in barcode rendering workflows.
// Prompt: Set barcode width reduction to 20 percent to fit narrow label spaces.
// Tags: code128, barwidthreduction, barcode generation, aspnet, aspose.barcode, png, label printing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates setting a 20 percent bar‑width reduction on a Code128 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode with a 20 percent bar‑width reduction and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define output directory in the temporary folder and ensure it exists
        string outputDir = Path.Combine(Path.GetTempPath(), "BarWidthReductionDemo");
        Directory.CreateDirectory(outputDir);

        // Full path for the resulting PNG image
        string outPath = Path.Combine(outputDir, "Code128_20PercentReduction.png");

        // Create a barcode generator for Code128 with the desired text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Set the module (X‑dimension) size to 10 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 10;

            // Reduce the bar width by 20% (2 pixels of a 10‑pixel module)
            generator.Parameters.Barcode.BarWidthReduction.Pixels = 2;

            // Save the generated barcode as a PNG file
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {outPath}");
    }
}