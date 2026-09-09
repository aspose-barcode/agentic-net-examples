// Title: Generate Code128 Barcode as CMYK TIFF for Print
// Description: Demonstrates how to generate a Code128 barcode and save it as a TIFF image in CMYK color space, suitable for print‑ready output.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator with EncodeTypes and BarCodeImageFormat to produce print‑ready graphics. Developers often need to create barcodes in specific color models (e.g., CMYK) for high‑quality printing, and this snippet shows the typical workflow of setting up the generator, specifying the format, and saving the file.
// Prompt: Configure the generator to output TIFF in CMYK color space for print‑ready files.
// Tags: code128, generation, tiff, cmyk, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode and saves it as a CMYK TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output file path to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output directory relative to the current working directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);
        // Build the full file path for the resulting TIFF image.
        string filePath = Path.Combine(outputDir, "barcode_cmyk.tif");

        // Initialize the barcode generator with Code128 symbology and the desired data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Save the barcode as a TIFF image using the CMYK color space.
            generator.Save(filePath, BarCodeImageFormat.TiffInCmyk);
        }

        // Inform the user where the barcode image was saved.
        Console.WriteLine($"Barcode saved to {filePath}");
    }
}