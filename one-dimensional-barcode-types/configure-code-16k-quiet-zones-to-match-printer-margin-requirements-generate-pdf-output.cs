// Title: Generate Code 16K barcode with custom quiet zones and save as PNG
// Description: This example configures the left and right quiet zones of a Code 16K barcode, adjusts its dimensions, and saves the result as a PNG image.
// Category-Description: Aspose.BarCode generation examples show how to create various barcode symbologies, customize rendering parameters (such as X‑dimension, aspect ratio, and quiet zones), and export to common image formats. Developers often need to fine‑tune quiet zones to meet printer margin requirements or scanning standards, using the BarcodeGenerator and its Parameters API.
// Prompt: Configure Code 16K quiet zones to match printer margin requirements, generate PDF output.
// Tags: barcode, code16k, quietzone, image generation, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code 16K barcode with custom quiet zones using Aspose.BarCode and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Configures barcode parameters, generates the barcode, and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code16K_QuietZone.png");

        // Text to encode in the barcode.
        string codeText = "Aspose.Code16K.Sample";

        // Create a barcode generator for Code 16K symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set the X‑dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Configure quiet zone coefficients for left and right margins.
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 10;

            // Adjust the aspect ratio of the barcode.
            generator.Parameters.Barcode.Code16K.AspectRatio = 10;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Code 16K barcode saved to: {outputPath}");
    }
}