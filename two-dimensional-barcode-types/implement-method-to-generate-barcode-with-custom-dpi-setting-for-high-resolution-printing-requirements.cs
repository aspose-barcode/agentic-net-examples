// Title: Generate High‑Resolution Barcode with Custom DPI
// Description: Demonstrates creating a Code128 barcode image with a user‑specified DPI for high‑resolution printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce high‑quality barcode images. Typical use cases include printing labels, tickets, or product packaging where precise resolution is required. Developers often need to adjust DPI and module size to meet printing standards, and this snippet illustrates those common steps.
// Prompt: Implement method to generate barcode with custom DPI setting for high‑resolution printing requirements.
// Tags: barcode, code128, high resolution, dpi, png, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that generates a high‑resolution Code128 barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Main method: prepares input data, invokes barcode generation, and reports the result.
    /// </summary>
    static void Main()
    {
        // Define barcode content and output location
        string codeText = "HIGHRES12345";
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "HighResolutionBarcode.png");
        float dpi = 300f; // Desired DPI for high‑resolution printing

        try
        {
            // Generate the barcode with the specified DPI
            GenerateBarcode(codeText, outputFile, dpi);
            Console.WriteLine($"Barcode saved to: {outputFile}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a Code128 barcode image at the given resolution and saves it to a file.
    /// </summary>
    /// <param name="text">The text to encode in the barcode.</param>
    /// <param name="outputPath">Full file path where the image will be saved.</param>
    /// <param name="resolutionDpi">Resolution in dots per inch (DPI) for the output image.</param>
    static void GenerateBarcode(string text, string outputPath, float resolutionDpi)
    {
        // Validate input text
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Barcode text cannot be null or empty.", nameof(text));

        // Ensure the output directory exists
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Create and configure the barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
        {
            // Apply the custom DPI setting
            generator.Parameters.Resolution = resolutionDpi;

            // Optionally adjust the module size (XDimension) for sharper rendering
            generator.Parameters.Barcode.XDimension.Millimeters = 0.5f;

            // Save the barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}