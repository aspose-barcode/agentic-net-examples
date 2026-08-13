// Title: Generate high‑resolution barcode with custom DPI
// Description: Demonstrates creating a Code128 barcode image at a specified DPI for high‑resolution printing.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showing how to configure the Resolution property of BarcodeGenerator to produce high‑resolution images. It covers using BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, typical for scenarios like printing labels, packaging, or documents where barcode clarity at high DPI is required. Developers often need to adjust DPI to meet printer specifications or quality standards.
// Prompt: Implement method to generate barcode with custom DPI setting for high‑resolution printing requirements.
// Tags: barcode, code128, dpi, high‑resolution, generation, aspose.barcode, image, png

using System;
using System.IO;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a barcode image with a custom DPI setting for high‑resolution output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and saves it to a temporary PNG file.
    /// </summary>
    static void Main()
    {
        // Define sample data: text to encode, output file path, and desired DPI.
        string codeText = "1234567890";
        string outputFile = Path.Combine(Path.GetTempPath(), "barcode_highres.png");
        float dpi = 300f; // Custom DPI for high‑resolution printing

        try
        {
            // Generate the barcode image with the specified parameters.
            GenerateBarcode(codeText, outputFile, dpi);
            Console.WriteLine($"Barcode generated and saved to: {outputFile}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a barcode image with a custom DPI (resolution) and saves it to the specified path.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">Full file path where the barcode image will be saved.</param>
    /// <param name="dpi">Desired resolution in dots per inch. Must be greater than 0.</param>
    static void GenerateBarcode(string codeText, string outputPath, float dpi)
    {
        // Validate input parameters.
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text cannot be null or empty.", nameof(codeText));

        if (string.IsNullOrEmpty(outputPath))
            throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

        if (dpi <= 0f)
            throw new ArgumentOutOfRangeException(nameof(dpi), "DPI must be greater than zero.");

        // Ensure the output directory exists.
        string directory = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Create the barcode generator with the desired symbology (Code128 in this example).
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set custom resolution (DPI).
            generator.Parameters.Resolution = dpi;

            // Save the barcode image in PNG format.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
    }
}