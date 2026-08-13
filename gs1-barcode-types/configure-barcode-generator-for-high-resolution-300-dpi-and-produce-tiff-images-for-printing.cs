// Title: Generate High‑Resolution Code128 Barcode and Save as TIFF
// Description: Demonstrates configuring Aspose.BarCode to generate a Code128 barcode at 300 DPI and saving it as a TIFF image suitable for printing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to set resolution, colors, and output format using BarcodeGenerator, EncodeTypes, and BarCodeImageFormat. Developers often need to create high‑resolution barcodes for print media, requiring precise DPI settings and lossless image formats like TIFF.
// Prompt: Configure the barcode generator for high resolution (300 DPI) and produce TIFF images for printing.
// Tags: code128, barcode generation, high resolution, tiff, aspose.barcode, image output

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a high‑resolution Code128 barcode and saves it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode with 300 DPI resolution and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the barcode.
        const string codeText = "1234567890";

        // Initialize the barcode generator for Code128 symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Configure the generator to use a high printing resolution (300 DPI).
            generator.Parameters.Resolution = 300f;

            // Set barcode and background colors appropriate for print.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Define the output file path and format (TIFF for lossless printing).
            const string outputPath = "barcode.tif";
            generator.Save(outputPath, BarCodeImageFormat.Tiff);

            // Inform the user where the file was saved.
            Console.WriteLine($"Barcode saved to {outputPath} at 300 DPI.");
        }
    }
}