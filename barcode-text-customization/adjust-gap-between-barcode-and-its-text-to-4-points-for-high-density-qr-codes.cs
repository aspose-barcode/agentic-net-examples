// Title: Adjust QR Code Text Gap for High-Density QR Codes
// Description: Demonstrates how to set the spacing between a QR code and its human‑readable text to 4 points, useful when generating high‑density QR codes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on QR code creation with custom visual parameters. It showcases key API classes such as BarcodeGenerator, EncodeTypes, and CodeTextParameters. Typical use cases include generating compact, high‑density QR codes for URLs or data payloads while maintaining readable text placement. Developers often need to fine‑tune module size and text spacing to meet design or printing requirements.
// Prompt: Adjust the gap between barcode and its text to 4 points for high‑density QR codes.
// Tags: qr code, barcode, text gap, high density, aspose.barcode, generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a high‑density QR code and sets a 4‑point gap between the barcode and its caption text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output directory, configures the QR code generator,
    /// adjusts visual parameters, saves the image, and writes the result path to the console.
    /// </summary>
    static void Main()
    {
        // Determine and ensure the output folder exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define the full path for the generated PNG file
        string outputPath = Path.Combine(outputDir, "HighDensityQR.png");

        // Long URL used to increase QR code density
        string longText = "https://www.example.com/this/is/a/very/long/url/that/creates/high/density/qr/code/for/testing/purposes";

        // Initialize the barcode generator for a QR code with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, longText))
        {
            // Reduce module size for higher density (optional)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Position the human‑readable text below the QR code
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Below;

            // Set the gap between the QR code and its text to 4 points
            generator.Parameters.Barcode.CodeTextParameters.Space.Point = 4f;

            // Save the generated QR code as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the QR code image was saved
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}