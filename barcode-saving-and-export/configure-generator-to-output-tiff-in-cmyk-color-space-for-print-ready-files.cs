// Title: Generate a Code128 barcode saved as CMYK TIFF for print-ready output
// Description: Demonstrates how to configure Aspose.BarCode to generate a Code128 barcode and save it as a TIFF image in the CMYK color space, suitable for high‑quality printing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to produce print‑ready barcode images. Developers often need to create barcodes in specific color spaces (e.g., CMYK) for offset printing, and this snippet shows the typical setup and saving process.
// Prompt: Configure the generator to output TIFF in CMYK color space for print‑ready files.
// Tags: code128, barcode generation, tiff, cmyk, print-ready, aspose.barcode, image format

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a CMYK TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes the output file path to the console.
    /// </summary>
    static void Main(string[] args)
    {
        // Define the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);
        // Build the full file path for the resulting TIFF image
        string filePath = Path.Combine(outputDir, "barcode_cmyk.tif");

        // Initialize the barcode generator with Code128 symbology and sample data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Optional: set barcode foreground and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the barcode as a TIFF image in CMYK color space
            generator.Save(filePath, BarCodeImageFormat.TiffInCmyk);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"Barcode saved to: {filePath}");
    }
}