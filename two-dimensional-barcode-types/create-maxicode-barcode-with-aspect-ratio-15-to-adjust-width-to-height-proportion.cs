// Title: Generate a MaxiCode barcode with custom aspect ratio
// Description: Demonstrates how to create a MaxiCode barcode and set its aspect ratio to 1.5, adjusting the width‑to‑height proportion of the generated image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and MaxiCode parameters. Typical use cases include creating shipping labels or inventory tags where MaxiCode is required, and developers often need to control size and aspect ratio for printing or display purposes. The snippet shows how to configure X‑Dimension, aspect ratio, and save the result as a PNG image.
// Prompt: Create a MaxiCode barcode with aspect ratio 1.5 to adjust width‑to‑height proportion.
// Tags: maxicode, barcode generation, aspect ratio, png, aspose.barcode, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a MaxiCode barcode with a custom aspect ratio.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary directory to store the generated barcode image.
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the output PNG image.
        string outPath = Path.Combine(outputDir, "maxicode.png");

        // Initialize the barcode generator for MaxiCode with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "SampleText"))
        {
            // Set the X-dimension (module size) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 15;

            // Adjust the aspect ratio to 1.5 to control width‑to‑height proportion.
            generator.Parameters.Barcode.MaxiCode.AspectRatio = 1.5f;

            // Save the generated barcode as a PNG file.
            generator.Save(outPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outPath}");
    }
}