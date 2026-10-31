// Title: Generate MaxiCode barcode with 300 DPI resolution
// Description: Demonstrates how to set the image resolution to 300 DPI when creating a MaxiCode barcode using Aspose.BarCode, improving print quality.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on image resolution settings for high‑quality output. It showcases the BarcodeGenerator class, EncodeTypes enumeration, and resolution parameters, which developers commonly use when creating printable barcodes such as MaxiCode, QR, or Code128. The snippet helps users understand how to configure DPI for better print results.
// Prompt: Set the barcode image DPI to 300 when generating a MaxiCode to improve print quality.
// Tags: maxicode, dpi, resolution, barcode generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a MaxiCode barcode image with a resolution of 300 DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a MaxiCode barcode, sets its resolution to 300 DPI, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCode_300DPI.png");

        // Ensure the target directory exists before attempting to save the image
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a BarcodeGenerator for MaxiCode with sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Set the image resolution to 300 DPI for high‑quality printing
            generator.Parameters.Resolution = 300f;

            // Optionally increase the module size to improve visual clarity
            generator.Parameters.Barcode.XDimension.Pixels = 10f;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}