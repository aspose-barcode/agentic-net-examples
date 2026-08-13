// Title: Generate a Code128 barcode saved as CMYK TIFF for print-ready output
// Description: Demonstrates configuring Aspose.BarCode to produce a TIFF image in CMYK color space, suitable for high‑quality printing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to use BarcodeGenerator with EncodeTypes, set barcode text, and save in specific image formats such as CMYK TIFF. Developers often need to create print‑ready barcodes with precise color profiles, using classes like BarcodeGenerator, BarCodeImageFormat, and CMYKColor for color management.
// Prompt: Configure the generator to output TIFF in CMYK color space for print‑ready files.
// Tags: code128, barcode generation, tiff, cmyk, print-ready, aspose.barcode, image format

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a CMYK TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures CMYK colors, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the CMYK TIFF barcode
        string outputPath = "barcode_cmyk.tif";

        // Initialize the barcode generator with Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text that will be encoded into the barcode
            generator.CodeText = "PrintReady123";

            // Optional: define CMYK colors for the barcode and background
            // generator.Parameters.Pdf.CMYKBarColor = new CMYKColor(0, 0, 0, 100); // Black in CMYK
            // generator.Parameters.Pdf.CMYKBackColor = new CMYKColor(0, 0, 0, 0);   // White in CMYK

            // Save the barcode as a TIFF image using the CMYK color space
            generator.Save(outputPath, BarCodeImageFormat.TiffInCmyk);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}